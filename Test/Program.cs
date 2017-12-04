using PDFLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PDFManipulator gen = new PDFManipulator();

            TemplateBodyItem bodyitem1 = new TemplateBodyItem()
            {
                Order = 1,
                Content = "hello world",
                Visible = true,
            };

            TemplateBodyItem bodyitem2 = new TemplateBodyItem()
            {
                Order = 1,
                Content = "hello world",
                Visible = true,
            };

            TemplateBodyItem bodyitem3 = new TemplateBodyItem()
            {
                Order = 1,
                Content = "السح اندح امبو",
                Visible = true,
            };

            TemplateBody body = new TemplateBody()
            {
                Header = "PDF Generator",
                Footer = "thank you for your visit",
                BodyItems = new List<TemplateBodyItem>() { bodyitem1, bodyitem2, bodyitem3 }
            };
            gen.GeneratePDFFromTemplate(PDFTemplate.AssenseRequest, body);
          //  PDFManipulator.GeneratePDF();
            Console.WriteLine("file has been generated");
        }
    }
}
