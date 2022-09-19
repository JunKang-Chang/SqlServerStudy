using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_SqlParameter_Step_2_tuozhan
{
    class Program
    {
        static void Main(string[] args)
        {
            //在SQL Server中存储过程参数一律以”@”开头,局部变量declare @ ，全局变量declare @@
            //输入参数要给值,输出参数接收值(返回给存储过程名)



            string constr = "server=. ;database=cjk_sqlTest; uid=sa ; pwd=123456;max pool size=5";
            SqlConnection sc = new SqlConnection(constr);
            sc.Open();
            string comstr = "select Name from Student where Score =@Score  and  Sex=@Sex ";
            SqlCommand scom = new SqlCommand(comstr, sc);

            //SqlParameter目的:把查询语句与控制语句分开,不现拼接字符串
            //作用:1.防止注入,2.防止输入字符串变量即:' "+XXX+" '时,如果变量值里有单引号就会截断字符串(SQL里字符串用的是单引号不是双引号)
            //

            //(1)
            //)单个输入参数
            //SqlParameter pa = new SqlParameter("@score", "40");//@的作用:标识参数,如果是变量,在申明时要还加declare 
            //scom.Parameters.Add(pa);

            //多个输入参数
            SqlParameter[] pa2 = new SqlParameter[2];
            pa2[0] = new SqlParameter("@Score", "40");
            pa2[1] = new SqlParameter("@Sex", "man");
            scom.Parameters.AddRange(pa2);//对应到T-SQL查询语句中


            //测试
            SqlDataReader dr = scom.ExecuteReader();
            while (dr.Read())
            {
                string str = dr.GetString(0);
                Console.WriteLine(str);
            }

            dr.Close();





            //(2)
            //输出参数(一般在存储过程中使用,参数的值返回给存储过程,相当于函数里的return)
            //存储过程的返回值可以（1）return，(2)也可以用output参数，因为return只能返回一个值，而output可以返回多个值，所以建议用output！
            SqlCommand scom2 = new SqlCommand();
            scom2.Connection = sc;
            scom2.CommandText = "GetPro";
            scom2.CommandType = CommandType.StoredProcedure;//默认是t-sql语句所以要改类型

            SqlParameter pa3 = new SqlParameter("@Sex", SqlDbType.NVarChar, 10);//类型、名称要和数据库中定义的一致
            pa3.Direction = ParameterDirection.Output;//默认是输入参数所以要改类型

            scom2.Parameters.Add(pa3);//添加单个输出参数


            //测试
            scom2.ExecuteScalar();
            Console.WriteLine(pa3.Value.ToString());



            //(3)
            //输入输出参数(双向参数),既输入值也输出值
            SqlCommand scom3 = new SqlCommand();
            scom3.Connection = sc;
            scom3.CommandText = "GetPro2";
            scom3.CommandType = CommandType.StoredProcedure;

            SqlParameter pa4 = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
            pa4.Direction = ParameterDirection.InputOutput;
            pa4.Value = "az";
            scom3.Parameters.Add(pa4);
            //测试
            scom3.ExecuteScalar();
            Console.WriteLine(pa4.Value.ToString());


            //(4)
            //返回值参数,即接受存储过程中用return返回的值.一般只返回int类型(返回其他类型会自动转为int类型有可能报错?)

            SqlCommand scom4 = new SqlCommand();
            scom4.Connection = sc;
            scom4.CommandText = "GetPro3";
            scom4.CommandType = CommandType.StoredProcedure;

            SqlParameter[] pa5 = {
                new SqlParameter("@UserId",19),
                new SqlParameter("@PaReturn",SqlDbType.Int,4)                                                                                                    };
            pa5[1].Direction = ParameterDirection.ReturnValue;
            scom4.Parameters.AddRange(pa5);
            //测试
            scom4.ExecuteScalar();
            Console.WriteLine(pa5[1].Value.ToString());



            sc.Close();

            Console.Read();
        }
    }
}
