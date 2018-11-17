using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class Interfaces
    {
        public string Name { get; set; }
        public List<Methods> Methods { get; set; }
        public List<Fields> Fields { get; set; }
    }

    public class Classes
    {
        public int X;
        public int Y;
        public string Base { get; set; }
        public string Name { get; set; }
        public List<Methods> Methods { get; set; }
        public List<Fields> Fields { get; set; }
    }
}
