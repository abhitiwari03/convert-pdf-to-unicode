using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace convert_pdf_to_unicode
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile && fileUpload.PostedFile.ContentType == "application/pdf")
            {
                try
                {
                    string folderName = "TempFiles";
                    string folderPath = Server.MapPath($"~/{folderName}/");
                    Directory.CreateDirectory(folderPath);
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName);
                    string tempFilePath = System.IO.Path.Combine(Server.MapPath("~/TempFiles/"), fileName + ".pdf");
                    fileUpload.SaveAs(tempFilePath);
                    string extractedText = ExtractTextFromPDF(tempFilePath);
                    string unicodeText = ConvertKurtidevToUnicode(extractedText);
                    string outputFileName = fileName + "_unicode.pdf";
                    string outputPath = Server.MapPath("~/Output/") + outputFileName;
                    GeneratePDF(unicodeText, outputPath);
                    downloadLink.NavigateUrl = $"~/Output/" + outputFileName;
                    downloadLink.Visible = true;
                }
                catch (Exception ex)
                {
                    lblError.Text = "An error occurred: " + ex.Message;
                    lblError.Visible = true;
                }
               
            }
            else
            {
                lblError.Text = "Please select a valid PDF file.";
                lblError.Visible = true;
            }
        }
        private string ExtractTextFromPDF(string pdfPath)
        {
            using (PdfReader reader = new PdfReader(pdfPath))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }
        private string ConvertKurtidevToUnicode(string kurtidevText)
        {
            Dictionary<string, string> kurtidevToUnicodeMap = new Dictionary<string, string>
        {
             {"क", "अ"},

        };
            StringBuilder unicodeText = new StringBuilder();
            foreach (char kurtidevChar in kurtidevText)
            {
                string kurtidevCharStr = kurtidevChar.ToString();

                if (kurtidevToUnicodeMap.ContainsKey(kurtidevCharStr))
                {
                    unicodeText.Append(kurtidevToUnicodeMap[kurtidevCharStr]);
                }
                else
                {
                    unicodeText.Append(kurtidevChar);
                }
            }

            return unicodeText.ToString();
        }

        private void GeneratePDF(string unicodeText, string outputPath)
        {
            Document document = new Document();

            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();
                string fontFilePath = Server.MapPath("~/fonts/new_athena_unicode.ttf");
                BaseFont unicodeFont = BaseFont.CreateFont(fontFilePath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(unicodeFont, 12);

                Paragraph paragraph = new Paragraph(unicodeText, font);
                document.Add(paragraph);

                document.Close();
            }
        }


    }
   


}