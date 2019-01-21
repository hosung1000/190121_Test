using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20181207.Modules
{
    class WebapiConn
    {
        //Select
        public bool SelectListView(string url, ListView listView)
        {
            try
            {
                WebClient wc = new WebClient();

                Stream stream = wc.OpenRead(url);
                //출력
                StreamReader sr = new StreamReader(stream);
               
                string result = sr.ReadToEnd();
               
                ArrayList list = JsonConvert.DeserializeObject<ArrayList>(result);
                listView.Items.Clear();
               
                for (int i = 0; i < list.Count; i++)
                {
                    JArray ja = (JArray)list[i];
                    string[] arr = new string[ja.Count];
                    for (int j = 0; j < ja.Count; j++)
                    {
                       
                        arr[j] = ja[j].ToString();
                    }
                    listView.Items.Add(new ListViewItem(arr));
                }
                return true;
            }
            catch
            {
              
                return false;
            }
        }
        //Insert
        public bool InsertListView(string url, Hashtable ht)
        {
            try
            {
                WebClient wb = new WebClient();
                NameValueCollection param = new NameValueCollection();
               
                foreach (DictionaryEntry data in ht)
                {
                  param.Add(data.Key.ToString(), data.Value.ToString());
                  
                }
                byte[] result = wb.UploadValues(url, "POST", param);
                string resultStr = Encoding.UTF8.GetString(result);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
