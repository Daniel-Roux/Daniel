using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Question_2.Class
{
    public class UsersClass
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string Shifts { get; set; }
    }
    
}
