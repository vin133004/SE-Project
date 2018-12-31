using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class TakeShowingImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Controller.CrossPageDAT.Keys.Contains("TEMP_ShowingImage"))
                Response.BinaryWrite((byte[])new ImageConverter().
                    ConvertTo(Controller.CrossPageDAT["TEMP_ShowingImage"], typeof(byte[])));
        }
    }
}