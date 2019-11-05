using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string givenSentance = "Please replace all characters equals to the letter 'A' with an underscore(_).";
            foreach(char A in givenSentance)
            {
                string replace = givenSentance.Replace("a", "_");
                Console.WriteLine(replace);
                Console.ReadKey();
            }
            
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
