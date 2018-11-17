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
            str = Regex.Replace(str, @"(?s)/\*.*?\*/", "");
            str = Regex.Replace(str, @"(?s)/\/\.*?\*\n", "");
            while(str.Contains("\""))
            {
                int i1 = str.IndexOf('"');
                int i2 = str.IndexOf('"', i1+1);
                if (i2 > 0)
                    str = str.Remove(i1, i2 - i1);
                else
                    str = str.Replace("\"", "");
            }
            var classLines = str.Split(new string[] { " class " }, StringSplitOptions.None).Skip(1).ToList();
            var match = Regex.Match(str, "(?<= class )(.*)(?= )");
            var cla = new Classes { Name = match.Value };
            var body = helper(classLines[0].Substring(classLines[0].IndexOf('{')));
            var meths = helper(body.Substring(classLines[0].IndexOf('{')));
            var classMethsRemoved = body;
            while (true)
            {
                var idx = classMethsRemoved.IndexOf("public ");
                if (idx < 0)
                    idx = classMethsRemoved.IndexOf("private ");
                if (idx < 0)
                    idx = classMethsRemoved.IndexOf("protected ");
                if (idx < 0)
                    break;
                var ix1 = classMethsRemoved.IndexOf(";");
                if (ix1 == -1)
                    ix1 = idx + 1;
                var ix2 = classMethsRemoved.IndexOf("=");
                if (ix2 == -1)
                    ix2 = idx + 1;
                if(idx < ix2 && idx < ix2)
                    classMethsRemoved = classMethsRemoved.Replace(helper(classMethsRemoved.Substring(idx), true), "");
                else
                {
                    var r = classMethsRemoved.Split(';')[0].Split('=')[0].Split(' ').Where(x => x != "").ToArray();
                    classMethsRemoved = classMethsRemoved.Replace(helper(classMethsRemoved.Substring(idx), true), $"{r[r.Length-2]} {r.Length-1}");
                }
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
                vals = vals.Where(x => x != "").ToArray();
                var p = new List<string>();
                para = para[1].Replace("(","").Split(',');
                foreach (var pr in para)
                    if(pr != "")
                    {
                        var pp = pr.Split(' ')[0].Trim();
                        if(pp != "")
                            p.Add(pp);
                    }
                cla.Methods.Add(new Methods {
                    Public=true,
                    Name = vals[vals.Length-1],
                    Return = vals.Length == 1 ? "" : vals[vals.Length-2],
                    Parameters = p
                });
            }
            project.Classes.Add(cla);
        }
    }
}
