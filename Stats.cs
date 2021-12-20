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

                            string[] table_outputs = new string[5];
                            table_outputs[0] = "| # of Entries    | " + count;
                            table_outputs[1] = "| Min. value      | " + min;
                            table_outputs[2] = "| Max. value      | " + max;
                            table_outputs[3] = "| Total value     | " + total;
                            table_outputs[4] = "| Avg. value      | " + total/count;

                            int max_length = 0;

                            for (int i = 0; i < table_outputs.Length; i++)
                            {
                                if (table_outputs[i].Length > max_length)
                                {
                                    max_length = table_outputs[i].Length;
                                }
                            }

                            Console.WriteLine(new string('-', max_length+1));

                            for (int i = 0; i < table_outputs.Length; i++)
                            {
                                if (table_outputs[i].Length < max_length)
                                {
                                    int space_diff = max_length - table_outputs[i].Length;
                                    for (int j = 0; j < space_diff; j++)
                                    {
                                        table_outputs[i] += " ";
                                    }
                                }
                                table_outputs[i] += "|";
                                Console.WriteLine(table_outputs[i]);
                            }

                            Console.WriteLine(new string('-', max_length+1));
                       }
                   }
                   else
                   {
                        if (args[0] == "help")
                        {
                            Console.WriteLine("List of commands available: ");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| Command  |   Syntax                                             |     Description                                         |");
                            Console.WriteLine("|---------------------------------------------------------------------------------------------------------------------------|");
                            Console.WriteLine("| record   |   record <filename.txt> <num1> [<num2>...<numx>]     |     Save one or more values into the specified file.    |");
                            Console.WriteLine("| summary  |   summary <filename.txt>                             |     Print a summary of the values into the console.     |");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");

                            Console.WriteLine("Keys:");
                            Console.WriteLine("<> - placeholder");
                            Console.WriteLine("[] - optional");
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
