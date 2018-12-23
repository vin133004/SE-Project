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
            model = new Model(view);
        }

        public void ToRegister()
        {
            if (model.State == StateEnum.Login || model.State == StateEnum.Register)
                model.State = StateEnum.Register;
        }

        public void ToLogin()
        {
            if (model.State == StateEnum.Login || model.State == StateEnum.Register)
                model.State = StateEnum.Login;
        }

        public void ToHome()
        {
            if (model.State != StateEnum.Register)
                model.State = StateEnum.Home;
        }

        public void ToUserPage()
        {
            if (model.State == StateEnum.Home)
                model.State = StateEnum.UserPage;
        }

        public void ToGroup()
        {
            if (model.State == StateEnum.Home)
                model.State = StateEnum.Group;
        }

        public void ToUserSetting()
        {
            if (model.State == StateEnum.Home)
                model.State = StateEnum.Setting;
        }

        public void ToArticle()
        {
            if (model.State == StateEnum.Home || model.State == StateEnum.UserPage || model.State == StateEnum.Board)
                model.State = StateEnum.Article;
        }

        public void ToBoard()
        {
            if (model.State == StateEnum.Group)
                model.State = StateEnum.Board;
        }

        public void ToEdit()
        {
            if (model.State == StateEnum.Board)
                model.State = StateEnum.EditArticle;
        }

        public void Register(UserInfo p_uif)
        {
            if (model.State != StateEnum.Register)
                return;
            try
            {
                model.Register(p_uif);
                ToLogin();
            }
            catch
            {
                ToRegister();
            }
        }

        public void Login(string p_ID, string p_Password)
        {
            if (model.State != StateEnum.Login)
                return;
            try
            {
                model.Login(p_ID, p_Password);
                ToHome();
            }
            catch
            {
                ToLogin();
            }
        }

        public void SetUserSetting(UserInfo p_uif, UserSetting p_ust)
        {
            if (model.State != StateEnum.Setting)
                return;
            try
            {
                model.SetUserSetting(p_uif, p_ust);
                ToUserSetting();
            }
            catch
            {
                ToUserSetting();
            }
        }

        public void CreateClass(string p_ClassName, string p_GroupName)
        {
            if (model.State != StateEnum.Board)
                return;
            try
            {
                model.CreateClass(p_ClassName, p_GroupName);
                ToGroup();
            }
            catch
            {
                ToGroup();
            }
        }

        public void CreateFamily(string p_GroupName)
        {
            if (model.State != StateEnum.Board)
                return;
            try
            {
                model.CreateFamily(p_GroupName);
                ToGroup();
            }
            catch
            {
                ToGroup();
            }
        }

        public void ReleaseArticle(string p_Title, string p_Content, string p_OfGroup, string p_OfBoard)
        {
            if (model.State != StateEnum.EditArticle)
                return;
            try
            {
                model.ReleaseArticle(p_Title, p_Content, p_OfGroup, p_OfBoard);
                ToGroup();
            }
            catch
            {
                ToEdit();
            }
        }

        public void ReleaseAMessage(string p_Message, string p_OfArticle)
        {
            if (model.State != StateEnum.Article)
                return;
            try
            {
                model.ReleaseAMessage(p_Message, p_OfArticle);
                ToArticle();
            }
            catch
            {
                ToArticle();
            }
        }

        public List<Article> GetArticlesFromGroup(string p_Group)
        {
            List<Article> Articles;
            if (model.State != StateEnum.Group)
                return null;
            try
            {
                Articles = model.GetArticlesFromGroup(p_Group);
                ToBoard();
                return Articles;
            }
            catch
            {
                ToGroup();
                return null;
            }
        }

        public List<Article> GetArticlesFromBoard(string p_Group, string p_Board)
        {
            List<Article> Articles;
            if (model.State != StateEnum.Group)
                return null;
            try
            {
                Articles = model.GetArticlesFromBoard(p_Group, p_Board);
                ToBoard();
                return Articles;
            }
            catch
            {
                ToGroup();
                return null;
            }
        }

        public List<object> GetDynamicPageContent()
        {
            List<object> rtn;
            if (model.State != StateEnum.Home)
                return null;
            try
            {
                rtn = model.GetDynamicPageContent();
                ToHome();
                return rtn;
            }
            catch
            {
                ToHome();
                return null;
            }
        }

        public List<object> GetUserPageContent(string uid)
        {
            List<object> rtn;
            if (model.State != StateEnum.Home)
                return null;
            try
            {
                rtn = model.GetUserPageContent(uid);
                ToUserPage();
                return rtn;
            }
            catch
            {
                ToHome();
                return null;
            }
        }
    }
}