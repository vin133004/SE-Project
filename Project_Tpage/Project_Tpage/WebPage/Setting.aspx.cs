﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;
using System.Drawing;
namespace Project_Tpage.WebPage
{
    public partial class Setting : System.Web.UI.Page
    {
        public event ViewEventHandler DoChange;
        public event ViewEventHandler ToBack;
        public DAT optDAT;

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
            
            AccountText.Text = user.Userinfo.ID;
            IDText.Text = user.Userinfo.StudentID;
            MailText.Text = user.Userinfo.Email;
            GenderList.SelectedIndex = (user.Userinfo.Gender == Gender.Null ? 0 : (user.Userinfo.Gender==Gender.Male? 1 :(user.Userinfo.Gender == Gender.Female ? 2 : 3)));
            RealNameText.Text = user.Userinfo.Realname;
            NickNameText.Text = user.Userinfo.Nickname;
            coinText.Text = user.TbitCoin.ToString();
            ViewStyleList.SelectedIndex = user.Usersetting.Viewstyle;
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

            Accountlabel.ForeColor = a;
            AccountText.ForeColor = a;
            IDLabel.ForeColor = a;
            IDText.ForeColor = a;
            Maillabel.ForeColor = a;
            MailText.ForeColor = a;
            Genderlabel.ForeColor = a;
            GenderList.ForeColor = a;
            RealNameLabel.ForeColor = a;
            RealNameText.ForeColor = a;
            NickNameLabel.ForeColor = a;
            NickNameText.ForeColor = a;

            coinlabel.ForeColor = a;
            coinText.ForeColor = a;
            ViewStylelabel.ForeColor = a;
            ViewStyleList.ForeColor = a;
            btnBack.ForeColor = a;
            btnSend.ForeColor = a;
            lblError.ForeColor = a;

            MailText.Style.Add("background-color", border);
            RealNameText.Style.Add("background-color", border);
            NickNameText.Style.Add("background-color", border);
            coinText.Style.Add("background-color", border);
            btnBack.Style.Add("background-color", border);
            btnSend.Style.Add("background-color", border);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ToBack(new ViewEventArgs(this), out optDAT);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["Email"] = MailText.Text;
            dat["Gender"] = (GenderList.SelectedIndex == 0 ? Gender.Null : (GenderList.SelectedIndex == 1 ? Gender.Male : (GenderList.SelectedIndex == 2 ? Gender.Female : Gender.Thirdgender)));
            dat["Realname"] = RealNameText.Text;
            dat["Nickname"] = NickNameText.Text;
            dat["Viewstyle"] = ViewStyleList.SelectedIndex;
            DoChange(new ViewEventArgs(dat, this), out optDAT);
            lblError.Visible = true;
            if (optDAT.Keys.Contains("failinfo"))
                lblError.Text = optDAT["failinfo"] as string;
        }
    }
}