// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Xml.Linq;
using Chem4Word.Common;
using Chem4Word.UI.Tools;
using Chem4Word.UI.TwoD;
using Microsoft.Office.Interop.Word;
using Numbo.Cml;
using Numbo.Coa;
using Image=System.Windows.Controls.Image;

namespace Chem4Word.UI.Navigator
{
    public class DepictionMenuItem : MenuItem
    {
        public DepictionMenuItem(ContextObject contextObject, DepictionOption depictionOption)
        {
            DepictionOption = depictionOption;
            CreateList(contextObject, depictionOption);
        }

        public DepictionOption DepictionOption { get; private set; }

        private void CreateList(ContextObject contextObject, DepictionOption depictionOption)
        {
            if (Depiction.Is2D(depictionOption))
            {
                // Invert image only in Word 2007
                SystemHelper systemHelper = new SystemHelper();
                bool invertY = systemHelper.WordVersion > 2007;
                CanvasContainer editor = new CanvasContainer(contextObject,
                                                             new CmlMolecule(
                                                                 (XElement) depictionOption.MachineUnderstandableOption).CloneMolecule(1.54, invertY));
                editor.GeneratePng(true);

                Image img = new Image
                                {
                                    Source = NavigatorItem.ToBitmapSource(new Bitmap(editor.PngFileOutput)),
                                    Height = 50,
                                };
                Header = img;

                // Sometimes the the open state of the file is not update quickly enough,
                // So that we need to invoke GC to refresh the environment states.
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Delete the png file
                File.Delete(editor.PngFileOutput);
                editor = null;
            }
            else
            {
                TextBlock textBlock = new TextBlock {FontSize = 15};
                TextTools.ConvertLatexFormattedStringToTextBlock(ref textBlock,
                                                                 depictionOption.GetAsLatexFormattedString());
                Header = textBlock;
            }
        }
    }
}