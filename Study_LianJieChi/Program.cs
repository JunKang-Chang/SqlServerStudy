using System;
using System.Data.SqlClient;

namespace Study_LianJieChi
{
    internal class Program
    {
        private static void Main(string[] args)
        {   //连接池通过连接字符串控制。访问数据库时即便引用不同,但连接字符串的内容相同就都是用的同一个连接池,连接池的内容不一样时则会创建一个新的连接池
            //max pool size 最大连接数
            //min pool size 最小连接数
            //pooling 是否启用连接池(默认启用)  true false
            string conn = "server=. ;database=cjk_sqlTest; uid=sa ; pwd=123456;max pool size=5";

            for (int i = 1; i <= 5; i++)//sc1和sc2因为连接字符串相同所以用的是同一个连接池,最多建立5条连接
            {
                SqlConnection sc1 = new SqlConnection(conn);
                sc1.Open();
                Console.WriteLine($"sc1的第{i}个连接已打开");
                SqlConnection sc2 = new SqlConnection(conn);
                sc2.Open();
                Console.WriteLine($"sc2的第{i}个连接已打开");
            }
        }
    }
}