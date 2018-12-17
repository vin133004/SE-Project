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
        public string StudentID { get; set; }
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

        /// <summary>
        /// 回傳一個新的UserInfo執行個體，其值為預設的空值。此為唯讀。
        /// </summary>
        public static UserInfo New
        {
            get
            {
                UserInfo rtn = new UserInfo();
                rtn.UID = null;
                rtn.ID = "";
                rtn.Password = "";
                rtn.Email = "";
                rtn.StudentID = "";
                rtn.ClassName = "";
                rtn.Realname = "";
                rtn.Nickname = null;
                rtn.Gender = Gender.Null;
                rtn.Picture = null;
                rtn.Birthday = DateTime.MinValue;

                return rtn;
            }
        }

        private UserInfo() { }
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

        public static UserSetting New
        {
            get
            {
                UserSetting rtn = new UserSetting();
                rtn.Userprivacy = UserPrivacy.Public;
                return rtn;
            }
        }

        public static UserSetting Clone(UserSetting us)
        {
            UserSetting rtn = new UserSetting();
            rtn.Userprivacy = us.Userprivacy;
            return rtn;
        }

        private UserSetting() { }
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
        public List<RelationshipGroup> Groups { get; set; }
        /// <summary>
        /// 台科幣的存量。
        /// </summary>
        public int TbitCoin { get; set; }
        
        /// <summary>
        /// 抽卡交友。
        /// </summary>
        public User Pickcard()
        {
            int maxuid = int.Parse((string)Model.DB.GetSqlData(@"SELECT MAX(UID) FROM " 
                + Model.DB.DB_UserData_TableName).Rows[0][0]);

            int myuid = int.Parse(Userinfo.UID);
            int nowuid = myuid;
            for(int noneloop = 0; noneloop<10000; noneloop++)
            {
                nowuid = new Random().Next(1, maxuid);
                if (nowuid == myuid) continue;
                break;
            }

            return (User)Model.DB.Get<User>(nowuid.ToString().PadLeft(10, '0'));
        }

        /// <summary>
        /// 檢查一個使用者資訊是否符合規定。
        /// </summary>
        /// <param name="p_uif">使用者資訊。</param>
        public static void ValidUserInfo(UserInfo p_uif)
        {
            string error = "";
            if (Model.DB.IsExist(Model.DB.DB_UserData_TableName, "ID", p_uif.ID))
                error += "帳號已存在－" + p_uif.ID + "。\r\n";
            if (!Regex.IsMatch(p_uif.ID, @"^\w+$"))
                error += "帳號格式錯誤，非英文、數字、底線所組成。\r\n";
            if (p_uif.Email.Split('@').Length != 2)
                error += "電子郵件格式錯誤。\r\n";
            if (!Regex.IsMatch(p_uif.Password, "^[A-Za-z0-9]+$"))
                error += "密碼格式錯誤，非英文、數字所組成。\r\n";
            if (p_uif.Password.Length < 8)
                error += "密碼長度不足，至少8位英文或數字組成。\r\n";


            if (error != "") throw new ModelException(
                ModelException.Error.InvalidUserInformation,
                "無效的帳號資料：\r\n" + error, 
                "帳號資料不符合格式：" + error);
        }

        public User()
        {
            Usersetting = UserSetting.New;
            Userinfo = UserInfo.New;

            Usersetting.Userprivacy = UserPrivacy.Public;
            Userinfo.UID = null;

            Friends = new FriendGroup();
            Groups = new List<RelationshipGroup>();
        }

        public User(DataRow dr)
        {
            Usersetting = UserSetting.New;
            Userinfo = UserInfo.New;
            Friends = new FriendGroup();
            try
            {
                Userinfo.UID = (string)Model.DB.AnlType<string>(dr["UID"]);
                Userinfo.ID = (string)Model.DB.AnlType<string>(dr["ID"]);
                Userinfo.Password = (string)Model.DB.AnlType<string>(dr["Password"]);
                Userinfo.Email = (string)Model.DB.AnlType<string>(dr["Email"]);
                Userinfo.StudentID = (string)Model.DB.AnlType<string>(dr["StudentNum"]);
                Userinfo.ClassName = (string)Model.DB.AnlType<string>(dr["ClassName"]);
                Userinfo.Realname = (string)Model.DB.AnlType<string>(dr["Realname"]);
                Usersetting.Userprivacy = (UserPrivacy)Model.DB.AnlType<UserPrivacy>(dr["UserPrivacy"]);
                TbitCoin = (int)Model.DB.AnlType<int>(dr["TbitCoin"]);

                Userinfo.Nickname = (string)Model.DB.AnlType<string>(dr["Nickname"]);
                Userinfo.Gender = (Gender)Model.DB.AnlType<Gender>(dr["Gender"]);
                Userinfo.Picture = (Image)Model.DB.AnlType<Image>(dr["Picture"]);
                Userinfo.Birthday = (DateTime)Model.DB.AnlType<DateTime>(dr["Birthday"]);

                Friends.Member_SetAll((List<string>)Model.DB.AnlType<List<string>>(dr["Friend"]));


                List<string> ClassGroupLs = (List<string>)Model.DB.AnlType<List<string>>(dr["ClassGroup"]);
                List<string> FamilyGroupLs = (List<string>)Model.DB.AnlType<List<string>>(dr["FamilyGroup"]);

                if (ClassGroupLs == null && FamilyGroupLs == null)
                    Groups = null;
                else if (ClassGroupLs == null)
                    Groups = FamilyGroupLs.Select(x => (RelationshipGroup)Model.DB.Get<FamilyGroup>(x)).ToList();
                else if (FamilyGroupLs == null)
                    Groups = ClassGroupLs.Select(x => (RelationshipGroup)Model.DB.Get<ClassGroup>(x)).ToList();
                else
                    Groups = Enumerable.Concat(
                    ClassGroupLs.Select(x => (RelationshipGroup)Model.DB.Get<FamilyGroup>(x)),
                    FamilyGroupLs.Select(x => (RelationshipGroup)Model.DB.Get<ClassGroup>(x))).ToList();
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailUser,
                    "User類別－建構式User(Datarow)發生錯誤：User設定物件欄位錯誤。\r\n"  + e.Message, 
                    "");
            }
        }
    }
}