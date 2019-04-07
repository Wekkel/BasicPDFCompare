using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DiffMatchPatch;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text;
using Path = iTextSharp.text.pdf.parser.Path;
using iTextSharp.text.html.simpleparser;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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

            //convert source files to plain text
            string source_file1_text = PDF2String(source_file1);
            string source_file2_text = PDF2String(source_file2);

            //backup
            if (source_file1_text == "")
            {
                source_file1_text = "This is a dog. Hint: your PDF file source 1 was not converted into text.....";
            }

            if (source_file2_text == "")
            {
                source_file2_text = "This is a cat. Hint: your PDF file source 2 was not converted into text.....";
            }

            //set up compare objects and perform compare
            diff_match_patch dmp = new diff_match_patch();
            List<Diff> diff = dmp.diff_main(source_file1_text, source_file2_text);

            //convert compare to HTML and output this file (saving) as HMTL file
            string _HTMLString = dmp.diff_prettyHtml(diff);

            _HTMLString = CleanUpHTMLstring(_HTMLString);




            ////write HTML file to disk
            //WriteHTMLFile(_HTMLString);

            WriteHTMLtoPDF(_HTMLString, destination_file);

        }

        private static string CleanUpHTMLstring(string _HTMLString)
        {
            //code to add a CSS style to the head of the HTML file and remove style elements from the individual HTML elements
            //this is done to ensure that the color sheme for the inserst and deletions in the markup show in the PDF output
            _HTMLString = "<head><style>del{ background-color: #ffe6e6; }ins{ background-color :#e6ffe6; }</style></head>" + _HTMLString;
            _HTMLString = _HTMLString.Replace("<del style=\"background:#ffe6e6;\">", "<del>");
            _HTMLString = _HTMLString.Replace("<ins style=\"background:#e6ffe6;\">", "<ins>");

            return _HTMLString;
        }

        private static string PDF2String(string filePath)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                PdfReader reader = new PdfReader(filePath);

                StringWriter output = new StringWriter();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                    output.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));

                return output.ToString();
            }
            catch (Exception ex)
            {
               Console.WriteLine("Problems reading PDF file: " + filePath + Environment.NewLine + ex.Message);
            }
            return strBuilder.ToString();
        }


        private static void WriteHTMLFile(string _HTMLString)
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Console.WriteLine(appPath);
            var directory = System.IO.Path.GetDirectoryName(appPath);

            string filePath = directory + @"\" + "Result.html";
            System.IO.StreamWriter s = new System.IO.StreamWriter(filePath, false);

            s.WriteLine(_HTMLString);
            s.Close();
           Console.WriteLine("Ready writing HTML file to disk!");
        }
        
        private static void WriteHTMLtoPDF(string _HTMLstring, string destination_file)
        {

            PdfSharp.Pdf.PdfDocument pdf = PdfGenerator.GeneratePdf(_HTMLstring, PdfSharp.PageSize.A4, 60);

                       
            pdf.Save(destination_file);

            Console.WriteLine("Ready writing PDF file to disk!");


        }

    }
}
