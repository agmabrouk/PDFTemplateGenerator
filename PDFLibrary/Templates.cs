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

    public interface IPDFTemplate
    {


    }

    public class LicenseRequestTemplate : IPDFTemplate
    {



    }

    public class AssenseRequestTemplate : IPDFTemplate
    {



    }


    public class SaudiCertificateTemplate : IPDFTemplate
    {



    }

    public class TemplateBody
    {
        public String Header { get; set; }
        public List<TemplateBodyItem> BodyItems;
        public String Footer { get; set; }
    }

    public class TemplateProperties
    {



    }

    public class TemplateBodyItem
    {
        public bool Visible { get; set; }
        public int Order { get; set; }
        public String Content { get; set; }
    }

}
