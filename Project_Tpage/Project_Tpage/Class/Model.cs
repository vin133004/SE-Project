using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Drawing;

namespace Project_Tpage.Class
{
    public class Model
    {
        /// <summary>
        /// 代表此資料模型所擲回的例外狀況。
        /// </summary>
        public class ModelException : Exception
        {
            public ModelException() : base("資料模型發生未知錯誤。")
            {

            }

            public ModelException(string message) : base(message)
            {

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
            /// 代表首頁。
            /// </summary>
            Home,
            /// <summary>
            /// 代表朋友的動態頁。
            /// </summary>
            FriendPage,
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
        /// 資料庫連線字串。
        /// </summary>
        public static string DB_Conn = @"";
        /// <summary>
        /// 資料庫－使用者資料表名稱。
        /// </summary>
        public static string DB_UserData_TableName = "UserData";
        /// <summary>
        /// 資料庫－班級團體資料表名稱。
        /// </summary>
        public static string DB_ClassGroupData_TableName = "ClassGroupData";
        /// <summary>
        /// 資料庫－家族團體資料表名稱。
        /// </summary>
        public static string DB_FamilyGroupData_TableName = "FamilyGroupData";
        /// <summary>
        /// 資料庫－文章資料表名稱。
        /// </summary>
        public static string DB_ArticleData_TableName = "ArticleData";
        /// <summary>
        /// 資料庫－留言資料表名稱。
        /// </summary>
        public static string DB_AMessageData_TableName = "AMessageData";

        /// <summary>
        /// 取得目前的狀態。
        /// </summary>
        public StateEnum State { get; set; }
        /// <summary>
        /// 目前狀態的附加資訊。如在看板狀態時看板的主題名稱。
        /// </summary>
        public string StateInfo { get; set; }
        /// <summary>
        /// 取得顯示。
        /// </summary>
        public View view { get; set; }

        /// <summary>
        /// 當前頁面的使用者。
        /// </summary>
        public User user { get; set; }



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
            User.Set_ToDB(temp);
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
                user = User.Get_FromDB(p_ID, false);
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
        /// 根據給定的文章識別碼，查詢文章物件，未找到則擲回例外。
        /// </summary>
        /// <param name="p_AID">文章識別碼。</param>
        /// <returns></returns>
        public Article GetArticle(string p_AID)
        {
            return Article.Get_FromDB(p_AID);
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

            Article.Set_ToDB(atc);
        }

        public AMessage GetAMessage(string p_AID)
        {
            //從資料庫查詢該筆留言，若未找到則傳回null
            throw new NotImplementedException();
        }

        public void ReleaseMessage(string p_Message, string p_OfArticle)
        {
            AMessage ame = new AMessage(user.Userinfo.UID, p_OfArticle);
            ame.Content = p_Message;

            //將留言上傳資料庫。
            //判斷例外。
        }

        /// <summary>
        /// 取得特定的團體的文章。
        /// </summary>
        /// <param name="p_Group">團體識別碼。</param>
        /// <returns></returns>
        public List<Article> GetArticlesFromGroup(string p_Group)
        {
            //從資料庫提取此Group的文章並回傳。
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得特定看板的文章。
        /// </summary>
        /// <param name="p_Group">團體識別碼。</param>
        /// <param name="p_Board">看板名稱。</param>
        /// <returns></returns>
        public List<Article> GetArticlesFromBoard(string p_Group, string p_Board)
        {
            //從資料庫提取此Board的文章並回傳。
            throw new NotImplementedException();
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
            User.Set_ToDB(user);
        }


        public Model(View view)
        {
            this.view = view;

            State = StateEnum.Login;
            user = null;
        }

        public static List<string> StringToList(string str)
        {
            if (str == null || str == "") return null;
            return str.Split(',').ToList();
        }
    }


    /// <summary>
    /// SQL Service類別提供SQL資料庫的服務。
    /// </summary>
    public abstract class SQLS
    {
        /// <summary>
        /// 開啟SQL連線。
        /// </summary>
        /// <param name="ConnString">連結字串。</param>
        /// <returns>開啟的SQL連線。</returns>
        public static SqlConnection OpenSqlConn(string ConnString)
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
        public static void CloseSqlConn(SqlConnection icn)
        {
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Dispose();
        }

        /// <summary>
        /// 判斷指定欄位有指定值的資料列是否存在於資料庫。
        /// </summary>
        /// <param name="p_ConnString">資料庫連結字串。</param>
        /// <param name="p_TableName">資料表名稱。</param>
        /// <param name="p_FiledName">欄位名稱。</param>
        /// <param name="p_FiledValue">欄位值。</param>
        /// <returns></returns>
        public static bool IsExist(string p_ConnString, string p_TableName, string p_FiledName, string p_FiledValue)
        {
            bool opt = false;
            using (SqlConnection icn = OpenSqlConn(p_ConnString))
            {
                SqlCommand isc = new SqlCommand(@"SELECT TOP 1 1 FROM " + p_TableName + " WHERE " + p_FiledName
                    + " = " + p_FiledValue, icn);
                opt = isc.ExecuteScalar() == null;
                CloseSqlConn(icn);
            }
            return opt;
        }


        /// <summary>
        /// 執行SQL存放命令。採不可拋式行為。
        /// </summary>
        /// <param name="icn">SQL連線物件。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        public static void ExeSqlCommand(SqlConnection icn, string SqlCommandString)
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
        }

        /// <summary>
        /// 執行SQL取用命令，並回傳取得資料表。採不可拋式行為。
        /// </summary>
        /// <param name="icn">SQL連線物件。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        /// <returns></returns>
        public static DataTable GetSqlData(SqlConnection icn, string SqlCommandString)
        {
            SqlCommand isc = new SqlCommand(SqlCommandString, icn);
            isc.CommandTimeout = 10;

            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(isc);
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// 執行SQL存放命令。採可拋式行為。
        /// </summary>
        /// <param name="ConnString">SQL連結字串。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        public static void ExeSqlCommand(string ConnString, string SqlCommandString)
        {
            using (SqlConnection icn = OpenSqlConn(ConnString))
            {
                ExeSqlCommand(icn, SqlCommandString);
                CloseSqlConn(icn);
            }
        }

        /// <summary>
        /// 執行SQL取用命令，並回傳取得資料表。採可拋式行為。
        /// </summary>
        /// <param name="ConnString">SQL連線物件。</param>
        /// <param name="SqlCommandString">要執行的命令。</param>
        /// <returns></returns>
        public static DataTable GetSqlData(string ConnString, string SqlCommandString)
        {
            DataTable dt;
            using (SqlConnection icn = OpenSqlConn(ConnString))
            {
                dt = GetSqlData(icn, SqlCommandString);
                CloseSqlConn(icn);
            }
            return dt;
        }


        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_str">字串格式。</param>
        /// <returns></returns>
        public static string Type(string p_str, bool isCH = false)
        {
            if (p_str == null)
                return "NULL";
            else if (isCH)
                return "N\'" + p_str + "\'";
            else
                return "\'" + p_str + "\'";
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_dt">日期格式。</param>
        /// <returns></returns>
        public static string Type(DateTime p_dt, bool onlyDate = false)
        {
            if (p_dt == null)
                return "NULL";
            else if (onlyDate)
                return string.Format("CAST('{0}' AS DATETIME)", p_dt.ToString("yyyy-MM-dd"));
            else
                return string.Format("CAST('{0}' AS DATETIME2)", p_dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_gd">性別格式。</param>
        /// <returns></returns>
        public static string Type(Gender p_gd)
        {
            return "" + ((byte)p_gd);
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_gd">使用者隱私格式。</param>
        /// <returns></returns>
        public static string Type(UserPrivacy p_upr)
        {
            return "" + ((byte)p_upr);
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_img">圖片格式。</param>
        /// <returns></returns>
        public static string Type(Image p_img)
        {
            if (p_img == null) return "NULL";

            byte[] buf = (byte[])new ImageConverter().ConvertTo(p_img, typeof(byte[]));
            string str = "0x";
            foreach(byte bt in buf) str += Convert.ToString(bt, 16).PadLeft(2, '0');
            return string.Format("CAST({0} AS IMAGE)", str);
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_ls">字串陣列格式。</param>
        /// <returns></returns>
        public static string Type(List<string> p_ls, bool isCH = false)
        {
            if (p_ls == null) return "NULL";
            else if (p_ls.Count <= 0) return "''";
            
            return (isCH ? "N" : "") + "\'" + string.Concat(p_ls.Select((x, indx) => indx == 0 ? x : "," + x)) + "\'";
        }
        /// <summary>
        /// 將特定型別的資料轉換為SQL字串的表示方式。
        /// </summary>
        /// <param name="p_int">整數格式。</param>
        /// <returns></returns>
        public static string Type(int p_int)
        {
            return "" + p_int;
        }

        /// <summary>
        /// 解析從資料庫讀取的資料是否為DBNull，如果是，則回傳指定型別的空值，否則回傳原值。
        /// </summary>
        /// <typeparam name="T">指定輸出型別。</typeparam>
        /// <param name="obj">要判斷的物件。</param>
        /// <returns></returns>
        public static object AtType<T>(object obj)
        {
            if (obj is DBNull)
            {
                if (typeof(T).Equals(typeof(DateTime)))         return DateTime.MinValue;
                else if (typeof(T).Equals(typeof(Gender)))      return (Gender)0;
                else if (typeof(T).Equals(typeof(UserPrivacy))) return (UserPrivacy)14;
                else if (typeof(T).Equals(typeof(int)))         return -1;
                else                                            return null;
            }
            else
                return (T)obj;
        }

        /*
        /// <summary>
        /// 取得資料庫中的資料。
        /// </summary>
        /// <param name="icn">已開啟的連線。</param>
        /// <param name="SqlString">取得資料的特定SQL語法。</param>
        /// <returns>目標取得的資料。</returns>
        public static DataTable GetSqlDataTable(SqlConnection icn, string SqlString)
        {
            DataTable myDataTable = new DataTable();
            SqlCommand isc = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(isc);
            isc.Connection = icn;
            isc.CommandText = SqlString;
            isc.CommandTimeout = 10;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            myDataTable = ds.Tables[0];
            return myDataTable;
        }

        /// <summary>
        /// 對SQL資料庫提出特定的SQL指令。
        /// </summary>
        /// <param name="icn">已開啟的連線。</param>
        /// <param name="SqlSelectString">SQL指令。</param>
        public static void SqlInsertUpdateDelete(SqlConnection icn, string SqlSelectString)
        {
            SqlCommand cmd = new SqlCommand(SqlSelectString, icn);
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
        }
        */
    }
}