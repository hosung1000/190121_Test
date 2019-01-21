using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary;
using MySql.Data.MySqlClient;

namespace WEBAPI.Controllers
{
    
    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DB db;
        
        //Select문
        [Route("Select")]
        [HttpGet]
        public ActionResult<ArrayList> Select()
        {
            db = new DB();
            MySqlDataReader sdr = db.GetReader("sp_Select");
            ArrayList list = new ArrayList();
            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                list.Add(arr);
            }
            db.ReaderClose(sdr);
            db.ConnectionClose();
            return list;
        }
        //Insert 문
        [Route("Insert")]
        [HttpPost]
        public ActionResult<string> Insert([FromForm] string nTitle, [FromForm] string nContents, [FromForm] string mName)
        {
            string param = string.Format("  {0}  :  {1}  :  {2} ", nTitle, nContents, mName);
            Hashtable ht = new Hashtable();
            ht.Add("_nTitle", nTitle);
            ht.Add("_nContents", nContents);
            ht.Add("_mName", mName);

            db = new DB();

            if (db.NonQuery("sp_Insert", ht))
            {
                db.ConnectionClose();
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
}
