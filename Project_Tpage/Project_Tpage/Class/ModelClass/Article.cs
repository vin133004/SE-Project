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
            AID = null;
            OfGroup = p_OfGroup;
            OfBoard = p_OfBoard;


            Date = new DateTime(DateTime.Now.Ticks);
            ReleaseUser = p_ReleaseUser;
            LikeCount = 0;
            Title = "";
            Content = "";
        }

        public Article()
        {
            AID = null;
        }

        public Article(DataRow dr)
        {
            try
            {
                AID = (string)Model.DB.AnlType<string>(dr["AID"]);
                Title = (string)Model.DB.AnlType<string>(dr["Title"]);
                Content = (string)Model.DB.AnlType<string>(dr["Content"]);
                ReleaseUser = (string)Model.DB.AnlType<string>(dr["ReleaseUser"]);
                Date = (DateTime)Model.DB.AnlType<DateTime>(dr["ReleaseDate"]);
                OfGroup = (string)Model.DB.AnlType<string>(dr["OfGroup"]);
                OfBoard = (string)Model.DB.AnlType<string>(dr["OfBoard"]);
                LikeCount = (int)Model.DB.AnlType<int>(dr["LikeCount"]);

            }
            catch (Exception e)
            {
                throw new Model.ModelException("Article類別－建構式Article(DataRow)發生錯誤：Article設定物件欄位錯誤。\r\n"
                    + e.Message);
            }
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
            MID = null;

            ReleaseUser = p_ReleaseUser;
            OfArticle = p_OfArticle;
            Date = new DateTime(DateTime.Now.Ticks);
            LikeCount = 0;

            Content = "";
        }

        public AMessage()
        {
            MID = null;
        }

        public AMessage(DataRow dr)
        {
            try
            {
                MID = (string)Model.DB.AnlType<string>(dr["MID"]);
                ReleaseUser = (string)Model.DB.AnlType<string>(dr["ReleaseUser"]);
                Date = (DateTime)Model.DB.AnlType<DateTime>(dr["ReleaseDate"]);
                Content = (string)Model.DB.AnlType<string>(dr["Content"]);
                LikeCount = (int)Model.DB.AnlType<int>(dr["LikeCount"]);
                OfArticle = (string)Model.DB.AnlType<string>(dr["OfArticle"]);

            }
            catch (Exception e)
            {
                throw new Model.ModelException("AMessage類別－建構式Amessage(DataRow)發生錯誤：AMessage設定物件欄位錯誤。\r\n"
                    + e.Message);
            }
        }
    }
}