using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFLibrary
{
    public interface IBinders
    {
        string BindTmplate(PDFTemplate TemplateName, TemplateBody Body);
    }


    public class StringBinder : IBinders
    {
        public string BindTmplate(PDFTemplate TemplateName, TemplateBody Body)
        {
            throw new NotImplementedException();
        }
    }


    public class CustomPLaceHolderBinderBinder<T> : IBinders
    {
        public string BindTmplate(PDFTemplate TemplateName, TemplateBody Body)
        {
            throw new NotImplementedException();
        }
    }


}
