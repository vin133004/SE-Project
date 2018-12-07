using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Tpage.Class
{
    public class View
    {
        public Controller controller { get; set; }
        public Model model { get; set; }

        public View(Controller controller)
        {
            this.controller = controller;
        }
    }
}