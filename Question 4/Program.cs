using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 59;
            if (args.Length == 1)
            {
                try
                {
                    max = Convert.ToInt32(args[0]);
                }
                catch { }
            }
            int num = 2;
            int total = 0;
            for(int n = 1; n <= max; n++)
            {
                //calling PN method
                while (PN(num) == false)
                {
                    //adding the prime number
                    num++;
                }
                //adding num with the total to get new total value
                total += num;
                num++;
            }
            //writing the total in memory and converting it to a Sting
            Console.WriteLine(total.ToString());
            //display the total
            Console.ReadLine();
        }
        public static bool PN(int n)
        {
            //getting squeroot of the number
            int x = (int)Math.Floor(Math.Sqrt(n));
            if (n == 1) return false;
            if (n == 2) return true;
            //Checking if value can be completely divided by a smaller number than itself
            for (int i = 2; i <= x; ++i)
            {
                if (n % i == 0) return false;
            }

            return true;
        }
    }
  
}
