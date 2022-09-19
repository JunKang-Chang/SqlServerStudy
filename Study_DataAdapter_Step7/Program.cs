using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Study_DataAdapter_Step7
{
    class Program
    {   //DataSet数据之间用来检索保存数据的桥梁
        //SqlDataAdapter类用Fill填充DataSet或者DataTable以及update更新数据源的一组命令和一个数据库连接
        //SqlDataAdapter是DataSet和SQLSERVER之间的桥接器
        //SqlDataAdapter对数据的操作是建立在SqlCommand的基础上
        static void Main(string[] args)
        {
            //4个重要属性(增删改查):
            //SelectCommand
            //InsertCommand
            //UpdateCommand
            //DeleteCommand

            string constr = ConfigurationManager.ConnectionStrings["scon"].ConnectionString;
            SqlConnection scon = new SqlConnection(constr);
            string scom = "select * from Student;select * from Customers";
            string scom2 = "select * from Student";



            //第一步:构建adapter,有四种方式:

            ////1.设置selectCommand
            //SqlDataAdapter sda1 = new SqlDataAdapter();
            //sda1.SelectCommand = new SqlCommand(scom, scon);
            ////2.通过一个SqlCommand对象实例化一个adapter
            //SqlCommand sc2 = new SqlCommand(scom, scon);
            //SqlDataAdapter sda2 = new SqlDataAdapter(sc2);
            ////3.查询语句和连接对象来实例化一个adapter
            //SqlDataAdapter sda3 = new SqlDataAdapter(scom,scon);
            ////4.查询语句和连接字符串
            //SqlDataAdapter sda4 = new SqlDataAdapter(scom, constr);

            //如果是T-SQL查询语句,推荐第三种
            //如果command带参数,选择第一种或第二种
            //不推荐第四种


            //第二步:用Fill方法填充到DataSet或者DataTable
            //(不需要手动open,fill时由dataadapter自动打开填充后也会自动关闭)
            //--fill属于断开式连接,scon状态全程都不变,fill之前是closed,填充之后还是closed,fill之前如果手动open,填充后还是open
            //虽然方便,但还是推荐手动open和close,速度更快

                //(1)填充DataSet---针对1个或者多个结果集
            SqlDataAdapter sda = new SqlDataAdapter(scom, scon);
            DataSet ds = new DataSet();
            //改表名(要在fill之前进行),多张表不改表名的话默认名字是table,table1,table2,table...n
            sda.TableMappings.Add("Table", "t0");
            sda.TableMappings.Add("Table1", "t1");
            scon.Open();//用fill可以不必手动open,但不推荐,理由如上↑
            sda.Fill(ds);
            scon.Close();//手动close
                //(2)填充DataTable---针对1个结果集
            SqlDataAdapter sda2 = new SqlDataAdapter(scom2, scon);
            DataTable dt = new DataTable("UserTable");//不需要手动加行,更方便
            sda2.Fill(dt);//--RosState:unchanged




            //把在dataset或者datatable内存中修改后数据同步到数据库中:
            //举例:
            //更新时-rowstate的状态变成Modified--对应sda.updateommand
            dt.Rows[0]["Score"] = "100";//更新dt里这一行数据
           //添加时-----------------------------------------------------sda.insertcommand
            DataRow dr = dt.NewRow();
            dr[0] = "32143";
            dr[1] = "deffsd";
            dr[2] = "65";
            dr[3] = "woman";
            dt.Rows.Add(dr);
            //删除时-----------------------------------------------------sda.deletecommand

            //使用commandbuilder可以自动根据rowstate为dataaapter生成更改的command命令
            //否则需要手动写sqlcommand的sql语句然后调用相应的dataapater的command语句
            SqlCommandBuilder scb = new SqlCommandBuilder(sda2);
            sda2.Update(dt);








            Console.ReadLine();
        }

    }
}
