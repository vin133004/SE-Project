using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class AD : System.Web.UI.Page
    {
        // 返回首頁
        public event ViewEventHandler ToHome;
        // 確認購買
        public event ViewEventHandler DoBuy;
        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        private Class.User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //在登入頁面，未初始化Controller的情況，初始化Controller
            if (!Controller.IsConstrut)
                Response.Redirect("Login.aspx");
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);

            // 初始化需要ID、現有價格、背景配色(?)
            user =  Controller.CrossPageDAT["user"] as User;
            IDInfo.Text = user.Userinfo.ID;
            moneyInfo.Text = user.TbitCoin.ToString();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            ToHome(new ViewEventArgs(this), out optDAT);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (FileTmp.HasFile && btnList.SelectedIndex != -1)
            {
                // 存檔
                string path = Server.MapPath("\\WebPage\\pictures\\");
                path += FileTmp.FileName;
                FileTmp.SaveAs(path);

                DAT dat = new DAT();
                // 購買方案
                if (btnList.SelectedIndex == 0) {
                    dat["cost"] = 1;
                    dat["minite"] = 5;
                }
                else if (btnList.SelectedIndex == 1) {
                    dat["cost"] = 2;
                    dat["minite"] = 15;
                }
                else if (btnList.SelectedIndex == 2) {
                    dat["cost"] = 3;
                    dat["minite"] = 30;
                }
                else {
                    dat["cost"] = 5;
                    dat["minite"] = 60;
                }
                dat["path"] = path;
                DoBuy(new ViewEventArgs(dat, this), out optDAT);
                // 購買失敗 e.x. 餘額不足
                if (optDAT.Keys.Contains("failinfo")) {
                    lblError.Visible = true;
                    lblError.Text = optDAT["failinfo"] as string;
                }
            }
            else if (btnList.SelectedIndex == -1)
            {
                lblError.Visible = true;
                lblError.Text = "請選擇方案";
            }
            else {
                lblError.Visible = true;
                lblError.Text = "請上傳檔案";
            }
        }
    }
}