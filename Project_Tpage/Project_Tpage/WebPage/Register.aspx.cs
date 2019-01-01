using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class Register : System.Web.UI.Page
    {
        /// <summary>
        /// 註冊事件。
        /// </summary>
        public event ViewEventHandler DoRegister;
        /// <summary>
        /// 返回前頁面事件。
        /// </summary>
        public event ViewEventHandler ToBack;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        protected void Page_Load(object sender, EventArgs e)
        {
            //不在登入頁面，Controller未初始化的情況，導向登入頁面。
            if (!Controller.IsConstrut)
                Controller.Initial(StateEnum.Login);
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);
        }

        public void btn1_Click(object sender, EventArgs e)
        {
            //當使用者按下註冊按鈕，引發註冊事件。

            if(!tbx2.Text.Equals(tbx3.Text))
            {
                lblError.Text = "確認密碼有誤！";
                lblError.Visible = true;
                return;
            }


            //設定輸入參數資料
            DAT dat = new DAT();
            dat["Id"] = tbx1.Text;
            dat["Password"] = tbx2.Text;
            dat["Email"] = tbx4.Text;
            dat["StudentID"] = tbx5.Text;
            

            //引發事件，取得輸出結果在optDAT裡。
            DoRegister(new ViewEventArgs(dat, this), out optDAT);

            //若有錯誤資訊，表示註冊發生錯誤，顯示錯誤資訊在頁面上。
            if(optDAT.Keys.Contains("failinfo"))
            {
                lblError.Visible = true;
                lblError.Text = optDAT["failinfo"] as string;
            }
            else
            {
                Response.Redirect("Login");
            }
        }
        
        public void btn2_Click(object sender, EventArgs e)
        {
            //當使用者按下返回按鈕，引發返回前頁面事件。

            //引發事件。
            ToBack(new ViewEventArgs(this), out optDAT);

            Response.Redirect("Login");
        }
    }
}