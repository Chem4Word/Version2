using System;

namespace Chem4Word.UI.OOXML
{
    public class C4wOptions
    {
        public Boolean ShowHydrogens { get; set; }
        public Boolean ColouredAtoms { get; set; }

        public C4wOptions()
        {
            ShowHydrogens = true;
            ColouredAtoms = true;
        }
    }
}
