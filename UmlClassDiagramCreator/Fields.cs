using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class Fields
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string ToString()
        {
            return $"&gt;- {Name}: {Type}&lt;br/";
        }
    }
}
