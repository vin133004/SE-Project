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
            Controller.controller.SubsribeEvent(this);
            if (Page.IsPostBack)
                return;
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);
            if (Session["UID"] == null)
                Response.Redirect("Login");
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
           
            user = Controller.CrossPageDAT["User"] as Class.User;

            // 判定編輯還是新文章
            if (Controller.CrossPageDAT.Keys.Contains("Article")) {
                // 編輯文章 
                article = Controller.CrossPageDAT["Article"] as Class.Article;
                Tittle.Text = article.Title;
                Content.Text = article.Content;
            }
            int style = 0;
            style = user.Usersetting.Viewstyle;
            Color a;
            string border;
            if (style == 0)
            {
                color.Style.Add("background-color", "lightblue");
                a = Color.Black;
                border = "white";
            }
            else if (style == 1)
            {
                color.Style.Add("background-color", "black");
                a = Color.WhiteSmoke;
                border = "DarkGray";
            }
            else
            {
                color.Style.Add("background-color", "BurlyWood");
                a = Color.BlueViolet;
                border = "CadetBlue";   
            }
            la1.ForeColor = a;
            Tittle.ForeColor = a;
            la2.ForeColor = a;
            Content.ForeColor = a;
            Back.ForeColor = a;
            Send.ForeColor = a;

            Tittle.Style.Add("background-color", border);
            Send.Style.Add("background-color", border);
            Back.Style.Add("background-color", border);
            Content.Style.Add("background-color", border);
        }
    

        protected void Back_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            if (Controller.CrossPageDAT.Keys.Contains("Article"))
                dat["AID"] = article.AID;
            else
                dat["BID"] = Controller.CrossPageDAT["BID"];
            ToBack(new ViewEventArgs(dat,this), out optDAT);
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["BID"] = Controller.CrossPageDAT["BID"];
            dat["Title"] = Tittle.Text;
            dat["Content"] = Content.Text;
            DoCreate(new ViewEventArgs(dat, this), out optDAT);
        }
    }
}