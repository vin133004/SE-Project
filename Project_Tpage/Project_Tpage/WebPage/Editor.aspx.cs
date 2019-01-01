using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;
using System.Drawing;
namespace Project_Tpage.WebPage
{
    public partial class Editor : System.Web.UI.Page
    {
        // 返回/放棄修改
        public event ViewEventHandler ToBack;
        // PO文
        public event ViewEventHandler DoCreate;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;
        private Class.Article article;
        private Class.User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);
            user = Controller.CrossPageDAT["User"] as Class.User;
            int style = 0;
            style = user.Usersetting.Viewstyle;
            // 判定編輯還是新文章
            if (Controller.CrossPageDAT.Keys.Contains("Article")) {
                // 編輯文章 
                article = Controller.CrossPageDAT["Article"] as Class.Article;
                Tittle.Text = article.Title;
                Content.Text = article.Content;
            }
            if (style == 0)
            {
                color.Style.Add("background-color", "lightblue");
                Color a = Color.Black;
                String border = "white";
                la1.ForeColor = a;
                Tittle.ForeColor = a;
                la2.ForeColor = a;
                Content.ForeColor = a;
                Back.ForeColor = a;
                Send.ForeColor = a;
                
                la1.Style.Add("background-color", border);
                Tittle.Style.Add("background-color", border);
                la2.Style.Add("background-color", border);
                Send.Style.Add("background-color", border);
                Back.Style.Add("background-color", border);
                Content.Style.Add("background-color", border);
                
            }
            else if (style == 1)
            {
                color.Style.Add("background-color", "black");
                Color a = Color.WhiteSmoke;
                String border = "DarkGray";
                la1.ForeColor = a;
                Tittle.ForeColor = a;
                la2.ForeColor = a;
                Content.ForeColor = a;
                Back.ForeColor = a;
                Send.ForeColor = a;

                la1.Style.Add("background-color", border);
                Tittle.Style.Add("background-color", border);
                la2.Style.Add("background-color", border);
                Send.Style.Add("background-color", border);
                Back.Style.Add("background-color", border);
                Content.Style.Add("background-color", border);
            }
            else
            {
                color.Style.Add("background-color", "BurlyWood");
                Color a = Color.BlueViolet;
                String border = "CadetBlue";
                la1.ForeColor = a;
                Tittle.ForeColor = a;
                la2.ForeColor = a;
                Content.ForeColor = a;
                Back.ForeColor = a;
                Send.ForeColor = a;

                la1.Style.Add("background-color", border);
                Tittle.Style.Add("background-color", border);
                la2.Style.Add("background-color", border);
                Send.Style.Add("background-color", border);
                Back.Style.Add("background-color", border);
                Content.Style.Add("background-color", border);
            }
        }
    

        protected void Back_Click(object sender, EventArgs e)
        {
            ToBack(new ViewEventArgs(this), out optDAT);
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["Title"] = Tittle.Text;
            dat["Content"] = Content.Text;
            DoCreate(new ViewEventArgs(dat, this), out optDAT);
        }
    }
}