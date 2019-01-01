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
    public partial class Setting : System.Web.UI.Page
    {
        public event ViewEventHandler DoChange;
        public event ViewEventHandler ToBack;
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
            
            AccountText.Text = user.Userinfo.ID;
            SecretText.Text = user.Userinfo.Password;
            IDText.Text = user.Userinfo.StudentID;
            MailText.Text = user.Userinfo.Email;
            GenderList.SelectedIndex = (user.Userinfo.Gender == Gender.Null ? 0 : (user.Userinfo.Gender==Gender.Male? 1 :(user.Userinfo.Gender == Gender.Female ? 2 : 3)));
            RealNameText.Text = user.Userinfo.Realname;
            NickNameText.Text = user.Userinfo.Nickname;
            coinText.Text = user.TbitCoin.ToString();
            ViewStyleList.SelectedIndex = user.Usersetting.Viewstyle;
            int style = 0;
            style = user.Usersetting.Viewstyle;
            if (style == 0)
            {
                color.Style.Add("background-color", "lightblue");
                Color a = Color.Black;
                String border = "white";
                Accountlabel.ForeColor = a;
                AccountText.ForeColor = a;
                Secretlabel.ForeColor = a;
                SecretText.ForeColor = a;
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

                Accountlabel.Style.Add("background-color", border);
                AccountText.Style.Add("background-color", border);
                Secretlabel.Style.Add("background-color", border);
                SecretText.Style.Add("background-color", border);
                IDLabel.Style.Add("background-color", border);
                IDText.Style.Add("background-color", border);
                Maillabel.Style.Add("background-color", border);
                MailText.Style.Add("background-color", border);
                Genderlabel.Style.Add("background-color", border);
                GenderList.Style.Add("background-color", border);
                RealNameLabel.Style.Add("background-color", border);
                RealNameText.Style.Add("background-color", border);
                NickNameLabel.Style.Add("background-color", border);
                NickNameText.Style.Add("background-color", border);

                coinlabel.Style.Add("background-color", border);
                coinText.Style.Add("background-color", border);
                ViewStylelabel.Style.Add("background-color", border);
                ViewStyleList.Style.Add("background-color", border);
                btnBack.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
            else if (style == 1)
            {
                color.Style.Add("background-color", "black");
                Color a = Color.WhiteSmoke;
                String border = "DarkGray";

                Accountlabel.ForeColor = a;
                AccountText.ForeColor = a;
                Secretlabel.ForeColor = a;
                SecretText.ForeColor = a;
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

                Accountlabel.Style.Add("background-color", border);
                AccountText.Style.Add("background-color", border);
                Secretlabel.Style.Add("background-color", border);
                SecretText.Style.Add("background-color", border);
                IDLabel.Style.Add("background-color", border);
                IDText.Style.Add("background-color", border);
                Maillabel.Style.Add("background-color", border);
                MailText.Style.Add("background-color", border);
                Genderlabel.Style.Add("background-color", border);
                GenderList.Style.Add("background-color", border);
                RealNameLabel.Style.Add("background-color", border);
                RealNameText.Style.Add("background-color", border);
                NickNameLabel.Style.Add("background-color", border);
                NickNameText.Style.Add("background-color", border);

                coinlabel.Style.Add("background-color", border);
                coinText.Style.Add("background-color", border);
                ViewStylelabel.Style.Add("background-color", border);
                ViewStyleList.Style.Add("background-color", border);
                btnBack.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
            else
            {
                color.Style.Add("background-color", "BurlyWood");
                Color a = Color.BlueViolet;
                String border = "CadetBlue";

                Accountlabel.ForeColor = a;
                AccountText.ForeColor = a;
                Secretlabel.ForeColor = a;
                SecretText.ForeColor = a;
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

                Accountlabel.Style.Add("background-color", border);
                AccountText.Style.Add("background-color", border);
                Secretlabel.Style.Add("background-color", border);
                SecretText.Style.Add("background-color", border);
                IDLabel.Style.Add("background-color", border);
                IDText.Style.Add("background-color", border);
                Maillabel.Style.Add("background-color", border);
                MailText.Style.Add("background-color", border);
                Genderlabel.Style.Add("background-color", border);
                GenderList.Style.Add("background-color", border);
                RealNameLabel.Style.Add("background-color", border);
                RealNameText.Style.Add("background-color", border);
                NickNameLabel.Style.Add("background-color", border);
                NickNameText.Style.Add("background-color", border);

                coinlabel.Style.Add("background-color", border);
                coinText.Style.Add("background-color", border);
                ViewStylelabel.Style.Add("background-color", border);
                ViewStyleList.Style.Add("background-color", border);
                btnBack.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ToBack(new ViewEventArgs(this), out optDAT);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (SecretText.Text == "") {
                lblError.Text = "密碼不得為空";
            }
            else if (IDText.Text == "") {
                lblError.Text = "學號不得為空";
            }
            else {
                DAT dat = new DAT();
                dat["Password"] = SecretText.Text;
                dat["StudentID"] = IDText.Text;
                dat["Email"] = MailText.Text;
                dat["Gender"] = (GenderList.SelectedIndex == 0 ? Gender.Null : (GenderList.SelectedIndex == 1 ? Gender.Male : (GenderList.SelectedIndex == 2 ? Gender.Female : Gender.Thirdgender)));
                dat["Realname"] = RealNameText.Text;
                dat["Nickname"] = NickNameText.Text;
                dat["Viewstyle"] = ViewStyleList.SelectedIndex;
                DoChange(new ViewEventArgs(dat, this), out optDAT);
            }
        }
    }
}