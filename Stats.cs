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
                   // The record function needs at least 3 arguments to be correct
                   if (args.Length >= 3)
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
                                        Console.WriteLine("Invalid argument: {0} - This command requires a decimal number after the file name.",args[i]);
                                    }
                            }
                        }
                   }
                   else
                   {
                       Console.WriteLine("Invalid input: Record needs at least 2 other arguments: filename and value.");
                   }
               }
               else
               {
                   if (args[0] == "summary")
                   {
                       string path = @args[1];
                       using (StreamReader sr = File.OpenText(path))
                       {
                            string s = "";
                            decimal total = 0;
                            decimal count = 0;

                            // Assuming that the numbers would not go beyond these extremes
                            decimal min = 9999999999;
                            decimal max = -9999999999;

                            while ((s = sr.ReadLine()) != null)
                            {
                                if (IsDecimal(s))
                                {
                                    decimal temp = Convert.ToDecimal(s);
                                    total += temp;
                                    count++;

                                    if (temp < min) 
                                    {
                                        min = temp;
                                    }

                                    if (temp > max)
                                    {
                                        max = temp;
                                    }
                                }
                            }

                            Console.WriteLine("# of Entries: " + count);
                            Console.WriteLine("Min. value: " + min);
                            Console.WriteLine("Max. value: " + max);
                            Console.WriteLine("Avg. value: " + total/count);
                       }
                   }
                   else
                   {
                       // if (args[0] == "help")
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
