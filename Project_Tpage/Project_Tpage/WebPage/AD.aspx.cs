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
                Controller.Initial(StateEnum.Login);
            //讓Controller內的function訂閱這個頁面上的事件。
            //Do this in each Page_Load()
            Controller.controller.SubsribeEvent(this);
           
            // 初始化需要ID、現有金額、背景配色(?)
            user = Controller.CrossPageDAT["User"] as User;
            int style = 0;
            style = user.Usersetting.Viewstyle;
            IDInfo.Text = user.Userinfo.ID;
            moneyInfo.Text = user.TbitCoin.ToString();
            if (style == 0)
            {
                color.Style.Add("background-color", "lightblue");
                Color a = Color.Black;
                String border = "white";
                IDLabel.ForeColor = a;
                IDInfo.ForeColor = a;
                moneyLabel.ForeColor = a;
                moneyInfo.ForeColor = a;
                upInfo.ForeColor = a;
                FileTmp.ForeColor = a;
                costInfo.ForeColor = a;
                btnList.ForeColor = a;
                btnHome.ForeColor = a;
                btnSend.ForeColor = a;
                lblError.ForeColor = a;
                
                IDLabel.Style.Add("background-color", border);
                IDInfo.Style.Add("background-color", border);
                moneyLabel.Style.Add("background-color", border);
                moneyInfo.Style.Add("background-color", border);
                upInfo.Style.Add("background-color", border);
                FileTmp.Style.Add("background-color", border);
                costInfo.Style.Add("background-color", border);
                btnList.Style.Add("background-color", border);
                btnHome.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
            else if (style == 1)
            {
                color.Style.Add("background-color", "black");
                Color a = Color.WhiteSmoke;
                String border = "DarkGray";
                IDLabel.ForeColor = a;
                IDInfo.ForeColor = a;
                moneyLabel.ForeColor = a;
                moneyInfo.ForeColor = a;
                upInfo.ForeColor = a;
                FileTmp.ForeColor = a;
                costInfo.ForeColor = a;
                btnList.ForeColor = a;
                btnHome.ForeColor = a;
                btnSend.ForeColor = a;
                lblError.ForeColor = a;

                IDLabel.Style.Add("background-color", border);
                IDInfo.Style.Add("background-color", border);
                moneyLabel.Style.Add("background-color", border);
                moneyInfo.Style.Add("background-color", border);
                upInfo.Style.Add("background-color", border);
                FileTmp.Style.Add("background-color", border);
                costInfo.Style.Add("background-color", border);
                btnList.Style.Add("background-color", border);
                btnHome.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
            else
            {
                color.Style.Add("background-color", "BurlyWood");
                Color a = Color.BlueViolet;
                String border = "CadetBlue";
                IDLabel.ForeColor = a;
                IDInfo.ForeColor = a;
                moneyLabel.ForeColor = a;
                moneyInfo.ForeColor = a;
                upInfo.ForeColor = a;
                FileTmp.ForeColor = a;
                costInfo.ForeColor = a;
                btnList.ForeColor = a;
                btnHome.ForeColor = a;
                btnSend.ForeColor = a;
                lblError.ForeColor = a;

                IDLabel.Style.Add("background-color", border);
                IDInfo.Style.Add("background-color", border);
                moneyLabel.Style.Add("background-color", border);
                moneyInfo.Style.Add("background-color", border);
                upInfo.Style.Add("background-color", border);
                FileTmp.Style.Add("background-color", border);
                costInfo.Style.Add("background-color", border);
                btnList.Style.Add("background-color", border);
                btnHome.Style.Add("background-color", border);
                btnSend.Style.Add("background-color", border);
                lblError.Style.Add("background-color", border);
            }
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            ToHome(new ViewEventArgs(this), out optDAT);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {


            if (FileTmp.HasFile && btnList.SelectedIndex != -1)
            {
                String fileExtension = System.IO.Path.GetExtension(FileTmp.FileName).ToLower();
                String[] restrictExtension = { ".gif", ".jpg", ".bmp", ".png" };
                bool isPicture = false;
                for (int i = 0; i < 4; i++) {
                    if (restrictExtension[i] == fileExtension)
                        isPicture = true;
                }
                if (!isPicture)
                {
                    lblError.Visible = true;
                    lblError.Text = "格式錯誤";
                }
                else
                {
                    // 存檔
                    System.Drawing.Image image = System.Drawing.Image.FromStream(FileTmp.FileContent);

                    DAT dat = new DAT();
                    // 購買方案
                    if (btnList.SelectedIndex == 0)
                    {
                        dat["Money"] = 1;
                        dat["Minute"] = 5;
                    }
                    else if (btnList.SelectedIndex == 1)
                    {
                        dat["Money"] = 2;
                        dat["Minute"] = 15;
                    }
                    else if (btnList.SelectedIndex == 2)
                    {
                        dat["Money"] = 3;
                        dat["Minute"] = 30;
                    }
                    else
                    {
                        dat["Money"] = 5;
                        dat["Minute"] = 60;
                    }
                    dat["Image"] = image;
                    DoBuy(new ViewEventArgs(dat, this), out optDAT);
                    // 購買失敗 e.x. 餘額不足
                    if (optDAT.Keys.Contains("failinfo"))
                    {
                        lblError.Visible = true;
                        lblError.Text = optDAT["failinfo"] as string;
                    }
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