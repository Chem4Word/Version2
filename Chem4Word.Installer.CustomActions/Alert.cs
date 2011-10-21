using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CustomInstaller
{
    public partial class Alert : Form
    {
        private Action currentInstallerAction;

        public Action CurrentInstallerAction
        {
            get { return this.currentInstallerAction; }
            set { currentInstallerAction = value; }
        }

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

        public Alert()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.Text = Resource.MESSAGEBOXCAPTION;
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            try
            {
                applicationList.Items.Clear();

                if (CurrentInstallerAction == Action.Install || CurrentInstallerAction==Action.UnInstall)
                {
                    Process[] wordInstances = Process.GetProcessesByName(Resource.WINWORD);
                    if (wordInstances.Length > 0)
                    {
                        foreach (Process wordInstance in wordInstances)
                        {
                            object item = wordInstance.MainWindowTitle;
                            if (string.IsNullOrEmpty(wordInstance.MainWindowTitle))
                            {
                                item = Resource.WORD_APPLICATION_TITLE;
                            }
                            applicationList.Items.Add(item);
                        }
                    }
                }               

                this.Focus();
                this.BringToFront();
                this.TopMost = true;
                this.TopMost = false;

                int frm_width = this.Width;
                int frm_height = this.Height;
                //Get the Width and Height (resolution) 
                //   of the screen
                Screen src = Screen.PrimaryScreen;
                int src_height = src.Bounds.Height;
                int src_width = src.Bounds.Width;

                //put the form in the center
                int x = (src_width - frm_width) / 2;
                int y = (src_height - frm_height) / 2;

                this.SetDesktopLocation(x, y);
            }
            catch (COMException ex)
            {
                eventLogger.WriteToLog(LogCategory.Installer, ex); 
            }
            catch (InvalidOperationException invalidOperationEx)
            {
                eventLogger.WriteToLog(LogCategory.Installer, invalidOperationEx);
            }
            catch (NotSupportedException notSupportedEx)
            {
                eventLogger.WriteToLog(LogCategory.Installer, notSupportedEx);
            }
        }
     
        
    }
}
