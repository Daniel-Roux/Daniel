using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_5
{
    class Program
    {
        public static int sqnce(int n)
        {
            int a = 0;
            int b = 1;

            for (int i = 0; i < n; i++)
            {
                int c = a;
                a = b;
                b = c + a;
            }
            return a;
        }
        static void Main(string[] args)
        {

            for (int i = 0; i <50;i++)
            {
                Console.WriteLine(sqnce(i));
                
            }
            Console.ReadKey();
        }
    }
}
