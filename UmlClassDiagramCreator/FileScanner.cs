using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UmlClassDiagramCreator
{
    public class FileScanner
    {
        public static string helper(string input, bool all = false)
        {
            string output = "";
            int i = 0;
            var brace = 1;
            while (input[i] != '{')
            {
                if(all)
                    output += input[i];
                i++;
            }
            if (all)
                output += input[i];
            i++;
            while (brace > 0)
            {
                if (input[i] == '{')
                    brace++;
                else if (input[i] == '}')
                    brace--;
                output += input[i];
                i++;
            }
            return output;
        }

        public static void ScanFileForClasses(Project project, string fileName)
        {
            var str = File.ReadAllText(fileName);
            var classLines = str.Split(new string[] { " class " }, StringSplitOptions.None).Skip(1).ToList();
            var match = Regex.Match(str, "(?<= class )(.*)(?= )");
            var cla = new Classes { Name = match.Value };
            var body = classLines[0].Substring(classLines[0].IndexOf('{')+1, classLines[0].LastIndexOf('}') - classLines[0].IndexOf('{'));
            var meths = helper(body.Substring(classLines[0].IndexOf('{')));
            var classMethsRemoved = body;
            while (true)
            {
                var idx = classMethsRemoved.IndexOf("public ");
                if (idx < 0)
                    break;
                classMethsRemoved = classMethsRemoved.Replace(helper(classMethsRemoved.Substring(idx), true), "");
            }
            cla.Fields = new List<Fields>();
            var flds = classMethsRemoved.Split('\n');
            foreach (var fld in flds)
            {
                var s = fld.Split(';')[0];
                var vals = s.Split(' ');
                if(vals.Length == 2)
                cla.Fields.Add(new Fields
                {
                    Name = vals[1],
                    Type = vals[0]
                });
            }
            cla.Methods = new List<Methods>();
            var publicMethods = Regex.Matches(body, @"(?<=public )(.*)(?=\))");
            foreach (Match meth in publicMethods)
            {
                var para = meth.Value.Split('(');
                var vals = para[0].Split(' ');
                var p = new List<string>();
                para = para[1].Replace("(","").Split(',');
                foreach (var pr in para)
                    if(pr != "")
                        p.Add(pr.Split(' ')[0].Trim());
                cla.Methods.Add(new Methods {
                    Public=true,
                    Name = vals[1],
                    Return = vals[0],
                    Parameters = p
                });
            }
            project.Classes.Add(cla);
        }
    }
}
