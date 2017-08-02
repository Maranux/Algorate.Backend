using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorate
{
    public class SwashbuckleConfig
    {
        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\XmlComments.xml",
                System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
