using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

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
                user.Userinfo.Password = SecretText.Text;
                user.Userinfo.StudentID = IDText.Text;
                user.Userinfo.Email = MailText.Text;
                user.Userinfo.Gender = (GenderList.SelectedIndex == 0 ? Gender.Null : (GenderList.SelectedIndex == 1 ? Gender.Male : (GenderList.SelectedIndex == 2 ? Gender.Female : Gender.Thirdgender)));
                user.Userinfo.Realname = RealNameText.Text;
                user.Userinfo.Nickname = NickNameText.Text;
                user.Usersetting.Viewstyle = ViewStyleList.SelectedIndex;
                dat["User"] = user;
                DoChange(new ViewEventArgs(dat, this), out optDAT);
            }
        }
    }
}