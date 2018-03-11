using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication10
{
    class Database
    {
        public SqlConnection con;
        public  SqlDataAdapter ad;
        public DataTable tb;
        public SqlCommandBuilder bld;
        public string constring = "Data Source=KIRO\\SQLEXPRESS;Initial Catalog=Connection;Integrated Security=True";
       
    }
}
