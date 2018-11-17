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
        static void Main(string[] args)
        {
            var pro = new Project();
            pro.Classes = new List<Classes>();
            DirectoryInfo di = new DirectoryInfo(@"E:\Git\room-observer\RoomObserverServer\src\room\observer\class_controllers\");

            foreach (var file in di.GetFiles())
            {
                FileScanner.ScanFileForClasses(pro, file.FullName);
            }
            int x = 10;
            int y = 10;
            var result = DiagramCreator.GetDiagram(pro);
        }
    }
}
