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
    public partial class Board : System.Web.UI.Page
    {
        // 前往文章
        public event ViewEventHandler ToArticle;
        // 前往PO文
        public event ViewEventHandler ToEditor;
        // 返回
        public event ViewEventHandler ToBack;
        // 追隨
        public event ViewEventHandler DoFollow;
        // 邀請成員(版主or管理者)
        public event ViewEventHandler DoInvite;
        // 邀請為管理者(版主or管理者)(可以外部or現有成員)
        public event ViewEventHandler DoAdmin;
        // 刪除成員(版主or管理者)
        public event ViewEventHandler DoDelPeople;
        // 刪除看板(版主)
        public event ViewEventHandler DoDelBoard;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        private Class.User user;
        private Class.Board board;
        // 資料庫在這邊給資料。顯示於listbox
        protected void Page_Load(object sender, EventArgs e)
        {
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);
            if (Session["UID"] == null)
                Response.Redirect("Login");
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);
            
            ListOfArticle.Items.Clear();
            board = Controller.CrossPageDAT["Board"] as Class.Board;
            user = Controller.CrossPageDAT["User"] as User;
            
            Follow.ImageUrl = Controller.CrossPageDAT["LikeImage"] as string;
            Title.Text = board.Name;

            // 清除上次累積的其他看板
            ListOfArticle.Items.Clear();

            List<Class.Article> list = new List<Class.Article>();
            list.Clear();
            list = Controller.CrossPageDAT["ArticleList"] as List<Class.Article>; 
            
            foreach (Class.Article element in list)
            {
                ListOfArticle.Items.Add(new ListItem(element.Title, element.AID));
            }
            
            bool isAdmin = false;
            bool isMaster = false;
            List<string> admin = new List<string>();
            admin.Clear();
            admin = board.Admin;

            if (board.PrivateMaster == user.Userinfo.UID)
            {
                isMaster = true;
                isAdmin = true;
            }

            foreach (string element in admin)
            {
                if (element == user.Userinfo.UID)
                {
                    isAdmin = true;
                    break;
                }
            }

            // 開啟管理者功能
            if (isAdmin) {
                btnDel.Enabled = true;
                btnAdmin.Enabled = true;
                if(isMaster)    // 版主有權刪除此看板
                    btnDelBoard.Enabled = true;
            }

            btnarticle.Style.Add("position", "absolute");
            btnarticle.Style.Add("top", "220px");
            btnarticle.Style.Add("left", "15%");
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
            Follow.ForeColor = a;
            btnarticle.ForeColor = a;
            ListOfArticle.ForeColor = a;
            btnBack.ForeColor = a;
            btnPo.ForeColor = a;
            peopleText.ForeColor = a;
            btnInvite.ForeColor = a;
            btnAdmin.ForeColor = a;
            btnDel.ForeColor = a;
            peopleInfo.ForeColor = a;
            btnDelBoard.ForeColor = a;

            btnarticle.Style.Add("background-color", border);
            ListOfArticle.Style.Add("background-color", border);
            btnBack.Style.Add("background-color", border);
            btnPo.Style.Add("background-color", border);
            peopleText.Style.Add("background-color", border);
            btnInvite.Style.Add("background-color", border);
            btnAdmin.Style.Add("background-color", border);
            btnDel.Style.Add("background-color", border);
            btnDelBoard.Style.Add("background-color", border);
        }
       
        //  選擇要看文章
        protected void SelectArticle(object sender, EventArgs e)
        {
           //不做事
        }

        //  返回首頁
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ToBack(new ViewEventArgs(this),out optDAT);
        }
       
        protected void follow_click(object sender, EventArgs e)
        {
            DoFollow(new ViewEventArgs(this), out optDAT);
        }

        //  發新文章
        protected void btnPo_Click(object sender, EventArgs e)
        {
            ToEditor(new ViewEventArgs(this), out optDAT);   
        }

        //  刪除成員(版主or管理者) Info 回傳到 peopleInfo
        protected void btnDel_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["ID"] = peopleText.Text;
            DoDelPeople(new ViewEventArgs(dat,this),out optDAT);
            // 刪除情況確認
            peopleInfo.Text = optDAT["Info"] as string;
        }
        //  邀請成為管理者(版主or管理者) Info 回傳到 peopleInfo
        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["ID"] = peopleText.Text;
            DoAdmin(new ViewEventArgs(dat, this), out optDAT);
            // 邀請狀況確認
            peopleInfo.Text = optDAT["Info"] as string;
        }
        //  邀請加入(版主or管理者) Info 回傳到 peopleInfo
        protected void btnInvite_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["ID"] = peopleText.Text;
            DoInvite(new ViewEventArgs(dat, this), out optDAT);
            // 邀請狀況確認
            peopleInfo.Text = optDAT["Info"] as string;
        }

        // 刪除看板(僅限版主)
        protected void btnDelBoard_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["BID"] = board.BID;
            DoDelBoard(new ViewEventArgs(dat,this), out optDAT); 
        }

        // 進入文章
        protected void btnarticle_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["AID"] = ListOfArticle.SelectedItem.Value;
            ToArticle(new ViewEventArgs(dat, this), out optDAT);     
        }
    }
}