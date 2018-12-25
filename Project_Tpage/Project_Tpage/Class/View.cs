using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Project_Tpage.Class
{
    public class View: System.Web.UI.Page
    {
        public Controller controller { get; set; }
        public Model model { get; set; }

        public View()
        {
            this.controller = new Controller();
            this.model = new Model(this);
        }
       

    }
}