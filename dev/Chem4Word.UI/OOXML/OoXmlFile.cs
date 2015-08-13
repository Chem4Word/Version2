using System.IO;
using Chem4Word.UI.Molecules;
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

                AddParagraph(docbody, "Hello World of OpenOfficeXml");
                AddPictureFromCml(docbody, cml, bookmarkName, options);
                AddParagraph(docbody, "Goodbye World of OpenOfficeXml");

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

        #region For Test
        public static string CreateFromAMC(string molecule, string guid)
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

                AddParagraph(docbody, "Hello World of OpenOfficeXml");
                AddPictureAMC(docbody, molecule, bookmarkName);
                AddParagraph(docbody, "Goodbye World of OpenOfficeXml");

                // Save changes to the main document part.
                package.MainDocumentPart.Document.Save();
            }

            return fileName;
        }

        private static void AddPictureAMC(Body docbody, string molecule, string bookmarkName = null)
        {
            Paragraph paragraph1 = new Paragraph();
            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkStart bookmarkstart = new BookmarkStart();
                bookmarkstart.Name = bookmarkName;
                bookmarkstart.Id = "1";
                paragraph1.Append(bookmarkstart);
            }

            // Should be real cml here, but we are bodging it by using button text
            switch (molecule)
            {
                case "Cyclohexane":
                    paragraph1.Append(Molecules.Cyclohexane.GenerateRun());
                    break;

                case "Benzene":
                    paragraph1.Append(Benzene.GenerateRun());
                    break;

                case "Pyridine":
                    paragraph1.Append(Molecules.Pyridine.GenerateRun());
                    break;

                case "Formic Acid":
                    paragraph1.Append(Molecules.FormicAcid.GenerateRun());
                    break;

                case "Probe A":
                    paragraph1.Append(Molecules.Probe1.GenerateRun());
                    break;

                case "Wiggly Lines":
                    paragraph1.Append(Molecules.WigglyLines.GenerateRun());
                    break;

                case "Big Molecule":
                    paragraph1.Append(Molecules.BigMolecule.GenerateRun());
                    break;
            }

            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkEnd bookmarkend = new BookmarkEnd();
                bookmarkend.Id = "1";
                paragraph1.Append(bookmarkend);
            }

            docbody.Append(paragraph1);
        }

        private static void AddParagraph(Body docbody, string theText, string bookmarkName = null)
        {
            Paragraph paragraph = new Paragraph();
            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkStart bookmarkstart = new BookmarkStart();
                bookmarkstart.Name = bookmarkName;
                bookmarkstart.Id = "0";
                paragraph.Append(bookmarkstart);
            }

            Run run = new Run();
            Text text = new Text(theText);
            run.Append(text);
            paragraph.Append(run);

            if (!string.IsNullOrEmpty(bookmarkName))
            {
                BookmarkEnd bookmarkend = new BookmarkEnd();
                bookmarkend.Id = "0";
                paragraph.Append(bookmarkend);
            }

            docbody.Append(paragraph);
        }
        #endregion
    }
}