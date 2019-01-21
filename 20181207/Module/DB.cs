using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;

namespace Module
{
    public class DB
    {
        public MySqlConnection 원본, conn;

        string strConnection =
                            string.Format("server={0};user={1};password={2};database={3}", "192.168.3.136", "root", "1234", "test2");

        //DB 접속
        public MySqlConnection GetConnection()
        {
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = strConnection;
                Console.WriteLine("접속");
                conn.Open();
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("접속 실패");
                return null;
            }
        }
        //DB 종료
        public bool ConnectionClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Select문
        public MySqlDataReader GetReader(string sql)
        {

            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = sql;
                comm.Connection = GetConnection();
                comm.CommandType = CommandType.StoredProcedure;
                return comm.ExecuteReader();
            }
            catch
            {
                return null;
            }

        }

        //Insert,Update문
        public bool NonQuery(string sql, Hashtable ht)
        {

            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = sql;
                comm.Connection = GetConnection();
                comm.CommandType = CommandType.StoredProcedure;

                foreach (DictionaryEntry data in ht)
                {
                    comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                }
                comm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }
        //쿼리문 실행 종료
        public void ReaderClose(MySqlDataReader GetReader)
        {
            GetReader.Close();
        }

    }
}
