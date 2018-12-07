using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Project_Tpage.Class
{
    /// <summary>
    /// 代表性別的資料型別。
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 未定義的性別。
        /// </summary>
        Null = 0,
        /// <summary>
        /// 代表男性。
        /// </summary>
        Male = 1,
        /// <summary>
        /// 代表女性。
        /// </summary>
        Female = 2,
        /// <summary>
        /// 代表第三性。
        /// </summary>
        Thirdgender = 3
    }

    /// <summary>
    /// 表示使用者的隱私設定選項。
    /// </summary>
    public enum UserPrivacy {
        /// <summary>
        /// 代表完全不公開。
        /// </summary>
        Private =       1,
        /// <summary>
        /// 代表僅限朋友閱覽。
        /// </summary>
        FriendOnly =    2,
        /// <summary>
        /// 代表僅限家族成員閱覽。
        /// </summary>
        FamilyOnly =    4,
        /// <summary>
        /// 代表僅限班級成員閱覽。
        /// </summary>
        ClassOnly =     8,
        /// <summary>
        /// 代表僅限朋友與家族成員閱覽。
        /// </summary>
        FriendFamily =  6,
        /// <summary>
        /// 代表僅限朋友與班級成員閱覽。
        /// </summary>
        FriendClass =   10,
        /// <summary>
        /// 代表僅限家族成員與班級成員閱覽。
        /// </summary>
        FamilyClass =   12,
        /// <summary>
        /// 代表完全公開。
        /// </summary>
        Public =        14
    }



    /// <summary>
    /// 一個使用者帳戶的資訊。
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 帳號識別碼。
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 帳號。
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 密碼。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 電子郵件帳號。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 學號。
        /// </summary>
        public string StudentNum { get; set; }
        /// <summary>
        /// 班級系級。
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 使用者的真實名稱。
        /// </summary>
        public string Realname { get; set; }
        /// <summary>
        /// 使用者的暱稱。
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 性別。
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 使用者的照片。
        /// </summary>
        public Image Picture { get; set; }
        /// <summary>
        /// 生日。
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 年齡。
        /// </summary>
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Birthday.Year
                    - (DateTime.Compare(Birthday.AddYears(DateTime.Now.Year - Birthday.Year), DateTime.Now) <= 0 ? 0 : 1);
            }
        }

    }

    /// <summary>
    /// 代表一個使用者的設定。
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// 使用者的隱私設定。
        /// </summary>
        public UserPrivacy Userprivacy { get; set; }

    }

    /// <summary>
    /// 代表登入的使用者。
    /// </summary>
    public class User
    {
        /// <summary>
        /// 使用者資訊。
        /// </summary>
        public UserInfo Userinfo { get; set; }
        /// <summary>
        /// 使用者設定。
        /// </summary>
        public UserSetting Usersetting { get; set; }
        /// <summary>
        /// 此使用者的朋友圈。
        /// </summary>
        public FriendGroup Friends { get; private set; }
        /// <summary>
        /// 使用者所在的團體。
        /// </summary>
        public List<RelationshipGroup> Groups { get; private set; }
        /// <summary>
        /// 台科幣的存量。
        /// </summary>
        public int TbitCoin { get; set; }

        /// <summary>
        /// 抽卡交友。
        /// </summary>
        public void Pickcard()
        {

        }

        /// <summary>
        /// 以帳號或帳號識別碼作為索引鍵從資料庫中取得使用者資料。
        /// </summary>
        /// <param name="p_UID">帳號或帳號識別碼。</param>
        /// <param name="isUID">是否將傳入參數作為帳號識別碼解讀，否則作為帳號解讀。</param>
        /// <returns></returns>
        public static User Get_FromDB(string p_UID, bool isUID = true)
        {
            //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
            DataTable dt = SQLS.GetSqlData(Model.DB_Conn, "SELECT * FROM " + Model.DB_UserData_TableName + " WHERE "
                       + (isUID ? "UID" : "ID") + " = " + p_UID);
            
            if (dt.Rows.Count == 0)
            {
                throw new Exception("無此帳號。" + (isUID ? "UID" : "ID") + " : "
                    + p_UID);
            }
            else
            {
                User rtn = new User();
                try
                {


                    DataRow dr = dt.Rows[0];

                    rtn.Userinfo.UID = (string)SQLS.AtType<string>(dr["UID"]);
                    rtn.Userinfo.ID = (string)SQLS.AtType<string>(dr["ID"]);
                    rtn.Userinfo.Password = (string)SQLS.AtType<string>(dr["Password"]);
                    rtn.Userinfo.Email = (string)SQLS.AtType<string>(dr["Email"]);
                    rtn.Userinfo.StudentNum = (string)SQLS.AtType<string>(dr["StudentNum"]);
                    rtn.Userinfo.ClassName = (string)SQLS.AtType<string>(dr["ClassName"]);
                    rtn.Userinfo.Realname = (string)SQLS.AtType<string>(dr["Realname"]);
                    rtn.Usersetting.Userprivacy = (UserPrivacy)SQLS.AtType<UserPrivacy>(dr["UserPrivacy"]);
                    rtn.TbitCoin = (int)SQLS.AtType<int>(dr["TbitCoin"]);

                    rtn.Userinfo.Nickname = (string)SQLS.AtType<string>(dr["Nickname"]);
                    rtn.Userinfo.Gender = (Gender)SQLS.AtType<Gender>(dr["Gender"]);
                    rtn.Userinfo.Picture = dr["Picture"] is DBNull ? null :
                        (Image)(new ImageConverter()).ConvertFrom((byte[])dr["Picture"]);
                    rtn.Userinfo.Birthday = (DateTime)SQLS.AtType<DateTime>(dr["Birthday"]);

                    rtn.Friends.Members = Model.StringToList((string)SQLS.AtType<List<string>>(dr["Friend"]));
                    rtn.Friends.UpdateMembersName();


                    List<string> ClassGroupLs = Model.StringToList((string)SQLS.AtType<List<string>>(dr["ClassGroup"]));
                    List<string> FamilyGroupLs = Model.StringToList((string)SQLS.AtType<List<string>>(dr["FamilyGroup"]));

                    if ((ClassGroupLs == null || ClassGroupLs.Count == 0) &&
                        (FamilyGroupLs == null || FamilyGroupLs.Count == 0))
                        rtn.Groups = null;
                    else if (ClassGroupLs == null || ClassGroupLs.Count == 0)
                        rtn.Groups = FamilyGroupLs.Select(x => (RelationshipGroup)FamilyGroup.Get_FromDB(x)).ToList();
                    else if (FamilyGroupLs == null || FamilyGroupLs.Count == 0)
                        rtn.Groups = ClassGroupLs.Select(x => (RelationshipGroup)ClassGroup.Get_FromDB(x)).ToList();
                    else
                        rtn.Groups = Enumerable.Concat(
                        ClassGroupLs.Select(x => (RelationshipGroup)ClassGroup.Get_FromDB(x)),
                        FamilyGroupLs.Select(x => (RelationshipGroup)FamilyGroup.Get_FromDB(x))).ToList();
                }
                catch(Exception)
                {
                    throw new Model.ModelException("User類別－Get_FromDB設定User欄位發生錯誤！");
                }
                return rtn;
            }

        }

        /// <summary>
        /// 將使用者資料儲存進資料庫。
        /// </summary>
        /// <param name="usr">使用者資料。</param>
        public static string Set_ToDB(User usr)
        {
            using (SqlConnection icn = SQLS.OpenSqlConn(Model.DB_Conn))
            {
                //若帳號已存在，為修改帳號資料的更新。
                if (SQLS.IsExist(Model.DB_Conn, Model.DB_UserData_TableName, "ID", usr.Userinfo.ID))
                {
                    SQLS.ExeSqlCommand(icn, string.Format(@"UPDATE " + Model.DB_UserData_TableName + @"
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
                    TbitCoin = {15} 
                    WHERE UID = {1}"
                        , SQLS.Type(usr.Userinfo.ID)
                        , SQLS.Type(usr.Userinfo.UID)
                        , SQLS.Type(usr.Userinfo.Password)
                        , SQLS.Type(usr.Userinfo.Email)
                        , SQLS.Type(usr.Userinfo.StudentNum)
                        , SQLS.Type(usr.Userinfo.ClassName, true)
                        , SQLS.Type(usr.Userinfo.Realname, true)
                        , SQLS.Type(usr.Userinfo.Nickname, true)
                        , SQLS.Type(usr.Userinfo.Picture)
                        , SQLS.Type(usr.Userinfo.Gender)
                        , SQLS.Type(usr.Userinfo.Birthday, true)
                        , SQLS.Type(usr.Usersetting.Userprivacy)
                        , SQLS.Type(usr.Friends.Members)
                        , SQLS.Type(usr.Groups.Where(x => x is ClassGroup).Select(x => x.GID).ToList())
                        , SQLS.Type(usr.Groups.Where(x => x is FamilyGroup).Select(x => x.GID).ToList())
                        , SQLS.Type(usr.TbitCoin)));
                }
                else//否則為新增帳號資料的更新。
                {
                    SqlCommand ism = new SqlCommand(@"SELECT MAX(UID) FROM " + Model.DB_UserData_TableName, icn);
                    string nextuid = (string)ism.ExecuteScalar();

                    SQLS.ExeSqlCommand(icn, @"UPDATE " + Model.DB_UserData_TableName + " SET UID = '" +
                        (int.Parse(nextuid) + 1).ToString().PadLeft(10, '0') + "' WHERE UID = '"
                        + nextuid + "'");

                    SQLS.ExeSqlCommand(icn, string.Format(@"INSERT INTO " + Model.DB_UserData_TableName + @" 
                    (ID, UID, Password, Email, StudentNum, ClassName, RealName, NickName, Picture, Gender, Birthday) 
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})"
                        , SQLS.Type(usr.Userinfo.ID)
                        , SQLS.Type(usr.Userinfo.UID = nextuid)
                        , SQLS.Type(usr.Userinfo.Password)
                        , SQLS.Type(usr.Userinfo.Email)
                        , SQLS.Type(usr.Userinfo.StudentNum)
                        , SQLS.Type(usr.Userinfo.ClassName, true)
                        , SQLS.Type(usr.Userinfo.Realname, true)
                        , SQLS.Type(usr.Userinfo.Nickname, true)
                        , SQLS.Type(usr.Userinfo.Picture)
                        , SQLS.Type(usr.Userinfo.Gender)
                        , SQLS.Type(usr.Userinfo.Birthday, true)
                        , SQLS.Type(usr.Usersetting.Userprivacy)
                        , SQLS.Type(usr.Friends.Members)
                        , SQLS.Type(usr.Groups.Where(x => x is ClassGroup).Select(x => x.GID).ToList())
                        , SQLS.Type(usr.Groups.Where(x => x is FamilyGroup).Select(x => x.GID).ToList())
                        , SQLS.Type(usr.TbitCoin)));
                }
                SQLS.CloseSqlConn(icn);
            }
            return usr.Userinfo.UID;
        }

        public static void ValidUserInfo(UserInfo p_uif)
        {
            string error = "";
            if (SQLS.IsExist(Model.DB_Conn, Model.DB_UserData_TableName, "ID", p_uif.ID))
                error += "帳號已存在－" + p_uif.ID + "。\r\n";
            if (!Regex.IsMatch(p_uif.ID, @"^\w+$"))
                error += "帳號格式錯誤，非英文、數字、底線所組成。\r\n";
            if (p_uif.Email.Split('@').Length != 2)
                error += "電子郵件格式錯誤。\r\n";
            if (!Regex.IsMatch(p_uif.Password, "^[A-Za-z0-9]+$"))
                error += "密碼格式錯誤，非英文、數字所組成。\r\n";
            if (p_uif.Password.Length < 8)
                error += "密碼長度不足，至少8位英文或數字組成。\r\n";


            if (error != "") throw new Model.ModelException("無效的帳號資料：\r\n" + error);
        }

        public User()
        {
            Friends = new FriendGroup();
            Groups = new List<RelationshipGroup>();

            Usersetting = new UserSetting();
            Userinfo = new UserInfo();
            Usersetting.Userprivacy = UserPrivacy.Public;
        }
    }
}