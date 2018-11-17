using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class Methods
    {
        public string Name { get; set; }
        public bool Public { get; set; }
        public string Return { get; set; }
        public List<string> Parameters { get; set; }

        public string ToString()
        {
            var parameterString = String.Join(", ", Parameters);
            return $"&gt;+ {DiagramCreator.escape(Name)}({DiagramCreator.escape(parameterString)}): {DiagramCreator.escape(Return)}&lt;br/";
        }
    }
}
