using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Project_Tpage.Class
{
    /// <summary>
    /// 代表一個社交關係的群集。
    /// </summary>
    public interface IRelationship
    {
        List<string> Members { get; set; }
    }

    /// <summary>
    /// 一個朋友圈的集合。
    /// </summary>
    public class FriendGroup : IRelationship
    {
        /// <summary>
        /// 代表此朋友圈的中心。
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 此群集的成員。以帳號識別碼儲存。
        /// </summary>
        public List<string> Members { get; set; }
        /// <summary>
        /// 儲存朋友名稱的集合。
        /// </summary>
        public List<string> MembersName { get; private set; }

        public FriendGroup(string p_Owner)
        {
            Members = new List<string>();
            MembersName = new List<string>();
            Owner = p_Owner;
        }

        public FriendGroup()
        {
            Members = new List<string>();
            MembersName = new List<string>();
            Owner = null;
        }

        public void UpdateMembersName()
        {
            if (Members == null || Members.Count == 0) return;

            List<DataRow> dr;
            using (DataTable dt = SQLS.GetSqlData(Model.DB_Conn,
                "SELECT UID, NickName, RealName FROM " + Model.DB_UserData_TableName + ""))
            {
                dr = Enumerable.Where(Enumerable.Cast<DataRow>(dt.Rows)
                    , x => Members.Contains(x["UID"])).ToList();
            }

            MembersName = Enumerable.Repeat("", Members.Count).ToList();
            for (int i = 0; i < Members.Count; i++)
            {
                DataRow drr = dr.Where(x => (string)x["UID"] == Members[i]).ToList()[0];
                MembersName[i] = (string)(drr["NickName"] is DBNull ? drr["RealName"] : drr["NickName"]);
            }
        }
    }

    /// <summary>
    /// 代表一個社交關係的團體。
    /// </summary>
    public abstract class RelationshipGroup : IRelationship
    {
        /// <summary>
        /// 成員。
        /// </summary>
        public List<string> Members { get; set; }

        /// <summary>
        /// 團體識別碼。
        /// </summary>
        public string GID { get; set; }

        /// <summary>
        /// 團體名稱。
        /// </summary>
        public string Groupname { get; set; }

        /// <summary>
        /// 所有文章的集合。
        /// </summary>
        public List<string> Articles { get; protected set; }
        /// <summary>
        /// 此團體的管理者。
        /// </summary>
        public List<string> Admin { get; protected set; }

        /// <summary>
        /// 看板內的主題
        /// </summary>
        public List<string> Topic { get; protected set; }
        /// <summary>
        /// 主題看板的版主。
        /// </summary>
        public List<string> BoardAdmin { get; protected set; }


        /// <summary>
        /// 管理此團體的管理者。
        /// </summary>
        public virtual void Admin_Manage()
        {

        }

        /// <summary>
        /// 管理主題看板。
        /// </summary>
        public virtual void Topic_Manage()
        {

        }

        /// <summary>
        /// 管理看板板主。
        /// </summary>
        public virtual void BoardAdmin_Manage()
        {

        }

        public abstract void Manage_Members();

        public RelationshipGroup()
        {
            Admin = new List<string>();
            Topic = new List<string>();
            BoardAdmin = new List<string>();
            Articles = new List<string>();
            Members = new List<string>();
        }
    }

    /// <summary>
    /// 代表一個班級團體。
    /// </summary>
    public class ClassGroup : RelationshipGroup
    {
        /// <summary>
        /// 班級系級。
        /// </summary>
        public string ClassName { get; set; }

        public ClassGroup() : base()
        {

        }

        /// <summary>
        /// 從資料庫中提取班級團體的資料。
        /// </summary>
        /// <param name="p_GID">團體識別碼。</param>
        /// <returns></returns>
        public static ClassGroup Get_FromDB(string p_GID)
        {
            //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
            DataTable dt = SQLS.GetSqlData(Model.DB_Conn, "SELECT * FROM " + Model.DB_ClassGroupData_TableName 
                + " WHERE GID = " + p_GID);

            if (dt.Rows.Count == 0)
            {
                throw new Exception("無此班級。GID : " + p_GID);
            }
            else
            {
                DataRow dr = dt.Rows[0];
                ClassGroup rtn = new ClassGroup();
                rtn.GID = (string)dr["GID"];
                rtn.Groupname = (string)dr["GroupName"];
                rtn.ClassName = (string)dr["ClassName"];
                rtn.Members = ((string)dr["Members"]).Split(',').ToList();
                rtn.Admin = ((string)dr["Admin"]).Split(',').ToList();
                rtn.BoardAdmin = ((string)dr["BoardAdmin"]).Split(',').ToList();
                rtn.Topic = ((string)dr["Topic"]).Split(',').ToList();
                rtn.Articles = ((string)dr["Articles"]).Split(',').ToList();

                return rtn;
            }
        }

        /// <summary>
        /// 將班級團體資料存入資料庫。傳回新的GID。
        /// </summary>
        /// <param name="p_cg">班級團體。</param>
        public static string Set_ToDB(ClassGroup p_cg)
        {
            using (SqlConnection icn = SQLS.OpenSqlConn(Model.DB_Conn))
            {
                //若帳號已存在，為修改帳號資料的更新。
                if (SQLS.IsExist(Model.DB_Conn, Model.DB_ClassGroupData_TableName, "GID", p_cg.GID))
                {
                    SQLS.ExeSqlCommand(icn, string.Format(@"UPDATE " + Model.DB_ClassGroupData_TableName + @"
                    SET GID = {0}, 
                    GroupName = {1}, 
                    ClassName = {2}, 
                    Members = {3}, 
                    Admin = {4}, 
                    BoardAdmin = {5}, 
                    Topic = {6}, 
                    Articles = {7}, 
                    WHERE GID = {0}"
                        , SQLS.Type(p_cg.GID)
                        , SQLS.Type(p_cg.Groupname, true)
                        , SQLS.Type(p_cg.ClassName, true)
                        , SQLS.Type(p_cg.Members)
                        , SQLS.Type(p_cg.Admin)
                        , SQLS.Type(p_cg.BoardAdmin)
                        , SQLS.Type(p_cg.Topic, true)
                        , SQLS.Type(p_cg.Articles)));
                }
                else//否則為新增帳號資料的更新。
                {
                    SqlCommand ism = new SqlCommand(@"SELECT MAX(GID) FROM " + Model.DB_ClassGroupData_TableName, icn);
                    string nextgid = (string)ism.ExecuteScalar();

                    SQLS.ExeSqlCommand(icn, @"UPDATE " + Model.DB_ClassGroupData_TableName + " SET GID = '" +
                        (int.Parse(nextgid) + 1).ToString().PadLeft(10, '0') + "' WHERE GID = '"
                        + nextgid + "'");

                    SQLS.ExeSqlCommand(icn, string.Format(@"INSERT INTO " + Model.DB_ClassGroupData_TableName + @" 
                    (GID, GroupName, ClassName, Members, Admin, BoardAdmin, Topic, Articles)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})"
                        , SQLS.Type(p_cg.GID = nextgid)
                        , SQLS.Type(p_cg.Groupname, true)
                        , SQLS.Type(p_cg.ClassName, true)
                        , SQLS.Type(p_cg.Members)
                        , SQLS.Type(p_cg.Admin)
                        , SQLS.Type(p_cg.BoardAdmin)
                        , SQLS.Type(p_cg.Topic, true)
                        , SQLS.Type(p_cg.Articles)));
                }
                SQLS.CloseSqlConn(icn);
            }
            return p_cg.GID;
        }

        public override void Manage_Members()
        {

        }
    }


    /// <summary>
    /// 代表一個家族團體。
    /// </summary>
    public class FamilyGroup : RelationshipGroup
    {
        public FamilyGroup() : base()
        {

        }

        /// <summary>
        /// 從資料庫中提取家族團體的資料。
        /// </summary>
        /// <param name="p_GID">家族識別碼。</param>
        /// <returns></returns>
        public static FamilyGroup Get_FromDB(string p_GID)
        {
            //從資料庫查詢此使用者。未找到則擲回例外(包含錯誤訊息(無此帳號、密碼錯誤))。
            DataTable dt = SQLS.GetSqlData(Model.DB_Conn, "SELECT * FROM " + Model.DB_FamilyGroupData_TableName
                + " WHERE GID = " + p_GID);

            if (dt.Rows.Count == 0)
            {
                throw new Exception("無此家族。GID : " + p_GID);
            }
            else
            {
                DataRow dr = dt.Rows[0];
                FamilyGroup rtn = new FamilyGroup();
                rtn.GID = (string)dr["GID"];
                rtn.Groupname = (string)dr["GroupName"];
                rtn.Members = ((string)dr["Members"]).Split(',').ToList();
                rtn.Admin = ((string)dr["Admin"]).Split(',').ToList();
                rtn.BoardAdmin = ((string)dr["BoardAdmin"]).Split(',').ToList();
                rtn.Topic = ((string)dr["Topic"]).Split(',').ToList();
                rtn.Articles = ((string)dr["Articles"]).Split(',').ToList();

                return rtn;
            }
        }

        /// <summary>
        /// 將家族團體資料存入資料庫。傳回新的GID。
        /// </summary>
        /// <param name="p_cg">家族團體。</param>
        public static string Set_ToDB(ClassGroup p_cg)
        {
            using (SqlConnection icn = SQLS.OpenSqlConn(Model.DB_Conn))
            {
                //若帳號已存在，為修改帳號資料的更新。
                if (SQLS.IsExist(Model.DB_Conn, Model.DB_FamilyGroupData_TableName, "GID", p_cg.GID))
                {
                    SQLS.ExeSqlCommand(icn, string.Format(@"UPDATE " + Model.DB_FamilyGroupData_TableName + @"
                    SET GID = {0}, 
                    GroupName = {1}, 
                    Members = {2}, 
                    Admin = {3}, 
                    BoardAdmin = {4}, 
                    Topic = {5}, 
                    Articles = {6}, 
                    WHERE GID = {0}"
                        , SQLS.Type(p_cg.GID)
                        , SQLS.Type(p_cg.Groupname, true)
                        , SQLS.Type(p_cg.Members)
                        , SQLS.Type(p_cg.Admin)
                        , SQLS.Type(p_cg.BoardAdmin)
                        , SQLS.Type(p_cg.Topic, true)
                        , SQLS.Type(p_cg.Articles)));
                }
                else//否則為新增帳號資料的更新。
                {
                    SqlCommand ism = new SqlCommand(@"SELECT MAX(GID) FROM " + Model.DB_FamilyGroupData_TableName, icn);
                    string nextgid = (string)ism.ExecuteScalar();

                    SQLS.ExeSqlCommand(icn, @"UPDATE " + Model.DB_FamilyGroupData_TableName + " SET GID = '" +
                        (int.Parse(nextgid) + 1).ToString().PadLeft(10, '0') + "' WHERE GID = '"
                        + nextgid + "'");

                    SQLS.ExeSqlCommand(icn, string.Format(@"INSERT INTO " + Model.DB_FamilyGroupData_TableName + @" 
                    (GID, GroupName, Members, Admin, BoardAdmin, Topic, Articles)
                    VALUES 
                    ({0}, {1}, {2}, {3}, {4}, {5}, {6})"
                        , SQLS.Type(p_cg.GID = nextgid)
                        , SQLS.Type(p_cg.Groupname, true)
                        , SQLS.Type(p_cg.Members)
                        , SQLS.Type(p_cg.Admin)
                        , SQLS.Type(p_cg.BoardAdmin)
                        , SQLS.Type(p_cg.Topic, true)
                        , SQLS.Type(p_cg.Articles)));
                }
                SQLS.CloseSqlConn(icn);
            }
            return p_cg.GID;
        }

        public override void Manage_Members()
        {

        }
    }
}