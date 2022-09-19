using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Study_DataReader_Step3
{   //提供一种从SQL数据库中一行一行读取的方式(只读不写)
    //只能往前读取不能往后读,不能修改数据,一直占用连接(没读完不能做其他操作)
    //读完要及时关闭(如果没读完就想要关闭,用sqlcommand的cancel()方法然后再用sqldatareader的close()方法)
    //创建方式:不能new,只能接受:executeReader的返回值:
    //例如:sqldatareader dr=xx.executeReader();
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "server =. ; database = cjk_sqlTest; uid = sa; pwd = 123456";
            try
            {
                using (SqlConnection sc = new SqlConnection(constr)) 
                {
                    sc.Open();
                    string str1 = "select * from Student";
                    SqlCommand scm = new SqlCommand(str1, sc);
                    SqlDataReader dr = scm.ExecuteReader(CommandBehavior.CloseConnection);


                    DataTable dt = new DataTable();
                   dt.Load(dr);//将dr读取到的数据加载到datatble中


                    #region 不推荐以下这种,推荐用↑datatable;因为read下面这种方式读一条丢一条,所以一般用list容器及时存储

                    //if (dr.HasRows)
                    //{

                    //    int indexid = dr.GetOrdinal("Id");//根据列名获取序列号
                    //    int Score = dr.GetOrdinal("Score");

                    //    string nameid = dr.GetName(0);//根据序列号获取列名

                    //List<Model> ls=new List<Model>();
                    //    while (dr.Read())
                    //    {

                    //        /*不推荐↓
                    //        int userid = (int)dr[0];//通过序列号读取指定列的数据
                    //        string username = dr["Name"].ToString();//通过列名读取指定列的数据
                    //       */
                    //        //推荐↓(在Read()之前先获取指定列的序列号)//避免拆箱操作
                    //         (读取后及时存储到容器)
                    //        Model m = new Model();
                    //        m.userid = dr.GetInt32(indexid);
                    //        m.userscore = dr.GetString(Score);
                    //        ls.Add(m);



                    //    }




                    //}



                    #endregion

                    dr.Close();
                }





            }
           
            
            
            
            catch (SqlException  ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }


    class Model
    {

        public int userid { get; set; }
        public string userscore { get; set; }

    }


}
