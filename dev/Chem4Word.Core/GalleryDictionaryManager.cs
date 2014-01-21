// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Globalization;
using System.IO;

namespace Chem4Word.Core
{
    /// <summary>
    /// This class funcation as a gallery files dictionary management.
    /// It controls how gallery file is saved and read.
    /// </summary>
    internal class GalleryDictionaryManager
    {
        private readonly String localFolder;
        private const string ChemistryGalleryPath = @"Chemistry Add-in for Word\Chemistry Gallery";

        /// <summary>
        /// Initializes a new instance of the CmlFilesManager class.
        /// </summary>
        internal GalleryDictionaryManager()
        {
            // Initialization
            localFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                           ChemistryGalleryPath);
            Directory.CreateDirectory(localFolder);
        }

        /// <summary>
        /// Save gallery instance.
        /// </summary>
        /// <param name="id">GUID value for indentify gallery instance.</param>
        /// <param name="cmlString">Cml string that is saved in the file.</param>
        internal void Add(String id, String cmlString)
        {
            StreamWriter writer =
                new StreamWriter(localFolder + String.Format(CultureInfo.InvariantCulture, @"\{0}.cml", id));
            writer.Write(cmlString);
            writer.Close();
        }

        /// <summary>
        /// Read Cml content from gallery dictionary.
        /// </summary>
        /// <param name="id">GUID value for indentify gallery instance.</param>
        /// <returns>Return Cml content base on input id.</returns>
        internal string GetCmlString(String id)
        {
            String result = String.Empty;
            if (!File.Exists(localFolder + String.Format(CultureInfo.InvariantCulture, @"\{0}.cml", id)))
            {
                return result;
            }

            StreamReader reader =
                new StreamReader(localFolder + String.Format(CultureInfo.InvariantCulture, @"\{0}.cml", id));
            result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
    }
}