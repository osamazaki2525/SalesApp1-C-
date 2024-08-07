using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


    public class DB
    {
    private string con = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString.ToString();
    SqlConnection cn;
    DataTable dt;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter ad = new SqlDataAdapter();
    object val = "";
    public SqlConnection fireSql()
    {
        cn = new SqlConnection(con);
        return cn;

    }

    public DB()
    {
        cn = fireSql();
    }
    public string excuteSql(string sql)
    {
        try
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            val = cmd.ExecuteScalar();
            if (val == null)
            {
                return "";

            }
            return val.ToString();
        }
        catch { return null; }
        finally
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cmd.Dispose();
        }

    }
    public DataTable excuteDt(string sql)
    {
dt = new DataTable();
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
        
            ad.SelectCommand = cmd;
            ad.Fill(dt);
            return dt;
        
       
    }
    public string excuteSql_ReturnId(string sql)
    {
        try
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql + " ; SELECT SCOPE_IDENTITY() ;"; ;
            val = cmd.ExecuteScalar();
            if (val == null)
            {
                return "";

            }
            return val.ToString();
        }
        catch { return null; }
        finally
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cmd.Dispose();
        }

    }
}

