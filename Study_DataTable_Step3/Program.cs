using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Study_DataTable_Step4
//内存中的一个表(包括行和列,就像一个普通的数据库中的表)
//结合DataAdapter使用
//架构：列（DataColumn)+约束
//方法:
    //AcceptChanges()提交更改
    //RejectChanges()回滚更改
    //Clear()清空数据
    //Copy()复制数据和架构(约束)
    //Clone()只复制架构,不包含数据
    //Load(IDataReader)通过提供的IDataReader,用某个数据源填充DataTable
    //Merge(DataTable)合并指定的DataTable到当前Datatable
    //NewRow()创建一个DataRow,与DataTable有相同架构(列)
    //Reset()将Datatable重置到初始状态
    //Select()获取DataTable所有行数据
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.创建,此时是空表
            DataTable dt = new DataTable();
            dt.TableName = "datatable_User";//创建时不命名的话系统会给一个

            
            
            
            //2.定义列和架构(约束)
                //第一种方式:
            DataColumn dc = new DataColumn();       
            dc.ColumnName = "cl_UserName";
            dc.DataType = typeof(string);//定义列的数据类型    
            dt.Columns.Add(dc);//添加一列
                //第二种方式:
            dt.Columns.Add("cl_UserId", typeof(int));//添加一列,更推荐这种

            //定义架构(就是约束):
                //主键约束
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                //唯一约束
            dt.Constraints.Add(new UniqueConstraint(dt.Columns[1]));

            
            
            
            
            
            
          //3.往空表里添加数据
                //（1）新建空行
            DataRow dr = dt.NewRow();//新建一行,这个行对象是以dt有为架构模板创建的
                
            //  (2)往行里赋值
            dr[0] = "aaa";//用下标赋值
            dr["cl_UserId"] = 0;//用列名赋值
                     //补充:此时dr的状态是detached(孤立),它没有被添加到任何表里
               
            
            //(3)把dr这一行对象添加到dt
            dt.Rows.Add(dr);//此时dr的状态被标记为added  
                  //dt.RejectChanges();回滚上一次对dt的操作
            dt.AcceptChanges(); //此时dr的状态是unchanged
            dr[0] = "bbb";//修改, 此时dr状态标记为Modified 已修改
                          // dr.Delete();//从dt表移除dr,此时dr状态标记为deleted
                          //dt.Rows.Remove(dr);//相当于先delete再acceptchanged(此时dr状态是detached)
                          // dt.Rows.RemoveAt(0);//多的At意思是移除指定第几行

            //dt.Clear();//清空数据(即remove所有行,此时dr状态是detached)

            //因为一个datatable表只能属于一个dataset,要添加相同的架构的表要生成新的一个对象
            DataTable dt2 =dt.Copy();//复制架构和数据
            DataTable dt3 = dt.Clone();//克隆只克隆架构,不包含数据


            //把dt2合并到dt1,如果数据相同则去重
            DataRow dr2 = dt2.NewRow();
            dr2[0] = "ccc";
            dr2["cl_UserId"] = 1;
            dt2.Rows.Add(dr2);
            dt.Merge(dt2);



            DataRow[] rows = dt.Select();//获取所有行对象
            DataRow[] rows2 = dt.Select("cl_UserId>-1","cl_UserId desc");//获取所有行对象并且筛选加排序








            Console.ReadLine();
        }

    }
}
