using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_Connect
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = "server=. ; database=cjk_sqlTest; uid=sa ; pwd=123456";     //连接数据库
            sc.Open();
            sc.Close();             //sc.dispose();     区别:close后可以再打开,dispose后连接的字符串(ConnectionString)也没有了要重新设置





            Console.ReadKey();
        }
    }
}
