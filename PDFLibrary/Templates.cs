using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFLibrary
{
    public enum PDFTemplate
    {
        LicenseRequest,
        AssenseRequest,
        SaudiCertificate,
    }

    public class TemplateBody
    {
        public String Header { get; set; }
        public List<TemplateBodyItem> BodyItems;
        public String Footer { get; set; }
    }


    public class TemplateBodyItem
    {
        public bool visible { get; set; }
        public int order { get; set; }
        public String Content { get; set; }
    }

}
