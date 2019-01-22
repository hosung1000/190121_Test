using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Data;
using System.IO;

namespace ClassLibrary
{
    public class DB
    {
        public MySqlConnection 원본, conn;

        public MySqlConnection GetConnection()
        {
            //DB 접속
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "/public/DBInfo.json"; 
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();

             
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
               
                foreach (JProperty col in jo.Properties())
                {
                    map.Add(col.Name, col.Value);
                }
                string strConnection1
                    = string.Format("server={0};user={1};password={2};database={3}", map["server"], map["user"], map["password"], map["database"]);
                Console.WriteLine(strConnection1);
                conn.ConnectionString = strConnection1;
                conn.Open();

                return conn;
            }
            //오류 걸릴때
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
                //
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
