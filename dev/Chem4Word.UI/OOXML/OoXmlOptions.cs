using System;
using System.Windows;

namespace Chem4Word.UI.OOXML
{
    public class OoXmlOptions
    {
        public Boolean ShowHydrogens { get; set; }
        public Boolean ColouredAtoms { get; set; }
        public Boolean Changed { get; set; }
        public Point TopLeft { get; set; }

        public OoXmlOptions()
        {
            ShowHydrogens = true;
            ColouredAtoms = true;
            Changed = false;
        }
    }
}
