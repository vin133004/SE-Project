<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="Project_Tpage.WebPage.Board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Width="650" Height="75" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="看板名稱" />
            <asp:ImageButton ID="Follow" runat="server" Height="30"  ImageUrl="./pictures/UnFollow.jpg" OnClick="follow_click" /><br/>
            <asp:Button ID="btnarticle" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Width="75" Height="75" BorderStyle="None" Text="進入" OnClick="btnarticle_Click"/>
            <asp:ListBox ID="ListOfArticle" runat="server" Font-Size="X-Large" AutoPostBack="false" Width="700" Rows="10"  OnSelectedIndexChanged="SelectArticle" >
                <asp:ListItem>aaa</asp:ListItem>
                <asp:ListItem>bbb</asp:ListItem>
                <asp:ListItem>ccc</asp:ListItem>
                <asp:ListItem>ddd</asp:ListItem>
                <asp:ListItem>eee</asp:ListItem>
                <asp:ListItem>fff</asp:ListItem>
                <asp:ListItem>ggg</asp:ListItem>
                <asp:ListItem>hhh</asp:ListItem>
                <asp:ListItem>iii</asp:ListItem>
                <asp:ListItem>jjj</asp:ListItem>
                <asp:ListItem>kkk</asp:ListItem>
                <asp:ListItem>lll</asp:ListItem>
            </asp:ListBox><br />
            <asp:Button ID="btnBack" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Width="350" Height="75" BorderStyle="None" Text="返回首頁" OnClick="btnBack_Click"/>
            <asp:Button ID="btnPo" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Width="350" Height="75" BorderStyle="None" Text="發文" OnClick="btnPo_Click"/>
            <br /><asp:Label ID="Blank4" runat="server" /><br />
            <asp:Label ID="peopleLabel" runat="server" Width="300" Height="50" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="看板成員" />
            <asp:DropDownList ID="peopleList" runat="server" Width="300" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large"/>
            <br />
            <asp:TextBox ID="peopleText" runat="server" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" />
            <asp:Button ID="btnInvite" runat="server" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="邀請" OnClick="btnInvite_Click" />
            <asp:Button ID="btnAdmin" runat="server" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="邀請為管理者" OnClick="btnAdmin_Click" />
            <asp:Button ID="btnDel" runat="server" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="刪除" OnClick="btnDel_Click" />
            <asp:Label ID="peopleInfo" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" />
            <br/>
            <asp:Button ID="btnDelBoard" runat="server" Width="300" Height="75" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="刪除看板" OnClick="btnDelBoard_Click"/>
            </center>
    </form>
</body>
</html>
