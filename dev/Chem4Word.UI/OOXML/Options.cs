using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Word2010AddIn.OOXML
{
    public class Options
    {
        public Boolean ShowHydrogens { get; set; }
        public Boolean ColouredAtoms { get; set; }

        public Options()
        {
            ShowHydrogens = true;
            ColouredAtoms = true;
        }
    }
}
