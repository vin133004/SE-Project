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
            using (DataTable dt = Model.DB.GetSqlData(
                "SELECT UID, NickName, RealName FROM " + Model.DB.DB_UserData_TableName + ""))
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
        public virtual void Admin_Init(List<string> Ivalue)
        {
            Admin = new List<string>(Ivalue);
        }
        /// <summary>
        /// 管理主題看板。
        /// </summary>
        public virtual void Topic_Init(List<string> Ivalue)
        {
            Topic = new List<string>(Ivalue);
        }
        /// <summary>
        /// 管理看板板主。
        /// </summary>
        public virtual void BoardAdmin_Init(List<string> Ivalue)
        {
            BoardAdmin = new List<string>(Ivalue);
        }
        /// <summary>
        /// 初始化文章。
        /// </summary>
        public virtual void Articles_Init(List<string> Ivalue)
        {
            Articles = new List<string>(Ivalue);
        }


        public abstract void Manage_Members();
        public RelationshipGroup()
        {
            GID = null;

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

        public ClassGroup(DataRow dr) : base()
        {
            try
            {
                GID = (string)Model.DB.AnlType<string>(dr["GID"]);
                Groupname = (string)Model.DB.AnlType<string>(dr["GroupName"]);
                ClassName = (string)Model.DB.AnlType<string>(dr["ClassName"]);
                Members = (List<string>)Model.DB.AnlType<List<string>>(dr["Members"]);

                Admin = (List<string>)Model.DB.AnlType<List<string>>(dr["Admin"]);
                BoardAdmin = (List<string>)Model.DB.AnlType<List<string>>(dr["BoardAdmin"]);
                Topic = (List<string>)Model.DB.AnlType<List<string>>(dr["Topic"]);
                Articles = (List<string>)Model.DB.AnlType<List<string>>(dr["Articles"]);
            }
            catch (Exception e)
            {
                throw new Model.ModelException("ClassGroup類別－建構式ClassGroup(DataRow)發生錯誤：" +
                    "ClassGroup設定物件欄位錯誤。\r\n" + e.Message);
            }
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

        public FamilyGroup(DataRow dr) : base()
        {
            try
            {
                GID = (string)Model.DB.AnlType<string>(dr["GID"]);
                Groupname = (string)Model.DB.AnlType<string>(dr["GroupName"]);
                Members = (List<string>)Model.DB.AnlType<List<string>>(dr["Members"]);

                Admin = (List<string>)Model.DB.AnlType<List<string>>(dr["Admin"]);
                BoardAdmin = (List<string>)Model.DB.AnlType<List<string>>(dr["BoardAdmin"]);
                Topic = (List<string>)Model.DB.AnlType<List<string>>(dr["Topic"]);
                Articles = (List<string>)Model.DB.AnlType<List<string>>(dr["Articles"]);
            }
            catch (Exception e)
            {
                throw new Model.ModelException("FamilyGroup類別－建構式FamilyGroup(DataRow)發生錯誤：" +
                    "FamilyGroup設定物件欄位錯誤。\r\n" + e.Message);
            }
        }

        public override void Manage_Members()
        {

        }
    }
}