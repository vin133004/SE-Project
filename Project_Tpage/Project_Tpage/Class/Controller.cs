using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Project_Tpage.WebPage;
using System.Data;
using System.Drawing;

namespace Project_Tpage.Class
{
    public class Controller
    {
        public static Controller controller { get; set; }
        public static Model model { get; set; }
        public static View view { get; set; }
        /// <summary>
        /// 指出控制器是否已建構的旗標。
        /// </summary>
        public static bool IsConstrut = false;
        /// <summary>
        /// 提供跨頁面的資料傳輸途徑。
        /// </summary>
        public static DAT CrossPageDAT { get; set; }

        public Controller()
        {

        }

        public static void Initial(StateEnum state)
        {
            model = new Model("server=lab2.w-net.ga;port=3306;user id=se2018;password=NTUST_2018Fall_SE;database=se2018;charset=utf8");
            view = new View();
            controller = new Controller();

            IsConstrut = true;
            CrossPageDAT = new DAT();

            model.State = state;
        }

        /// <summary>
        /// 註冊在p頁面上的事件。
        /// </summary>
        /// <param name="p">目標頁面。</param>
        public void SubsribeEvent(Page p)
        {
            //將在頁面內設計好的事件在此註冊。

            if (p is Login)             // Login State
            {
                (p as Login).DoLogin += LoginState_Login;
                (p as Login).ToRegister += LoginState_ToRegister;
            }
            else if (p is Register)     // Register State
            {
                (p as Register).DoRegister += RegisterState_Register;
                (p as Register).ToBack += RegisterState_ToBack;
            }
            else if (p is Home)         //  Home State
            {
                (p as Home).ToBoard += HomeState_ToBoard;
                (p as Home).ToCreateBoard += HomeState_ToCreateBoard;
                (p as Home).ToAD += HomeState_ToAD;
                (p as Home).DoSearch += HomeState_DoSearch;
                (p as Home).DoCard += HomeState_DoCard;
                (p as Home).ToSetBoard += HomeState_ToSetBoard;
                (p as Home).DoYesInvite += HomeState_DoYesInvite;
                (p as Home).DoNoInvite += HomeState_DoNoInvite;
            }
            else if (p is WebPage.Board)        //  Board State
            {
                (p as WebPage.Board).ToArticle += BoardState_ToArticle;
                (p as WebPage.Board).ToEditor += BoardState_ToEditor;
                (p as WebPage.Board).ToBack += BoardState_ToBack;
                (p as WebPage.Board).DoFollow += BoardState_DoFollow;
                (p as WebPage.Board).DoInvite += BoardState_DoInvite;
                (p as WebPage.Board).DoAdmin += BoardState_DoAdmin;
                (p as WebPage.Board).DoDelPeople += BoardState_DoDelPeople;
                (p as WebPage.Board).DoDelBoard += BoardState_DoDelBoard;
            }
            else if (p is CreateBoard)  //  Create Board State
            {
                (p as CreateBoard).DoCreate += CreateBoardState_DoCreate;
                (p as CreateBoard).DoInvite += CreateBoardState_DoInvite;
                (p as CreateBoard).ToHome += CreateBoardState_ToHome;
            }
            else if (p is WebPage.Article)      //  Article State
            {
                (p as WebPage.Article).DoMessage += ArticleState_DoMessage;
                (p as WebPage.Article).DoLike += ArticleState_DoLike;
                (p as WebPage.Article).DoDelArticle += ArticleState_DoDelArticle;
                (p as WebPage.Article).ToEdit += ArticleState_ToEdit;
                (p as WebPage.Article).ToBack += ArticleState_ToBack;
                (p as WebPage.Article).ToHome += ArticleState_ToHome;
            }
            else if (p is Editor)       //  Editor State
            {
                (p as Editor).ToBack += EditorState_ToBack;
                (p as Editor).DoCreate += EditorState_DoCreate;
            }
            else if (p is AD)           //  AD State
            {
                (p as AD).ToHome += ADState_ToHome;
                (p as AD).DoBuy += ADState_DoBuy;
            }
            else if (p is Setting)      //  Setting State
            {
                (p as Setting).ToBack += SettingState_ToBack;
                (p as Setting).DoChange += SettingState_DoChange;
            }
        }
        //  Login State
        public void LoginState_Login(ViewEventArgs e, out DAT opt)
        {
            //取得帳號與密碼
            string ID = e.data["ID"] as string;
            string Password = e.data["Password"] as string;

            opt = new DAT();
            try
            {
                //嘗試登入
                model.user = model.Login(ID, Password);
                CrossPageDAT["User"] = model.user;
                
                opt.Append(model.RequestPageData(StateEnum.Home, e.data));
                e.page.Session["UID"] = model.user.Userinfo.UID;
                e.page.Response.Redirect("Home");
            }
            catch (ModelException me)
            {
                opt["failinfo"] = me.userMessage == "" ? "登入失敗" : me.userMessage;
            }
            catch (Exception)
            {
                opt["failinfo"] = "登入失敗";
            }

        }
        public void LoginState_ToRegister(ViewEventArgs e, out DAT opt)
        {
            opt = model.RequestPageData(StateEnum.Register, e.data);
        }

        //  Register State
        public void RegisterState_Register(ViewEventArgs e, out DAT opt)
        {
            //建構新的使用者資訊
            UserInfo uif = UserInfo.New;

            //設定欄位
            uif.ID = e.data["Id"] as string;
            uif.Password = e.data["Password"] as string;
            uif.Email = e.data["Email"] as string;
            uif.StudentID = e.data["StudentID"] as string;

            opt = new DAT();
            try
            {
                //嘗試註冊新的使用者。
                model.Register(uif);

                opt.Append(model.RequestPageData(StateEnum.Login, e.data));
                return;
            }
            catch (ModelException me) when (me.ErrorNumber == ModelException.Error.InvalidUserInformation)
            {
                opt["failinfo"] = me.userMessage;
            }
            catch (Exception)
            {
                opt["failinfo"] = "註冊失敗。";
            }
            opt.Append(model.RequestPageData(StateEnum.Register, e.data));
        }
        public void RegisterState_ToBack(ViewEventArgs e, out DAT opt)
        {
            opt = model.RequestPageData(StateEnum.Login, e.data);
        }

        //  Home State
        public void HomeState_ToBoard(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Board, e.data);
                e.page.Response.Redirect("Board.aspx");
            }
            catch(Exception)
            {
                ;
            }
        }
        public void HomeState_ToCreateBoard(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.CreateBoard, e.data);
                e.page.Response.Redirect("CreateBoard.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void HomeState_ToAD(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.AD, e.data);
                e.page.Response.Redirect("AD.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void HomeState_DoSearch(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            List<Board> all;
            try
            {
                all = model.GetBoardFromName(e.data["Search"] as string);
                if (all.Count > 0)
                {
                    opt["Board"] = all[0];
                    opt["ArticleList"] = model.GetArticlesOfBoard(all[0].BID);
                    User usr = (CrossPageDAT.Keys.Contains("User") ?
                                CrossPageDAT["User"] : e.data["User"]) as User;
                    opt["LikeImage"] = usr.FollowBoard.Contains(all[0].BID);
                    CrossPageDAT = opt;
                    CrossPageDAT["User"] = model.user;
                    e.page.Response.Redirect("Board.aspx");
                }
                else
                {
                    opt["Info"] = "error";
                }
            }
            catch (Exception)
            {
                opt["Info"] = "error";
            }
        }
        public void HomeState_DoCard(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                opt["Info"] = model.user.Pickcard().Userinfo.ID;
            }
            catch (ModelException error)
            {
                opt["Info"] = error.userMessage;
            }
            catch(Exception)
            {
                opt["Info"] = "error";
            }
        }
        public void HomeState_ToSetBoard(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Setting, e.data);
                e.page.Response.Redirect("Setting.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void HomeState_DoYesInvite(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                model.BoardFollow_AllowAdd(model.user, "", e.data["BID"] as string);
            }
            catch (Exception)
            {
                ;
            }
        }
        public void HomeState_DoNoInvite(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                model.user.FollowBoardQueue.RemoveAll(x => x.Split('@')[1] == (e.data["BID"] as string));
                Model.DB.Set<User>(model.user);
            }
            catch (Exception)
            {
                ;
            }
        }
        //  Board State
        public void BoardState_ToArticle(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Article, e.data);
                e.page.Response.Redirect("Article.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void BoardState_ToEditor(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.EditArticle, e.data);
                CrossPageDAT["BID"] = e.data["BID"];
                e.page.Response.Redirect("Editor.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void BoardState_ToBack(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void BoardState_DoFollow(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                model.BoardFollow_Follow(model.user, e.data["BID"] as string);
            }
            catch (Exception)
            {

            }
        }
        public void BoardState_DoInvite(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                User usr = Model.DB.Get<User>(Model.DB.UserID_UIDconvert(e.data["ID"] as string, true));
                string sender = model.user.Userinfo.UID;
                string bid = e.data["BID"] as string;
                model.BoardFollow_Add(usr,sender,bid);
                opt["Info"] = "邀請成功";
            }
            catch (Exception)
            {
                opt["Info"] = "error";
            }
        }
        public void BoardState_DoAdmin(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                Board brd = Model.DB.Get<Board>(e.data["BID"] as string);
                string uid = Model.DB.UserID_UIDconvert(e.data["ID"] as string, true);
                model.Admin_Add(brd,uid);
                opt["Info"] = "加入管理者群組成功";
            }
            catch (Exception)
            {
                opt["Info"] = "error";
            }
        }
        public void BoardState_DoDelPeople(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                Board brd = Model.DB.Get<Board>(e.data["BID"] as string);
                User usr = Model.DB.Get<User>(Model.DB.UserID_UIDconvert(e.data["ID"] as string, true));
                model.RemoveAFollowedUser(brd,usr);
                opt["Info"] = "刪除成功";
            }
            catch (Exception)
            {
                opt["Info"] = "error";
            }
        }
        public void BoardState_DoDelBoard(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                List <User> UserList= model.GetUserOfBoard(e.data["BID"] as string);
                foreach(User u in UserList)
                    model.RemoveAFollowedUser(Model.DB.Get<Board>(e.data["BID"] as string), u);
                model.Board_Remove(e.data["BID"] as string);
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch(Exception)
            {
                opt["Info"] = "error";
            }
        }

        //  Create Board State
        public void CreateBoardState_DoCreate(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                if (e.data["BoardName"] as string == "")
                    throw new Exception();
                Board brd = Board.New(e.data["Master"] as string);
                brd.Name = e.data["BoardName"] as string;
                string s = e.data["Public"] as string;
                brd.IsPublic = s == "true" ? true : false;
                List<string> Peoples = (List<string>)e.data["PeopleList"];

                model.Board_New(brd,Peoples);   

                opt.Append(model.RequestPageData(StateEnum.Home, e.data));
                return;
            }
            catch (ModelException me) when (me.ErrorNumber == ModelException.Error.InvalidUserInformation)
            {
                opt["failinfo"] = me.userMessage;
            }
            catch (Exception)
            {
                opt["failinfo"] = "建立看板失敗。";
            }
            opt.Append(model.RequestPageData(StateEnum.CreateBoard, e.data));
        }
        public void CreateBoardState_DoInvite(ViewEventArgs e, out DAT opt)
        {
            string ID = e.data["People"] as string;

            opt = new DAT();
            try
            {
                if (model.GetUserFromID(ID))
                    opt["success"] = "success";
                else
                    opt["failinfo"] = "使用者不存在";

                opt.Append(model.RequestPageData(StateEnum.CreateBoard, e.data));
                return;
            }
            catch (ModelException me) when (me.ErrorNumber == ModelException.Error.InvalidUserInformation)
            {
                opt["failinfo"] = me.userMessage;
            }
            catch (Exception)
            {
                opt["failinfo"] = "使用者不存在。";
            }
            opt.Append(model.RequestPageData(StateEnum.CreateBoard, e.data));

        }
        public void CreateBoardState_ToHome(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch (Exception)
            {

            }
        }

        //  Article State
        public void ArticleState_DoMessage(ViewEventArgs e, out DAT opt)
        {
            AMessage message = e.data["Message"] as AMessage;
            //設定錯誤資訊。
            string failinfo = "";
            opt = new DAT();
            try
            {
                model.ReleaseAMessage(message.Content, message.OfArticle);
            }
            catch (ModelException me)
            {
                failinfo = me.userMessage == "" ? "留言失敗" : me.userMessage;
                opt = model.RequestPageData(StateEnum.Article, e.data);
                opt["failinfo"] = failinfo;

            }
            catch (Exception)
            {
                failinfo = "留言失敗";
                opt = model.RequestPageData(StateEnum.Article, e.data);
                opt["failinfo"] = failinfo;
            }
        }
        public void ArticleState_DoLike(ViewEventArgs e, out DAT opt)
        {
            Article article;
            opt = new DAT();
            try
            {
                article = Model.DB.Get<Article>(e.data["AID"] as string);
                article.LikeCount++;
            }
            catch (ModelException)
            {
                opt["Info"] = "error";
            }
        }
        public void ArticleState_DoDelArticle(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                Model.DB.Remove<Article>(e.data["AID"] as string);
                CrossPageDAT = model.RequestPageData(StateEnum.Board, e.data);
                e.page.Response.Redirect("Board.aspx");
            }
            catch (ModelException)
            {
                opt["Info"] = "error";
            }
        }
        public void ArticleState_ToEdit(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.EditArticle, e.data);
                e.page.Response.Redirect("Editor.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void ArticleState_ToBack(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Board, e.data);
                e.page.Response.Redirect("Board.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void ArticleState_ToHome(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch (Exception)
            {

            }
        }

        //  Editor State
        public void EditorState_DoCreate(ViewEventArgs e, out DAT opt)
        {
            Article article = e.data["Article"] as Article;
            //設定錯誤資訊。
            string failinfo = "";
            opt = new DAT();
            try
            {
                //嘗試發文
                model.ReleaseArticle(article.ReleaseUser, article.OfBoard, article.Title, article.Content);
                if (e.data.Keys.Contains("BID"))
                {
                    CrossPageDAT = model.RequestPageData(StateEnum.Board, e.data);
                    e.page.Response.Redirect("Board.aspx");
                }
                else {
                    CrossPageDAT = model.RequestPageData(StateEnum.Article, e.data);
                    e.page.Response.Redirect("Article.aspx");
                }
            }
            catch (ModelException me)
            {
                failinfo = me.userMessage == "" ? "文章發佈失敗" : me.userMessage;
                opt = model.RequestPageData(StateEnum.EditArticle, e.data);
                opt["failinfo"] = failinfo;

            }
            catch (Exception)
            {
                failinfo = "文章發佈失敗";
                opt = model.RequestPageData(StateEnum.EditArticle, e.data);
                opt["failinfo"] = failinfo;
            }
        }
        public void EditorState_ToBack(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                if (e.data.Keys.Contains("Article"))
                {
                    CrossPageDAT = model.RequestPageData(StateEnum.Article, e.data);
                    e.page.Response.Redirect("Article.aspx");
                }
                else
                {
                    CrossPageDAT = model.RequestPageData(StateEnum.Board, e.data);
                    e.page.Response.Redirect("Board.aspx");
                }
            }
            catch(Exception)
            {
                opt["Info"] = "error";
            }
        }

        // AD State
        public void ADState_DoBuy(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                model.BuyAD(model.user, (int)e.data["Money"], e.data["Image"] as Image,
                    new DateTime(model.GetLatestAD().Deadline.Ticks + TimeSpan.TicksPerMinute * ((int)e.data["Minute"])));
            }
            catch (Exception)
            {
                opt["failinfo"] = "error";
            }
        }
        public void ADState_ToHome(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch (Exception)
            {

            }
        }

        // Setting State
        public void SettingState_ToBack(ViewEventArgs e, out DAT opt)
        {
            opt = new DAT();
            try
            {
                CrossPageDAT = model.RequestPageData(StateEnum.Home, e.data);
                e.page.Response.Redirect("Home.aspx");
            }
            catch (Exception)
            {

            }
        }
        public void SettingState_DoChange(ViewEventArgs e, out DAT opt)
        {
            UserInfo uif = UserInfo.New;
            UserSetting ust = UserSetting.New;

            uif.Email = e.data["Email"] as string;
            uif.Gender = (Gender)(int)e.data["Gender"];
            uif.Realname = e.data["Realname"] as string;
            uif.Nickname = e.data["Nickname"] as string;
            ust.Viewstyle = (int)e.data["Viewstyle"];

            opt = new DAT();
            try
            {
                User usr = CrossPageDAT["User"] as User;
                model.SetUserSetting(uif, ust, ref usr);
                CrossPageDAT["User"] = usr;
                
                opt.Append(model.RequestPageData(StateEnum.Home, e.data));

                e.page.Response.Redirect("Home");
            }
            catch(ModelException me)
            {
                opt["failinfo"] = me.userMessage;
            }
            catch(Exception)
            {
                opt["failinfo"] = "設定失敗。";
            }
        }

    }

    /// <summary>
    /// 代表在View上引發的事件。
    /// </summary>
    /// <param name="page">引發事件的頁面。</param>
    /// <param name="sender">引發事件的控制項。</param>
    /// <param name="e">事件附帶的資料。</param>
    public delegate void ViewEventHandler(ViewEventArgs e, out DAT opt);

    /// <summary>
    /// 代表附帶在ViewEventHandler上的事件資料。
    /// </summary>
    public class ViewEventArgs
    {
        /// <summary>
        /// 儲存資料的字典物件。
        /// </summary>
        public DAT data { get; }
        /// <summary>
        /// 引發事件的頁面。
        /// </summary>
        public Page page { get; }

        /// <summary>
        /// 初始化ViewEventArgs類別的新執行個體，其中data值為null。
        /// </summary>
        public ViewEventArgs(Page pag)
        {
            data = new DAT();
            page = pag;
        }

        /// <summary>
        /// 初始化ViewEventArgs類別的新執行個體，其中包含從指定的DAT複製至data的項目。
        /// </summary>
        /// <param name="dat"></param>
        public ViewEventArgs(DAT dat, Page pag)
        {
            data = new DAT(dat);
            page = pag;
        }
    }

    /// <summary>
    /// 用來儲存資料的結構。
    /// </summary>
    public class DAT : Dictionary<string, object>
    {
        public new object this[string s]
        {
            get
            {
                return ((Dictionary<string, object>)this)[s];
            }
            set
            {
                try
                {
                    ((Dictionary<string, object>)this)[s] = value;
                }
                catch (KeyNotFoundException)
                {
                    Add(s, value);
                }
            }
        }

        public void Clone(Dictionary<string, object> src)
        {
            Clear();
            foreach (string key in src.Keys)
                Add(key, src[key]);
        }

        public void Append(Dictionary<string, object> src)
        {
            foreach (KeyValuePair<string, object> x in src)
                this[x.Key] = x.Value;
        }

        /// <summary>
        /// 初始化DAT類別的新執行個體，這個執行個體是空白的、具有預設的初始容量。
        /// </summary>
        public DAT() : base() { }

        /// <summary>
        /// 初始化DAT類別的新執行個體，這個執行個體是空白的、具有指定的初始容量。
        /// </summary>
        /// <param name="capacity"></param>
        public DAT(int capacity) : base(capacity) { }

        /// <summary>
        /// 初始化DAT類別的新執行個體，其中包含從指定的Dictionary複製的項目。
        /// </summary>
        /// <param name="dat"></param>
        public DAT(Dictionary<string, object> dat) : base(dat) { }
    }
}