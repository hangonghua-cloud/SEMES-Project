using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MESClient
{
    class databaseoperate
    {
        //使用指定的连接字符串连接数据库，返回一个SqlConnection对象
        public SqlConnection getcon()
        {
            string sqlstr ="Data Source=.\\sql2008;Integrated Security=SSPI;Initial Catalog=Hospital_MIS";
            //实例化SqlConnection对象并进行返回
            SqlConnection myconn = new SqlConnection(sqlstr);
            return myconn;
        }
        //执行指定的SQL语句，返回一个SqlDataReader对象
        public SqlDataReader getread(string tempstr)
        {
            SqlConnection sqlconn = this.getcon();//获取连接
            //实例化一个具有指定连接和命令的SqlCommand对象
            SqlCommand sqlcom = new SqlCommand(tempstr, sqlconn);
            //打开连接
            sqlconn.Open();
            //调用ExecuteReader方法返回一个SqlDataReader对象
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
        //执行指定的SQL语句，返回一个DataSet对象
        public DataSet getds(string tempstr, string temptable)
        {
            SqlConnection mycon = this.getcon();//获取连接
            //使用指定的连接和SQL命令创建一个SqlDataAdapter对象
            SqlDataAdapter myadapter = new SqlDataAdapter(tempstr, mycon);
            //实例化一个DataSet
            DataSet myds = new DataSet();
            //使用SqlDataAdapter对象填充数据集
            myadapter.Fill(myds, temptable);
            //返回DataSet对象
            return myds;
        }
        //执行指定的SQL语句，适用于Insert/Update/Delete语句
        public void getcom(string tempstr)
        {
            SqlConnection sqlconn = this.getcon();//获取连接
            sqlconn.Open();//打开连接
            //实例化一个SqlCommand对象
            SqlCommand sqlcom = new SqlCommand(tempstr, sqlconn);
            //执行SQL语句
            sqlcom.ExecuteNonQuery();
            //关闭并释放连接
            sqlconn.Close();
            sqlconn.Dispose();
        }
        //
        public DataSet getys(string tempsqlstr,string tempsqltable)
        {
            SqlConnection myconn = this.getcon();
            SqlDataAdapter da = new SqlDataAdapter(tempsqlstr , myconn);
            DataSet ds = new DataSet();
            da.Fill(ds, tempsqltable );
            
            return ds;
        }
        //执行付费存储过程
        public int payproc(string patientno,string regno,decimal thispay,decimal totalpay,decimal havepay)
        {
            SqlConnection mycon=this.getcon ();
            SqlCommand mycom=new SqlCommand ("proc_pay",mycon);
            mycom .CommandType =CommandType .StoredProcedure ;
            mycom .Parameters .Add("@patientno",SqlDbType.VarChar ,20).Value=patientno ;
            mycom.Parameters.Add("@regno", SqlDbType.VarChar, 20).Value = regno;
            mycom.Parameters.Add("@thispay", SqlDbType.Money).Value = thispay;
            mycom.Parameters.Add("@totalpay", SqlDbType.Money).Value = totalpay;
            mycom.Parameters.Add("@havepay", SqlDbType.Money).Value = havepay;
            //SqlParameter returnvalue=mycom .Parameters .Add ("returnvalue",SqlDbType.Int,4);
            //returnvalue.Direction =ParameterDirection .ReturnValue ;
            mycon .Open ();
            mycom .ExecuteNonQuery ();
            
                mycom .Dispose ();
                mycon .Close ();
                mycon .Dispose ();
            
            return 1;
        }
        public string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留 
                    tempStr += c.ToString();
                }
                else
                {//累加拼音声母 
                    //SqlConnection mycon = this.getcon();
                    //SqlDataAdapter myadapter = new SqlDataAdapter("select py from chinese where hz='"+c+"'", mycon);
                    //DataSet myds = new DataSet();
                    //myadapter.Fill(myds, "chinese");
                    //strt = myds.Tables[0].Rows[0][0].ToString() ;
                    //tempStr += strt;
                }
            }
            return tempStr;
        }

    }
}
