<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Project_Tpage.WebPage.Setting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Accountlabel" runat="server" Width="10%" Text="帳號：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:Label ID="AccountText" runat="server" Width="10%" Text="" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="Secretlabel" runat="server" Width="10%" Text="密碼：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:TextBox ID="SecretText" runat="server" Width="30%" Height="20px" TextMode="Password"/><br />
        <asp:Label ID="IDLabel" runat="server" Width="10%" Text="學號：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:TextBox ID="IDText" runat="server" Width="30%" Height="20px" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="Maillabel" runat="server" Width="10%" Text="電子郵件：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:TextBox ID="MailText" runat="server" Width="30%" Height="20px"  Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="Genderlabel" runat="server" Width="10%" Text="性別：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:RadioButtonList ID="GenderList" runat="server" RepeatColumns="4" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體">
            <asp:ListItem>秘密</asp:ListItem>
            <asp:ListItem>男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
            <asp:ListItem>第三性</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="RealNameLabel" runat="server"   Width="10%" Text="真實姓名：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:TextBox ID="RealNameText" runat="server" Width="30%" Height="20px"  Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="NickNameLabel" runat="server"   Width="10%" Text="暱稱：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:TextBox ID="NickNameText" runat="server" Width="30%" Height="20px"  Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="coinlabel" runat="server" Width="10%" Text="台科幣：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:Label ID="coinText" runat="server" Width="10%" Text="" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
        <asp:Label ID="ViewStylelabel" runat="server" Width="10%" Text="瀏覽樣式：" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" />
        <asp:RadioButtonList ID="ViewStyleList" runat="server" RepeatColumns="3" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體">
            <asp:ListItem>日間模式</asp:ListItem>
            <asp:ListItem>夜間模式</asp:ListItem>
            <asp:ListItem>第三模式</asp:ListItem>
        </asp:RadioButtonList>
        
        <asp:Button ID="btnBack" runat="server" Height="40" Width="230" Text="返回/放棄" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnBack_Click" />
        <asp:Button ID="btnSend" runat="server" Height="40" Width="230" Text="修改" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnSend_Click" />
        <asp:Label ID="lblError" runat="server" Width="10%" Text="" Font-Bold="true" Font-Size="X-Large" Font-Names="微軟正黑體" /><br />
    </form>
</body>
</html>
