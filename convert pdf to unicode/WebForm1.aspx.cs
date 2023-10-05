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
            {"‘", "‘"},
            {"’", "’"},
            {"“", "\""},
            {"”", "\""},
            {"(", "("},
            {")", ")"},
            {"{", "{"},
            {"}", "}"},
            {"=", "="},
            {"।", "।"},
            {"?", "?"},
            {"-", "-"},
            {"µ", "µ"},
            {"॰", "॰"},
            {",", ","},
            {".", "."},
            {"्", "्"},
            {"०", "०"},
            {"१", "१"},
            {"२", "२"},
            {"३", "३"},
            {"४", "४"},
            {"५", "५"},
            {"६", "६"},
            {"७", "७"},
            {"८", "८"},
            {"९", "९"},
            {"x", "x"},
            {"फ़्", "फ़्"},
            {"क़", "क़"},
            {"ख़", "ख़"},
            {"ग़", "ग़"},
            {"ज़्", "ज़्"},
            {"ज़", "ज़"},
            {"ड़", "ड़"},
            {"ढ़", "ढ़"},
            {"फ़", "फ़"},
            {"य़", "य़"},
            {"ऱ", "ऱ"},
            {"ऩ", "ऩ"},
            {"त्त्", "त्त्"},
            {"त्त", "त्त"},
            {"क्त", "क्त"},
            {"दृ", "दृ"},
            {"कृ", "कृ"},
            {"ह्न", "ह्न"},
            {"ह्य", "ह्य"},
            {"हृ", "हृ"},
            {"ह्म", "ह्म"},
            {"ह्र", "ह्र"},
            {"ह्", "ह्"},
            {"द्द", "द्द"},
            {"क्ष्", "क्ष्"},
            {"क्ष", "क्ष"},
            {"त्र्", "त्र्"},
            {"त्र", "त्र"},
            {"ज्ञ", "ज्ञ"},
            {"छ्य", "छ्य"},
            {"ट्य", "ट्य"},
            {"ठ्य", "ठ्य"},
            {"ड्य", "ड्य"},
            {"ढ्य", "ढ्य"},
            {"द्य", "द्य"},
            {"द्व", "द्व"},
            {"श्र", "श्र"},
            {"ट्र", "ट्र"},
            {"ड्र", "ड्र"},
            {"ढ्र", "ढ्र"},
            {"छ्र", "छ्र"},
            {"क्र", "क्र"},
            {"फ्र", "फ्र"},
            {"द्र", "द्र"},
            {"प्र", "प्र"},
            {"ग्र", "ग्र"},
            {"रु", "रु"},
            {"रू", "रू"},
            {"्र", "्र"},
            {"ओ", "ओ"},
            {"औ", "औ"},
            {"आ", "आ"},
            {"अ", "अ"},
            {"ई", "ई"},
            {"इ", "इ"},
            {"उ", "उ"},
            {"ऊ", "ऊ"},
            {"ऐ", "ऐ"},
            {"ए", "ए"},
            {"ऋ", "ऋ"},
            {"क्", "क्"},
            {"क", "क"},
            {"क्क", "क्क"},
            {"ख्", "ख्"},
            {"ख", "ख"},
            {"ग्", "ग्"},
            {"ग", "ग"},
            {"घ्", "घ्"},
            {"घ", "घ"},
            {"ङ", "ङ"},
            {"चै", "चै"},
            {"च्", "च्"},
            {"च", "च"},
            {"छ", "छ"},
            {"ज्", "ज्"},
            {"ज", "ज"},
            {"झ्", "झ्"},
            {"झ", "झ"},
            {"ञ", "ञ"},
            {"ट्ट", "ट्ट"},
            {"ट्ठ", "ट्ठ"},
            {"ट", "ट"},
            {"ठ", "ठ"},
            {"ड्ड", "ड्ड"},
            {"ड्ढ", "ड्ढ"},
            {"ड", "ड"},
            {"ढ", "ढ"},
            {"ण्", "ण्"},
            {"ण", "ण"},
            {"त्", "त्"},
            {"त", "त"},
            {"थ्", "थ्"},
            {"थ", "थ"},
            {"द१", "द१"},
            {"द", "द"},
            {"ध्", "ध्"},
            {"ध", "ध"},
            {"न्", "न्"},
            {"न", "न"},
            {"प्", "प्"},
            {"प", "प"},
            {"फ्", "फ्"},
            {"फ", "फ"},
            {"ब्", "ब्"},
            {"ब", "ब"},
            {"भ्", "भ्"},
            {"भ", "भ"},
            {"म्", "म्"},
            {"म", "म"},
            {"य्", "य्"},
            {"य", "य"},
            {"र", "र"},
            {"ल्", "ल्"},
            {"ल", "ल"},
            {"ळ", "ळ"},
            {"व्", "व्"},
            {"व", "व"},
            {"श्", "श्"},
            {"श", "श"},
            {"ष्", "ष्" }

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
                string fontFilePath = Server.MapPath("~/fonts/KrutiBold.ttf");
                BaseFont unicodeFont = BaseFont.CreateFont(fontFilePath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(unicodeFont, 12);

                Paragraph paragraph = new Paragraph(unicodeText, font);
                document.Add(paragraph);

                document.Close();
            }
        }


    }
   


}