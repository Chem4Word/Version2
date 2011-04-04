// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
namespace Chem4Word.UI.Import
{
    /// <summary>
    /// The interface which all import validators/controls used by the mediator must implement
    /// </summary>
    internal interface IValidator
    {
        /// <summary>
        /// Perform the validation / normalisation etc. 
        /// This should only be called by the mediator.
        /// </summary>
        /// <returns>false if any problems have occured, true otherwise</returns>
        bool Process();

        /// <summary>
        /// Request that the mediator begin the validation / normalisation process. 
        /// This should only be called by the mediator.
        /// </summary>
        void Start();

        /// <summary>
        /// A brief summary of any problems encountered - the 
        /// string will be created if Process() returns false
        /// (if Process() returns true then no problems were encountered)
        /// </summary>
        /// <returns>A brief summary of any problems encountered</returns>
        string BriefMessage();

        /// <summary>
        /// A detailed description of any problems encountered - the 
        /// string will be created if Process() returns false
        /// (if Process() returns true then no problems were encountered)
        /// </summary>
        /// <returns>A detailed description of any problems encountered</returns>
        string LongMessage();
    }
}