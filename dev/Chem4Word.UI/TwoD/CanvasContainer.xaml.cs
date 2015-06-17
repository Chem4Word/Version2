// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using log4net;
using Numbo.Cml;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD
{
    /// <summary>
    /// Interaction logic for CanvasContainer.xaml
    /// </summary>
    public partial class CanvasContainer
    {
        #region DrawingMode enum

        public enum DrawingMode
        {
            Select,
            Draw,
            Delete,
            BondSelect
        } ;

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof (CanvasContainer));

        private ContextObject originalContextObject;
        private string pngFile = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        public CanvasContainer()
        {
            //StartWPFApp();
            //Dispatcher.Run();
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextObject"></param>
        /// <param name="molecule"></param>
        public CanvasContainer(ContextObject contextObject, CmlMolecule molecule)
            : this()
        {
            SetData(contextObject, molecule);
        }

       

        public event EventHandler<EventArgs> Refreshed;
        /// <summary>
        /// Gets PNG file name.
        /// </summary>
        public string PngFileOutput
        {
            get { return pngFile; }
        }

        public ChemCanvas Canvas
        {
            get { return chemCanvas; }
        }

        /// <summary>
        /// Get output Cml, formatted in XDocument class.
        /// </summary>
        public XDocument CmlOutPut
        {
            get { return this.originalContextObject.Cml; }
        }

        public ContextObject ContextObject {
            get { return chemCanvas.ContextObject; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ConnectionTableChanged
        {
            get { return chemCanvas.ConnectionTableChanged; }
        }

        public void SetData(ContextObject contextObject,
                            CmlMolecule molecule)
        {
            originalContextObject = contextObject;
            chemCanvas.SetData(contextObject, molecule);
            chemCanvas.Refresh();
        }

        public void Render()
        {
            chemCanvas.Refresh();
        }

        /// <summary>
        /// Generate PNG image froma givent temporary file name.
        /// </summary>
        public void GeneratePng(bool transparentBackground) {
            Log.Info("drawing (transparent = "+transparentBackground+")");
            // redraw the molecule with new bounds and using the png border
            chemCanvas.Refresh(true);
            var originalBkg = chemCanvas.Background;
            try
            {
                if (transparentBackground)
                {
                    chemCanvas.Background = new SolidColorBrush(Colors.Transparent);
                }

                pngFile = Path.GetTempFileName();
                pngFile = pngFile.Replace(".tmp", ".png");
                CreatePng(pngFile);
            }
            finally
            {
                chemCanvas.Background = originalBkg;
            }
        }

        /// <summary>
        /// Create PNG image.
        /// </summary>
        /// <param name="fileName">PNG image file name.</param>
        internal void CreatePng(string fileName)
        {
            if (chemCanvas == null)
            {
                throw new Exception("Surface was not initialized.");
            }

            // Do something with the Cml
            ChemCanvas surface = this.chemCanvas;
            string path = fileName;

            // Save current canvas transform
            Transform transform = surface.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            surface.LayoutTransform = null;

            foreach (UIElement child in chemCanvas.Children)
            {
                child.RenderTransform = null;
            }

            var width = Math.Abs(surface.ToScreenX(surface.ContextObject.ViewBoxDimensions.Width));
            var height = Math.Abs(surface.ToScreenY(surface.ContextObject.ViewBoxDimensions.Height));

            // Get the size of canvas
            var size = new Size(width,height);

            // Measure and arrange the surface

            // VERY IMPORTANT
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            var renderBitmap =
                new RenderTargetBitmap(
                    (int) size.Width,
                    (int) size.Height,
                    96d,
                    96d,
                    PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            // Create a file stream for saving image
            using (var outStream = new FileStream(path, FileMode.Create))
            {
                // Use png encoder for our data
                var encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }

            // Restore previously saved layout
            surface.LayoutTransform = transform;
        }

        private void SelectMode_Click(object sender, RoutedEventArgs e)
        {
            this.chemCanvas.DrawingMode = DrawingMode.Select;
        }

        private void BondSelectMode_Click(object sender, RoutedEventArgs e)
        {
            this.chemCanvas.DrawingMode = DrawingMode.BondSelect;
        }

        private void DrawMode_Click(object sender, RoutedEventArgs e)
        {
            this.chemCanvas.DrawingMode = DrawingMode.Draw;
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            this.chemCanvas.Flip();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.chemCanvas.DrawingMode = DrawingMode.Delete;
        }
    }
}