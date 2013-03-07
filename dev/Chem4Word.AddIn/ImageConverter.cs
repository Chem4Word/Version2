// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.AddIn {
    internal class ImageConverter : System.Windows.Forms.AxHost {
        private ImageConverter()
            : base(null) {}

        public static stdole.IPictureDisp Convert(System.Drawing.Image image) {
            return (stdole.IPictureDisp) GetIPictureDispFromPicture(image);
        }
    }
}