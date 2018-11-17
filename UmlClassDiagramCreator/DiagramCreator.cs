using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class DiagramCreator
    {
        public static string GetDiagram(Project pro)
        {
            var input = File.ReadAllText(@"C:\Users\chris\Documents\UML\interface.tpt");
            int x = 10;
            int y = 10;
            var result = "";
            int id = 0;
            foreach (var cls in pro.Classes)
            {
                id++;
                y += cls.Y + 10;
                var output = input.Replace("##interface##", cls.Name);
                var fields = "";
                foreach (var fld in cls.Fields)
                    fields += fld.ToString();
                output = output.Replace("##fields##", fields);
                var methods = "";
                foreach (var mtd in cls.Methods)
                    methods += mtd.ToString();
                output = output.Replace("##methods##", methods);
                output = output.Replace("##x##", "200");
                cls.X = 200;
                cls.Y = (cls.Fields.Count + cls.Methods.Count) * 15 + 55;
                output = output.Replace("##y##", cls.Y.ToString());
                output = output.Replace("##X##", x.ToString());
                output = output.Replace("##Y##", y.ToString());
                output = output.Replace("##id##", id.ToString());
                output = output.Replace("Interface", "");
                result += output;
                y += cls.Y + 10;
            }
            result = File.ReadAllText(@"C:\Users\chris\Documents\UML\diagram.tpt").Replace("##content##", result);
            return result;
        }
    }
}
