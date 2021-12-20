using System;
using System.IO;

namespace Fenwick_Technical_Task
{
    class Stats
    {
        static bool IsDecimal(String value)
        {
            decimal number = 0;
            return decimal.TryParse(value, out number);
        }
        static void Main(string[] args)
        {
            // To check the length of 
            // Command line arguments   
            if(args.Length > 0)
            {
               if (args[0] == "record")
               {
                   string path = @args[1];

                   // Create text file if the file doesn't exist
                   if (!File.Exists(path))
                   {
                       for (int i = 2; i < args.Length; i++)
                       {
                            if (IsDecimal(args[i]))
                            {
                                // Create file to write to
                                using (StreamWriter sw = File.CreateText(path))
                                {
                                    sw.WriteLine(args[i]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid argument: {0} - This command requires a decimal number after the file name.",args[i]);
                            }
                       }
                       
                   }
                   else
                   {
                       for (int i = 2; i < args.Length; i++)
                       {
                           if (IsDecimal(args[i]))
                            {
                                // Append record to text file if it already exists
                                using (StreamWriter sw = File.AppendText(path))
                                {
                                    sw.WriteLine(args[i]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid argument: This command requires a decimal number after the file name.");
                            }
                       }
                   }
               }
            }  
              
            else
            {
                Console.WriteLine("Invalid command: No command line arguments found.");
            }
        }
    }
}
