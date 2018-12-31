using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

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
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);

            user = Controller.CrossPageDAT["User"] as Class.User;
            article = Controller.CrossPageDAT["Article"] as Class.Article;
           
            Content.Text = article.Content;
            Title.Text = article.Title;
            ReleaseUser.Text = Controller.CrossPageDAT["ReleaseUser"] as string;
            ReleaseTime.Text = article.Date.Year.ToString() + "/" 
                + article.Date.Month.ToString() + "/"
                + article.Date.Day.ToString();
            // 缺少留言的資訊
            allMessage.Items.Clear();
            numLike.Text = "x" + article.LikeCount.ToString();

            // 宥騰的MVC概念
            bool admin = Controller.model.IsBoardAdmin(article.OfBoard, user.Userinfo.UID);
            if (admin) {
                btnDel.Enabled = true;
                if(article.ReleaseUser == user.Userinfo.UID)
                    btnEdit.Enabled = true;
            }
        }

        //  返回看板
        protected void btnBack_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["BID"] = article.OfBoard;
            ToBack(new ViewEventArgs(dat, this), out optDAT);
        }

        //  留言
        protected void btnSend_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            string message = user.Userinfo.ID + "：" + Message.Text;
            dat["Message"] = message;
            dat["AID"] = article.AID;
            dat["UID"] = user.Userinfo.UID;

            DoMessage(new ViewEventArgs(dat, this), out optDAT);
            Message.Text = "";

            if (optDAT.Keys.Contains("failinfo")) {
                lblError.Visible = true;
                lblError.Text = optDAT["failinfo"] as string;
            }
        }

        //  傳送點讚事件
        protected void btnLike_Click(object sender, ImageClickEventArgs e)
        {
            DAT dat = new DAT();
            dat["UID"] = user.Userinfo.UID;
            DoLike(new ViewEventArgs(dat,this), out optDAT);
            if (!optDAT.Keys.Contains("failinfo")) {
                btnLike.ImageUrl = optDAT["imageurl"] as string;
                numLike.Text = "x" + optDAT["LikeCount"] as string;
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
            dat["AID"] = article.AID;
            dat["BID"] = article.OfBoard;
            DoDelArticle(new ViewEventArgs(dat, this), out optDAT);
        }

        // 編輯文章
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["AID"] = article.AID;
            ToEdit(new ViewEventArgs(dat, this), out optDAT);
        }
    }
}