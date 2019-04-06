using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiffMatchPatch;

namespace BasicPDFCompare
{
    class Program
    {
        static void Main(string[] args)
        {

            //menu structure
            if (args == null)
                {
                    Console.WriteLine("you did not provide arguments to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.WriteLine("Press key to exit");
                    Console.ReadLine();
                }
                else if (args.Length == 1)
                {
                    Console.WriteLine("you only provided 1 argument to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.WriteLine("Press key to exit");
                    Console.ReadLine();
                }
                else if (args.Length == 2)
                {
                    Console.WriteLine("you only provided 2 arguments to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.WriteLine("Press key to exit");
                    Console.ReadLine();
                }
                else if (args.Length == 3)
                {
                ComparePDF(args[0], args[1], args[2]);
                }
                else if (args.Length > 3)
                {
                    Console.WriteLine("you provided more than 3 arguments to the executable");
                    Console.WriteLine("please provide 3 arguments: [source_file1] [source2_file] [destination_file]");
                    Console.WriteLine("Press key to exit");
                    Console.ReadLine();
                }
            
        }

        private static void ComparePDF(string source_file1, string source_file2, string destination_file)
        {
            //set up compare objects and perform compare
            diff_match_patch dmp = new diff_match_patch();
            List<Diff> diff = dmp.diff_main(source_file1, source_file2);

            //convert compare to HTML and output this file (saving) as HMTL file
            string _HTMLString = dmp.diff_prettyHtml(diff);
            WriteHTMLFile(_HTMLString);
                    
        }

        private static void PDF2Text()
        {
        }

        private static void WriteHTMLFile(string _HTMLString)
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(appPath);
            var directory = System.IO.Path.GetDirectoryName(appPath);

            string filePath = directory + @"\" + "Result.html";
            System.IO.StreamWriter s = new System.IO.StreamWriter(filePath, false);

            s.WriteLine(_HTMLString);
            s.Close();
           Console.WriteLine("Klaar met wegschrijven HTML bestand!");
        }


    }
}
