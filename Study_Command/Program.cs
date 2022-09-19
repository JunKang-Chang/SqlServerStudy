using System;
using System.Data;
using System.Data.SqlClient;

namespace Study_Command
{
    internal class Program
    {
        private static void Main(string[] args)//注意:一开始写的时候因为没写方法而是在main方法里直接调用测试,
                                               //而如果在main方法外写方法然后在main里调用的话,
                                               //那么这三个方法名前都得加上static修饰(因为Main方法是静态的,不能在静态方法里调用非静态方法)
        {
            string str1 = "server=. ;database=cjk_sqlTest; uid=sa ; pwd=123456;max pool size=5";

            try
            {
                //using的作用:
                //当在某个代码段中使用了类的实例，
                //而希望无论因为什么原因，只要离开了这个代码段就自动调用这个类实例的Dispose。
                //要达到这样的目的，用try...catch来捕捉异常也是可以的，但用using也很方便。
                using (SqlConnection con = new SqlConnection(str1))
                {
                    con.Open();

                    //SqlCommand的重要属性
                    //1.SqlConnection:SqlCommand对象使用的连接
                    //2.CommandText:设置或获取要执行的(1)T-SQL语句或(2)存储过程名
                    //3.CommandType:    (默认)1):CommandType.Text:执行的是一个SQL语句
                    //                                     2):CommandType.StoredProcedure:执行的是一个存储过程
                    //4.Parameters:SqlCommand对象的命令参数集合 空集合
                    //5.Transaction:获取或设置要在其中执行的事务

                    //SqlCommand的创建:
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    
                    object o = null;
                    string testBianliang = "fffff";

                    //SqlCommand的执行:
                    //常用的三个方法:1.ExecuteNonQuery()//执行T-sql语句或存储过程并返回受影响行数(int类型)(增删改)
                    //                      2.ExecuteScalar()//执行查询语句或存储过程并返回第一行第一列的值(可以是任何类型),忽略其他行列
                    //                      3.ExecuteReader()执行查询并返回一个SqlDataReader对象;实时读取,只能一直往前读取不能往后读(不灵活),整个读取过程都得OPEN状态

                    //1.******测试第一个方法的语句******
                    com.CommandText = "insert into Student " +
                                                        "(Name,Score,Sex) " +//在语句里用变量的话要用的格式:     "+内容+"
                                                        "values (   ' " + testBianliang + " ',   70,  'man')";//sql语法里字符串类型用的是单引号!!
                    int count = com.ExecuteNonQuery();
                    //******************************

                    //2.******测试第二个方法的语句******
                    com.CommandText = "select count(1) from Student where Score<50";//count（1）:统计第一列下且Score小于50的元素个数（即有多少行）
                    o = com.ExecuteScalar();
                    //******************************

                    //3.******测试第三个方法的语句******
                    com.CommandText = "select Name,Score from Student";
                    SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())//开始从第一条数据往下一条条读取,每次读一行(Read()的含义:游标永远指向第一个元素且不可更改,读完后指向下一个)
                    {   //sqldatareader读数据的时候要及时存储,因为读一条就丢一条
                        string str = dr.GetString(0);
                        string str2 = dr.GetString(1);
                        //测试第三个方法
                        Console.WriteLine($"Name:{str}");
                        Console.WriteLine($"Score:{str2}");
                    }
                    
                    //如果在new初始化的时候参数里没写：CommandBehavior.CloseConnection的话
                    //Read完后切记得用dr.Close()手动关闭！
                    //CloseConnection的作用:dr.close会和con.close关联,关闭其中一个另外一个也会自动关闭(正常顺序:先关dr再关con)
                    //******************************

                    con.Close();//原则:最晚打开,最早关闭

                    //测试第一个方法
                    if (count > 0)
                    { Console.WriteLine("用户信息insert成功!"); }
                    else
                    { Console.WriteLine("用户信息insert失败!"); }

                    //测试第二个方法
                    if (o != null)
                    {
                        Console.WriteLine("返回值:" + o.ToString());
                    }

                    //con.dispose()    自动执行(因为用了using)
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}