using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Collections;


namespace Project_Tpage.Class
{
    /// <summary>
    /// 代表此資料模型所擲回的例外狀況。若要回報錯誤給使用者瀏覽，請提取userMessage屬性。
    /// </summary>
    public class ModelException : Exception
    {
        /// <summary>
        /// 顯示給使用者看得錯誤訊息。
        /// </summary>
        public string userMessage { get; }

        /// <summary>
        /// 代表一個資料模型例外狀況的錯誤代號列舉值。
        /// </summary>
        public enum Error
        {
            /// <summary>
            /// 未知錯誤。
            /// </summary>
            Unknow = 0,


            //(錯誤代號:1000)資料庫綜合錯誤
            /// <summary>
            /// 資料庫讀取錯誤。
            /// </summary>
            DbGetFailure =                  1020,
            /// <summary>
            /// 資料庫寫入錯誤。
            /// </summary>
            DbSetFailure =                  1030,
            /// <summary>
            /// 資料庫寫入SQL行為錯誤。
            /// </summary>
            DbSetSqlOperationFail =         1031,

            //(錯誤代號:1100)資料庫物件未找到
            /// <summary>
            /// 帳號未找到。
            /// </summary>
            UIDnotFound =                   1101,
            /// <summary>
            /// 文章未找到。
            /// </summary>
            AIDnotFound =                   1102,
            /// <summary>
            /// 留言未找到。
            /// </summary>
            MIDnotFound =                   1103,
            /// <summary>
            /// 廣告未找到。
            /// </summary>
            DIDnotFound =                   1104,
            /// <summary>
            /// 團體未找到。
            /// </summary>
            GIDnotFound =                   1105,

            //(錯誤代號:1200)資料庫資料解析錯誤(資料格式錯誤)
            /// <summary>
            /// 日期資料解析錯誤。
            /// </summary>
            AnlTypeErrDatetime =            1201,
            /// <summary>
            /// 字串陣列資料解析錯誤。
            /// </summary>
            AnlTypeErrListOfString =        1202,
            /// <summary>
            /// 看板版主對陣列資料解析錯誤。
            /// </summary>
            AnlTypeErrListOfBoardAdmin =    1203,
            /// <summary>
            /// 圖片資料解析錯誤。
            /// </summary>
            AnlTypeErrImage =               1204,
            /// <summary>
            /// 尺寸資料解析錯誤。
            /// </summary>
            AnlTypeErrSize =                1205,

            //(錯誤代號:1300)將資料列寫入物件執行個體錯誤
            /// <summary>
            /// 使用者執行個體建立失敗。
            /// </summary>
            SetFiledFailUser =              1301,
            /// <summary>
            /// 文章執行個體建立失敗。
            /// </summary>
            SetFiledFailArticle =           1302,
            /// <summary>
            /// 留言執行個體建立失敗。
            /// </summary>
            SetFiledFailAMessage =          1303,
            /// <summary>
            /// 廣告執行個體建立失敗。
            /// </summary>
            SetFiledFailAdvertise =         1304,
            /// <summary>
            /// 班級團體執行個體建立失敗。
            /// </summary>
            SetFiledFailClassGroup =        1305,
            /// <summary>
            /// 家族團體執行個體建立失敗。
            /// </summary>
            SetFiledFailFamilyGroup =       1306,


            //(錯誤代號:2000)資料模型綜合錯誤
            /// <summary>
            /// 創立團體失敗。
            /// </summary>
            CreateGroupFail =               2001,
            /// <summary>
            /// 無效帳號資料。
            /// </summary>
            InvalidUserInformation =        2002,
            /// <summary>
            /// 嘗試加入團體失敗。
            /// </summary>
            JoinGroupFail =                 2003,
            /// <summary>
            /// 頁面資料格式錯誤。
            /// </summary>
            PageDataFromatError =           2004,
            /// <summary>
            /// 抽卡交友抽卡迭代次數達上限，抽卡失敗。
            /// </summary>
            PickCardFailed =                2005,


            //(錯誤代號:3000)資料結構錯誤
            /// <summary>
            /// 朋友圈成員操作錯誤。
            /// </summary>
            FriendMemberOperationError =    3001,
            /// <summary>
            /// 加入朋友錯誤。
            /// </summary>
            AddFriendFail =                 3002

        }
        /// <summary>
        /// 此例外狀況的錯誤代號。
        /// </summary>
        public Error ErrorNumber { get; private set; }

        public ModelException() : base("資料模型發生未知錯誤。")
        {
            userMessage = "發生未知錯誤。";
            ErrorNumber = Error.Unknow;
        }

        public ModelException(Error enm, string message, string umessage) : base(message)
        {
            ErrorNumber = enm;
            userMessage = umessage;
        }
    }

    /// <summary>
    /// 代表狀態機的各個狀態。
    /// </summary>
    public enum StateEnum
    {
        /// <summary>
        /// 代表註冊頁面。
        /// </summary>
        Register,
        /// <summary>
        /// 代表登入頁面。
        /// </summary>
        Login,
        /// <summary>
        /// 代表首頁(動態頁)。
        /// </summary>
        Home,
        /// <summary>
        /// 代表使用者頁面。
        /// </summary>
        UserPage,
        /// <summary>
        /// 代表班級首頁或家族首頁。
        /// </summary>
        Group,
        /// <summary>
        /// 代表設定頁面。
        /// </summary>
        Setting,
        /// <summary>
        /// 代表瀏覽文章頁面。
        /// </summary>
        Article,
        /// <summary>
        /// 代表瀏覽看板頁面。
        /// </summary>
        Board,
        /// <summary>
        /// 代表編輯文章頁面。
        /// </summary>
        EditArticle
    }

    /// <summary>
    /// 資料模型。
    /// </summary>
    public class Model
    {
        /// <summary>
        /// 取得目前的狀態。
        /// </summary>
        public StateEnum State { get; set; }
        /// <summary>
        /// 取得顯示。
        /// </summary>
        public View view { get; set; }
        /// <summary>
        /// 當前頁面的使用者。
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 資料模型所使用的資料庫服務。
        /// </summary>
        public static SqlServ DB { get; set; }



        /// <summary>
        /// 註冊帳號。註冊成功時回傳空字串。否則回傳錯誤訊息。
        /// </summary>
        /// <param name="p_ID">帳號。</param>
        /// <param name="p_Password">密碼。</param>
        /// <param name="p_Email">電子郵件。</param>
        /// <param name="p_ClassName">班級系級。</param>
        /// <param name="p_StudentNum">學號。</param>
        /// <param name="p_Name">姓名。</param>
        public void Register(UserInfo p_uif)
        {
            //以必要的帳號資料(帳號、密碼、電子郵件、班級、學號、姓名)註冊新的使用者帳號，檢測資料是否符合格式與條件。

            User.ValidUserInfo(p_uif);

            User temp = new User();
            temp.Userinfo = p_uif;
            DB.Set<User>(temp);
        }
        /// <summary>
        /// 登入帳號。成功登入時回傳空字串。否則回傳登入錯誤的資訊。
        /// </summary>
        /// <param name="p_ID">帳號。</param>
        /// <param name="p_Password">密碼。</param>
        /// <returns>回傳登入錯誤的資訊。成功登入時回傳空字串。</returns>
        public string Login(string p_ID, string p_Password)
        {
            try
            {
                user = DB.Get<User>(DB.UserID_UIDconvert(p_ID));
                if (user == null)
                    return "發生未知錯誤，登入失敗。";
                else if (!user.Userinfo.Password.Equals(p_Password))
                {
                    user = null;
                    return "密碼錯誤。";
                }
                else
                    return string.Empty;
            }
            catch (Exception e)
            {
                user = null;
                return e.Message;
            }
        }
        /// <summary>
        /// 設定使用者資訊與使用者設定。
        /// </summary>
        /// <param name="_uif">使用者資訊。</param>
        /// <param name="_ust">使用者設定。</param>
        public void SetUserSetting(UserInfo p_uif, UserSetting p_ust)
        {
            User.ValidUserInfo(p_uif);

            user.Userinfo = p_uif;
            user.Usersetting = p_ust;
            DB.Set<User>(user);
        }
        /// <summary>
        /// 創立班級團體。
        /// </summary>
        /// <param name="p_ClassName">班級。</param>
        /// <param name="p_GroupName">團體名稱。</param>
        public void CreateClass(string p_ClassName, string p_GroupName)
        {
            if (DB.IsExist(DB.DB_ClassGroupData_TableName, "ClassName", p_ClassName))
                throw new ModelException(
                    ModelException.Error.CreateGroupFail,
                    "Model類別－CreateClass()發生例外：此班級已存在。",
                    "班級已存在");
            else if (DB.IsExist(DB.DB_ClassGroupData_TableName, "GroupName", p_GroupName))
                throw new ModelException(
                    ModelException.Error.CreateGroupFail,
                    "Model類別－CreateClass()發生例外：班級團體名稱已被使用。",
                    "班級團體名稱已被使用");
            else if (user.Userinfo.ClassName != p_ClassName)
                throw new ModelException(
                    ModelException.Error.CreateGroupFail,
                    "Model類別－CreateClass()發生例外：不得創立非自己班級的班級團體。",
                    "不得創立非自己班級的班級團體。");
            else
            {
                ClassGroup cg = new ClassGroup();
                cg.ClassName = p_ClassName;
                cg.Groupname = p_GroupName;

                cg.Admin.Add(user.Userinfo.UID);
                cg.Members_AllowAdd(user);
            }
        }
        /// <summary>
        /// 創立家族團體。
        /// </summary>
        /// <param name="p_GroupName">團體名稱。</param>
        public void CreateFamily(string p_GroupName)
        {
            if (DB.IsExist(DB.DB_FamilyGroupData_TableName, "GroupName", p_GroupName))
                throw new ModelException(
                    ModelException.Error.CreateGroupFail,
                    "Model類別－CreateFamily()發生例外：家族團體名稱已被使用。",
                    "家族團體名稱已被使用");
            else
            {
                FamilyGroup fg = new FamilyGroup();
                fg.Groupname = p_GroupName;

                fg.Admin.Add(user.Userinfo.UID);
                fg.Members_AllowAdd(user);
            }
        }
        /// <summary>
        /// 使用者發布文章。
        /// </summary>
        /// <param name="p_Title">文章標題。</param>
        /// <param name="p_Content">文章內容。</param>
        /// <param name="p_OfGroup">文章所屬的團體。</param>
        /// <param name="p_OfBoard">文章所屬的看板。</param>
        public void ReleaseArticle(string p_Title, string p_Content, string p_OfGroup, string p_OfBoard)
        {
            Article atc = new Article(user.Userinfo.UID, p_OfGroup, p_OfBoard);
            atc.Content = p_Content;
            atc.Title = p_Title;

            DB.Set<Article>(atc);
        }
        /// <summary>
        /// 使用者留言。
        /// </summary>
        /// <param name="p_Message">留言內容。</param>
        /// <param name="p_OfArticle">留言所屬文章。</param>
        public void ReleaseAMessage(string p_Message, string p_OfArticle)
        {
            AMessage ame = new AMessage(user.Userinfo.UID, p_OfArticle);
            ame.Content = p_Message;
            DB.Set<AMessage>(ame);
        }

        /// <summary>
        /// 取得特定的團體的文章。
        /// </summary>
        /// <param name="p_Group">團體識別碼。</param>
        /// <returns></returns>
        public List<Article> GetArticlesFromGroup(string p_Group)
        {
            List<Article> rtn;
            using (DataTable dt = DB.GetSqlData("SELECT * FROM " + DB.DB_ArticleData_TableName))
            {
                rtn = Enumerable.Cast<DataRow>(dt.Rows).Where(x => (string)x["OfGroup"] == p_Group)
                    .Select(y => new Article(y)).ToList();
            }
            rtn.Sort(
                delegate (Article a1, Article a2)
                {
                    return -DateTime.Compare(a1.Date, a2.Date);
                });
            return rtn;
        }
        /// <summary>
        /// 取得特定看板的文章。
        /// </summary>
        /// <param name="p_Group">團體識別碼。</param>
        /// <param name="p_Board">看板名稱。</param>
        /// <returns></returns>
        public List<Article> GetArticlesFromBoard(string p_Group, string p_Board)
        {
            List<Article> rtn;
            using (DataTable dt = DB.GetSqlData("SELECT * FROM " + DB.DB_ArticleData_TableName))
            {
                rtn = Enumerable.Cast<DataRow>(dt.Rows).Where(x => (string)x["OfGroup"] == p_Group && 
                (string)x["OfBoard"] == p_Board).Select(y => new Article(y)).ToList();
            }
            rtn.Sort(
                delegate (Article a1, Article a2)
                {
                    return -DateTime.Compare(a1.Date, a2.Date);
                });
            return rtn;
        }
        /// <summary>
        /// 取得目前使用者的動態頁面的內容，內容為該使用者的好友之發文與留言物件。
        /// </summary>
        /// <returns></returns>
        public List<object> GetDynamicPageContent()
        {
            List<Article> dr1;
            using (DataTable dt = DB.GetSqlData("SELECT * FROM " + DB.DB_ArticleData_TableName))
            {
                dr1 = Enumerable.Cast<DataRow>(dt.Rows).Where(
                    x => user.Friends.Members.Contains(x["ReleaseUser"])).Select(y => new Article(y)).ToList();
            }
            List<AMessage> dr2;
            using (DataTable dt = DB.GetSqlData("SELECT * FROM " + DB.DB_AMessageData_TableName))
            {
                dr2 = Enumerable.Cast<DataRow>(dt.Rows).Where(
                    x => user.Friends.Members.Contains(x["ReleaseUser"])).Select(y => new AMessage(y)).ToList();
            }


            List<object> rtn = dr1.Concat<object>(dr2).ToList();
            rtn.Sort(
                delegate (object dt1, object dt2)
                {
                    return -DateTime.Compare(
                          dt1 is Article ? (dt1 as Article).Date : (dt1 as AMessage).Date
                        , dt2 is Article ? (dt2 as Article).Date : (dt2 as AMessage).Date);
                });

            return rtn;
        }
        /// <summary>
        /// 取得指定使用者的使用者頁面主要內容。
        /// </summary>
        /// <param name="uid">指定使用者識別碼。</param>
        /// <returns></returns>
        public List<object> GetUserPageContent(string uid)
        {
            List<Article> dr1;
            using (DataTable dt = DB.GetSqlData(string.Format("SELECT * FROM " + DB.DB_ArticleData_TableName + 
                " WHERE ReleaseUser = '{0}'", uid)))
            {
                dr1 = Enumerable.Cast<DataRow>(dt.Rows).Select(y => new Article(y)).ToList();
            }
            List<AMessage> dr2;
            using (DataTable dt = DB.GetSqlData(string.Format("SELECT * FROM " + DB.DB_AMessageData_TableName +
                " WHERE ReleaseUser = '{0}'", uid)))
            {
                dr2 = Enumerable.Cast<DataRow>(dt.Rows).Select(y => new AMessage(y)).ToList();
            }


            List<object> rtn = dr1.Concat<object>(dr2).ToList();
            rtn.Sort(
                delegate (object dt1, object dt2)
                {
                    return -DateTime.Compare(
                          dt1 is Article ? (dt1 as Article).Date : (dt1 as AMessage).Date
                        , dt2 is Article ? (dt2 as Article).Date : (dt2 as AMessage).Date);
                });

            return rtn;
        }

        public bool IsAdmin(string Family_GID, string UID)
        {
            return string.Concat(DB.Get<FamilyGroup>(Family_GID).Members.Select
                ((x, indx) => indx == 0 ? x : "," + x)).Contains(UID);
        }

        /// <summary>
        /// 在切換頁面時，向Model要求新的頁面資料。
        /// </summary>
        /// <param name="ToState">要前往的狀態。</param>
        /// <returns></returns>
        public PageData RequestPageData(StateEnum ToState)
        {
            if (ToState == StateEnum.Home)
            {
                PageData.Out.SetData(
                    delegate ()
                    {
                        PageData.Out["Content"] = GetDynamicPageContent();
                        PageData.Out["User"] = user;
                    });
            }
            else if (ToState == StateEnum.UserPage)
            {
                PageData.Out.SetData(
                    delegate ()
                    {
                        PageData.Out["Content"] = GetUserPageContent((string)PageData.In["UserUID"]);
                    });
            }
            else if (ToState == StateEnum.Group)
            {
                PageData.Out.SetData(
                    delegate ()
                    {
                        PageData.Out["Content"] = GetArticlesFromGroup((string)PageData.In["GID"]);
                    });
            }
            else if (ToState == StateEnum.Board)
            {
                PageData.Out.SetData(
                    delegate ()
                    {
                        PageData.Out["Content"] = GetArticlesFromBoard
                            ((string)PageData.In["GID"], (string)PageData.In["Board"]);
                    });
            }
            else if (ToState == StateEnum.Article)
            {
                PageData.Out.SetData(
                    delegate ()
                    {
                        PageData.Out["Content"] = DB.Get<Article>((string)PageData.In["AID"]);
                    });
            }
            else if (ToState == StateEnum.EditArticle)
            {

            }
            else if (ToState == StateEnum.Setting)
            {

            }
            else if (ToState == StateEnum.Login)
            {
                PageData.Out.SetData(delegate () { });
            }
            else if (ToState == StateEnum.Register)
            {
                PageData.Out.SetData(delegate () { });
            }

            return PageData.Out;
        }

        public Model(View view)
        {
            this.view = view;

            State = StateEnum.Login;
            user = null;
            DB = new SqlServ_MSSql("");
            PageData.InitPageData();
        }
    }

    /// <summary>
    /// 提供一個靜態欄位，存放切換狀態時，該狀態要求的頁面資料。
    /// </summary>
    public class PageData : Dictionary<string, object>
    {
        /// <summary>
        /// 切換狀態時Control提供給Model的參數資料。
        /// </summary>
        public static PageData In;
        /// <summary>
        /// 切換狀態時View所要求的頁面資料。
        /// </summary>
        public static PageData Out;

        //public StateEnum State { get; set; }

        public static void InitPageData()
        {
            In = new PageData(32);

            Out = new PageData(32);          
        }

        
        /// <summary>
        /// 清空PageData內的資料並設定新的資料。
        /// </summary>
        /// <param name="SettingFunc">設定新資料的委派函數。</param>
        public void SetData(Action SettingFunc)
        {
            Clear();
            try
            {
                //SettingFunc();
            }
            catch (KeyNotFoundException)
            {
                throw new ModelException(
                    ModelException.Error.PageDataFromatError,
                    "PageData類別－SetData(Action)發生例外：未找到需要的參數。\r\n",
                    "");
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.PageDataFromatError,
                    "PageData類別－SetData(Action)發生例外：設定資料錯誤。\r\n" + e.Message, 
                    "");
            }
        }
        

        ~PageData()
        {
            
        }

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

        private PageData() : base() { }

        private PageData(int capacity) : base(capacity) { }

        /* PageDate.In(Control to Model): 切換狀態時Control提供給Model的參數資料。
         * PageData.In的內容根據不同的狀態要求而存放不同資料。
         * 
         *      To "UserPage" State
         *          
         *          PageData.In["UserUID"] = a UID          (type: string)
         *          存放要前往的使用者頁面之使用者的識別碼。
         *          
         *          PageData.In["AdvertiseBlocks"] = a list (type: List<int>)
         *          存放頁面上的廣告區塊代碼。
         *          
         *      To "Group" State
         *      
         *          PageData.In["GroupGID"] = a GID         (type: string)
         * 
         */

        /* PageData.Out(Model to View): View所要求的資料。
         * PageData.Out的內容根據不同的狀態要求而存放不同資料。
         * 
         *      To "Home" State
         *      
         *          Instant["Content"] =  a list  (type: List<object>)
         *          存放要顯示的文章與留言。其內的物件不是Article就是AMessage。
         *          
         *          Instant["User"] =   a object  (type: User)
         *          存放現在使用者的資訊(包含好友與團體資訊User.Friends, User.Groups)
         *          
         *      To "UserPage" State
         *      
         *          PUserPage[""] =     
         *          
         *          
         *      To "Login" || "Register" State
         *      
         *          無。
         * 
         */
    }

    /// <summary>
    /// SQL Service類別提供SQL資料庫的服務。
    /// </summary>
    public abstract class SqlServ
    {
        /// <summary>
        /// 資料庫連線字串。
        /// </summary>
        public string DB_Conn { get; set; }
        /// <summary>
        /// 資料庫－使用者資料表名稱。
        /// </summary>
        public string DB_UserData_TableName { get; set; }
        /// <summary>
        /// 資料庫－班級團體資料表名稱。
        /// </summary>
        public string DB_ClassGroupData_TableName { get; set; }
        /// <summary>
        /// 資料庫－家族團體資料表名稱。
        /// </summary>
        public string DB_FamilyGroupData_TableName { get; set; }
        /// <summary>
        /// 資料庫－文章資料表名稱。
        /// </summary>
        public string DB_ArticleData_TableName { get; set; }
        /// <summary>
        /// 資料庫－留言資料表名稱。
        /// </summary>
        public string DB_AMessageData_TableName { get; set; }
        /// <summary>
        /// 資料庫－廣告資料表名稱。
        /// </summary>
        public string DB_AdvertiseData_TableName { get; set; }


        /// <summary>
        /// 判斷指定欄位有指定值的資料列是否存在於資料庫。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="FiledName">欄位名稱。</param>
        /// <param name="FiledValue">欄位值。</param>
        /// <returns></returns>
        public abstract bool IsExist(string TableName, string FiledName, string FiledValue);
        /// <summary>
        /// 執行SQL存放命令。
        /// </summary>
        /// <param name="SqlCommandString">要執行的命令。</param>
        public abstract void ExeSqlCommand(string SqlCommandString);
        /// <summary>
        /// 執行SQL取用命令，並回傳取得資料表。
        /// </summary>
        /// <param name="SqlCommandString">要執行的命令。</param>
        /// <returns></returns>
        public abstract DataTable GetSqlData(string SqlCommandString);




        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="str">字串格式。</param>
        /// <param name="isCH">是否含中文字元。</param>
        /// <returns></returns>
        public string Type(string str, bool isCH = false)
        {
            if (str == null || str == string.Empty)
                return "''";
            else if (isCH)
                return "N'" + str + "'";
            else
                return "'" + str + "'";
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="gd">性別格式。</param>
        /// <returns></returns>
        public string Type(Gender gd)
        {
            return "" + ((byte)gd);
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="upr">使用者隱私格式。</param>
        /// <returns></returns>
        public string Type(UserPrivacy upr)
        {
            return "" + ((byte)upr);
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_int">整數格式。</param>
        /// <returns></returns>
        public string Type(int p_int)
        {
            return "" + p_int;
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="img">圖片格式。</param>
        /// <returns></returns>
        public string Type(Image img)
        {
            if (img == null) return "0x00";

            return string.Format("CAST({0} AS IMAGE)",
                "0x" + string.Concat(((byte[])new ImageConverter().ConvertTo(img, typeof(byte[]))).Select
                (x => Convert.ToString(x, 16).PadLeft(2, '0'))));
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="ls">字串陣列格式。</param>
        /// <param name="isCH">是否含中文字元。</param>
        /// <returns></returns>
        public string Type(List<string> ls, bool isCH = false)
        {
            if (ls == null || ls.Count <= 0) return "''";

            return (isCH ? "N" : "") + "'" + string.Concat(ls.Select((x, indx) => indx == 0 ? x : "," + x)) + "'";
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="dt">日期格式。</param>
        /// <returns></returns>
        public string Type(DateTime dt, bool onlyDate = false)
        {
            if (dt == DateTime.MinValue)
                return "''";

            if (onlyDate)
                return string.Format("'{0}'", dt.ToString("yyyy-MM-dd 00:00:00"));
            else
                return string.Format("'{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="ls">看板版主資料對陣列格式。</param>
        /// <returns></returns>
        public string Type(List<BoardAdminPair> ls)
        {
            if (ls == null || ls.Count <= 0) return "''";

            return string.Format("N'{0}'", string.Concat(ls.Select((x, indx) => 
                    (indx == 0 ? "" : ",") + x.Admin + "@" + x.Board)));
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="se">狀態機狀態格式。</param>
        /// <returns></returns>
        public string Type(StateEnum se)
        {
            return "" + (byte)se;
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="sz">尺寸格式。</param>
        /// <returns></returns>
        public string Type(Size sz)
        {
            if (sz == Size.Empty) return "''";

            return "'" + sz.Width + "@" + sz.Height + "'";
        }
        /// <summary>
        /// 將指定型別資料解析為SQL字串的表示方式。
        /// </summary>
        /// <param name="ls">使用者陣列格式。</param>
        /// <returns></returns>
        public string Type(List<User> ls)
        {
            return Type(ls.Select(x => x.Userinfo.UID).ToList());
        }
        /// <summary>
        /// 將User的帳號與帳號識別碼進行轉換。
        /// </summary>
        /// <param name="ipt">輸入</param>
        /// <param name="foword">將帳號轉換為識別碼(true)或是將識別碼轉換為帳號(false)。</param>
        /// <returns></returns>
        public string UserID_UIDconvert(string ipt, bool foword = true)
        {
            DataTable dt = GetSqlData(string.Format("SELECT UID, ID FROM " + DB_UserData_TableName +
                " WHERE {0} = '{1}'", foword ? "ID" : "UID", ipt));

            if (dt.Rows.Count <= 0) throw new ModelException(
                ModelException.Error.UIDnotFound,
                "SqlServ類別－UserID_UIDconvert" + "發生錯誤：指定的帳號不存在。",
                "帳號不存在");

            return (string)AnlType<string>(dt.Rows[0][foword ? "UID" : "ID"]);
        }
        
        /// <summary>
        /// 反解析資料庫讀取的資料，或將空值轉換為對應型別之空值。
        /// </summary>
        /// <typeparam name="T">指定輸出型別。</typeparam>
        /// <param name="obj">從資料庫讀取的資料。</param>
        /// <returns></returns>
        public object AnlType<T>(object obj)
        {
            if (typeof(T).Equals(typeof(DateTime)))
            {
                if (obj is DBNull || obj == null || (string)obj == "") return DateTime.MinValue;

                try
                {
                    List<string> s1 = ((string)obj).Split(' ').ToList();
                    s1 = Enumerable.Concat(s1[0].Split('-'), s1[1].Split(':')).ToList();
                    List<int> s2 = s1.Select(x => int.Parse(x)).ToList();
                    return new DateTime(s2[0], s2[1], s2[2], s2[3], s2[4], s2[5]);
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.AnlTypeErrDatetime,
                        "SqlServ類別－AnlType<Datetime>發生錯誤：資料庫內日期資料不符合格式" +
                        "[yyyy-MM-dd hh:mm:ss]\r\n" + e.Message
                        , "");
                }
            }
            else if (typeof(T).Equals(typeof(List<string>)))
            {
                if (obj is DBNull || obj == null || (string)obj == "") return new List<string>();

                try
                {
                    return ((string)obj).Split(',').ToList();
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.AnlTypeErrListOfString,
                        "SqlServ類別－AnlType<List<string>>發生例外：資料庫內字串陣列" +
                        "資料格式錯誤無法解析！\r\n" + e.Message, "");
                }
            }
            else if (typeof(T).Equals(typeof(List<User>)))
            {
                return ((List<string>)AnlType<List<string>>(obj)).Select(x => Model.DB.Get<User>(x)).ToList();
            }
            else if(typeof(T).Equals(typeof(List<BoardAdminPair>)))
            {
                if (obj is DBNull || obj == null || (string)obj == "") return new List<BoardAdminPair>();

                try
                {
                    Func<string, BoardAdminPair> f = delegate (string s)
                    {
                        string[] temp = s.Split('@');
                        return new BoardAdminPair(temp[0], temp[1]);
                    };

                    return ((string)obj).Split(',').Select(x => f(x)).ToList();
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.AnlTypeErrListOfBoardAdmin,
                        "SqlServ類別－AnlType<BoardAdminPair>發生例外：" +
                        "看板版主資料對資料格式錯誤無法解析。\r\n" + e.Message, "");
                }
            }
            else if (typeof(T).Equals(typeof(Image)))
            {
                Func<List<byte>, List<byte>, bool> ListEqual = delegate(List<byte> ls1, List<byte> ls2) 
                {
                    if (ls1 == null || ls2 == null) return false;

                    if (ls1.Count != ls2.Count) return false;
                                        
                    return ls1.Select((x, indx) => x == ls2[indx]).Where(x => !x).Count() == 0;
                };

                if (obj is DBNull || obj == null || ListEqual(((byte[])obj).ToList(), new List<byte>() { 0 })) return null;

                try
                {
                    return (Image)(new ImageConverter()).ConvertFrom((byte[])obj);
                }
                catch(Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.AnlTypeErrImage,
                        "SqlServ類別－AnlType<Image>發生例外：圖片格式轉換錯誤:" + e.Message, 
                        "");
                }
            }
            else if (typeof(T).Equals(typeof(int)))
            {
                if (obj is DBNull || obj == null) return -1;

                return (int)obj;
            }
            else if (typeof(T).Equals(typeof(Gender)))
            {
                if (obj is DBNull || obj == null) return Gender.Null;

                return (Gender)(byte)obj;
            }
            else if (typeof(T).Equals(typeof(UserPrivacy)))
            {
                if (obj is DBNull || obj == null) return UserPrivacy.Public;

                return (UserPrivacy)(byte)obj;
            }
            else if (typeof(T).Equals(typeof(string)))
            {
                if (obj is DBNull || obj == null || (string)obj == "") return null;

                return (string)obj;
            }
            else if (typeof(T).Equals(typeof(StateEnum)))
            {
                if (obj is DBNull || obj == null) return StateEnum.Home;

                return (StateEnum)(byte)obj;
            }
            else if (typeof(T).Equals(typeof(Size)))
            {
                if (obj is DBNull || obj == null || (string)obj == "") return Size.Empty;

                try
                {
                    int[] temp = ((string)obj).Split('@').Select(x => int.Parse(x)).ToArray();
                    return new Size(temp[0], temp[1]);
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.AnlTypeErrSize,
                        "SqlServ類別－AnlType<Size>發生錯誤：資料庫內尺寸資料格式錯誤無法解析！\r\n" + e.Message, 
                        "");
                }
            }
            else
            {
                if (obj is DBNull) return null;
                return obj;
            }
        }


        /// <summary>
        /// 依識別碼取得指定型別的物件資料。未找到則擲回例外。
        /// </summary>
        /// <typeparam name="T">指定的型別（User、Article、AMessage、ClassGroup、FamilyGroup）。若非特定型別則擲回例外。</typeparam>
        /// <param name="Iden">識別碼。</param>
        /// <returns></returns>
        public T Get<T>(string Iden)
        {
            return (T)Get_workshop<T>(Iden);
        }
        /// <summary>
        /// (工作函數)依識別碼取得指定型別的物件資料。未找到則擲回例外。
        /// </summary>
        /// <typeparam name="T">指定的型別（User、Article、AMessage、ClassGroup、FamilyGroup）。若非特定型別則擲回例外。</typeparam>
        /// <param name="Iden">識別碼。</param>
        /// <returns></returns>
        private object Get_workshop<T>(string Iden)
        {
            if (typeof(T).Equals(typeof(User)))
            {
                //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE UID = {1}"
                    , DB_UserData_TableName, Iden));

                if (dt.Rows.Count <= 0)
                {
                    throw new ModelException(
                        ModelException.Error.UIDnotFound,
                        "無此帳號。UID = " + Iden, 
                        "無此帳號");
                }
                else
                {
                    User rtn = new User(dt.Rows[0]);
                    return rtn;
                }
            }
            else if (typeof(T).Equals(typeof(Article)))
            {
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE AID = {1}",
                    DB_ArticleData_TableName, Iden));

                if (dt.Rows.Count == 0)
                    throw new ModelException(
                        ModelException.Error.AIDnotFound,
                        "無此文章。AID = " + Iden, 
                        "無此文章");
                else
                    return new Article(dt.Rows[0]);
            }
            else if (typeof(T).Equals(typeof(AMessage)))
            {
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE MID = {1}",
                    DB_AMessageData_TableName, Iden));

                if (dt.Rows.Count == 0)
                    throw new ModelException(
                        ModelException.Error.MIDnotFound,
                        "無此留言。MID = " + Iden, 
                        "無此留言");
                else
                    return new AMessage(dt.Rows[0]);
            }
            else if (typeof(T).Equals(typeof(ClassGroup)))
            {
                //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE GID = {1}"
                    , DB_ClassGroupData_TableName, Iden));

                if (dt.Rows.Count == 0)
                    throw new ModelException(
                        ModelException.Error.GIDnotFound,
                        "無此班級。GID : " + Iden, 
                        "無此班級");
                else
                    return new ClassGroup(dt.Rows[0]);
            }
            else if (typeof(T).Equals(typeof(FamilyGroup)))
            {
                //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE GID = {1}"
                    , DB_FamilyGroupData_TableName, Iden));

                if (dt.Rows.Count == 0)
                    throw new ModelException(
                        ModelException.Error.GIDnotFound,
                        "無此家族。GID : " + Iden, 
                        "無此家族");
                else
                    return new FamilyGroup(dt.Rows[0]);
            }
            else if (typeof(T).Equals(typeof(Advertise)))
            {
                //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
                DataTable dt = GetSqlData(string.Format("SELECT * FROM {0} WHERE DID = {1}"
                    , DB_AdvertiseData_TableName, Iden));

                if (dt.Rows.Count <= 0)
                {
                    throw new ModelException(
                        ModelException.Error.DIDnotFound,
                        "無此廣告。DID = " + Iden, 
                        "無此廣告");
                }
                else
                {
                    Advertise rtn = Advertise.Instance(dt.Rows[0]);
                    return rtn;
                }
            }
            else
            {
                throw new ModelException(
                    ModelException.Error.DbGetFailure,
                    "SqlServ類別－Get<T>發生錯誤：要求非特定型別的物件。\r\nclass : " + typeof(T)
                    , "");
            }
        }
        /// <summary>
        /// 將特定型別的物件存入資料庫。
        /// </summary>
        /// <typeparam name="T">指定型別。</typeparam>
        /// <param name="obj">物件。</param>
        public void Set<T>(object obj)
        {
            if (!(obj is T))
                throw new ModelException(
                    ModelException.Error.DbSetFailure,
                    "SqlServ類別－Set<T>發生錯誤：物件參數與指定型別不符合。\r\n" +
                    "class: " + typeof(T) + "\r\n" +
                    "Object class: " + obj.GetType(), 
                    "");

            if (typeof(T).Equals(typeof(User)))
            {
                try
                {
                    User usr = (User)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (usr.Userinfo.UID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_UserData_TableName + @"
                    SET ID = {0}, 
                    UID = {1}, 
                    Password = {2}, 
                    Email = {3}, 
                    StudentNum = {4}, 
                    ClassName = {5}, 
                    RealName = {6}, 
                    NickName = {7}, 
                    Picture = {8}, 
                    Gender = {9}, 
                    Birthday = {10},
                    UserPrivacy = {11}, 
                    Friend = {12}, 
                    ClassGroup = {13}, 
                    FamilyGroup = {14}, 
                    TbitCoin = {15},
                    FriendRequest = {16},
                    LastComputeTbit = {17} 
                    WHERE UID = {1}"
                            , Type(usr.Userinfo.ID)
                            , Type(usr.Userinfo.UID)
                            , Type(usr.Userinfo.Password)
                            , Type(usr.Userinfo.Email)
                            , Type(usr.Userinfo.StudentID)
                            , Type(usr.Userinfo.ClassName, true)
                            , Type(usr.Userinfo.Realname, true)
                            , Type(usr.Userinfo.Nickname, true)
                            , Type(usr.Userinfo.Picture)
                            , Type(usr.Userinfo.Gender)
                            , Type(usr.Userinfo.Birthday, true)
                            , Type(usr.Usersetting.Userprivacy)
                            , Type(usr.Friends.Members)
                            , Type(usr.Groups.Where(x => x is ClassGroup).Select(x => x.GID).ToList())
                            , Type(usr.Groups.Where(x => x is FamilyGroup).Select(x => x.GID).ToList())
                            , Type(usr.TbitCoin)
                            , Type(usr.FriendRequestQueue)
                            , Type(usr.LastComputeTbit)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextuid = (string)GetSqlData(@"SELECT MAX(UID) FROM " + DB_UserData_TableName).Rows[0][0];

                        usr.Userinfo.UID = nextuid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET UID = '{1}' WHERE UID = '{2}'"
                            , DB_UserData_TableName, (int.Parse(nextuid) + 1).ToString().PadLeft(10, '0'), nextuid));

                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_UserData_TableName + @" 
        (ID, UID, Password, Email, StudentNum, ClassName, RealName, NickName, Picture, Gender, Birthday, UserPrivacy, Friend
		, ClassGroup, FamilyGroup, TbitCoin, FriendRequest, LastComputeTbit) 
                    VALUES 
        ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17})"
                            , Type(usr.Userinfo.ID)
                            , Type(usr.Userinfo.UID)
                            , Type(usr.Userinfo.Password)
                            , Type(usr.Userinfo.Email)
                            , Type(usr.Userinfo.StudentID)
                            , Type(usr.Userinfo.ClassName, true)
                            , Type(usr.Userinfo.Realname, true)
                            , Type(usr.Userinfo.Nickname, true)
                            , Type(usr.Userinfo.Picture)
                            , Type(usr.Userinfo.Gender)
                            , Type(usr.Userinfo.Birthday, true)
                            , Type(usr.Usersetting.Userprivacy)
                            , Type(usr.Friends.Members)
                            , Type(usr.Groups.Where(x => x is ClassGroup).Select(x => x.GID).ToList())
                            , Type(usr.Groups.Where(x => x is FamilyGroup).Select(x => x.GID).ToList())
                            , Type(usr.TbitCoin)
                            , Type(usr.FriendRequestQueue)
                            , Type(usr.LastComputeTbit)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail, 
                        "SqlServ類別－Set<T>發生錯誤：User設定物件欄位錯誤。\r\n" + e.Message, 
                        "發生未知錯誤－儲存失敗");
                }
            }
            else if (typeof(T).Equals(typeof(Article)))
            {
                try
                {
                    Article p_art = (Article)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (p_art.AID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_ArticleData_TableName + @"
                    SET AID = {0}, 
                    Title = {1}, 
                    Content = {2}, 
                    ReleaseUser = {3}, 
                    ReleaseDate = {4}, 
                    LikeCount = {5}, 
                    OfGroup = {6}, 
                    OfBoard = {7}, 
                    TbitLikeCount = {8} 
                    WHERE AID = {0}"
                            , Type(p_art.AID)
                            , Type(p_art.Title, true)
                            , Type(p_art.Content, true)
                            , Type(p_art.ReleaseUser)
                            , Type(p_art.Date)
                            , Type(p_art.LikeCount)
                            , Type(p_art.OfGroup)
                            , Type(p_art.OfBoard, true)
                            , Type(p_art.LastComputeTbitLikeCount)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextaid = (string)GetSqlData("SELECT MAX(AID) FROM " + DB_ArticleData_TableName).Rows[0][0];

                        p_art.AID = nextaid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET AID = '{1}' WHERE AID = '{2}'"
                            , DB_ArticleData_TableName, (int.Parse(nextaid) + 1).ToString().PadLeft(10, '0'), nextaid));

                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_ArticleData_TableName + @" 
                    (AID, Title, Content, ReleaseUser, ReleaseDate, LikeCount, OfGroup, OfBoard, TbitLikeCount)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})"
                            , Type(p_art.AID)
                            , Type(p_art.Title, true)
                            , Type(p_art.Content, true)
                            , Type(p_art.ReleaseUser)
                            , Type(p_art.Date)
                            , Type(p_art.LikeCount)
                            , Type(p_art.OfGroup)
                            , Type(p_art.OfBoard, true)
                            , Type(p_art.LastComputeTbitLikeCount)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail,
                        "SqlServ類別－Set<T>發生錯誤：Article設定物件欄位錯誤。\r\n" + e.Message, 
                        "發生未知錯誤－儲存失敗");
                }
            }
            else if (typeof(T).Equals(typeof(AMessage)))
            {
                try
                {
                    AMessage p_ame = (AMessage)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (p_ame.MID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_AMessageData_TableName + @"
                    SET MID = {0}, 
                    ReleaseUser = {1}, 
                    ReleaseDate = {2}, 
                    Content = {3}, 
                    LikeCount = {4}, 
                    OfArticle = {5},
                    TbitLikeCount = {6} 
                    WHERE MID = {0}"
                            , Type(p_ame.MID)
                            , Type(p_ame.ReleaseUser)
                            , Type(p_ame.Date)
                            , Type(p_ame.Content, true)
                            , Type(p_ame.LikeCount)
                            , Type(p_ame.OfArticle)
                            , Type(p_ame.LastComputeTbitLikeCount)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextmid = (string)GetSqlData("SELECT MAX(MID) FROM " + DB_AMessageData_TableName).Rows[0][0];

                        p_ame.MID = nextmid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET MID = '{1}' WHERE MID = '{2}'"
                            , DB_AMessageData_TableName, (int.Parse(nextmid) + 1).ToString().PadLeft(10, '0'), nextmid));


                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_AMessageData_TableName + @" 
                    (MID, ReleaseUser, ReleaseDate, Content, LikeCount, OfArticle, TbitLikeCount)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6})"
                            , Type(p_ame.MID)
                            , Type(p_ame.ReleaseUser)
                            , Type(p_ame.Date)
                            , Type(p_ame.Content, true)
                            , Type(p_ame.LikeCount)
                            , Type(p_ame.OfArticle)
                            , Type(p_ame.LastComputeTbitLikeCount)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail,
                        "SqlServ類別－Set<T>發生錯誤：AMessage設定物件欄位錯誤。\r\n" + e.Message, 
                        "發生未知錯誤－儲存失敗");
                }
            }
            else if (typeof(T).Equals(typeof(ClassGroup)))
            {
                try
                {
                    ClassGroup p_cg = (ClassGroup)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (p_cg.GID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_ClassGroupData_TableName + @"
                    SET GID = {0}, 
                    GroupName = {1}, 
                    ClassName = {2}, 
                    Members = {3}, 
                    Admin = {4}, 
                    BoardAdmin = {5}, 
                    Topic = {6},
                    MemberRequest = {7} 
                    WHERE GID = {0}"
                            , Type(p_cg.GID)
                            , Type(p_cg.Groupname, true)
                            , Type(p_cg.ClassName, true)
                            , Type(p_cg.Members)
                            , Type(p_cg.Admin)
                            , Type(p_cg.BoardAdmin)
                            , Type(p_cg.Topic, true)
                            , Type(p_cg.MemberRequestQueue)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextgid = (string)GetSqlData("SELECT MAX(GID) FROM " + DB_ClassGroupData_TableName).Rows[0][0];

                        p_cg.GID = nextgid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET GID = '{1}' WHERE GID = '{2}'"
                            , DB_ClassGroupData_TableName, (int.Parse(nextgid) + 1).ToString().PadLeft(10, '0'), nextgid));


                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_ClassGroupData_TableName + @" 
                    (GID, GroupName, ClassName, Members, Admin, BoardAdmin, Topic, MemberRequest)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})"
                            , Type(p_cg.GID)
                            , Type(p_cg.Groupname, true)
                            , Type(p_cg.ClassName, true)
                            , Type(p_cg.Members)
                            , Type(p_cg.Admin)
                            , Type(p_cg.BoardAdmin)
                            , Type(p_cg.Topic, true)
                            , Type(p_cg.MemberRequestQueue)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail,
                        "SqlServ類別－Set<T>發生錯誤：ClassGroup設定物件欄位錯誤。\r\n" + e.Message, 
                        "發生未知錯誤－儲存失敗");
                }
            }
            else if (typeof(T).Equals(typeof(FamilyGroup)))
            {
                try
                {
                    FamilyGroup p_cg = (FamilyGroup)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (p_cg.GID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_FamilyGroupData_TableName + @"
                    SET GID = {0}, 
                    GroupName = {1}, 
                    Members = {2}, 
                    Admin = {3}, 
                    BoardAdmin = {4}, 
                    Topic = {5},
                    MemberRequest = {6} 
                    WHERE GID = {0}"
                            , Type(p_cg.GID)
                            , Type(p_cg.Groupname, true)
                            , Type(p_cg.Members)
                            , Type(p_cg.Admin)
                            , Type(p_cg.BoardAdmin)
                            , Type(p_cg.Topic, true)
                            , Type(p_cg.MemberRequestQueue)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextgid = (string)GetSqlData("SELECT MAX(GID) FROM " + DB_FamilyGroupData_TableName).Rows[0][0];

                        p_cg.GID = nextgid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET GID = '{1}' WHERE GID = '{2}'"
                            , DB_FamilyGroupData_TableName, (int.Parse(nextgid) + 1).ToString().PadLeft(10, '0'), nextgid));


                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_FamilyGroupData_TableName + @" 
                    (GID, GroupName, Members, Admin, BoardAdmin, Topic, MemberRequest)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6})"
                            , Type(p_cg.GID)
                            , Type(p_cg.Groupname, true)
                            , Type(p_cg.Members)
                            , Type(p_cg.Admin)
                            , Type(p_cg.BoardAdmin)
                            , Type(p_cg.Topic, true)
                            , Type(p_cg.MemberRequestQueue)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail,
                        "SqlServ類別－Set<T>發生錯誤：FamilyGroup設定物件欄位錯誤。\r\n" + e.Message, 
                        "發生未知錯誤－儲存失敗");
                }
            }
            else if (typeof(T).Equals(typeof(Advertise)))
            {
                try
                {
                    Advertise p_ad = (Advertise)obj;

                    //若帳號已存在，為修改帳號資料的更新。
                    if (p_ad.DID != null)
                    {
                        ExeSqlCommand(string.Format(@"UPDATE " + DB_AdvertiseData_TableName + @"
                            SET DID = {0}, 
                            Body = {1}, 
                            Location = {2}, 
                            Size = {3}, 
                            Deadline = {4} 
                            WHERE DID = {0}"
                            , Type(p_ad.DID)
                            , Type(p_ad.Body)
                            , Type(p_ad.Location)
                            , Type(p_ad.Size)
                            , Type(p_ad.Deadline)));
                    }
                    else//否則為新增帳號資料的更新。
                    {
                        string nextdid = (string)GetSqlData("SELECT MAX(DID) FROM " + DB_AdvertiseData_TableName).Rows[0][0];

                        p_ad.DID = nextdid;

                        ExeSqlCommand(string.Format("UPDATE {0} SET DID = '{1}' WHERE DID = '{2}'"
                            , DB_AdvertiseData_TableName, (int.Parse(nextdid) + 1).ToString().PadLeft(10, '0'), nextdid));

                        ExeSqlCommand(string.Format(@"INSERT INTO " + DB_AdvertiseData_TableName + @" 
                            (DID, Body, Location, Size, Deadline)
                            VALUES 
                            ({0}, {1}, {2}, {3}, {4})"
                            , Type(p_ad.DID)
                            , Type(p_ad.Body)
                            , Type(p_ad.Location)
                            , Type(p_ad.Size)
                            , Type(p_ad.Deadline)));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelException(
                        ModelException.Error.DbSetSqlOperationFail,
                        "SqlServ類別－Set<T>發生錯誤：Advertise設定物件欄位錯誤。\r\n" + e.Message,
                        "發生未知錯誤－儲存失敗");
                }
            }
            else
            {
                throw new ModelException(
                    ModelException.Error.DbSetFailure,
                    "SqlServ類別－Set<T>發生錯誤：要求儲存非特定型別的物件。class : " + typeof(T),
                    "");
            }
        }



        protected SqlServ(string p_DBconn)
        {
            DB_Conn = p_DBconn;

            DB_UserData_TableName = "UserData";
            DB_ArticleData_TableName = "ArticleData";
            DB_AMessageData_TableName = "AMessageData";
            DB_AdvertiseData_TableName = "AdvertiseData";
            DB_ClassGroupData_TableName = "ClassGroupData";
            DB_FamilyGroupData_TableName = "FamilyGroupData";
        }
    }
    
    /// <summary>
    /// 實作MySql資料庫的SelService類別。
    /// </summary>
    public class SqlServ_MySql : SqlServ
    {
        /// <summary>
        /// 開啟MySql連線。
        /// </summary>
        /// <param name="Conn">連結字串。</param>
        /// <returns></returns>
        private static MySqlConnection OpenSqlConn(string Conn)
        {
            MySqlConnection icn = new MySqlConnection();
            icn.ConnectionString = Conn;
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Open();
            return icn;
        }
        /// <summary>
        /// 關閉MySQL連線。
        /// </summary>
        /// <param name="icn">要關閉的連線。</param>
        private static void CloseSqlConn(MySqlConnection icn)
        {
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Dispose();
        }


        /// <summary>
        /// 判斷指定欄位有指定值的資料列是否存在於資料庫。
        /// </summary>
        /// <param name="ConnString">資料庫連結字串。</param>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="FiledName">欄位名稱。</param>
        /// <param name="FiledValue">欄位值。</param>
        /// <returns></returns>
        public override bool IsExist(string TableName, string FiledName, string FiledValue)
        {
            bool rtn = false;
            using (MySqlConnection icn = OpenSqlConn(DB_Conn))
            {
                MySqlCommand ism = new MySqlCommand(@"SELECT TOP 1 1 FROM " + TableName + " WHERE " + FiledName
                    + " = " + FiledValue, icn);
                rtn = ism.ExecuteScalar() == null;
                CloseSqlConn(icn);
            }
            return rtn;
        }
        /// <summary>
        /// 執行SQL存放命令。
        /// </summary>
        /// <param name="ConnString">SQL連結字串。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        public override void ExeSqlCommand(string SqlCommandString)
        {
            using (MySqlConnection icn = OpenSqlConn(DB_Conn))
            {
                MySqlCommand cmd = new MySqlCommand(SqlCommandString, icn);
                MySqlTransaction mySqlTransaction = icn.BeginTransaction();
                try
                {
                    cmd.Transaction = mySqlTransaction;
                    cmd.ExecuteNonQuery();
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    mySqlTransaction.Rollback();
                    throw (ex);
                }
                CloseSqlConn(icn);
            }
        }
        /// <summary>
        /// 執行SQL取用命令，並回傳取得資料表。採可拋式行為。
        /// </summary>
        /// <param name="ConnString">SQL連線物件。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        /// <returns></returns>
        public override DataTable GetSqlData(string SqlCommandString)
        {
            DataTable dt;
            using (MySqlConnection icn = OpenSqlConn(DB_Conn))
            {
                MySqlCommand isc = new MySqlCommand(SqlCommandString, icn);
                isc.CommandTimeout = 10;

                DataSet ds = new DataSet();
                ds.Clear();
                MySqlDataAdapter da = new MySqlDataAdapter(isc);
                da.Fill(ds);
                dt = ds.Tables[0];
                CloseSqlConn(icn);
            }
            return dt;
        }


        public SqlServ_MySql(string p_dbConn) : base(p_dbConn)
        {

        }
    }

    /// <summary>
    /// 實作MS Sql Server的SqlService類別。
    /// </summary>
    public class SqlServ_MSSql : SqlServ
    {
        /// <summary>
        /// 開啟SQL連線。
        /// </summary>
        /// <param name="ConnString">連結字串。</param>
        /// <returns>開啟的SQL連線。</returns>
        private static SqlConnection OpenSqlConn(string ConnString)
        {
            SqlConnection icn = new SqlConnection();
            icn.ConnectionString = ConnString;
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Open();
            return icn;
        }
        /// <summary>
        /// 關閉SQL連線。
        /// </summary>
        /// <param name="icn">要關閉的連線。</param>
        private static void CloseSqlConn(SqlConnection icn)
        {
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Dispose();
        }



        /// <summary>
        /// 判斷指定欄位有指定值的資料列是否存在於資料庫。
        /// </summary>
        /// <param name="ConnString">資料庫連結字串。</param>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="FiledName">欄位名稱。</param>
        /// <param name="FiledValue">欄位值。</param>
        /// <returns></returns>
        public override bool IsExist(string TableName, string FiledName, string FiledValue)
        {
            bool opt = false;
            using (SqlConnection icn = OpenSqlConn(DB_Conn))
            {
                SqlCommand isc = new SqlCommand(@"SELECT TOP 1 1 FROM " + TableName + " WHERE " + FiledName
                    + " = " + FiledValue, icn);
                opt = isc.ExecuteScalar() == null;
                CloseSqlConn(icn);
            }
            return opt;
        }
        /// <summary>
        /// 執行SQL存放命令。
        /// </summary>
        /// <param name="ConnString">SQL連結字串。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        public override void ExeSqlCommand(string SqlCommandString)
        {
            using (SqlConnection icn = OpenSqlConn(DB_Conn))
            {
                SqlCommand cmd = new SqlCommand(SqlCommandString, icn);
                SqlTransaction mySqlTransaction = icn.BeginTransaction();
                try
                {
                    cmd.Transaction = mySqlTransaction;
                    cmd.ExecuteNonQuery();
                    mySqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    mySqlTransaction.Rollback();
                    throw (ex);
                }
                CloseSqlConn(icn);
            }
        }
        /// <summary>
        /// 執行SQL取用命令，並回傳取得資料表。採可拋式行為。
        /// </summary>
        /// <param name="ConnString">SQL連線物件。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        /// <returns></returns>
        public override DataTable GetSqlData(string SqlCommandString)
        {
            DataTable dt;
            using (SqlConnection icn = OpenSqlConn(DB_Conn))
            {
                SqlCommand isc = new SqlCommand(SqlCommandString, icn);
                isc.CommandTimeout = 10;

                DataSet ds = new DataSet();
                ds.Clear();
                SqlDataAdapter da = new SqlDataAdapter(isc);
                da.Fill(ds);
                dt = ds.Tables[0];
                CloseSqlConn(icn);
            }
            return dt;
        }


        public SqlServ_MSSql(string p_DBconn) : base(p_DBconn)
        {

        }
    }
    
}