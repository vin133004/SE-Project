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
    /// 代表一個看板版主的資訊，包含版主UID以及看板名稱。
    /// </summary>
    public class BoardAdminPair
    {
        public string Admin { get; }
        public string Board { get; }

        public static BoardAdminPair New(string s, string ss)
        {
            return new BoardAdminPair(s, ss);
        }
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
            ClassName = "";
        }

        public ClassGroup(DataRow dr) : base()
        {
            try
            {
                GID = Model.DB.AnlType<string>(dr["GID"]);
                Groupname = Model.DB.AnlType<string>(dr["GroupName"]);
                ClassName = Model.DB.AnlType<string>(dr["ClassName"]);
                Members = Model.DB.AnlType<List<string>>(dr["Members"]);

                Admin = Model.DB.AnlType<List<string>>(dr["Admin"]);
                BoardAdmin = Model.DB.AnlType<List<BoardAdminPair>>(dr["BoardAdmin"]);
                Board = Model.DB.AnlType<List<string>>(dr["Topic"]);

                MemberRequestQueue = Model.DB.AnlType<List<string>>(dr["MemberRequest"]);
                BoardRequestQueue = Model.DB.AnlType<List<string>>(dr["BoardRequest"]);
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
            else if (MemberRequestQueue.Contains(usr.Userinfo.UID))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "ClassGroup類別－Add_Members()發生例外：已存在於要求佇列內。",
                    "你已申請加入該班級");
            else
            {
                MemberRequestQueue.Add(usr.Userinfo.UID);
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
                GID = Model.DB.AnlType<string>(dr["GID"]);
                Groupname = Model.DB.AnlType<string>(dr["GroupName"]);
                Members = Model.DB.AnlType<List<string>>(dr["Members"]);

                Admin = Model.DB.AnlType<List<string>>(dr["Admin"]);
                BoardAdmin = Model.DB.AnlType<List<BoardAdminPair>>(dr["BoardAdmin"]);
                Board = Model.DB.AnlType<List<string>>(dr["Topic"]);

                MemberRequestQueue = Model.DB.AnlType<List<string>>(dr["MemberRequest"]);
                BoardRequestQueue = Model.DB.AnlType<List<string>>(dr["BoardRequest"]);
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
            else if (MemberRequestQueue.Contains(usr.Userinfo.UID))
                throw new ModelException(
                    ModelException.Error.JoinGroupFail,
                    "FamilyGroup類別－Add_Members()發生例外：已存在於要求佇列內。",
                    "你已申請加入該家族");
            else
            {
                MemberRequestQueue.Add(usr.Userinfo.UID);
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
        public List<string> Board { get; set; }
        /// <summary>
        /// 主題看板的版主。
        /// </summary>
        public List<BoardAdminPair> BoardAdmin { get; set; }
        /// <summary>
        /// 要求加入團體的使用者佇列。
        /// </summary>
        public List<string> MemberRequestQueue { get; set; }
        /// <summary>
        /// 申請主題看板的佇列。
        /// </summary>
        public List<string> BoardRequestQueue { get; set; }     //format:   uid+"@"+board
                                                                //ex.       "0000000001@Topic"


        /// <summary>
        /// 管理員接受一名使用者的加入要求。
        /// </summary>
        /// <param name="usr">接受的使用者。</param>
        public void Members_AllowAdd(string uid)
        {
            if (MemberRequestQueue.Contains(uid)) MemberRequestQueue.RemoveAll(x => x.Equals(uid));

            if (Members.Contains(uid)) return;

            Members.Add(uid);
            this.DBSet();

            User usr = Model.DB.Get<User>(uid);
            if (!usr.Groups.Contains(this))
            {
                usr.Groups.Add(this);
                Model.DB.Set<User>(usr);
            }
        }
        /// <summary>
        /// 移除一名成員。若傳入的使用者參數並非此團體成員，不會執行。
        /// </summary>
        /// <param name="usr">要移除的使用者。</param>
        public void Members_Remove(string uid)
        {
            int indx = Members.IndexOf(uid);
            if (indx == -1) return;

            Members.RemoveAt(indx);
            this.DBSet();

            User usr = Model.DB.Get<User>(uid);
            if (usr.Groups.Contains(this))
            {
                usr.Groups.Remove(this);
                Model.DB.Set<User>(usr);
            }
        }
        /// <summary>
        /// 將一名成員晉升為管理員。若傳入的使用者參數並非此團體成員，不會執行。
        /// </summary>
        /// <param name="usr">目標使用者。</param>
        public void Admin_Add(string uid)
        {
            if (!Members.Contains(uid) || Admin.Contains(uid)) return;

            Admin.Add(uid);
            this.DBSet();
        }
        /// <summary>
        /// 將一名成員撤銷掉管理員。若傳入的使用者參數並非此團體成員，不會執行。
        /// </summary>
        /// <param name="usr">目標使用者。</param>
        public void Admin_Remove(string uid)
        {
            int indx = Admin.IndexOf(uid);
            if (indx == -1) return;

            Admin.RemoveAt(indx);
            this.DBSet();
        }
        /// <summary>
        /// 將一名成員晉升為版主。若傳入的使用者參數並非此團體成員，不會執行。若無此看版，不會執行。
        /// </summary>
        /// <param name="usr">目標使用者。</param>
        /// <param name="board">目標看板。</param>
        public void BoardAdmin_Add(string uid, string board)
        {
            if (!Members.Contains(uid) || !Board.Contains(board)
                || BoardAdmin.Where(x => x.Admin == uid && x.Board == board).Count() > 0) return;
            
            BoardAdmin.Add(BoardAdminPair.New(uid, board));
            this.DBSet();
        }
        /// <summary>
        /// 將一名成員撤銷掉管理員。若傳入的使用者參數並非此團體成員，不會執行。
        /// </summary>
        /// <param name="usr">目標使用者。</param>
        public void BoardAdmin_Remove(string uid, string board)
        {
            if (!Board.Contains(board)) return;

            List<int> indx = BoardAdmin.Select((x, indxe) => (x.Admin == uid && x.Board == board) ? -1 : indxe)
                .Where(y => y >= 0).ToList();
            if (indx.Count <= 0) return;

            BoardAdmin.RemoveAt(indx[0]);
            this.DBSet();
        }
        /// <summary>
        /// 使用者申請一個新的主題看板。
        /// </summary>
        /// <param name="uid">申請的使用者。</param>
        /// <param name="board">看板名稱。</param>
        public void Board_Add(string uid, string board)
        {
            if (Board.Contains(board))
                throw new ModelException(
                    ModelException.Error.ApplyForBoardFail,
                    "RelationshipGroup類別－Board_Add(string)發生例外：此看板已存在。",
                    "已有此看板。");
            else if (BoardRequestQueue.Contains(uid + "@" + board))
                throw new ModelException(
                    ModelException.Error.ApplyForBoardFail,
                    "RelationshipGroup類別－Board_Add(string)發生例外：已存在於要求佇列內。",
                    "你已申請該看板，等候審核。");
            else
            {
                BoardRequestQueue.Add(uid + "@" + board);
                this.DBSet();
            }
        }
        /// <summary>
        /// 管理員接受一個看板申請的要求。
        /// </summary>
        /// <param name="board">申請的看板。</param>
        public void Board_AllowAdd(string board)
        {
            try
            {
                BoardRequestQueue.RemoveAll(x => x.Split('@')[1] == board);
            }
            catch(IndexOutOfRangeException)
            {
                BoardRequestQueue.RemoveAll(x => !x.Contains("@"));
                BoardRequestQueue.RemoveAll(x => x.Split('@')[1] == board);
            }

            if (Board.Contains(board)) return;

            Board.Add(board);
            this.DBSet();
        }
        /// <summary>
        /// 修改一個看板的名稱。
        /// </summary>
        /// <param name="board_old">看板的舊名稱。</param>
        /// <param name="board_new">看板的新名稱。</param>
        public void Board_Modify(string board_old, string board_new)
        {
            int position = Board.IndexOf(board_old);
            if (position == -1) return;

            Board[position] = board_new;
            this.DBSet();
        }
        /// <summary>
        /// 將一個看板移除。將原本屬於此看板的文章轉移至團體之下。
        /// </summary>
        /// <param name="board">要移除的看板。</param>
        public void Board_Remove(string board)
        {
            int position = Board.IndexOf(board);
            if (position == -1) return;

            Board.RemoveAt(position);
            this.DBSet();

            List<Article> colle = Enumerable.Cast<DataRow>(Model.DB.GetSqlData(string.Format("SELECT * FROM {0} WHERE OfBoard = {1}", 
                Model.DB.DB_ArticleData_TableName, board)).Rows).Select(x => new Article(x)).ToList();
            foreach (Article art in colle)
            {
                art.OfBoard = "";
                Model.DB.Set<Article>(art);
            }
        }


        private void DBSet()
        {
            if (this is ClassGroup)
                Model.DB.Set<ClassGroup>(this);
            else
                Model.DB.Set<FamilyGroup>(this);
        }

        /// <summary>
        /// 使用者嘗試加入團體，將加入要求佇列。
        /// </summary>
        /// <param name="usr">要求的使用者。</param>
        public abstract void Members_Add(User usr);
        public RelationshipGroup()
        {
            GID = null;

            Groupname = "";
            Admin = new List<string>();
            Board = new List<string>();
            BoardAdmin = new List<BoardAdminPair>();
            Members = new List<string>();

            MemberRequestQueue = new List<string>();
            BoardRequestQueue = new List<string>();
        }
    }
}