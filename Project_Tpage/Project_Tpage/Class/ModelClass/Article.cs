using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Project_Tpage.Class
{
    /// <summary>
    /// 代表一篇文章。
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 文章識別碼。
        /// </summary>
        public string AID { get; set; }
        /// <summary>
        /// 屬於哪個班級或家族。
        /// </summary>
        public string OfGroup { get; set; }
        /// <summary>
        /// 屬於哪個看板。
        /// </summary>
        public string OfBoard { get; set; }

        /// <summary>
        /// 文章的發布日期。
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 文章的發布者。
        /// </summary>
        public string ReleaseUser { get; set; }
        /// <summary>
        /// 文章的標題。
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文章的內容。
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 按讚數量。
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 建構式。
        /// </summary>
        /// <param name="p_ReleaseUser">發文使用者帳號識別碼。</param>
        public Article(string p_ReleaseUser, string p_OfGroup, string p_OfBoard)
        {
            //設定識別碼，在資料庫中找到可使用的識別碼後設定。
            OfGroup = p_OfGroup;
            OfBoard = p_OfBoard;


            Date = new DateTime(DateTime.Now.Ticks);
            ReleaseUser = p_ReleaseUser;
            LikeCount = 0;
            Title = "";
            Content = "";
        }

        private Article()
        {

        }

        /// <summary>
        /// 從資料庫中提取文章的資料。
        /// </summary>
        /// <param name="p_AID">文章識別碼。</param>
        /// <returns></returns>
        public static Article Get_FromDB(string p_AID)
        {
            DataTable dt = SQLS.GetSqlData(Model.DB_Conn, string.Format("SELECT * FROM {0} WHERE AID = {1}",
                Model.DB_ArticleData_TableName, p_AID));

            if (dt.Rows.Count == 0)
                throw new Model.ModelException("無此文章。AID = " + p_AID);
            else
            {
                Article rtn = new Article();

                DataRow dr = dt.Rows[0];

                rtn.AID = (string)SQLS.AtType<string>(dr["AID"]);
                rtn.Title = (string)SQLS.AtType<string>(dr["Title"]);
                rtn.Content = (string)SQLS.AtType<string>(dr["Content"]);
                rtn.ReleaseUser = (string)SQLS.AtType<string>(dr["ReleaseUser"]);
                rtn.Date = (DateTime)SQLS.AtType<DateTime>(dr["ReleaseDate"]);
                rtn.OfGroup = (string)SQLS.AtType<string>(dr["OfGroup"]);
                rtn.OfBoard = (string)SQLS.AtType<string>(dr["OfBoard"]);
                rtn.LikeCount = (int)SQLS.AtType<int>(dr["LikeCount"]);

                return rtn;
            }
        }
        /// <summary>
        /// 將文章資料存入資料庫。傳回新的AID。
        /// </summary>
        /// <param name="p_art">文章資料。</param>
        /// <returns></returns>
        public static string Set_ToDB(Article p_art)
        {
            using (SqlConnection icn = SQLS.OpenSqlConn(Model.DB_Conn))
            {
                //若帳號已存在，為修改帳號資料的更新。
                if (SQLS.IsExist(Model.DB_Conn, Model.DB_ArticleData_TableName, "AID", p_art.AID))
                {
                    SQLS.ExeSqlCommand(icn, string.Format(@"UPDATE " + Model.DB_ArticleData_TableName + @"
                    SET AID = {0}, 
                    Title = {1}, 
                    Content = {2}, 
                    ReleaseUser = {3}, 
                    ReleaseDate = {4}, 
                    LikeCount = {5}, 
                    OfGroup = {6}, 
                    OfBoard = {7}, 
                    WHERE AID = {0}"
                        , SQLS.Type(p_art.AID)
                        , SQLS.Type(p_art.Title, true)
                        , SQLS.Type(p_art.Content, true)
                        , SQLS.Type(p_art.ReleaseUser)
                        , SQLS.Type(p_art.Date)
                        , SQLS.Type(p_art.LikeCount)
                        , SQLS.Type(p_art.OfGroup)
                        , SQLS.Type(p_art.OfBoard, true)));
                }
                else//否則為新增帳號資料的更新。
                {
                    SqlCommand ism = new SqlCommand(@"SELECT MAX(AID) FROM " + Model.DB_ArticleData_TableName, icn);
                    string nextaid = (string)ism.ExecuteScalar();

                    SQLS.ExeSqlCommand(icn, @"UPDATE " + Model.DB_ArticleData_TableName + " SET AID = '" +
                        (int.Parse(nextaid) + 1).ToString().PadLeft(10, '0') + "' WHERE AID = '"
                        + nextaid + "'");

                    SQLS.ExeSqlCommand(icn, string.Format(@"INSERT INTO " + Model.DB_ArticleData_TableName + @" 
                    (AID, Title, Content, ReleaseUser, ReleaseDate, LikeCount, OfGroup, OfBoard)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})"
                        , SQLS.Type(p_art.AID = nextaid)
                        , SQLS.Type(p_art.Title, true)
                        , SQLS.Type(p_art.Content, true)
                        , SQLS.Type(p_art.ReleaseUser)
                        , SQLS.Type(p_art.Date)
                        , SQLS.Type(p_art.LikeCount)
                        , SQLS.Type(p_art.OfGroup)
                        , SQLS.Type(p_art.OfBoard, true)));
                }
                SQLS.CloseSqlConn(icn);
            }
            return p_art.AID;
        }
    }

    /// <summary>
    /// 代表一篇留言。
    /// </summary>
    public class AMessage
    {
        /// <summary>
        /// 留言識別碼。
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        /// 屬於哪篇文章。
        /// </summary>
        public string OfArticle { get; set; }

        /// <summary>
        /// 文章的發布日期。
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 文章的發布者。
        /// </summary>
        public string ReleaseUser { get; set; }
        /// <summary>
        /// 文章的內容。
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 按讚數量。
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 建構式。
        /// </summary>
        /// <param name="p_ReleaseUser">發布者。</param>
        /// <param name="p_OfArticle">屬於哪篇文章。</param>
        public AMessage(string p_ReleaseUser, string p_OfArticle)
        {
            //設定識別碼，在資料庫中找到可使用的識別碼後設定。

            ReleaseUser = p_ReleaseUser;
            OfArticle = p_OfArticle;
            Date = new DateTime(DateTime.Now.Ticks);
            LikeCount = 0;

            Content = "";
        }

        private AMessage()
        {

        }

        /// <summary>
        /// 從資料庫中提取留言的資料。
        /// </summary>
        /// <param name="p_MID">留言識別碼。</param>
        /// <returns></returns>
        public static AMessage Get_FromDB(string p_MID)
        {
            DataTable dt = SQLS.GetSqlData(Model.DB_Conn, string.Format("SELECT * FROM {0} WHERE AID = {1}",
                Model.DB_AMessageData_TableName, p_MID));

            if (dt.Rows.Count == 0)
                throw new Model.ModelException("無此留言。MID = " + p_MID);
            else
            {
                AMessage rtn = new AMessage();

                DataRow dr = dt.Rows[0];

                rtn.MID = (string)SQLS.AtType<string>(dr["MID"]);
                rtn.ReleaseUser = (string)SQLS.AtType<string>(dr["ReleaseUser"]);
                rtn.Date = (DateTime)SQLS.AtType<DateTime>(dr["ReleaseDate"]);
                rtn.Content = (string)SQLS.AtType<string>(dr["Content"]);
                rtn.LikeCount = (int)SQLS.AtType<int>(dr["LikeCount"]);
                rtn.OfArticle = (string)SQLS.AtType<string>(dr["OfArticle"]);

                return rtn;
            }
        }
        /// <summary>
        /// 將留言資料存入資料庫。傳回新的MID。
        /// </summary>
        /// <param name="p_ame">留言資料。</param>
        public static string Set_ToDB(AMessage p_ame)
        {
            using (SqlConnection icn = SQLS.OpenSqlConn(Model.DB_Conn))
            {
                //若帳號已存在，為修改帳號資料的更新。
                if (SQLS.IsExist(Model.DB_Conn, Model.DB_AMessageData_TableName, "AID", p_ame.MID))
                {
                    SQLS.ExeSqlCommand(icn, string.Format(@"UPDATE " + Model.DB_AMessageData_TableName + @"
                    SET MID = {0}, 
                    ReleaseUser = {1}, 
                    ReleaseDate = {2}, 
                    Content = {3}, 
                    LikeCount = {4}, 
                    OfArticle = {5}, 
                    WHERE MID = {0}"
                        , SQLS.Type(p_ame.MID)
                        , SQLS.Type(p_ame.ReleaseUser)
                        , SQLS.Type(p_ame.Date)
                        , SQLS.Type(p_ame.Content, true)
                        , SQLS.Type(p_ame.LikeCount)
                        , SQLS.Type(p_ame.OfArticle)));
                }
                else//否則為新增帳號資料的更新。
                {
                    SqlCommand ism = new SqlCommand(@"SELECT MAX(MID) FROM " + Model.DB_AMessageData_TableName, icn);
                    string nextmid = (string)ism.ExecuteScalar();

                    SQLS.ExeSqlCommand(icn, @"UPDATE " + Model.DB_AMessageData_TableName + " SET MID = '" +
                        (int.Parse(nextmid) + 1).ToString().PadLeft(10, '0') + "' WHERE MID = '"
                        + nextmid + "'");

                    SQLS.ExeSqlCommand(icn, string.Format(@"INSERT INTO " + Model.DB_AMessageData_TableName + @" 
                    (MID, ReleaseUser, ReleaseDate, Content, LikeCount, OfArticle)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5})"
                        , SQLS.Type(p_ame.MID)
                        , SQLS.Type(p_ame.ReleaseUser)
                        , SQLS.Type(p_ame.Date)
                        , SQLS.Type(p_ame.Content, true)
                        , SQLS.Type(p_ame.LikeCount)
                        , SQLS.Type(p_ame.OfArticle)));
                }
                SQLS.CloseSqlConn(icn);
            }
            return p_ame.MID;
        }
    }
}