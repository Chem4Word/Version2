// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using Chem4Word.UI.Converters;
using Chem4Word.UI.Navigator;
using Chem4Word.UI.TwoD;
using Numbo.Cml;
using Numbo.Coa;
using Image = System.Windows.Controls.Image;

namespace Chem4Word.UI.WebServices
{
    /// <summary>
    /// Interaction logic for OpsinLookUp.xaml
    /// </summary>
    public partial class OpsinLookUp : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged events

        ///<summary>
        /// <see cref="INotifyPropertyChanged"/>
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged events

        private readonly PrettyXmlConverter converter = new PrettyXmlConverter();
        private XDocument xDocument;

        public OpsinLookUp()
        {
            InitializeComponent();
        }

        public XDocument ResultDocument
        {
            get { return xDocument; }
            private set
            {
                if (xDocument != value)
                {
                    xDocument = value;
                    OnPropertyChanged("ResultDocument");
                }
            }
        }

        public string SearchTerm
        {
            get { return searchBox.Text; }
            set { searchBox.Text = value; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ClearResults()
        {
            label.Content = string.Empty;
            ResultDocument = null;
            //reader.Document = null;
            grid.Children.Clear();
        }

        private void ShowResults(XDocument xDocument)
        {
            //reader.Document = converter.Render(xDocument.Root);
            ContextObject contextObject = new ContextObject(xDocument);
            
            IEnumerable<DepictionOption> possibleDepictions = Depiction.PossibleDepictionOptions(contextObject);
            DepictionOption chosenDepiction = possibleDepictions.FirstOrDefault();
            foreach(DepictionOption possibleDepictionOption in possibleDepictions)
            {
                if (Depiction.Is2D(possibleDepictionOption))
                {
                    chosenDepiction = possibleDepictionOption;
                    break;
                }
            }
            CmlMolecule parent = new CmlMolecule((XElement)chosenDepiction.MachineUnderstandableOption);
            var editor = new CanvasContainer(contextObject, parent);

            editor.GeneratePng(true);

            Image img = new Image
            {
                Source = NavigatorItem.ToBitmapSource(new Bitmap(editor.PngFileOutput)),
                Height = 142,
                Margin = new Thickness(0, 1, 0, 1)
            };
            grid.Children.Clear();
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(img);

            // Sometimes the the open state of the file is not update quickly enough,
            // So that we need to invoke GC to refresh the environment states.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Delete the png file
            File.Delete(editor.PngFileOutput);
        }

        private bool ProcessResponse(HttpWebResponse response)
        {
            bool ok = HttpStatusCode.OK.Equals(response.StatusCode);
            if (ok)
            {
                // we will read data via the response stream
                using (
                    Stream resStream = response.GetResponseStream())
                {
                    ResultDocument = XDocument.Load(new StreamReader(resStream));
                    ShowResults(ResultDocument);
                }
            }
            else
            {
                if (HttpStatusCode.NotFound.Equals(response.StatusCode))
                {
                    label.Content = string.Format(CultureInfo.InvariantCulture, "No valid representation of the name {0} has been found", SearchTerm);
                }
                else
                {
                    label.Content = "An unexpected problem has occured when parsing this name";
                }
            }
            return ok;
        }

        private void Search()
        {
            ClearResults();
            if (PerformSearch())
            {
                Keyboard.Focus(importButton);
            }
        }

        private bool PerformSearch()
        {
            Cursor originalCursor = Cursor;
            Cursor = Cursors.Wait;
            UriBuilder builder = new UriBuilder(Properties.Resources.OPSIN_URL_CAMB + SearchTerm);

            HttpWebRequest request = (HttpWebRequest)
                                     WebRequest.Create(builder.Uri);
            request.Timeout = 3000;
            request.Accept = "chemical/x-cml";
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                return ProcessResponse(response);
            }
            catch (WebException ex)
            {
                if (WebExceptionStatus.Timeout.Equals(ex.Status))
                {
                    label.Content = "Please try again later - the service has timed out";
                }
                else
                {
                    if ("The remote server returned an error: (404) Not Found.".Equals(ex.Message))
                    {
                        label.Content = string.Format(CultureInfo.InvariantCulture, "It was not possible to construct a connection table from {0}",
                                                      SearchTerm);
                    }
                    else
                    {
                        label.Content = string.Format(CultureInfo.InvariantCulture, "An unexpected error has occurred:\n{0} {1}", ex.Status,
                                                      ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                label.Content = string.Format(CultureInfo.InvariantCulture, "An unexpected error has occurred:\n{0}", ex.Message);
            }
            finally
            {
                Cursor = originalCursor;
            }
            return false;
        }

        private void SearchBoxSearch(object sender, RoutedEventArgs e)
        {
            SearchTerm = searchBox.Text.Trim();
            Search();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (SearchTerm.Trim().Length > 0)
            {
                Search();
            }
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }

        private void opsinLookUp_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(searchBox);
        }
    }

    public class EditingToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEditing = false;
            if (values[0] is bool)
            {
                isEditing = (bool) values[0];
            }
            // if editing then collapsed
            return isEditing ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextInputToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null
            // (to avoid crash bugs for views in the designer)
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool) values[0];
                bool hasFocus = (bool) values[1];

                if (hasFocus || hasText)
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextInputToEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null
            // (to avoid crash bugs for views in the designer)
            if (values[0] is bool)
            {
                return !(bool) values[0];
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}