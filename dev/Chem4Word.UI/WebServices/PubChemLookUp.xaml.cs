﻿// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using Chem4Word.UI.Tools;



namespace Chem4Word.UI.WebServices
{
    /// <summary>
    /// Interaction logic for PubChemSearch.xaml
    /// </summary>
    public partial class PubChemSearch : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PubChemResultItem> results = new ObservableCollection<PubChemResultItem>();
        private SortAdorner curAdorner;
        private GridViewColumnHeader curSortCol;
        private PubChemResultItem selectedItem;
        private readonly XslCompiledTransform xslt = new XslCompiledTransform();
        private string errorMessage;
        private string searchResultsAvailable;
        private int resultsCount;
        private string webEnv;
        private int resultPosition;
        private int retStart;
        private bool moreResults;
        private bool prevResults;
        private const int numResults = 20;

        private XDocument xDocument;
        
        public PubChemSearch()
        {
            InitializeComponent();
            ErrorMessage = string.Empty;
            // Handling Search Results
            SearchResultsAvailable = string.Empty;
            // Need to make sure more and prev buttons are hidden
            prevResults = false;
            moreResults = false;
            resultPosition = 0;
            xslt.Load(XmlReader.Create(new StringReader(Properties.Resources.pubchem2cml)));
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

        public PubChemResultItem SelectedItem
        {
            get { return selectedItem; }
            private set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<PubChemResultItem> Results
        {
            get { return results; }
        }


        private void AddResult(PubChemResultItem pubChemResultItem)
        {
            results.Add(pubChemResultItem);
            OnPropertyChanged("Results");
        }

        private void ClearResults()
        {
            results.Clear();
            OnPropertyChanged("Results");
        }

        private void SortClick(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var field = column.Tag as String;
            if (curSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(curSortCol).Remove(curAdorner);
                resultsView.Items.SortDescriptions.Clear();
            }
            var newDir = ListSortDirection.Ascending;
            if (curSortCol == column && curAdorner.Direction == newDir)
            {
                newDir = ListSortDirection.Descending;
            }

            curSortCol = column;
            curAdorner = new SortAdorner(curSortCol, newDir);
            AdornerLayer.GetAdornerLayer(curSortCol).Add(curAdorner);
            resultsView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }

        private void PerformSearch()
        {
            prevResults = false;
            moreResults = false;
            var originalCursor = Cursor;
            Cursor = Cursors.Wait;
            var request = (HttpWebRequest)
                                     WebRequest.Create(
                                         string.Format(CultureInfo.InvariantCulture, 
                                             "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pccompound&term={0}&retmode=xml&relevanceorder=on&usehistory=y&retmax={1}",
                                             SearchTerm, numResults));
            request.Timeout = 30000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                if (HttpStatusCode.OK.Equals(response.StatusCode))
                {
                    using (
                        var resStream = response.GetResponseStream())
                    {
                        var resultDocument = XDocument.Load(new StreamReader(resStream));
                        // Get the count of results
                        resultsCount = int.Parse(resultDocument.XPathSelectElement("//Count").Value);
                        // Current position
                        retStart = int.Parse(resultDocument.XPathSelectElement("//RetStart").Value) + 1;
                        // Max records
                        resultPosition = int.Parse(resultDocument.XPathSelectElement("//RetMax").Value);
                        // WebEnv for history
                        webEnv = resultDocument.XPathSelectElement("//WebEnv").Value;
                        // Set flags for More/Prev buttons
                        PrevResults = false;
                        if (resultPosition < resultsCount)
                        {
                            MoreResults = true;
                        }
                        else
                        {
                            MoreResults = false;
                        }
                        // Set property used for label on form
                        SearchResultsAvailable = "Displaying " + retStart.ToString() + " to " + resultPosition.ToString() + " of " + resultsCount.ToString() + " results";
                        var ids = resultDocument.XPathSelectElements("//Id");
                        var count = ids.Count();
                        if (count > 0)
                        {
                            var sb = new StringBuilder();
                            for (var i = 0; i < count; i++)
                            {
                                var id = ids.ElementAt(i);
                                if (i > 0)
                                {
                                    sb.Append(",");
                                }
                                sb.Append(id.Value);
                            }
                            GetData(sb.ToString());
                        } 
                        else
                        {
                            ErrorMessage = "Sorry, no results were found.";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sorry bad request. Status code: " + response.StatusCode, "Name2Structure",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "The operation has timed out".Equals(ex.Message)
                                    ? "Please try again later - the service has timed out"
                                    : ex.Message;
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void PrepareForSearch()
        {
            ClearResults();
            ErrorMessage = string.Empty;
            SelectedItem = null;
        }

        private void SearchBoxSearch(object sender, RoutedEventArgs e)
        {
            SearchTerm = searchBox.Text.Trim();
            PrepareForSearch();
            PerformSearch();
        }

        private void GetData(string idlist)
        {
            var originalCursor = Cursor;
            Cursor = Cursors.Wait;
            var request = (HttpWebRequest)
                                     WebRequest.Create(
                                         string.Format(CultureInfo.InvariantCulture, 
                                             "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esummary.fcgi?db=pccompound&id={0}&retmode=xml",
                                             idlist));
            request.Timeout = 30000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                if (HttpStatusCode.OK.Equals(response.StatusCode))
                {
                    // we will read data via the response stream
                    using (
                        var resStream = response.GetResponseStream())
                    {
                        var resultDocument = XDocument.Load(new StreamReader(resStream));
                        var compounds = resultDocument.XPathSelectElements("//DocSum");
                        foreach (var compound in compounds)
                        {
                            var id = compound.XPathSelectElement("./Id");
                            var name = compound.XPathSelectElement("./Item[@Name='IUPACName']");
                            var smiles = compound.XPathSelectElement("./Item[@Name='CanonicalSmile']");
                            var formula = compound.XPathSelectElement("./Item[@Name='MolecularFormula']");
                            AddResult( new PubChemResultItem
                                                         {
                                                             Id = id.Value,
                                                             Name = name.Value,
                                                             //Smiles=smiles.Value,
                                                             //Changed it to string.empty as it was throwing Null exception
                                                             //Smiles = string.Empty,
                                                             // Debugging
                                                             Smiles=id.Value,
                                                             Formula = formula.Value
                                                         });

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sorry bad request. Status code: " + response.StatusCode, "Name2Structure",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "The operation has timed out".Equals(ex.Message)
                                    ? "Please try again later - the service has timed out"
                                    : ex.Message;
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void Import(string id)
        {
            var originalCursor = Cursor;
            Cursor = Cursors.Wait;
            /*
             * Using Pubchem REST PUG search to return compounds
             */
            var request = (HttpWebRequest)
                                     WebRequest.Create(
                                         string.Format(CultureInfo.InvariantCulture,
                                         "http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/{0}/record/XML",
                                             id));
            
            request.Timeout = 30000;
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (HttpStatusCode.OK.Equals(response.StatusCode))
                {
                    // we will read data via the response stream
                    using (
                        var resStream = response.GetResponseStream())
                    {
                        var xml = XDocument.Load(new StreamReader(resStream));
                        var result = new XDocument();
                        using (var writer = result.CreateWriter())
                        {
                            xslt.Transform(xml.CreateReader(), writer);
                            ResultDocument = result;
                            DialogResult = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sorry bad request. Status code: " + response.StatusCode, "Name2Structure",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "The operation has timed out".Equals(ex.Message)
                                    ? "Please try again later - the service has timed out"
                                    : ex.Message;
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
           Import(SelectedItem.Id);
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        /// <summary>
        ///  Added by ADW
        /// </summary>
        public string SearchResultsAvailable
        {
            get { return searchResultsAvailable; }
            set
            {
                searchResultsAvailable = value;
                OnPropertyChanged("SearchResultsAvailable");
            }
        }

        public bool MoreResults 
        { 
            get {return moreResults;}
            set
            {
                moreResults = value;
                OnPropertyChanged("MoreResults");
            }
        }

        public bool PrevResults
        {
            get { return prevResults; }
            set
            {
                prevResults = value;
                OnPropertyChanged("PrevResults");
            }
        }
        // End of ADW added

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }

        private void ResultsViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = (PubChemResultItem) resultsView.SelectedItem;
        }

        private void PubChemSearchActivated(object sender, EventArgs e)
        {
            // Removed by ADW - was resetting the search if user returns to search form
            /*if (SearchTerm.Trim().Length > 0)
            {
                PerformSearch();
            }*/
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PubChemSearch_OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(searchBox);
        }

        private void ResultsViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Import(SelectedItem.Id);
        }

        private void ResultsViewKeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Enter.Equals(e.Key))
            {
                Import(SelectedItem.Id);
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            PrepareForSearch();
            prevResults = false;
            moreResults = false;
            var originalCursor = Cursor;
            Cursor = Cursors.Wait;
            var request = (HttpWebRequest)
                                     WebRequest.Create(
                                         string.Format(CultureInfo.InvariantCulture,
                                             "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pccompound&term={0}&retmode=xml&relevanceorder=on&usehistory=y&retmax={1}&WebEnv={2}&RetStart={3}",
                                             SearchTerm, numResults, webEnv, retStart-numResults-1));
            request.Timeout = 30000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (HttpStatusCode.OK.Equals(response.StatusCode))
                {
                    using (
                        var resStream = response.GetResponseStream())
                    {
                        var resultDocument = XDocument.Load(new StreamReader(resStream));
                        // Get the count of results
                        resultsCount = int.Parse(resultDocument.XPathSelectElement("//Count").Value);
                        // Current position
                        retStart = int.Parse(resultDocument.XPathSelectElement("//RetStart").Value);
                        // Reset the value to 1
                        if (retStart == 0)
                        {
                            retStart = 1;
                        }
                        // Max records
                        resultPosition = resultPosition - int.Parse(resultDocument.XPathSelectElement("//RetMax").Value);
                        if (resultPosition < 20)
                        {
                            resultPosition=20;
                        }
                        // WebEnv for history
                        webEnv = resultDocument.XPathSelectElement("//WebEnv").Value;
                        // Set flags for More/Prev buttons
                        if (retStart<=1)
                        {
                            PrevResults = false;
                        }
                        else
	                    {
                            PrevResults = true;
	                    }
                        if (resultPosition < resultsCount)
                        {
                            MoreResults = true;
                        }
                        else
                        {
                            MoreResults = false;
                        }
                        // Set property used for label on form
                        SearchResultsAvailable = "Displaying " + retStart.ToString() + " to " + resultPosition.ToString() + " of " + resultsCount.ToString() + " results";
                        var ids = resultDocument.XPathSelectElements("//Id");
                        var count = ids.Count();
                        if (count > 0)
                        {
                            var sb = new StringBuilder();
                            for (var i = 0; i < count; i++)
                            {
                                var id = ids.ElementAt(i);
                                if (i > 0)
                                {
                                    sb.Append(",");
                                }
                                sb.Append(id.Value);
                            }
                            GetData(sb.ToString());
                        }
                        else
                        {
                            ErrorMessage = "Sorry, no results were found.";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sorry bad request. Status code: " + response.StatusCode, "Name2Structure",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "The operation has timed out".Equals(ex.Message)
                                    ? "Please try again later - the service has timed out"
                                    : ex.Message;
            }
            finally
            {
                Cursor = originalCursor;
            }
        }
        

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            PrepareForSearch();
            prevResults = false;
            moreResults = false;
            var originalCursor = Cursor;
            Cursor = Cursors.Wait;
            var request = (HttpWebRequest)
                                     WebRequest.Create(
                                         string.Format(CultureInfo.InvariantCulture,
                                             "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pccompound&term={0}&retmode=xml&relevanceorder=on&usehistory=y&retmax={1}&WebEnv={2}&RetStart={3}",
                                             SearchTerm, numResults, webEnv, resultPosition));
            request.Timeout = 30000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (HttpStatusCode.OK.Equals(response.StatusCode))
                {
                    using (
                        var resStream = response.GetResponseStream())
                    {
                        var resultDocument = XDocument.Load(new StreamReader(resStream));
                        // Get the count of results
                        resultsCount = int.Parse(resultDocument.XPathSelectElement("//Count").Value);
                        // Current position
                        retStart = int.Parse(resultDocument.XPathSelectElement("//RetStart").Value) + 1;
                        // Max records
                        resultPosition = resultPosition + int.Parse(resultDocument.XPathSelectElement("//RetMax").Value);
                        // WebEnv for history
                        webEnv = resultDocument.XPathSelectElement("//WebEnv").Value;
                        // Set flags for More/Prev buttons
                        PrevResults = true;
                        if (resultPosition < resultsCount)
                        {
                            MoreResults = true;
                        }
                        else
                        {
                            MoreResults = false;
                        }
                        // Set property used for label on form
                        SearchResultsAvailable = "Displaying " + retStart.ToString() + " to " + resultPosition.ToString() + " of " + resultsCount.ToString() + " results";
                        var ids = resultDocument.XPathSelectElements("//Id");
                        var count = ids.Count();
                        if (count > 0)
                        {
                            var sb = new StringBuilder();
                            for (var i = 0; i < count; i++)
                            {
                                var id = ids.ElementAt(i);
                                if (i > 0)
                                {
                                    sb.Append(",");
                                }
                                sb.Append(id.Value);
                            }
                            GetData(sb.ToString());
                        }
                        else
                        {
                            ErrorMessage = "Sorry, no results were found.";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sorry bad request. Status code: " + response.StatusCode, "Name2Structure",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "The operation has timed out".Equals(ex.Message)
                                    ? "Please try again later - the service has timed out"
                                    : ex.Message;
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

      
    }
}