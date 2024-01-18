using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System.IO;

namespace Transcription.Services
{
    internal class PDFGenerator
    {
        public void GeneratePdf(string text, string outFile)
        {
            // Création du document
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(outFile, FileMode.Create));
            doc.Open();

            // Palette de couleur
            BaseColor blue = new BaseColor(0, 75, 153);

            // Police d'écriture
            Font policetext = new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD, blue);

            // Création du paragraphe
            Paragraph p1 = new Paragraph(text + "\n\n", policetext);
            p1.Alignment = Element.ALIGN_LEFT;
            doc.Add(p1);

            // Fermer le document
            doc.Close();
        }

        public void OpenPdf(string pdfPath)
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    FileName = pdfPath
                };
                p.Start();
            }
        }
    }
}
