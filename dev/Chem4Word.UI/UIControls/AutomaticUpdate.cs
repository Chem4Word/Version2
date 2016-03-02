// Created by Mike Williams - 06/01/2016
//
// -----------------------------------------------------------------------
//   Copyright (c) 2016, The Outercurve Foundation.
//   This software is released under the Apache License, Version 2.0.
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Chem4Word.Common;
using Chem4Word.Common.Utilities;
using Microsoft.Win32;

namespace Chem4Word.UI.UIControls
{
    public partial class AutomaticUpdate : Form
    {
        public XDocument NewVersions { get; set; }
        public XDocument CurrentVersion { get; set; }

        private string _existingVersionGuid = string.Empty;
        private string _downloadUrl = string.Empty;
        private string _latestMsiFilePath = string.Empty;

        private Telemetry _telemetry;

        private WebClient _webClient;

        public AutomaticUpdate(Telemetry telemetry)
        {
            string module = "AutomaticUpdate()";
            _telemetry = telemetry;
            InitializeComponent();
            _telemetry.Write(module, "AutomaticUpdate", "Shown");
        }

        private void linkCodeplexPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string module = "linkCodeplexPage_LinkClicked()";
            _telemetry.Write(module, "AutomaticUpdate", "Open CodePlex Page");
            Process.Start("https://chem4word.codeplex.com/releases/view/618360");
            DialogResult = DialogResult.Cancel;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string module = "richTextBox1_LinkClicked()";
            _telemetry.Write(module, "AutomaticUpdate", "Open page " + e.LinkText);

            Process.Start(e.LinkText);
        }

        private void AutomaticUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            string module = "AutomaticUpdate_FormClosing()";
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _telemetry.Write(module, "AutomaticUpdate", "User Dismissed Form");
            }
            if (_webClient != null)
            {
                _webClient.CancelAsync();
            }
        }

        private void btnUpdateNow_Click(object sender, EventArgs e)
        {
            string module = "btnUpdateNow_Click()";
            _telemetry.Write(module, "AutomaticUpdate", "Starting download of " + _downloadUrl);

            progressBar1.Value = 0;
            btnUpdateNow.Enabled = false;
            Cursor.Current = Cursors.AppStarting;

            _webClient = new WebClient();
            _webClient.Headers.Add("user-agent", "Chem4Word Add-In");
            var uri = new Uri(_downloadUrl);

            _latestMsiFilePath = Path.Combine(Path.GetTempPath(), GetFileName(_downloadUrl));

            _webClient.DownloadProgressChanged += OnDownloadProgressChanged;
            _webClient.DownloadFileCompleted += OnDownloadComplete;
            _webClient.DownloadFileAsync(uri, _latestMsiFilePath);
        }

        private void btnUpdateLater_Click(object sender, EventArgs e)
        {
            string module = "btnUpdateLater_Click()";
            _telemetry.Write(module, "AutomaticUpdate", "User Defered Update");
            DialogResult = DialogResult.Cancel;
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            string module = "OnDownloadComplete()";
            _telemetry.Write(module, "AutomaticUpdate", "Download Complete");

            _webClient.DownloadProgressChanged -= OnDownloadProgressChanged;
            _webClient.DownloadFileCompleted -= OnDownloadComplete;

            _webClient.Dispose();
            _webClient = null;

            if (!e.Cancelled)
            {
                StringBuilder sb = new StringBuilder();

                // Un-Install previous version
                if (!string.IsNullOrEmpty(_existingVersionGuid))
                {
                    sb.AppendLine("call msiexec.exe /x " + _existingVersionGuid);
                }
                sb.AppendLine("if %errorlevel%==0 goto success");
                sb.AppendLine("goto end");

                // Install new version if uninstall was successful
                sb.AppendLine(":success");
                sb.AppendLine(_latestMsiFilePath);

                sb.AppendLine(":end");

                string cmdFile = Path.Combine(Path.GetTempPath(), "remove-old-chem4word.bat");
                File.WriteAllText(cmdFile, sb.ToString());

                _telemetry.Write(module, "AutomaticUpdate", "Running update batch file");
                // Execcute batch file
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = cmdFile,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(processStartInfo);
            }
        }

        private void AutomaticUpdate_Load(object sender, EventArgs e)
        {
            string module = "AutomaticUpdate_Load()";

            try
            {
                string currentVersionNumber = CurrentVersion.Root.Element("Number").Value;
                DateTime currentReleaseDate = SafeDate.Parse(CurrentVersion.Root.Element("Released").Value);

                lblInfo.Text = "Your current version of Chem4Word is " + currentVersionNumber + "; Released " + currentReleaseDate.ToString("dd-MMM-yyyy");
                _telemetry.Write(module, "AutomaticUpdate", lblInfo.Text);

                _existingVersionGuid = GetGuid();

                var versions = NewVersions.XPathSelectElements("//Version");
                foreach (var version in versions)
                {
                    if (string.IsNullOrEmpty(_downloadUrl))
                    {
                        _downloadUrl = version.Element("Url").Value;
                    }

                    var thisVersionNumber = version.Element("Number").Value;
                    DateTime thisVersionDate = SafeDate.Parse(version.Element("Released").Value);

                    if (currentReleaseDate >= thisVersionDate)
                    {
                        break;
                    }

                    AddHeaderLine("Version " + thisVersionNumber + "; Released " + thisVersionDate.ToString("dd-MMM-yyyy"), Color.Blue);
                    var changes = version.XPathSelectElements("Changes/Change");
                    foreach (var change in changes)
                    {
                        AddBulletItem(change.Value, Color.Black);
                    }
                }
            }
            catch (Exception)
            {
                //
                this.Close();
            }
        }

        private void AddHeaderLine(string line, Color colour)
        {
            richTextBox1.Select(richTextBox1.TextLength, 0);
            richTextBox1.SelectionFont = new Font("Arial", 16);
            richTextBox1.SelectionColor = colour;
            richTextBox1.AppendText(line + Environment.NewLine);
        }

        private void AddLine(string line, Color colour)
        {
            richTextBox1.Select(richTextBox1.TextLength, 0);
            richTextBox1.SelectionFont = new Font("Arial", 12);
            richTextBox1.SelectionColor = colour;
            richTextBox1.AppendText(line + Environment.NewLine);
        }

        private void AddBulletItem(string line, Color colour)
        {
            richTextBox1.Select(richTextBox1.TextLength, 0);
            richTextBox1.BulletIndent = 25;
            richTextBox1.SelectionBullet = true;
            richTextBox1.SelectionFont = new Font("Arial", 12);
            richTextBox1.SelectionColor = colour;
            richTextBox1.AppendText(line + Environment.NewLine);
            richTextBox1.SelectionBullet = false;
        }

        private static string GetFileName(string url)
        {
            var fileName = string.Empty;
            var uri = new Uri(url);
            if (uri.Scheme.Equals(Uri.UriSchemeHttp) || uri.Scheme.Equals(Uri.UriSchemeHttps))
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                httpWebRequest.Method = "HEAD";
                httpWebRequest.UserAgent = "Chem4Word Add-In";
                httpWebRequest.AllowAutoRedirect = false;
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode.Equals(HttpStatusCode.Redirect) ||
                    httpWebResponse.StatusCode.Equals(HttpStatusCode.Moved) ||
                    httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently))
                {
                    if (httpWebResponse.Headers["Location"] != null)
                    {
                        var location = httpWebResponse.Headers["Location"];
                        fileName = GetFileName(location);
                        return fileName;
                    }
                }
                var contentDisposition = httpWebResponse.Headers["content-disposition"];
                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    const string lookForFileName = "filename=";
                    var index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase);
                    if (index >= 0)
                        fileName = contentDisposition.Substring(index + lookForFileName.Length);
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Substring(1, fileName.Length - 2);
                    }
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(uri.LocalPath);
            }
            return fileName;
        }

        private string LookForUninstall(string root, string branch, string name)
        {
            string result = string.Empty;

            try
            {
                RegistryKey key = null;
                switch (root)
                {
                    case "HKLM":
                        key = Registry.LocalMachine.OpenSubKey(branch);
                        break;

                    case "HKCU":
                        key = Registry.CurrentUser.OpenSubKey(branch);
                        break;
                }

                if (key != null)
                {
                    //string[] xx = RegistryUtility.GetSubKeys();

                    foreach (string subkeyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                        {
                            //Debug.WriteLine("Found key " + subkeyName);
                            if (subkey != null)
                            {
                                string displayName = subkey.GetValue("DisplayName") as string;
                                //Debug.WriteLine(" Display Name is " + displayName);
                                if (!string.IsNullOrEmpty(displayName))
                                {
                                    if (displayName.ToLower().Equals(name.ToLower()))
                                    {
                                        result = subkeyName;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    key = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //
            }

            return result;
        }

        private string LookForProduct(string root, string branch, string name)
        {
            string result = string.Empty;

            try
            {
                RegistryKey key = null;
                switch (root)
                {
                    case "HKLM":
                        key = Registry.LocalMachine.OpenSubKey(branch);
                        break;

                    case "HKCR":
                        key = Registry.ClassesRoot.OpenSubKey(branch);
                        break;
                }

                if (key != null)
                {
                    foreach (string subkeyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                        {
                            if (subkey != null)
                            {
                                string productName = subkey.GetValue("ProductName") as string;
                                if (!string.IsNullOrEmpty(productName))
                                {
                                    if (productName.ToLower().Equals(name.ToLower()))
                                    {
                                        string productIcon = subkey.GetValue("ProductIcon") as string;
                                        if (!string.IsNullOrEmpty(productIcon))
                                        {
                                            char[] delimiters = new[] { '{', '}' };
                                            string[] parts = productIcon.Split(delimiters);
                                            if (parts.Length == 3)
                                            {
                                                result = "{" + parts[1] + "}";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    key = null;
                }
            }
            catch
            {
                //
            }

            return result;
        }

        private string GetGuid()
        {
            string module = "GetGuid()";

            string title = "Chemistry Add-in for Word";

            string root = "HKLM";
            string branch = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            string result = LookForUninstall(root, branch, title);

            if (string.IsNullOrEmpty(result))
            {
                root = "HKLM";
                branch = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                result = LookForUninstall(root, branch, title);
            }

            if (string.IsNullOrEmpty(result))
            {
                root = "HKCU";
                branch = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                result = LookForUninstall(root, branch, title);
            }

            if (string.IsNullOrEmpty(result))
            {
                root = "HKCR";
                branch = @"Installer\Products";
                result = LookForProduct(root, branch, title);
            }

            if (string.IsNullOrEmpty(result))
            {
                root = "HKLM";
                branch = @"Software\Classes\Installer\Products";
                result = LookForProduct(root, branch, title);
            }

            if (string.IsNullOrEmpty(result))
            {
                // Default to product code of Beta 6
                result = "{3CCB73D3-0CC8-4AD2-AE08-E207A57CA8FD}";
                _telemetry.Write(module, "AutomaticUpdate", "Using default Guid of " + result);
            }
            else
            {
                _telemetry.Write(module, "AutomaticUpdate", "Found Guid " + result + " in " + root + "\\" + branch);
            }

            return result;
        }
    }
}