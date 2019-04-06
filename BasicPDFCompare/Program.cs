using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            Console.WriteLine("You have entered the following arguments:");
            Console.WriteLine("argument 1 (source file 1): " + source_file1);
            Console.WriteLine("argument 2 (source file 2): " + source_file2);
            Console.WriteLine("argument 3 (destination file): " + destination_file);
            Console.WriteLine("Press a key to exit");
            Console.ReadLine();
        }
    }
    }
