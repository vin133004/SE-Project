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
    public partial class CreateBoard : System.Web.UI.Page
    {
        // 創建看板
        public event ViewEventHandler DoCreate;
        // 邀請成員
        public event ViewEventHandler DoInvite;
        // 返回首頁
        public event ViewEventHandler ToHome;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;
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
            int style = 2;
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
            nameLabel.ForeColor = a;
            boardName.ForeColor = a;
            infoLabel.ForeColor = a;
            boardInfo.ForeColor = a;
            Announce.ForeColor = a;
            peopleName.ForeColor = a;
            btnInvite.ForeColor = a;
            btnNoInvite.ForeColor = a;
            inviteInfo.ForeColor = a;
            listLabel.ForeColor = a;
            inviteList.ForeColor = a;
            btnBack.ForeColor = a;
            btnPo.ForeColor = a;
            lblError.ForeColor = a;

            boardName.Style.Add("background-color", border);
            peopleName.Style.Add("background-color", border);
            btnInvite.Style.Add("background-color", border);
            btnNoInvite.Style.Add("background-color", border);
            inviteList.Style.Add("background-color", border);
            btnBack.Style.Add("background-color", border);
            btnPo.Style.Add("background-color", border);
            lblError.Style.Add("background-color", border);
        }
    

        protected void btnPo_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            // 看板名稱
            dat["BoardName"] = boardName.Text;
            // 是否公開
            dat["Public"] = boardInfo.SelectedIndex == 0 ? "true" : "false";

            // 邀請清單
            List<string> people = new List<string>();
            foreach (var item in inviteList.Items)
            {
                people.Add(item.ToString());
            }
            dat["PeopleList"] = people;
            DoCreate(new ViewEventArgs(dat, this), out optDAT);
            if (optDAT.Keys.Contains("failinfo"))
            {
                lblError.Visible = true;
                lblError.Text = optDAT["failinfo"] as string;
            }
            else
            {
                foreach (KeyValuePair<string, object> v in optDAT)
                    Controller.CrossPageDAT[v.Key] = v.Value;
                Response.Redirect("Home");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ToHome(new ViewEventArgs(this), out optDAT);
            Response.Redirect("Home");
        }

        protected void btnInvite_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["People"] = peopleName.Text;
            DoInvite(new ViewEventArgs(dat, this),out optDAT);
            // 接受邀請失敗 e.x. 已邀請、無此人
            if (optDAT.Keys.Contains("failinfo"))
            {
                inviteInfo.Visible = true;
                inviteInfo.Text = optDAT["failinfo"] as string;
            }
            // 成功資訊顯示
            else if (optDAT.Keys.Contains("info"))
            {
                inviteInfo.Visible = true;
                inviteInfo.Text = "邀請成功";
                inviteList.Items.Add(peopleName.Text);
            }
        }

        protected void btnNoInvite_Click(object sender, EventArgs e)
        {
            if (inviteList.Items.FindByText(peopleName.Text) != null)
            {
                inviteList.Items.Remove(peopleName.Text);
                inviteInfo.Text = "移除成功";
            }
            else {
                inviteInfo.Text = "移除失敗(無此人)";
            }
        }

        protected void boardInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}