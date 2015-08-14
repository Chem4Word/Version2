﻿using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Word2010AddIn.OOXML;

namespace Chem4Word.UI.OOXML
{
    public static class OoXmlFile
    {
        /// <summary>
        /// Create an OpenXml Word Document from the CML
        /// </summary>
        /// <param name="cml">Input Chemistry</param>
        /// <param name="guid">Bookmark to create</param>
        /// <param name="options">Options to use when rendering</param>
        /// <returns></returns>
        public static string CreateFromCml(string cml, string guid, C4wOptions options)
        {
            string fileName = Path.Combine(Path.GetTempPath(), guid + ".docx");
            string bookmarkName = "C4W_" + guid;

            // Create a Wordprocessing document.
            using (WordprocessingDocument package = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
            {
                // Add a new main document part.
                MainDocumentPart mdp = package.AddMainDocumentPart();
                mdp.Document = new Document(new Body());
                Body docbody = package.MainDocumentPart.Document.Body;

                AddPictureFromCml(docbody, cml, bookmarkName, options);

                // Save changes to the main document part.
                package.MainDocumentPart.Document.Save();
            }

            return fileName;
        }

        /// <summary>
        /// Creates the DrawingML objects and adds them to the document
        /// </summary>
        /// <param name="docbody"></param>
        /// <param name="cml"></param>
        /// <param name="bookmarkName"></param>
        private static void AddPictureFromCml(Body docbody, string cml, string bookmarkName, C4wOptions options)
        {
            Paragraph paragraph1 = new Paragraph();
            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkStart bookmarkstart = new BookmarkStart();
                bookmarkstart.Name = bookmarkName;
                bookmarkstart.Id = "1";
                paragraph1.Append(bookmarkstart);
            }

            OoXmlMolecule pic = new OoXmlMolecule(cml, options);
            paragraph1.Append(pic.GenerateRun());

            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkEnd bookmarkend = new BookmarkEnd();
                bookmarkend.Id = "1";
                paragraph1.Append(bookmarkend);
            }

            docbody.Append(paragraph1);
        }
    }
}