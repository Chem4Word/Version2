// Created by Mike Williams - 22/09/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Chem4Word.Common
{
    public class SystemHelper
    {
        public string MachineId { get; set; }
        public string SystemOs { get; set; }
        public string WordVersion { get; set; }

        private string CryptoRoot = @"SOFTWARE\Microsoft\Cryptography";
        private string ProductsRoot = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

        public SystemHelper()
        {
            #region Get Machine Guid
            try
            {
                // Need special routine here as MachineGuid does not exist in the wow6432 path
                MachineId = RegistryWOW6432.GetRegKey64(RegHive.HKEY_LOCAL_MACHINE, CryptoRoot, "MachineGuid");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MachineId = "Exception " + ex.Message;
            }
            #endregion

            #region Get OS Version
            try
            {
                OperatingSystem operatingSystem = Environment.OSVersion;

                string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
                string CsdVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");

                if (!string.IsNullOrEmpty(ProductName))
                {
                    StringBuilder sb = new StringBuilder();
                    if (!ProductName.StartsWith("Microsoft"))
                    {
                        sb.Append("Microsoft ");
                    }
                    sb.Append(ProductName);
                    if (!string.IsNullOrEmpty(CsdVersion))
                    {
                        sb.Append(" ");
                        sb.AppendLine(CsdVersion);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(operatingSystem.ServicePack))
                        {
                            sb.Append(" ");
                            sb.Append(operatingSystem.ServicePack);
                        }
                    }
                    sb.Append(" [");
                    sb.Append(operatingSystem.Version.ToString());
                    sb.Append("]");
                    SystemOs = sb.ToString().Replace(Environment.NewLine, "").Replace("Service Pack ", "SP");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                SystemOs = "Exception " + ex.Message;
            }

            #endregion

            #region Get Office/Word Version

            try
            {
                #region Get Office Product String

                string officeProductName = "";

                RegistryKey localMachine = Registry.LocalMachine;
                RegistryKey products = localMachine.OpenSubKey(ProductsRoot, false);
                if (products != null)
                {
                    string[] productFolders = products.GetSubKeyNames();

                    foreach (string p in productFolders)
                    {
                        RegistryKey installProperties = products.OpenSubKey(p);
                        if (installProperties != null)
                        {
                            string parentDisplayName = (string)installProperties.GetValue("ParentDisplayName");
                            string displayName = (string)installProperties.GetValue("DisplayName");
                            if ((parentDisplayName != null) && (parentDisplayName.StartsWith("Microsoft Office")))
                            {
                                //Debug.WriteLine(parentDisplayName);
                                officeProductName = parentDisplayName;
                                break;
                            }
                        }
                    }
                }

                #endregion

                #region Get Word Version

                string wordVersionNumber = "";
                string name = @"Software\Microsoft\Windows\CurrentVersion\App Paths\winword.exe";

                // 1. looks inside CURRENT_USER:
                RegistryKey mainKey = Registry.CurrentUser;
                mainKey = mainKey.OpenSubKey(name, false);

                if (mainKey == null)
                {
                    //2. looks inside LOCAL_MACHINE:
                    mainKey = Registry.LocalMachine;
                    mainKey = mainKey.OpenSubKey(name, false);
                }

                if (mainKey != null)
                {
                    string path = mainKey.GetValue(string.Empty).ToString();
                    try
                    {
                        path = path.Replace("\"", "");
                        FileVersionInfo fi = FileVersionInfo.GetVersionInfo(path);
                        wordVersionNumber = fi.FileVersion;

                        // Handle product not found in uninstall section
                        if (string.IsNullOrEmpty(officeProductName))
                        {
                            officeProductName = fi.ProductName;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    string sp = GetOfficeServicePack(wordVersionNumber);
                    if (!string.IsNullOrEmpty(sp))
                    {
                        officeProductName = officeProductName + sp;
                    }

                    WordVersion = (officeProductName + " [" + wordVersionNumber + "]");
                }
                else
                {
                    WordVersion = "Microsoft Word not found !";
                }
                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                WordVersion = "Exception " + ex.Message;
            }

            #endregion
        }

        private string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path, false);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch
            {
                return "";
            }
        }

        private int GetOfficeVersionNumber(string wordVersionString)
        {
            int version = -1;

            if (!string.IsNullOrEmpty(wordVersionString))
            {
                string[] parts = wordVersionString.Split('.');
                int major = int.Parse(parts[0]);
                switch (major)
                {
                    case 12:
                        version = 2007;
                        break;
                    case 14:
                        version = 2010;
                        break;
                    case 15:
                        version = 2013;
                        break;
                }
            }
            return version;
        }

        private string GetOfficeServicePack(string wordVersionString)
        {
            string servicePack = "";
            int major = GetOfficeVersionNumber(wordVersionString);
            if (major > 2000)
            {
                string[] parts = wordVersionString.Split('.');
                int build = int.Parse(parts[2]);
                switch (major)
                {
                    case 2007:
                        if (build >= 6213)
                        {
                            servicePack = "SP1";
                        }
                        if (build >= 6425)
                        {
                            servicePack = "SP2";
                        }
                        if (build >= 6607)
                        {
                            servicePack = "SP3";
                        }
                        break;
                    case 2010:
                        if (build >= 6023)
                        {
                            servicePack = "SP1";
                        }
                        if (build >= 7015)
                        {
                            servicePack = "SP2";
                        }
                        break;
                    case 2013:
                        if (build >= 4569)
                        {
                            servicePack = "SP1";
                        }
                        break;
                }
            }

            if (!string.IsNullOrEmpty(servicePack))
            {
                servicePack = " " + servicePack;
            }

            return servicePack;
        }
    }

}
