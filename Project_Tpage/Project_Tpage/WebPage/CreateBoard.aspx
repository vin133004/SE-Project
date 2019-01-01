<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateBoard.aspx.cs" Inherits="Project_Tpage.WebPage.CreateBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="color" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="nameLabel" runat="server" Text="看板名稱：" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" />
        <asp:TextBox ID="boardName" runat="server" Height="30" Width="300" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體"/><br />
        <asp:Label ID="blank1" runat="server" /><br />
        <asp:Label ID="infoLabel" runat="server" Text="公開他人瀏覽：" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="blank2" runat="server" /><br />
        <asp:RadioButtonList ID="boardInfo" runat="server" AutoPostBack="true" Font-Size="Large" RepeatColumns="2" OnSelectedIndexChanged="boardInfo_SelectedIndexChanged">
            <asp:ListItem>公開(任何人都可察看文章)</asp:ListItem>
            <asp:ListItem>非公開(僅限內部成員文章)</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="blank3" runat="server" /><br />
        <asp:Label ID="Announce" runat="server" Text="以ID邀請用戶加入此看板：" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="blank4" runat="server" /><br />
        <asp:TextBox ID="peopleName" runat="server" Height="30" Width="300" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體"/>
        <asp:Button ID="btnInvite" runat="server" Text="邀請" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" BorderStyle="None" OnClick="btnInvite_Click"/>
        <asp:Button ID="btnNoInvite" runat="server" Text="取消邀請" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" BorderStyle="None" OnClick="btnNoInvite_Click"/>
        <asp:Label ID="inviteInfo" runat="server" Text="" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" Visible="false" /><br />
        <asp:Label ID="blank5" runat="server" /><br />  
        <asp:Label ID="listLabel" runat="server" Text="已邀請對象：" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" />
        <asp:DropDownList ID="inviteList" runat="server" Width="200" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="blank6" runat="server" /><br />
        <asp:Button ID="btnBack" runat="server" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="120" Height="40" BorderStyle="None" Text="返回首頁" OnClick="btnBack_Click"/>
        <asp:Label ID="blank7" runat="server" Width="50" />
        <asp:Button ID="btnPo" runat="server" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="80" Height="40" BorderStyle="None" Text="申請" OnClick="btnPo_Click"/>
        <asp:Label ID="lblError" runat="server" Text="" Font-Size="X-Large" Font-Bold="true" Font-Names="微軟正黑體" Visible="false" /><br />
    </form>
</body>
</html>