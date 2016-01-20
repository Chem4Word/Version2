// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.
//  This software is released under the Apache License, Version 2.0.
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Chem4Word.Api;
using Chem4Word.UI.ManageView;
using Chem4Word.UI.Tools;
using Chem4Word.UI.TwoD;
using log4net;
using Numbo.Cml;
using Numbo.Coa;
using Image = System.Windows.Controls.Image;

namespace Chem4Word.UI.Navigator
{
    /// <summary>
    /// Interaction logic for NavigatorItem.xaml
    /// </summary>
    public partial class NavigatorItem : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NavigatorItem));

        private readonly IChemistryZone chemistryZone;
        private bool hasImage;

        private bool isSelected;
        private bool collapseNavigatorDepiction;

        public NavigatorItem()
        {
            InitializeComponent();
            IsSelected = false;
            HasImage = false;
        }

        /// <summary>
        /// Initializes a new instance of the Navigator class.
        /// </summary>
        /// <param name="chemistryZone">must not be null</param>
        public NavigatorItem(IChemistryZone chemistryZone)
            : this()
        {
            this.chemistryZone = chemistryZone;
            chemistryZone.ChemistryZoneUpdated += ChemistryZoneUpdatedHandler;
            chemistryZone.ChemistryZonePropertiesUpdated += ChemistryZonePropertiesUpdatedHandler;
            Refresh();
            UpdateDepicitonOptions();
        }

        private void ChemistryZonePropertiesUpdatedHandler(object sender, EventArgs e)
        {
            Refresh();
            UpdateDepicitonOptions();
        }

        public IChemistryZone ChemistryZone
        {
            get { return chemistryZone; }
        }

        public bool HasImage
        {
            get { return hasImage; }
            set
            {
                if (value != hasImage)
                {
                    hasImage = value;
                    OnPropertyChanged("HasImage");
                }
            }
        }

        internal IChemistryZone Source
        {
            get { return ChemistryZone; }
        }

        /// <summary>
        /// Gets or Sets a selected navigator item.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool CollapseNavigatorDepiction
        {
            get { return collapseNavigatorDepiction; }
            set
            {
                if (collapseNavigatorDepiction != value)
                {
                    if (chemistryZone != null)
                    {
                        var newChemistryZoneProperties = chemistryZone.Properties.Clone();
                        newChemistryZoneProperties.CollapseNavigatorDepiction = value;
                        ChemistryZone.Properties = newChemistryZoneProperties;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or Sets display text.
        /// </summary>
        public string DisplayText
        {
            get { return title.Text; }
            set
            {
                title.Inlines.Clear();
                TextTools.ConvertLatexFormattedStringToTextBlock(ref title, value);
            }
        }

        private ContextObject ContextObjectProperty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DepictionOptionEventArgs> InsertLinkAs;

        public event EventHandler<DepictionOptionEventArgs> InsertCopyAs;

        /// <summary>
        /// Event fired when the selection of this zone changes
        /// </summary>
        public event EventHandler<RoutedEventArgs> SelectedChanged;

        private void UpdateDepicitonOptions()
        {
            linkMenuItem.Items.Clear();
            copyMenuItem.Items.Clear();
            foreach (
                var depictionOption in
                    Depiction.PossibleDepictionOptions(ContextObjectProperty))
            {
                var linkDepictionMenuItem = new DepictionMenuItem(ContextObjectProperty, depictionOption, chemistryZone.WordVersion);
                linkDepictionMenuItem.Click += LinkedMenuItemClick;
                linkMenuItem.Items.Add(linkDepictionMenuItem);
                var copyDepictionMenuItem = new DepictionMenuItem(ContextObjectProperty, depictionOption, chemistryZone.WordVersion);
                copyDepictionMenuItem.Click += CopyMenuItemClick;
                copyMenuItem.Items.Add(copyDepictionMenuItem);

                if (Depiction.Is2D(depictionOption))
                {
                    linkMenuItem.Items.Add(new Separator());
                    copyMenuItem.Items.Add(new Separator());
                }
            }
            linkMenuItem.Items.Add(new Separator());
            copyMenuItem.Items.Add(new Separator());
            var linkAddNewMenuItem = new MenuItem { Header = "Other..." };
            var copyAddNewMenuItem = new MenuItem { Header = "Other..." };
            linkAddNewMenuItem.Click += AddNewLinkMenuItemClick;
            copyAddNewMenuItem.Click += AddNewCopyMenuItemClick;
            linkMenuItem.Items.Add(linkAddNewMenuItem);
            copyMenuItem.Items.Add(copyAddNewMenuItem);
        }

        private void ChemistryZoneUpdatedHandler(object sender, EventArgs e)
        {
            var chemistryZoneSender = sender as IChemistryZone;
            if (chemistryZoneSender != null)
            {
                if (chemistryZoneSender.ID.Equals(chemistryZone.ID))
                {
                    Refresh();
                    UpdateDepicitonOptions();
                }
            }
        }

        private void AddImage(DepictionOption twoDDepictionOption)
        {
            bool invertY = chemistryZone.WordVersion > 2007;
            expander.IsExpanded = true;
            thumbnailGrid.Children.Clear();
            var parent = new CmlMolecule((XElement)twoDDepictionOption.MachineUnderstandableOption).CloneMolecule(1.54, invertY);

            var editor = new CanvasContainer(ContextObjectProperty, parent);
            editor.GeneratePng(true);
            // Create new bitmap image
            Image ImageTemp = new Image();
            // Get png from editor as bitmap
            Bitmap imageBitmap = (Bitmap)Bitmap.FromFile(editor.PngFileOutput);

            //if (chemistryZone.WordVersion > 2007)
            //{
            //    imageBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //}

            ImageTemp.Source = ToBitmapSource(imageBitmap);
            // Put it in the right position
            ImageTemp.VerticalAlignment = VerticalAlignment.Center;
            ImageTemp.HorizontalAlignment = HorizontalAlignment.Center;
            ImageTemp.Margin = new Thickness(0, 1, 0, 1);

            // Fix the height to the size of the original png unless...
            if (ImageTemp.Source.Width > 235)
            {
                // Maximum width is the width of the container
                ImageTemp.Width = 235;
            }
            else
            {
                // Otherwise scale to height
                ImageTemp.Height = ImageTemp.Source.Height;
            }
            

            /* Previous code
            var img = new Image
            {
                Source = ToBitmapSource(new Bitmap(editor.PngFileOutput)),
                Height = 71,
                Margin = new Thickness(0, 1, 0, 1)
            };
             */

            // Clear out existing image
            thumbnailGrid.Children.Clear();
            // Add new thumbnail
            thumbnailGrid.Children.Add(ImageTemp);

            // Sometimes the the open state of the file is not update quickly enough,
            // So that we need to invoke GC to refresh the environment states.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                // Delete the png file
                File.Delete(editor.PngFileOutput);
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void Refresh()
        {
            collapseNavigatorDepiction = chemistryZone.Properties.CollapseNavigatorDepiction;
            ContextObjectProperty = ChemistryZone.AsContextObject();
            var navigatorDepictionOption = DepictionOption.CreateDepictionOption(ContextObjectProperty.Cml,
                                                                                             ChemistryZone.
                                                                                                 Properties.
                                                                                                 NavigatorDepictionOptionXPath);
            DisplayText = navigatorDepictionOption.GetAsLatexFormattedString();
            var associatedDepictionOption = navigatorDepictionOption.GetAssociatedTwoDDepictionOption();
            HasImage = (associatedDepictionOption != null);

            if (!CollapseNavigatorDepiction && HasImage)
            {
                AddImage(associatedDepictionOption);
            }
            else
            {
                expander.IsExpanded = false;
            }
            ClearAvailableDepitionOptions();
        }

        private void ClearAvailableDepitionOptions()
        {
            linkMenuItem.Items.Clear();
            copyMenuItem.Items.Clear();
        }

        private void AddNewLinkMenuItemClick(object sender, RoutedEventArgs e)
        {
            var newDepictionOption = GetNewDepictionOption();
            if (newDepictionOption != null)
            {
                InsertLinkAs(this, new DepictionOptionEventArgs
                {
                    NewDepictionOption = newDepictionOption
                });
            }
        }

        private void AddNewCopyMenuItemClick(object sender, RoutedEventArgs e)
        {
            var newDepictionOption = GetNewDepictionOption();
            if (newDepictionOption != null)
            {
                InsertCopyAs(this, new DepictionOptionEventArgs
                {
                    NewDepictionOption = newDepictionOption
                });
            }
        }

        private DepictionOption GetNewDepictionOption()
        {
            var currentDepictionOptions = Depiction.PossibleDepictionOptions(ContextObjectProperty);
            var editLabels = new EditLabels(ContextObjectProperty, null, null, false);
            editLabels.AddNewLabel();
            if (editLabels.ShowDialog() == true)
            {
                chemistryZone.Cml = editLabels.ContextObject.Cml;
                var newDepictionOptions =
                    Depiction.PossibleDepictionOptions(editLabels.ContextObject);
                return newDepictionOptions.FirstOrDefault(newDepictionOption => !currentDepictionOptions.Contains(newDepictionOption));
            }
            return null;
        }

        private void LinkedMenuItemClick(object sender, RoutedEventArgs e)
        {
            Log.Info("linked menu item clicked");
            if (InsertLinkAs != null)
            {
                var menuItem = (DepictionMenuItem)sender;
                InsertLinkAs(this, new DepictionOptionEventArgs
                {
                    NewDepictionOption = menuItem.DepictionOption
                });
            }
        }

        private void CopyMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (InsertCopyAs != null)
            {
                var menuItem = (DepictionMenuItem)sender;
                InsertCopyAs(this, new DepictionOptionEventArgs
                {
                    NewDepictionOption = menuItem.DepictionOption
                });
            }
        }

        internal static BitmapSource ToBitmapSource(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                                                         Int32Rect.Empty,
                                                         BitmapSizeOptions.FromEmptyOptions());
        }

        private void ThumbnailGridMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
            if (SelectedChanged != null)
            {
                SelectedChanged(this, null);
            }
        }

        private void TitleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
            if (SelectedChanged != null)
            {
                SelectedChanged(this, null);
            }
        }

        private void TitleMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            navigatorItemTitleDepictionOptionsMenu.Items.Clear();
            var possibleDepictionOptions =
                Depiction.PossibleDepictionOptions(ContextObjectProperty).SkipWhile(Depiction.Is2D);
            foreach (var titleMenuItem in
                possibleDepictionOptions.Select(depictionOption => new DepictionMenuItem(ContextObjectProperty, depictionOption, chemistryZone.WordVersion)))
            {
                navigatorItemTitleDepictionOptionsMenu.Items.Add(titleMenuItem);
                titleMenuItem.Click += TitleMenuItemClick;
            }
        }

        private void TitleMenuItemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as DepictionMenuItem;
            if (menuItem != null)
            {
                var newChemistryZoneProperties = chemistryZone.Properties.Clone();
                newChemistryZoneProperties.SetNavigatorDepictionOption(menuItem.DepictionOption);
                ChemistryZone.Properties = newChemistryZoneProperties;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ExpanderExpanded(object sender, RoutedEventArgs e)
        {
            CollapseNavigatorDepiction = false;
        }

        private void ExpanderCollapsed(object sender, RoutedEventArgs e)
        {
            CollapseNavigatorDepiction = true;
        }
    }
}