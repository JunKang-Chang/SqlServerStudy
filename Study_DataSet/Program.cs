using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Study_DataSet//数据在内存中的缓存(数据库),包含多个DataTable(临时表)对象
{       
    class Program
    {//由datatable组成,DataRelation相互关联
        static void Main(string[] args)
        {
            //应用:DataAdapter将数据填充到DataSet中
            //       DataAdapter将数据DataSet中的更改提交到数据库
            //       XML文档或文本加载到DataSet中

            //作用:DataSet将数据加载到内存中来执行,速度快,提高硬盘数据安全性

            //特性:独立性:不依赖数据库,离线和连接. 数据操作灵活性强

            //往ds里添加dt
            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable("dt1");
            ds1.Tables.Add(dt1);

            DataTable dt2 = ds1.Tables[0];//通过下标获取表(用列名也可以,忘了看step4的datatable)

            //  ds1.Relations.Add(); 添加datarelation关系到ds

            //常用方法:
            ds1.AcceptChanges();
            ds1.RejectChanges();//把add到dataset的datatable移除
            ds1.Clear();
            ds1.Copy();
            ds1.Clone();
            //  ds1.Merge(datatable/DataSet/rows);包含3种情况
            ds1.Reset();
           // ds1.Load(IDataReader);





            Console.ReadKey();
        }
    }
}
