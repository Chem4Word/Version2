using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Security;
using System.Collections.Generic;
using System.Text;
using System.Security.AccessControl;
using Microsoft.Build.Utilities;

namespace CustomInstaller
{
    public enum Action
    {
        Install,
        UnInstall
    }

    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        #region Error Logger
        private static ILogger _eventLogger;
        public static ILogger eventLogger
        {
            get
            {
                if (_eventLogger == null)
                {
                    _eventLogger = new Logger();
                }
                return _eventLogger;
            }
            set { _eventLogger = value; }
        }
        #endregion

        private const string AssemblyPath = "assemblypath";
        static string installLocation;

        /// <summary>
        /// Chemistry Addin for Word config file name.
        /// </summary>
        private const string configFileName = "Chem4Word.AddIn.dll.config";

        /// <summary>
        /// Install folder param name passed from the installer.
        /// </summary>
        private const string installDirParam = "installDir";

        /// <summary>
        /// ALLUSERS parameter passed from the installer.
        /// </summary>
        private const string allUsersInstallationParam = "allUsersInstallation";

        /// <summary>
        /// Registry entry for Chemistry Add-in for Word VSTO manifest.
        /// </summary>        
        private static string RegistryManifestPath = String.Empty;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CustomInstaller()
        {
            InitializeComponent();
        }

        #region Install Event Handlers

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {            
            try
            {
                string assemblyPath = this.Context.Parameters[AssemblyPath];
                installLocation = assemblyPath.Substring(0, assemblyPath.LastIndexOf(Resource.SLASH, StringComparison.OrdinalIgnoreCase) + 1);

                RegistryManifestPath = Path.Combine(installLocation, Resource.ChemistryWordAddInManifest);
                RegistryManifestPath = RegistryManifestPath + "|" + Resource.VSTOLocal;

                base.OnBeforeInstall(savedState);

                Logger.CreateEventSource();
                if (eventLogger == null)
                {
                    eventLogger = new Logger();
                }
                eventLogger.WriteToLog(LogCategory.Installer, Resource.BEFOREINSTALLMSG + Resource.ADDINTITLE);

                //Applications should be closed before continuing the install:
                ApplicationIsOpen(Action.Install);

                // Check for 64 bit machine.
                if (NativeMethods.Is64Bit())
                {
                    WriteTo64BitRegistry();
                }
            }
            catch (ArgumentNullException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREINSTALL, ex.StackTrace);
            }
            catch (SystemException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREINSTALL, ex.StackTrace);
            }
            catch (Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREINSTALL, ex.StackTrace);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                base.Install(stateSaver);
                
                //Update Program Files path if it is 64 bit machine
                Is64BitMachine();

            }
            catch (NullReferenceException nullReferenceEx)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORINSTALL, nullReferenceEx.StackTrace);
            }

            catch (ArgumentOutOfRangeException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORINSTALL, ex.StackTrace);
            }
            catch (ArgumentException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORINSTALL, ex.StackTrace);
            }
            catch (Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORINSTALL, ex.StackTrace);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void OnCommitted(IDictionary savedState)
        {            
            try
            {  
                base.OnCommitted(savedState);

                try
                {
                    if (eventLogger != null)
                    {
                        eventLogger.Dispose();
                        eventLogger = null;
                    }
                }
                catch (Exception)
                {
                    //Failed to dispose event logger
                }
            }
            catch (ObjectDisposedException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORCOMMIT, ex.StackTrace);
            }
            catch (IOException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORCOMMIT, ex.StackTrace);
            }
            catch (Win32Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORCOMMIT, ex.StackTrace);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            try
            {
                base.OnBeforeUninstall(savedState);
                Logger.CreateEventSource();
                if (eventLogger == null)
                {
                    eventLogger = new Logger();
                }
                eventLogger.WriteToLog(LogCategory.Installer, Resource.BEFOREUNINSTALLMSG + Resource.ADDINTITLE);
                
                // Applications should be closed before continuing the un-install:
                ApplicationIsOpen(Action.UnInstall);

            }
            catch (NullReferenceException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREUNINSTALL, ex.StackTrace);
            }
            catch (Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREUNINSTALL, ex.StackTrace);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public override void Uninstall(IDictionary savedState)
        {            
            try
            {
                base.Uninstall(savedState);

                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Resource.ADDINNAME);

                //Update Program Files path if it is 64 bit machine
                Is64BitMachine();

                //Remove User profile specific files
                RemoveSettingsFileToUserProfile(Resource.UNINSTALLACTION);               
            }

            catch (SecurityException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (UnauthorizedAccessException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (IOException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (ArgumentException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void OnAfterUninstall(IDictionary savedState)
        {
            try
            {
                base.OnAfterUninstall(savedState);

                if (NativeMethods.Is64Bit())
                {
                    DeleteFrom64BitRegistry();
                }

                if (eventLogger != null)
                {
                    eventLogger.Dispose();
                    eventLogger = null;
                }
            }
            catch (SecurityException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (UnauthorizedAccessException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
            catch (Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORUNINSTALL, ex.StackTrace);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method looks for all the Microsoft Word process which are opened 
        /// and kills such processes before the installation or uninstalling process
        /// </summary>
        /// <param name="currentAction"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private void ApplicationIsOpen(Action currentAction)
        {
            try
            {
                if (currentAction == Action.Install || currentAction == Action.UnInstall)
                {
                    Process[] wordInstances;
                    wordInstances = Process.GetProcessesByName(Resource.WINWORD);
                    if (wordInstances.Length > 0)
                    {
                        Alert applicationOpenAlert = new Alert();
                        applicationOpenAlert.CurrentInstallerAction = currentAction;

                        if (currentAction == Action.Install)
                        {
                            applicationOpenAlert.lblMessage.Text = Resource.CLOSE_APPLICATIONS_DURING_INSTALL;
                        }
                        else if (currentAction == Action.UnInstall)
                        {
                            applicationOpenAlert.lblMessage.Text = Resource.CLOSE_APPLICATIONS_DURING_UNINSTALL;
                        }

                        DialogResult alterDialogResult = applicationOpenAlert.ShowDialog();                        
                        if (alterDialogResult == DialogResult.Retry)
                        {
                            applicationOpenAlert.Dispose();
                            ApplicationIsOpen(currentAction);
                        }
                        else if (alterDialogResult == DialogResult.OK)
                        {
                            Process[] openWordInstances = Process.GetProcessesByName(Resource.WINWORD);
                            foreach (Process instance in openWordInstances)
                            {
                                instance.Kill();
                            }
                        }
                    }
                }               
            }
            catch (NullReferenceException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORAPPLICATIONOPEN, ex.StackTrace);
            }
            catch (InvalidOperationException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORAPPLICATIONOPEN, ex.StackTrace);
            }
            catch (Win32Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORAPPLICATIONOPEN, ex.StackTrace);
            }
            catch (NotSupportedException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORAPPLICATIONOPEN, ex.StackTrace);
            }
        }

        /// <summary>
        /// This method writes the add-in entries to the registry under SOFTWARE\Microsoft 
        /// for 64 bit machines.
        /// </summary>
        private void WriteTo64BitRegistry()
        {
            // Handle to the registry key.
            UIntPtr handle;
            // Handle to the subkey under the Office\Word.
            UIntPtr subKeyHandle;
            // The load behavior of the add-in.
            int loadBehavior = 3;

            const int KEY_WOW64_64KEY = 0x0100;
            const int KEY_QUERY_VALUE = 0x1;            

            UIntPtr HKEY_PTR = HKeyForInstallType();

            // Get the handle to the HKLM entry of Office.
            int readResult = NativeMethods.RegOpenKeyEx(HKEY_PTR, @"SOFTWARE\Microsoft\Office\", 0,
              KEY_QUERY_VALUE | KEY_WOW64_64KEY, out handle);

            if (readResult.Equals(0))
            {
                // Create the 'Word' key if it does not exist.
                int handleReturnCode = NativeMethods.RegCreateKey(handle, Resource.WORD_REGISTRY_KEY, out subKeyHandle);
                if (!handleReturnCode.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.WORD_REGISTRY_KEY), true);
                }

                // Create the addin key if it does not exist.
                int subKeyhandleReturnCode = NativeMethods.RegCreateKey(subKeyHandle, Resource.ADDIN_REG_KEY, out subKeyHandle);
                if (!subKeyhandleReturnCode.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.ADDIN_REG_KEY), true);
                }

                // Create the ChemistryWord addin key.
                int result = NativeMethods.RegCreateKey(subKeyHandle, Resource.REGKEYNAME, out subKeyHandle);
                if (!result.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.REGKEYNAME), true);
                }

                // Write the value pairs to the registry.            
                int addName = NativeMethods.RegSetValueEx(subKeyHandle, Resource.FRIENDLYNAME, IntPtr.Zero, RegistryValueKind.String, Resource.ADDINNAME,
                            Resource.ADDINNAME.Length * NativeMethods.NativeBytesPerCharacter);
                if (!addName.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.FRIENDLYNAME), true);
                }

                int description = NativeMethods.RegSetValueEx(subKeyHandle, Resource.DESCRIPTIONSTRING, IntPtr.Zero, RegistryValueKind.String,
                    Resource.ADDINDESCRIPTION, Resource.ADDINDESCRIPTION.Length * NativeMethods.NativeBytesPerCharacter);
                if (!description.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.DESCRIPTIONSTRING), true);
                }

                int word = NativeMethods.RegSetValueEx(subKeyHandle, Resource.LOADBEHAVIORSTRING, IntPtr.Zero, RegistryValueKind.DWord,
                    ref loadBehavior, NativeMethods.Int32ByteSize);
                if (!word.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.LOADBEHAVIORSTRING), true);
                }

                int manifest = NativeMethods.RegSetValueEx(subKeyHandle, Resource.Manifest, IntPtr.Zero, RegistryValueKind.String, RegistryManifestPath,
                            RegistryManifestPath.Length * NativeMethods.NativeBytesPerCharacter);
                if (!manifest.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.Manifest), true);
                }
            }
            else
            {
                HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                    Resource.REGISTRYERROR), true);
            }
        }

        /// <summary>
        /// This method deletes the add-in entries from the registry under SOFTWARE\Microsoft 
        /// for 64 bit machines.
        /// </summary>
        private void DeleteFrom64BitRegistry()
        {
            const int KEY_WOW64_64KEY = 0x0100;
            const int KEY_QUERY_VALUE = 0x1;
            UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;

            UIntPtr handle;

            UIntPtr HKEY_PTR = HKeyForInstallType();

            int readResult = NativeMethods.RegOpenKeyEx(HKEY_PTR, Resource.ADD_IN_REG_KEY, 0,
                KEY_QUERY_VALUE | KEY_WOW64_64KEY, out handle);
            if (readResult.Equals(0))
            {
                int descriptionCode = NativeMethods.RegDeleteValue(handle, Resource.DESCRIPTIONSTRING);
                if (!descriptionCode.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.DESCRIPTIONSTRING), false);
                }
                int loadBehaviorCode = NativeMethods.RegDeleteValue(handle, Resource.LOADBEHAVIORSTRING);
                if (!loadBehaviorCode.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.LOADBEHAVIORSTRING), false);
                }
                int friendlyNameCode = NativeMethods.RegDeleteValue(handle, Resource.FRIENDLYNAME);
                if (!friendlyNameCode.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.FRIENDLYNAME), false);
                }

                int manifest = NativeMethods.RegDeleteValue(handle, Resource.Manifest);
                if (!manifest.Equals(0))
                {
                    this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.Manifest), false);
                }
            }
            else
            {
                this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                         Resource.WORD_REGISTRY_KEY), false);
            }

            readResult = NativeMethods.RegOpenKeyEx(HKEY_PTR, @"SOFTWARE\Microsoft\Office\Word\Addins", 0,
                KEY_QUERY_VALUE | KEY_WOW64_64KEY, out handle);
            if (!readResult.Equals(0))
            {
                this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                        Resource.WORD_REGISTRY_KEY), false);
            }

            int keyNameCode = NativeMethods.RegDeleteKey(handle, Resource.REGKEYNAME);
            if (!keyNameCode.Equals(0))
            {
                this.HandleRegistryError(String.Format(CultureInfo.InvariantCulture, Resource.REGISTRYERROR,
                    Resource.WORD_REGISTRY_KEY), false);
            }            
        }

        private const string CONST_64BIT_PROCESSOR = "(x86)";
        private const string PROGRAM_FILE_CONST = "Program Files";
        private const string PROGRAM_FILE_64BIT_CONST = "Program Files (x86)";

        private static void Is64BitMachine()
        {
            try
            {
                List<string> wordLocations = GetWordInstallationPath();
                foreach (string wordInstallationLocation in wordLocations)
                {
                    if (!string.IsNullOrEmpty(wordInstallationLocation))
                    {
                        if (wordInstallationLocation.Contains(CONST_64BIT_PROCESSOR) && !installLocation.Contains(CONST_64BIT_PROCESSOR))
                        {
                            installLocation = installLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Environment.SpecialFolder.ProgramFiles.ToString()+@" (x86)");
                            break;                            
                        }
                    }
                }
            }
            catch (ArgumentNullException ex)
            {                
                eventLogger.WriteToLog(LogCategory.Installer, Resource.CHECKMACHINETYPERROR, ex.StackTrace);
            }
            catch (NullReferenceException ex)
            {             
                eventLogger.WriteToLog(LogCategory.Installer, Resource.CHECKMACHINETYPERROR, ex.StackTrace);
                //Failed to check machine type (64 or 32 bit)
            }
            catch (SystemException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.ERRORBEFOREINSTALL, ex.StackTrace);
            }
        }

        /// <summary>
        /// Method which returns the MS Word installation paths list
        /// </summary>
        /// <returns>word Installation Paths list</returns>
        private static List<string> GetWordInstallationPath()
        {
            List<string> wordPaths = new List<string>();
            {
                try
                {
                    UIntPtr handle;
                    const int KEY_WOW64_64KEY = 0x0100;
                    const int KEY_QUERY_VALUE = 0x1;
                    UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;

                    int size = 1024;
                    uint type;
                    StringBuilder keyBuffer;
                    RegistryValueKind regValue = new RegistryValueKind();

                    //For 32 or 64 bit machine
                    List<string> paths = GetOfficeRegistryPaths();

                    if (paths.Count > 0)
                    {
                        foreach (string strItem in paths)
                        {
                            int readResult = NativeMethods.RegOpenKeyEx(HKEY_LOCAL_MACHINE,
                                                                            strItem,
                                                                            0,
                                                                            KEY_QUERY_VALUE | KEY_WOW64_64KEY,
                                                                            out handle);
                            if (readResult.Equals(0))
                            {
                                readResult = NativeMethods.RegQueryValueEx(handle,
                                                                Resource.PATH,
                                                                IntPtr.Zero,
                                                                ref regValue,
                                                                IntPtr.Zero,
                                                                ref size);
                                if (readResult.Equals(0))
                                {
                                    keyBuffer = new StringBuilder(size);

                                    readResult = NativeMethods.RegQueryValueEx(handle,
                                                                                Resource.PATH,
                                                                                0,
                                                                                out type,
                                                                                keyBuffer,
                                                                                ref size);

                                    if (readResult.Equals(0))
                                    {
                                        wordPaths.Add(keyBuffer.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    eventLogger.WriteToLog(LogCategory.Installer, ex);
                    //Failed to check Word Installation
                }
                catch (COMException ex)
                {
                    eventLogger.WriteToLog(LogCategory.Installer, ex);
                    //Failed to check Word Installation 
                }
                catch (Exception ex)
                {
                    eventLogger.WriteToLog(LogCategory.Installer, ex);
                    //Failed to check Word Installation
                }
            }
            return wordPaths;
        }

        /// <summary>
        /// Method gets the Word registry installation paths list
        /// </summary>
        /// <returns>list of office installation paths</returns>
        private static List<string> GetOfficeRegistryPaths()
        {
            List<string> registryPaths = new List<string>();
            string OFFICE12_REG_32 = @"SOFTWARE\Microsoft\Office";
            string wordPath = @"\Word\InstallRoot";
            string OFFICE12_REG_64 = @"SOFTWARE\Wow6432Node\Microsoft\Office";
            try
            {
                //This is how we call the recursive function GetSubKeys     
                RegistryKey OurKey = Registry.Users;
                RegistryKey classRoot = Registry.LocalMachine;
                OurKey = classRoot.OpenSubKey(OFFICE12_REG_32, false);
                if (OurKey != null)
                {
                    foreach (string sub in OurKey.GetSubKeyNames())
                    {
                        //Console.WriteLine(sub);
                        float result;
                        if (float.TryParse(sub, NumberStyles.Float, CultureInfo.CurrentCulture, out result))
                        {
                            if (result >= 12)
                            {
                                registryPaths.Add(OFFICE12_REG_32 + @"\" + sub + wordPath);
                            }
                        }
                    }
                }
                OurKey = classRoot.OpenSubKey(OFFICE12_REG_64, false);
                if (OurKey != null)
                {
                    foreach (string sub in OurKey.GetSubKeyNames())
                    {
                        //Console.WriteLine(sub);
                        float result;

                        if (float.TryParse(sub, NumberStyles.Float, CultureInfo.CurrentCulture, out result))
                        {
                            if (result >= 12)
                            {
                                registryPaths.Add(OFFICE12_REG_64 + @"\" + sub + wordPath);
                            }
                        }
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex);
            }
            catch (SecurityException ex)
            {                
                eventLogger.WriteToLog(LogCategory.Installer, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex);
            }
            catch (IOException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex);
                //Error occurred while checking whether Office11 is installed or not.
            }
            catch (ArgumentException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex);
                //Error occurred while checking whether Office11 is installed or not.
            }
            return registryPaths;
        }

        private static void RemoveSettingsFileToUserProfile(string action)
        {
            try
            {
                string osName = GetOSName();

                string[] listOfUsersPresentOnThisMachine = GetListOfUsersPresentOnThisMachine(osName);

                if (listOfUsersPresentOnThisMachine != null && listOfUsersPresentOnThisMachine.Length > 0)
                {
                    if (!string.IsNullOrEmpty(action) && string.Compare(action, Resource.UNINSTALLACTION, true, CultureInfo.CurrentCulture) == 0)
                    {
                        foreach (string userProfileLocation in listOfUsersPresentOnThisMachine)
                        {
                            DeleteSettingFileFromAllUserProfile(userProfileLocation, osName);
                        }
                    }                   
                }
            }
            catch (NullReferenceException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.REMOVESETTINGFILEERR, ex.StackTrace);
                //Error occered while copying or removing settings file.
            }
            catch (ArgumentNullException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.REMOVESETTINGFILEERR, ex.StackTrace);
                //Error occered while copying or removing settings file.
            }

        }

        /// <summary>
        /// This method will remove chemistry word add-in application data for all user
        /// account h the add-in is installed to
        /// </summary>
        /// <param name="userProfilePath"></param>
        /// <param name="osName"></param>
        private static void DeleteSettingFileFromAllUserProfile(string userProfilePath, string osName)
        {

            try
            {
                string filePath = string.Empty;

                if (osName.Contains(Resource.VISTA_OS_NAME))
                {
                    filePath = userProfilePath + Resource.CHEMISTRY_WORD_ADDIN_FOLDER_PATH_VISTA;
                }
                else
                {
                    //C:\Documents and Settings\v-makard\Application Data\Chemistry Add-in for Word
                    filePath = userProfilePath + Resource.CHEMISTRY_ADDIN_FOLDER_PATH_NONVISTA;
                }
                if (Directory.Exists(filePath))
                {
                    Directory.Delete(filePath, true);
                }
            }
            catch (NullReferenceException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.DELSETTINGFORALLERR, ex.StackTrace);
                //Failed to delete settings file from all user profile.
            }
            catch (ArgumentNullException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.DELSETTINGFORALLERR, ex.StackTrace);
                //Failed to delete settings file from all user profile.
            }
            catch (IOException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.DELSETTINGFORALLERR, ex.StackTrace);
                //Failed to delete settings file from all user profile.
            }
            catch (UnauthorizedAccessException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.DELSETTINGFORALLERR, ex.StackTrace);
                //Failed to delete settings file from all user profile.
            }
        }

        /// <summary>
        /// This method gets the Currently running Operating system name
        /// </summary>
        /// <returns>OS Name</returns>
        private static string GetOSName()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            string osName = "UNKNOWN";

            try
            {
                switch (osInfo.Platform)
                {
                    case PlatformID.Win32Windows:
                        {
                            switch (osInfo.Version.Minor)
                            {
                                case 0:
                                    {
                                        osName = "Windows 95";
                                        break;
                                    }

                                case 10:
                                    {
                                        if (osInfo.Version.Revision.ToString(CultureInfo.CurrentCulture) == "2222A")
                                        {
                                            osName = "Windows 98 Second Edition";
                                        }
                                        else
                                        {
                                            osName = "Windows 98";
                                        }
                                        break;
                                    }

                                case 90:
                                    {
                                        osName = "Windows Me";
                                        break;
                                    }
                            }
                            break;
                        }

                    case PlatformID.Win32NT:
                        {
                            switch (osInfo.Version.Major)
                            {
                                case 3:
                                    {
                                        osName = "Windows NT 3.51";
                                        break;
                                    }

                                case 4:
                                    {
                                        osName = "Windows NT 4.0";
                                        break;
                                    }

                                case 5:
                                    {
                                        if (osInfo.Version.Minor == 0)
                                        {
                                            osName = "Windows 2000";
                                        }
                                        else if (osInfo.Version.Minor == 1)
                                        {
                                            osName = "Windows XP";
                                        }
                                        else if (osInfo.Version.Minor == 2)
                                        {
                                            osName = "Windows Server 2003";
                                        }
                                        break;
                                    }

                                case 6:
                                    {
                                        osName = "Windows Vista";
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
            catch (NullReferenceException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.GETOSNAMEERR, ex.StackTrace);
                //Failed to get type of operating system.
            }
            catch (Win32Exception ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, Resource.GETOSNAMEERR, ex.StackTrace);
            }
            return osName;
        }

        /// <summary>
        /// This method returns all the active users in the current system
        /// </summary>
        /// <param name="osName">Operating System Name</param>
        /// <returns>All Users</returns>
        private static string[] GetListOfUsersPresentOnThisMachine(string osName)
        {
            string[] listOfUsersPresentOnThisMachine = null;
            try
            {
                string userProfilePath = string.Empty;
                if (osName.Contains(Resource.VISTA_OS_NAME))
                {
                    userProfilePath = Environment.SystemDirectory.Remove(1) + Resource.USERS_PATH;
                }
                else
                {
                    userProfilePath = Environment.SystemDirectory.Remove(1) + Resource.DOCUMENTS_AND_SETTINGS;
                }

                if (Directory.Exists(userProfilePath))
                {
                    listOfUsersPresentOnThisMachine = Directory.GetDirectories(userProfilePath);
                }
            }
            catch (ArgumentNullException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex.StackTrace);
            }
            return listOfUsersPresentOnThisMachine;
        }

        #region Error handling
        /// <summary>
        /// This method handles logging the error.
        /// </summary>
        /// <param name="error">Error to be logged.</param>
        /// <param name="throwError">Variable indicating if the error should be thrown.</param>
        private void HandleRegistryError(string error, bool throwError)
        {
            // Log the error.   
            this.LogMessage(error);

            if (throwError)
            {
                // Throw the Install exception.
                throw new InstallException(error);
            }
        }

        /// <summary>
        /// Writes a message to the console and to the log file for the installation.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        private void LogMessage(string message)
        {
            if (this.Context != null)
            {
                this.Context.LogMessage(message);
            }
        }

        /// <summary>
        /// Returns the appropriate registry key based on the installation type.
        /// </summary>
        /// <returns></returns>
        private UIntPtr HKeyForInstallType()
        {
            UIntPtr HKEY_CURRENT_USER = (UIntPtr)0x80000001;
            UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;

            string allUsers = base.Context.Parameters[allUsersInstallationParam];
            int allUserInstallation;

            if (!String.IsNullOrEmpty(allUsers) &&
                Int32.TryParse(allUsers, out allUserInstallation) &&
                allUserInstallation == 1)
            {
                return HKEY_LOCAL_MACHINE;
            }
            else
            {
                return HKEY_CURRENT_USER;
            }
        }
        #endregion Error handling

        #endregion
    }

    internal static class NativeMethods
    {
        public const int Int32ByteSize = 4;
        public static int NativeBytesPerCharacter = Marshal.SystemDefaultCharSize;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegCreateKey")]
        public static extern int RegCreateKey(UIntPtr keyBase, string keyName, out UIntPtr keyHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyEx")]
        public static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, uint options, int sam, out UIntPtr phkResult);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegSetValueEx")]
        public static extern int RegSetValueEx(UIntPtr keyBase,
                string valueName, IntPtr reserved, RegistryValueKind type,
                string data, int rawDataLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegSetValueEx")]
        public static extern int RegSetValueEx(UIntPtr keyBase,
                string valueName, IntPtr reserved, RegistryValueKind type,
                ref int data, int rawDataLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegDeleteKey")]
        public static extern int RegDeleteKey(UIntPtr keyHandle, string valueName);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegDeleteValue")]
        public static extern int RegDeleteValue(UIntPtr keyHandle, string valueName);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", SetLastError = true)]
        public static extern int RegQueryValueEx(
                                            UIntPtr hKey,
                                            string lpValueName,
                                            int lpReserved,
                                            out uint lpType,
                                            System.Text.StringBuilder lpData,
                                            ref int lpcbData);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        public static extern int RegQueryValueEx(UIntPtr keyBase,
                string valueName, IntPtr reserved, ref RegistryValueKind type,
                IntPtr zero, ref int dataSize);


        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] [Out] out bool lpSystemInfo);

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static bool Is64Bit()
        {
            bool retVal;
            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);            
            return retVal;
        }
    }
}
