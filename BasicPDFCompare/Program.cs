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
            WriteHTMLFile(_HTMLString);

            //convert compare to HTML and output this file (saving) as PDF file
            WriteHTMLtoPDF(_HTMLString);

        }

        public static string PDF2String(string filePath)
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

        private static void WriteHTMLtoPDF(string _HTMLstring)
        {
            //Create a byte array that will eventually hold our final PDF
            Byte[] bytes;

            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        //Open the document for writing
                        doc.Open();

                        //Our sample HTML and CSS
                        var example_html = _HTMLstring;
                            var example_css = @".headline{font-size:200%}";

                        /**************************************************
                         * Example #1                                     *
                         *                                                *
                         * Use the built-in HTMLWorker to parse the HTML. *
                         * Only inline CSS is supported.                  *
                         * ************************************************/

                        //Create a new HTMLWorker bound to our document
                        using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
                        {

                            //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                            using (var sr = new StringReader(example_html))
                            {

                                //Parse the HTML
                                htmlWorker.Parse(sr);
                            }
                        }

                     

                        doc.Close();
                    }
                }

                //After all of the PDF "stuff" above is done and closed but **before** we
                //close the MemoryStream, grab all of the active bytes from the stream
                bytes = ms.ToArray();
            }

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.

            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Console.WriteLine(appPath);
            var directory = System.IO.Path.GetDirectoryName(appPath);

            string filePath = directory + @"\" + "Result.pdf";
                            
            System.IO.File.WriteAllBytes(filePath, bytes);

            Console.WriteLine("Ready writing PDF file to disk!");
        }
    }
}
