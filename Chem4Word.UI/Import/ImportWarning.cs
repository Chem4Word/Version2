// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.UI.Import
{
    public class ImportWarning
    {
        public ImportWarning(string message, string location)
        {
            this.Message = message;
            this.Location = location;
        }

        public string Message { private set; get; }
        public string Location { private set; get; }
    }
}