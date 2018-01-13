using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Je_Banken
{
    public static class Databas
    {
        private static SqlConnection _con;
        private static SqlCommand _com;
        private static SqlDataAdapter _adapter;
        private static DataTable _table;
        public static HtmlGenericControl errorMessage { get; set; } = new HtmlGenericControl("div");

        private static void connect()
        {                   
            _con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            try
            {
                _con.Open();
            }
            catch (Exception ex)
            {
                errormessage(ex.Message);
                _con.Close();
            }
        }

        public static DataTable getData(string sqlstring, List<SqlParameter> parameters)
        {
            try
            {
                _table = new DataTable();
                connect();
                _com = new SqlCommand(sqlstring, _con);
                if (parameters != null)
                {
                    _com.Parameters.AddRange(parameters.ToArray());
                }                
                
                if (_con.State == ConnectionState.Open)
                { 
                    _adapter = new SqlDataAdapter(_com);
                    _adapter.Fill(_table);
                }
            }
            catch (Exception ex)
            {
                errormessage(ex.Message);
            }

            finally
            {
                if (_con != null && _con.State == ConnectionState.Open)
                {
                    _con.Close();
                }

                if (parameters != null)
                {
                    parameters.Clear();
                }
                
            }

            return _table;
        }

        public static void dataToDB(string sqlstring, List<SqlParameter> parameters)
        {            
            try
            {
                connect();
                _com = new SqlCommand(sqlstring, _con);
                if (parameters != null)
                {
                    _com.Parameters.AddRange(parameters.ToArray());
                }
               
                if (_con.State == ConnectionState.Open)
                {
                    _com.ExecuteNonQuery();
                }
            }

            catch (Exception ex)
            {
                errormessage(ex.Message);
            }

            finally
            {
                if (_con != null && _con.State == ConnectionState.Open)
                {
                    _con.Close();
                }

                if (parameters != null)
                {
                    parameters.Clear();
                }
            }
        }

        public static void errormessage(string ex)
        {
            if (errorMessage != null)
            {
                errorMessage.Visible = true;
                errorMessage.InnerText = ex;
            }
        }

    }
}