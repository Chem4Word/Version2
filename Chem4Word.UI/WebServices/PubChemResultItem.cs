// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.UI.WebServices
{
    public class PubChemResultItem
    {
        private const string NotFound = "not found";

        public PubChemResultItem()
        {
            Id = NotFound;
            Name = NotFound;
            Smiles = NotFound;
            Formula = NotFound;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Smiles { get; set; }
        public string Formula { get; set; }
    }
}