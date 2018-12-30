using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 登入事件。
        /// </summary>
        public event ViewEventHandler DoLogin;
        /// <summary>
        /// 前往註冊頁面事件。
        /// </summary>
        public event ViewEventHandler ToRegister;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        protected void Page_Load(object sender, EventArgs e)
        {
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);


            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);
        }

        public void btn1_Click(object sender, EventArgs e)
        {
            //當使用者按下登入按鈕，引發登入事件。

            //設定輸入參數資料
            DAT dat = new DAT();
            dat["ID"] = tbx1.Text;
            dat["Password"] = tbx2.Text;

            //引發事件，取得輸出結果在optDAT裡。
            DoLogin(new ViewEventArgs(dat, this), out optDAT);
        }


        public void btn2_Click(object sender, EventArgs e)
        {
            //當使用者按下註冊按鈕，引發前往註冊頁面事件。

            //引發事件。
            ToRegister(new ViewEventArgs(this), out optDAT);
        }

    }
}