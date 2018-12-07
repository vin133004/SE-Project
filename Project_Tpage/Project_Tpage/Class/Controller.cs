using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Tpage.Class
{
    public class Controller
    {
        public Model model { get; set; }
        public View view { get; set; }

        public Controller()
        {
            view = new View(this);
            model = new Model(view);
        }
    }
}