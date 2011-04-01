using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using log4net.Config;

namespace TwoDUITest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
           protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetUpLogging();
        }

        private void SetUpLogging()
        {
            string AppDataFolder =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Chemistry Add-in for Word");

            string configFilePath = Path.Combine(AppDataFolder, "Log4NetConfig.xml");

            if (File.Exists(configFilePath))
            {
                try
                {
                    File.SetAttributes(configFilePath, FileAttributes.Normal);
                    File.Delete(configFilePath);
                }
                catch
                {
                    //don't do anything, just let it go
                }
            }

            FileInfo configFile = new FileInfo(configFilePath);

            if (!configFile.Exists)
            {
                //if they don't already have a config file, go find it and copy it

                string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                              "Chemistry Add-in for Word\\Log4NetConfig.xml");

                if (File.Exists(logPath))
                {
                    try
                    {
                        File.Copy(logPath, configFile.FullName);
                        XmlDocument document = new XmlDocument();
                        document.Load(configFile.FullName);

                        foreach (XmlNode node in document.GetElementsByTagName("file"))
                        {
                            node.Attributes.GetNamedItem("value").Value =
                                Path.Combine(AppDataFolder,
                                             "C4WLog.txt");
                            break;
                        }
                        document.Save(configFile.FullName);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("Could not find logging configuration file.");
                }
            }
            XmlConfigurator.Configure(configFile);
        }
    }
}