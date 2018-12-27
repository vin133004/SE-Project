using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

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
        /// 紀錄上次結算台科幣的按讚數量。
        /// </summary>
        public int LastComputeTbitLikeCount { get; set; }


        public Article New
        {
            get
            {
                Article rtn = new Article();
                rtn.OfGroup = "";
                rtn.OfBoard = "";
                rtn.Date = new DateTime(DateTime.Now.Ticks);
                rtn.ReleaseUser = null;
                rtn.Title = "";
                rtn.Content = "";
                rtn.LikeCount = 0;
                rtn.LastComputeTbitLikeCount = 0;


                return rtn;
            }
        }


        
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

        private Article()
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
                LastComputeTbitLikeCount = (int)Model.DB.AnlType<int>(dr["TbitLikeCount"]);
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailArticle,
                    "Article類別－建構式Article(DataRow)發生錯誤：Article設定物件欄位錯誤。\r\n" + e.Message, 
                    "");
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
        /// 紀錄上次結算台科幣的按讚數量。
        /// </summary>
        public int LastComputeTbitLikeCount { get; set; }


        public AMessage New
        {
            get
            {
                AMessage rtn = new AMessage();
                rtn.OfArticle = "";
                rtn.ReleaseUser = null;
                rtn.Date = new DateTime(DateTime.Now.Ticks);
                rtn.Content = "";
                rtn.LikeCount = 0;
                LastComputeTbitLikeCount = 0;

                return rtn;
            }
        }

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

        private AMessage()
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
                LastComputeTbitLikeCount = (int)Model.DB.AnlType<int>(dr["TbitLikeCount"]);
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailAMessage,
                    "AMessage類別－建構式Amessage(DataRow)發生錯誤：AMessage設定物件欄位錯誤。\r\n" + e.Message, 
                    "");
            }
        }
    }

    /// <summary>
    /// 代表一個廣告。
    /// </summary>
    public class Advertise
    {
        /// <summary>
        /// 廣告識別碼。
        /// </summary>
        public string DID { get; set; }
        /// <summary>
        /// 廣告的本體，以圖片展示。
        /// </summary>
        public Image Body { get; set; }
        /// <summary>
        /// 廣告位置區塊索引。
        /// </summary>
        public int Location { get; set; }
        /// <summary>
        /// 此廣告圖片的大小。
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// 廣告展示的截止日期。
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// 建立新的Advertise執行個體，其屬性為預設值。
        /// </summary>
        public static Advertise New
        {
            get
            {
                Advertise rtn = new Advertise();
                rtn.DID = null;
                rtn.Body = null;
                rtn.Location = -1;
                rtn.Size = Size.Empty;
                rtn.Deadline = DateTime.MinValue;

                return rtn;
            }
        }

        /// <summary>
        /// 依照傳入的資料列建立新的Advertise執行個體。
        /// </summary>
        /// <param name="dr">資料列。</param>
        /// <returns></returns>
        public static Advertise Instance(DataRow dr)
        {
            Advertise rtn = new Advertise();

            try
            {
                rtn.DID = Model.DB.AnlType<string>(dr["DID"]);
                rtn.Body = Model.DB.AnlType<Image>(dr["Body"]);
                rtn.Location = Model.DB.AnlType<int>(dr["Location"]);
                rtn.Size = Model.DB.AnlType<Size>(dr["Size"]);
                rtn.Deadline = Model.DB.AnlType<DateTime>(dr["Deadline"]);

                return rtn;
            }
            catch(Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailAdvertise,
                    "Advertise類別－建立執行個體Instance(in DataRow)發生錯誤：Advertise設定物件欄位錯誤。\r\n" + e.Message,
                    "");
            }
        }

        private Advertise()
        {

        }
    }
}