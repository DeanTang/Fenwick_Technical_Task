using System;
using System.IO;

namespace Fenwick_Technical_Task
{
    class Stats
    {
        /* Verifies if value is a decimal number */
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
                /* --- Start of Record function --- */
                // If first argument is record
               if (args[0] == "record")
               {
                   // The record function needs at least 3 arguments to be correct
                   if (args.Length >= 3)
                   {
                       // Get file path
                       string path = @args[1];

                        // Create text file if the file doesn't exist
                        if (!File.Exists(path))
                        {
                            /* From argument 3 onwards are the values to be recorded */
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
                                        // Error handling: Not a decimal number
                                        Console.WriteLine("Invalid argument: {0} - This command requires a decimal number after the file name.",args[i]);
                                    }
                            }
                            
                        }
                        else
                        {
                            // If file exists
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
                       // The record function needs at least 3 arguments to be correct, 2 more excluding the 'record' argument
                       Console.WriteLine("Invalid input: Record needs at least 2 other arguments: filename and value.");
                   }
               }
               /* --- End of Record function --- */
               else
               {
                   /* --- Start of Summary function --- */
                   if (args[0] == "summary")
                   {
                       // Get file name
                       string path = @args[1];

                       // Check if file exists
                       if (!File.Exists(path))
                       {
                           // File not found
                           Console.WriteLine("Error: File does not exist: " + path);
                       }
                       else
                       {
                           using (StreamReader sr = File.OpenText(path))
                            {
                                // Initialize tracker variables
                                    string s = "";
                                    decimal total = 0;
                                    decimal count = 0;

                                    // Assuming that the numbers would not go beyond these extremes
                                    decimal min = 9999999999;
                                    decimal max = -9999999999;

                                    // Traverse the text file line by line
                                    while ((s = sr.ReadLine()) != null)
                                    {
                                        // Checks if the line can be converted to a decimal value
                                        if (IsDecimal(s))
                                        {
                                            decimal temp = Convert.ToDecimal(s);
                                            total += temp;
                                            count++;

                                            // Check for new minimum
                                            if (temp < min)
                                            {
                                                min = temp;
                                            }

                                            // Check for new maximum
                                            if (temp > max)
                                            {
                                                max = temp;
                                            }
                                        }
                                    }

                                    /* Display in a nice table */

                                    string[] table_outputs = new string[5];
                                    table_outputs[0] = "| # of Entries    | " + count;
                                    table_outputs[1] = "| Min. value      | " + min;
                                    table_outputs[2] = "| Max. value      | " + max;
                                    table_outputs[3] = "| Total value     | " + total;
                                    table_outputs[4] = "| Avg. value      | " + total/count;

                                    int max_length = 0;

                                    // Determines the maximum length of all strings
                                    for (int i = 0; i < table_outputs.Length; i++)
                                    {
                                        if (table_outputs[i].Length > max_length)
                                        {
                                            max_length = table_outputs[i].Length;
                                        }
                                    }

                                    // Make a top line string
                                    // +1 for the extra '|'
                                    Console.WriteLine(new string('-', max_length+1));

                                    for (int i = 0; i < table_outputs.Length; i++)
                                    {
                                        if (table_outputs[i].Length < max_length)
                                        {
                                            // Basically, adds spaces to match the one with max_length
                                            int space_diff = max_length - table_outputs[i].Length;
                                            for (int j = 0; j < space_diff; j++)
                                            {
                                                table_outputs[i] += " ";
                                            }
                                        }

                                        table_outputs[i] += "|";

                                        Console.WriteLine(table_outputs[i]);
                                    }

                                    // Make a bottom line string
                                    Console.WriteLine(new string('-', max_length+1));

                                    /* Display in a nice table - end */
                            }
                       }
                   }
                   /* --- End of Summary function --- */
                   else
                   {
                        /* --- Start of Help function --- */
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
                        /* --- End of Help function --- */
                        else
                        {
                            // The first argument is not recognized
                            Console.WriteLine("Invalid command: Command not recognized.");
                        }
                   }
               }
            }  
              
            else
            {
                // No arguments provided
                Console.WriteLine("Invalid command: No command line arguments found.");
            }
        }
    }
}
