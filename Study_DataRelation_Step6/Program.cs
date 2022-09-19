using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_DataRelation_Step6
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet ds = new DataSet("ds");
            DataTable dt1 = new DataTable("User");
            DataTable dt2 = new DataTable("Dept");
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            //DataTable包含:1.列 2.架构(约束)

            

          

            //两个表要建立关系(连接),需要用一个相同的字段连接--DeptId
            dt1.Columns.Add("UserId", typeof(int));
            dt1.Columns.Add("UserName", typeof(string));
            dt1.Columns.Add("DeptId", typeof(string));
            dt1.Columns.Add("DeptName", typeof(string));

            dt2.Columns.Add("DeptId", typeof(string));
            dt2.Columns.Add("DeptName", typeof(string));


            //初始化dt1和dt2为其添加数据
            DataRow dr1;
            DataRow dr2;
            
            dr1=dt1.NewRow();
            dr1["UserId"] = "1";
            dr1["UserName"] = "aaa";
            dr1["DeptId"] = "1-1";//这是子表的外键,可以重复不唯一
            dr1["DeptName"] = "事业部";
            dt1.Rows.Add(dr1);
           
            dr1 = dt1.NewRow();
            dr1["UserId"] = "2";
            dr1["UserName"] = "bbb";
            dr1["DeptId"] = "1-1";//这是子表的外键,可以重复不唯一
            dr1["DeptName"] = "人事部";
            dt1.Rows.Add(dr1);

            dr2 = dt2.NewRow();
            dr2["DeptId"] = "1-1";//这是父表的主键,唯一
            dr2["DeptName"] = "人事部";
            dt2.Rows.Add(dr2);

            dr2 = dt2.NewRow();
            dr2["DeptId"] = "1-2";//不能重复
            dr2["DeptName"] = "事业部";
            dt2.Rows.Add(dr2);






            //可以设置的约束有3个:主键约束,唯一约束,外键约束
            

            //添加主键约束和唯一约束(只针对一个表)
            dt1.PrimaryKey = new DataColumn[] { dt1.Columns[0] };
            dt1.Constraints.Add(new UniqueConstraint("aaa", dt1.Columns[1]));

          
            //添加外键约束(针对2个表),一对多关系,如果是多对多关系要引入中间表,
            //dt2是父表,dt1是子表
            //即1:父表，多:子表,用子表设置外键约束!!!
            //目的:通关父表浏览子表
            //dt1.Constraints.Add(new ForeignKeyConstraint("fk",
            //    dt2.Columns [0], dt1.Columns[2]));
           

            //添加关系,会默认为父表的列设定唯一约束(因为子表的外键是父表的主键,主键就是唯一的),子表作为外键的列建立外键约束.子表可以不唯一
            //就不需要像上面这样手动添加外键和唯一约束了
            DataRelation dre1 = new DataRelation("relationname1",
                dt2.Columns[0], dt1.Columns[2],true);


            //将关系添加到ds中并使用验证关系
            ds.Relations.Add(dre1);


            //1.通过关系可以从父表浏览子表
            foreach (DataRow dr in dt2.Rows)
            {
                DataRow[] rows = dr.GetChildRows(dre1);
                foreach (DataRow r in rows) {
                    Console.WriteLine($"UserId:{r[0].ToString()}," +
                        $"UserName:{r[1].ToString()}," +
                        $"DeptId:{r[2].ToString()}");
                }
            }
            //2.从子表浏览父表
            DataRow row = dt1.Rows[1].GetParentRow(dre1);
            Console.WriteLine($"DeptId:{row[0]},DeptName:{row[1]}");

            


            Console.ReadKey();
        }
    }
}
