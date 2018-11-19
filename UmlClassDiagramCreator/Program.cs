using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    class Program
    {
        static List<FileInfo> getFiles(DirectoryInfo di)
        {
            var list = di.GetFiles().ToList();
            foreach (var dir in di.GetDirectories())
                list.AddRange(getFiles(dir));
            return list;
        }

        static void Main(string[] args)
        {
            var pro = new Project();
            pro.Classes = new List<Classes>();
            var files = getFiles(new DirectoryInfo(args[0]));

            foreach (var file in files)
            {
                if(file.Name.EndsWith(".java"))
                    FileScanner.ScanFileForClasses(pro, file.FullName);
            }
            int x = 10;
            int y = 10;
            var result = DiagramCreator.GetDiagram(pro);
        }
    }
}
