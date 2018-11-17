using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class Project
    {
        public List<Interfaces> Interfaces { get; set; }
        public List<Classes> Classes { get; set; }
    }
}
