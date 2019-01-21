using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20181207.Modules;

namespace _20181207.Controller
{
    class MainController
    {
        private Commons comm;
        private Panel head, contents, view, controller;
        private Button btn1, btn2, btn3;
        private ListView listView;
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5, textBox6;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;
        private WebapiConn api;

        public MainController(Form parentForm)
        {
            this.parentForm = parentForm;
            comm = new Commons();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 100);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 1);
            hashtable.Add("name", "head");
            head = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 700);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 100);
            hashtable.Add("color", 4);
            hashtable.Add("name", "contents");
            contents = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 20);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 3);
            hashtable.Add("name", "controller");
            controller = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 680);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 20);
            hashtable.Add("color", 0);
            hashtable.Add("name", "view");
            view = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 100);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "입력");
            hashtable.Add("click", (EventHandler) SetInsert);
            btn1 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 400);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "수정");
            hashtable.Add("click", (EventHandler) SetUpdate);
            btn2 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 700);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "삭제");
            hashtable.Add("click", (EventHandler) SetDelete);
            btn3 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("color", 0);
            hashtable.Add("name", "listView");
            hashtable.Add("click", (MouseEventHandler)listView_click);
            listView = comm.getListView(hashtable, view);

            hashtable = new Hashtable();
            hashtable.Add("width", 45);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox1");
            hashtable.Add("enabled", false);
            textBox1 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 100);
            hashtable.Add("pX", 45);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox2");
            hashtable.Add("enabled", true);
            textBox2 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 350);
            hashtable.Add("pX", 145);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox3");
            hashtable.Add("enabled", true);
            textBox3 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 100);
            hashtable.Add("pX", 495);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox4");
            hashtable.Add("enabled", true);
            textBox4 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 200);
            hashtable.Add("pX", 595);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox5");
            hashtable.Add("enabled", false);
            textBox5 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 200);
            hashtable.Add("pX", 795);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox6");
            hashtable.Add("enabled", false);
            textBox6 = comm.getTextBox(hashtable, controller);

            GetSelect();
        }

        private void GetSelect()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listView.Clear();

            listView.Columns.Add("nNo", 45, HorizontalAlignment.Center);        /* Notice 번호 */
            listView.Columns.Add("nTitle", 100, HorizontalAlignment.Left);      /* Notice 제목 */
            listView.Columns.Add("rContents", 350, HorizontalAlignment.Left);   /* Notice 내용 */
            listView.Columns.Add("mName", 100, HorizontalAlignment.Center);     /* Notice 작성자 이름 */
            listView.Columns.Add("regDate", 200, HorizontalAlignment.Left);     /* Notice 작성 현재날짜 */
            listView.Columns.Add("modDate", 200, HorizontalAlignment.Left);     /* Notice 수정 현재날짜 */

            
            api = new WebapiConn();
            api.SelectListView("http://192.168.3.134:80/Select", listView);

        }

        private void SetInsert(object o, EventArgs e)
        {
            api = new WebapiConn();
            Hashtable ht = new Hashtable();
            ht.Add("nTitle", textBox2.Text);
            ht.Add("nContents", textBox3.Text);
            ht.Add("mName", textBox4.Text);
            api.InsertListView("http://192.168.3.134:80/Insert", ht);
        }
        private void SetUpdate(object o, EventArgs e)
        {
            MessageBox.Show("SetUpdate");
        }
        private void SetDelete(object o, EventArgs e)
        {
            MessageBox.Show("SetDelete");
        }
        private void listView_click(object o, EventArgs a)
        {
            MessageBox.Show("listView_click");
        }
    }
}
