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

            bool playagain = true;
            
            {
                if (args == null)
                {
                    Console.WriteLine("you did not provide arguments to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.ReadLine();
                }
                else if (args.Length == 1)
                {
                    Console.WriteLine("you only provided 1 argument to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.ReadLine();
                }
                else if (args.Length == 2)
                {
                    Console.WriteLine("you only provided 2 arguments to the executable");
                    Console.WriteLine("please provide 3 arguments ([source_file1] [source2_file] [destination_file]");
                    Console.ReadLine();
                }
                else if (args.Length == 3)
                {
                    Console.WriteLine("you provided 3 arguments to the executable");
                    Console.WriteLine("Thanks. The rest of the program is still under construction");
                    Console.ReadLine();
                }
                else if (args.Length > 3)
                {
                    Console.WriteLine("you provided more than 3 arguments to the executable");
                    Console.WriteLine("please provide 3 arguments: [source_file1] [source2_file] [destination_file]");
                    Console.ReadLine();
                }
            }
            
        }
    }
}
