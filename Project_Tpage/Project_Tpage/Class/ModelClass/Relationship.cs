﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Project_Tpage.Class
{
    /// <summary>
    /// 代表一個看板版主的資訊，包含版主UID以及看板名稱。
    /// </summary>
    public class BoardAdminPair
    {
        public string Admin { get; }
        public string Board { get; }

        public BoardAdminPair(string s, string ss) { Admin = s;Board = ss; }
    }

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

        /// <summary>
        /// 將一個使用者加入此朋友圈並更新朋友名字陣列。
        /// </summary>
        /// <param name="newmem">使用者識別碼。</param>
        public void Member_Add(string newmem)
        {
            try
            {
                DataTable dt = Model.DB.GetSqlData(string.Format("SELECT RealName, NickName FROM {0} WHERE UID = {1}"
                    , Model.DB.DB_UserData_TableName, newmem));
                if (dt.Rows.Count <= 0)
                    throw new Exception("用戶未找到。\r\nUID: " + newmem);

                Members.Add(newmem);


                string nickname = (string)Model.DB.AnlType<string>(dt.Rows[0]["NickName"])
                    , realname = (string)Model.DB.AnlType<string>(dt.Rows[0]["RealName"]);
                MembersName.Add(string.IsNullOrEmpty(nickname) ? (realname ?? "") : nickname);
            }
            catch(Exception e)
            {
                throw new ModelException(
                    ModelException.Error.FriendMemberOperationError,
                    "FriendGroup類別－Member_Add(string)發生例外：" + e.Message,
                    "未知的使用者！");
            }
        }
        /// <summary>
        /// 將一個使用者移出此朋友圈並更新朋友名字陣列。
        /// </summary>
        /// <param name="remmem">使用者識別碼。</param>
        public void Member_Remove(string remmem)
        {
            int indx = Members.IndexOf(remmem);
            if (indx == -1) throw new ModelException(
                    ModelException.Error.FriendMemberOperationError,
                    "FriendGroup類別－Member_Remove(string)發生錯誤：無此成員： " + remmem, 
                    "");

            Members.RemoveAt(indx);
            MembersName.RemoveAt(indx);
        }
        /// <summary>
        /// 將一列使用者識別碼設定為此朋友圈的成員，並更新朋友名字陣列。
        /// </summary>
        /// <param name="lismem">使用者識別碼。</param>
        public void Member_SetAll(List<string> lismem)
        {
            Members = new List<string>(lismem);
            MembersName = new List<string>(Members.Count);

            try
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    DataRow dr = Model.DB.GetSqlData(string.Format("SELECT RealName, NickName FROM {0} WHERE UID = {1}"
                    , Model.DB.DB_UserData_TableName, Members[i])).Rows[0];

                    string nickname = (string)Model.DB.AnlType<string>(dr["NickName"])
                        , realname = (string)Model.DB.AnlType<string>(dr["RealName"]);
                    MembersName.Add(string.IsNullOrEmpty(nickname) ? realname : nickname);
                }
            }
            catch(Exception e)
            {
                throw new ModelException(
                    ModelException.Error.FriendMemberOperationError,
                    "FriendGroup類別－Member_Add(string)發生錯誤：" + e.Message, 
                    "");
            }
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
                BoardAdmin = (List<BoardAdminPair>)Model.DB.AnlType<List<BoardAdminPair>>(dr["BoardAdmin"]);
                Topic = (List<string>)Model.DB.AnlType<List<string>>(dr["Topic"]);

                MemberRequestQueue = (List<User>)Model.DB.AnlType<List<User>>(dr["MemberRequest"]);
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailClassGroup,
                    "ClassGroup類別－建構式ClassGroup(DataRow)發生錯誤：" +
                    "ClassGroup設定物件欄位錯誤。\r\n" + e.Message, 
                    "");
            }
        }

        /// <summary>
        /// 使用者嘗試加入團體，將加入要求佇列。
        /// </summary>
        /// <param name="usr">要求的使用者。</param>
        public override void Members_Add(User usr)
        {
            if (usr.Userinfo.ClassName != ClassName)
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "ClassGroup類別－Add_Members()發生例外：非此班級之使用者不得加入。",
                    "非此班級之使用者不得加入");
            else if (Members.Contains(usr.Userinfo.UID))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "ClassGroup類別－Add_Members()發生例外：此使用者已為班級成員。",
                    "你已為班級成員");
            else if (MemberRequestQueue.Contains(usr))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "ClassGroup類別－Add_Members()發生例外：已存在於要求佇列內。",
                    "你已申請加入該班級");
            else
            {
                MemberRequestQueue.Add(usr);
                Model.DB.Set<ClassGroup>(this);
            }
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
                BoardAdmin = (List<BoardAdminPair>)Model.DB.AnlType<List<BoardAdminPair>>(dr["BoardAdmin"]);
                Topic = (List<string>)Model.DB.AnlType<List<string>>(dr["Topic"]);

                MemberRequestQueue = (List<User>)Model.DB.AnlType<List<User>>(dr["MemberRequest"]);
            }
            catch (Exception e)
            {
                throw new ModelException(
                    ModelException.Error.SetFiledFailFamilyGroup,
                    "FamilyGroup類別－建構式FamilyGroup(DataRow)發生錯誤：" +
                    "FamilyGroup設定物件欄位錯誤。\r\n" + e.Message, 
                    "");
            }
        }

        /// <summary>
        /// 使用者嘗試加入團體，將加入要求佇列。
        /// </summary>
        /// <param name="usr">要求的使用者。</param>
        public override void Members_Add(User usr)
        {
            if (Members.Contains(usr.Userinfo.UID))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "FamilyGroup類別－Add_Members()發生例外：此使用者已為家族成員。",
                    "你已為家族成員");
            else if (MemberRequestQueue.Contains(usr))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "FamilyGroup類別－Add_Members()發生例外：已存在於要求佇列內。",
                    "你已申請加入該家族");
            else
            {
                MemberRequestQueue.Add(usr);
                Model.DB.Set<FamilyGroup>(this);
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
        /// 此團體的管理者。
        /// </summary>
        public List<string> Admin { get; set; }
        /// <summary>
        /// 看板內的主題
        /// </summary>
        public List<string> Topic { get; set; }
        /// <summary>
        /// 主題看板的版主。
        /// </summary>
        public List<BoardAdminPair> BoardAdmin { get; set; }
        /// <summary>
        /// 要求加入團體的使用者佇列。
        /// </summary>
        public List<User> MemberRequestQueue { get; set; }


        /// <summary>
        /// 管理員接受一名使用者的加入要求。
        /// </summary>
        /// <param name="usr">接受的使用者。</param>
        public void Members_AllowAdd(User usr)
        {
            if (MemberRequestQueue.Contains(usr)) MemberRequestQueue.Remove(usr);

            Members.Add(usr.Userinfo.UID);
            if (this is ClassGroup)
                Model.DB.Set<ClassGroup>(this);
            else
                Model.DB.Set<FamilyGroup>(this);

            usr.Groups.Add(this);
            Model.DB.Set<User>(usr);

        }




        /// <summary>
        /// 使用者嘗試加入團體，將加入要求佇列。
        /// </summary>
        /// <param name="usr">要求的使用者。</param>
        public abstract void Members_Add(User usr);
        public RelationshipGroup()
        {
            GID = null;

            Admin = new List<string>();
            Topic = new List<string>();
            BoardAdmin = new List<BoardAdminPair>();
            Members = new List<string>();
        }
    }
}