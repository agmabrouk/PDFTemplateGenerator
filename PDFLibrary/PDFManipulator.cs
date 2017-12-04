using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFLibrary
{
    public class PDFManipulator : IPDFManipulator
    {
        public static void GeneratePDF()
        {

            //Declare a itextSharp document 
            Document document = new Document(PageSize.A4);
            Random ran = new Random();
            string PDFFileName = string.Format(@"C:\Test{0}.Pdf", ran);
            //Create our file stream and bind the writer to the document and the stream 
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(PDFFileName, FileMode.Create));
            //Open the document for writing 
            document.Open();
            //Add a new page 
            document.NewPage();

            var ArialFontFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.ttf");
            //Reference a Unicode font to be sure that the symbols are present. 
            BaseFont bfArialUniCode = BaseFont.CreateFont(ArialFontFile, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Create a font from the base font 
            Font font = new Font(bfArialUniCode, 12);
            //Use a table so that we can set the text direction 
            var table = new PdfPTable(1)
            {
                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
            };
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            table.DefaultCell.NoWrap = false;

            ContentObject CO = new ContentObject();
            CO.Name = "Ahmed Gomaa";
            CO.StartDate = DateTime.Now.AddMonths(-5);
            CO.EndDate = DateTime.Now.AddMonths(43);

            string content = string.Format(" تم إبرام هذا العقد في هذا اليوم من قبل {0} في تاريخ بين {1} و {2}", CO.Name, CO.StartDate, CO.EndDate);
            var phrase = new Phrase(content, font);
            //var phrase = new Phrase("الحمد لله رب العالمين", font);
            //Create a cell and add text to it 
            PdfPCell text = new PdfPCell(phrase)
            {
                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                Border = 0
            };
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            text.NoWrap = false;

            //Add the cell to the table 
            table.AddCell(text);

            //Add the table to the document 
            document.Add(table);

            //Close the document 
            document.Close();

            //Launch the document if you have a file association set for PDF's 
            Process AcrobatReader = new Process();
            AcrobatReader.StartInfo.FileName = PDFFileName;
            AcrobatReader.Start();
        }
        public void GeneratePDFFromString()
        {
        }
        public bool GeneratePDFFromTemplate(PDFTemplate TemplateType, TemplateBody Body)
        {
            string TemplatePath = getTemplatePath(TemplateType);
            string TemplateContent = File.ReadAllText(TemplatePath);
            List<string> Params = new List<string>();
            fillParamList(Params, Body);
            string StringWithParam = string.Format(@TemplateContent, Params.ToArray());
            var TemplateCss = File.ReadAllText(gtTemplateCSS(TemplateType));
            ExportHTMLToPDF(StringWithParam, @"C:\", TemplateCss);
            return true;
        }
        private string gtTemplateCSS(PDFTemplate templateType)
        {
            return @"C:\Styles.css";
        }
        private void fillParamList(List<string> @params, TemplateBody body)
        {
            @params.Add(body.Header);
            body.BodyItems.OrderBy(x => x.order);
            foreach (var item in body.BodyItems)
            {
                if (item.visible) @params.Add(item.Content.ToString());
            }
            @params.Add(body.Footer);
        }
        private string getTemplatePath(PDFTemplate templateType)
        {
            return @"C:\Template1.html";
        }
        public void ExportHTMLToPDF(String HTMLString, String OutputPath, string Css)
        {
            Document pdfDoc = new Document(PageSize.A4, 70, 55, 40, 25);//new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            Random ran = new Random();
            string PDFFileName = string.Format(@"{1}Test{0}.Pdf", ran.Next(), OutputPath);

            Byte[] bytes;//in case return file stream 

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4, 70, 55, 40, 25))
                {
                   // doc.SetPageSize(PageSize.A4.Rotate());

                    using (PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(PDFFileName, FileMode.Create)))
                    {
                        doc.Open();

                        //Get a stream of our HTML
                        using (var msHTML = new MemoryStream(Encoding.UTF8.GetBytes(HTMLString)))
                        {

                            //Get a stream of our CSS
                            using (var msCSS = new MemoryStream(Encoding.UTF8.GetBytes(Css)))
                            {

                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHTML, msCSS, Encoding.UTF8, new Fontawy());
                            }
                        }

                        doc.Close();
                    }
                }

                //bytes = ms.ToArray();
            }
            
            Process AcrobatReader = new Process();
            AcrobatReader.StartInfo.FileName = PDFFileName;
            AcrobatReader.Start();
        }
    }




    public class Fontawy : IFontProvider
    {
        public Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color)
        {
            var ArialFontFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.ttf");
            BaseFont bfArialUniCode = BaseFont.CreateFont(ArialFontFile, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(bfArialUniCode, 12);
            return font;
        }

        public bool IsRegistered(string fontname)
        {
            return true;
        }
    }
    public class ContentObject
    {
        public string Name { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }

}
