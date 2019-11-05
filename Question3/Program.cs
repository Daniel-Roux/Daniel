using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Question3
{
    class Program
    {
        static void Main(string[] args)
        {
            string branch = "";
            string shift = "";
            if (args.Length == 2)
            {
                if (args[0] == "-branch") branch = args[1];
                else if (args[0] == "-shift") shift = args[1];
                //branch = args[0];
                //shift = args[1];
            }
            else if (args.Length == 4)
            {
                if (args[0] == "-branch") branch = args[1];
                else if (args[0] == "-shift") shift = args[1];
                if (args[2] == "-branch") branch = args[3];
                else if (args[2] == "-shift") shift = args[3];
            }
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
            DataTable dt = new DataTable("Users"); ;
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd = new SqlCommand("SellectAllUsers", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
            }

            Console.WriteLine("UserName\tFullName\tBranch\tShifts");
            Console.WriteLine("----------------------------------------------------");
            foreach (DataRow item in dt.Rows)
            {
                if ((branch == "" && shift == "") || 
                    (branch != "" && branch == item["Branch"].ToString()) ||
                    (shift != "" && shift == item["Shifts"].ToString()) ||
                    (branch == item["Branch"].ToString() && shift == item["Shifts"].ToString()))
                    Console.WriteLine($"{item["UserName"]}\t{item["FullName"]}\t{item["Branch"]}\t{item["Shifts"]}");
            }

            //Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
