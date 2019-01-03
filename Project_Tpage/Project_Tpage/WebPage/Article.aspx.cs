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
    public partial class Article : System.Web.UI.Page
    {
        // 發送留言
        public event ViewEventHandler DoMessage;
        // 點讚
        public event ViewEventHandler DoLike;
        // 刪除文章
        public event ViewEventHandler DoDelArticle;
        // 編輯文章
        public event ViewEventHandler ToEdit;
        // 返回看板
        public event ViewEventHandler ToBack;
        // 返回首頁
        public event ViewEventHandler ToHome;
        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        /// <summary>
        /// 文章資料
        /// </summary>
        private Class.Article article;

        /// <summary>
        /// 使用者
        /// </summary>
        private Class.User user;

        //  初始化
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
 
            if (Controller.CrossPageDAT["User"] != null)
                user = Controller.CrossPageDAT["User"] as Class.User;
            if (Controller.CrossPageDAT["Article"] != null)
                article = Controller.CrossPageDAT["Article"] as Class.Article;

            Content.Text = article.Content;
            Title.Text = article.Title;
            ReleaseUser.Text = Controller.CrossPageDAT["ReleaseUser"] as string;
            ReleaseTime.Text = article.Date.Year.ToString() + "/" 
                + article.Date.Month.ToString() + "/"
                + article.Date.Day.ToString();

            List<AMessage> messageAll = new List<AMessage>();
            messageAll.Clear();
            messageAll = Controller.CrossPageDAT["AllMessage"] as List<AMessage>;
            allMessage.Items.Clear();
            foreach (AMessage item in messageAll)
            {
                allMessage.Items.Add(item.Content);
            }

            numLike.Text = "x" + article.LikeCount.ToString();
            

            if (Controller.CrossPageDAT.Keys.Contains("Admin")) {
                btnDel.Enabled = true;
                if(article.ReleaseUser == user.Userinfo.UID)
                    btnEdit.Enabled = true;
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
            Title.ForeColor = a;
            blank3.ForeColor = a;
            btnDel.ForeColor = a;
            ReleaseUser.ForeColor = a;
            ReleaseTime.ForeColor = a;
            Content.ForeColor = a;
            allMessage.ForeColor = a;
            numLike.ForeColor = a;
            btnLike.ForeColor = a;
            Message.ForeColor = a;
            btnSend.ForeColor = a;
            lblError.ForeColor = a;
            btnBack.ForeColor = a;
            btnHome.ForeColor = a;
            btnEdit.ForeColor = a;

            blank3.Style.Add("background-color", border);
            btnDel.Style.Add("background-color", border);
            allMessage.Style.Add("background-color", border);
            btnLike.Style.Add("background-color", border);
            Message.Style.Add("background-color", border);
            btnSend.Style.Add("background-color", border);
            btnBack.Style.Add("background-color", border);
            btnHome.Style.Add("background-color", border);
            btnEdit.Style.Add("background-color", border);
        }

        //  返回看板
        protected void btnBack_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["BID"] = (Controller.CrossPageDAT["Article"] as Class.Article).OfBoard;
            ToBack(new ViewEventArgs(dat, this), out optDAT);
        }

        //  留言
        protected void btnSend_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            string message = (Controller.CrossPageDAT["User"] as User).Userinfo.ID + "：" + Message.Text;
            dat["Message"] = message;
            dat["AID"] = (Controller.CrossPageDAT["Article"] as Class.Article).AID;
            dat["BID"] = (Controller.CrossPageDAT["Article"] as Class.Article).OfBoard;
            Message.Text = "";
            DoMessage(new ViewEventArgs(dat, this), out optDAT);

            if (optDAT.Keys.Contains("failinfo"))
            {
                lblError.Visible = true;
                lblError.Text = optDAT["failinfo"] as string;
            }
        }

        //  傳送點讚事件
        protected void btnLike_Click(object sender, ImageClickEventArgs e)
        {
            DAT dat = new DAT();
            dat["AID"] = (Controller.CrossPageDAT["Article"] as Class.Article).AID;
            DoLike(new ViewEventArgs(dat, this), out optDAT);
            if (optDAT.Keys.Contains("LikeCount"))
            {
                numLike.Text = "x" + optDAT["LikeCount"] as string;
                btnLike.Enabled = false;
            }
        }
        //  返回首頁
        protected void btnHome_Click(object sender, EventArgs e)
        {
            ToHome(new ViewEventArgs(this), out optDAT);
        }

        // 刪除文章並自動跳轉回看板
        protected void btnDel_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["AID"] = (Controller.CrossPageDAT["Article"] as Class.Article).AID;
            dat["BID"] = (Controller.CrossPageDAT["Article"] as Class.Article).OfBoard;
            DoDelArticle(new ViewEventArgs(dat, this), out optDAT);
        }

        // 編輯文章
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["AID"] = (Controller.CrossPageDAT["Article"] as Class.Article).AID;
            ToEdit(new ViewEventArgs(dat, this), out optDAT);
        }
    }
}