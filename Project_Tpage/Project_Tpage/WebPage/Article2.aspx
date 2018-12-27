<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Article2.aspx.cs" Inherits="Project_Tpage.WebPage.Article" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Height="100" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="文章主題" />
            <br/>
            <asp:Label ID="Content" runat="server" Height="275" Width="700" Font-Names="微軟正黑體" Font-Size="Larger" Text="文章內容" />
            <br/>
            <asp:Label ID="Blank1" runat="server" Height="75"  />
            <br/>
            <asp:ListBox ID="allMessage" runat="server" Height="225" Width="700" Font-Names="微軟正黑體" Font-Size="Large" >
                <asp:ListItem>留言aaa</asp:ListItem>
                <asp:ListItem>留言bbb</asp:ListItem>
                <asp:ListItem>留言ccc</asp:ListItem>
                <asp:ListItem>留言ddd</asp:ListItem>
            </asp:ListBox>
            <br />
             <asp:Label ID="Blank2" runat="server" Height="50"  />
            <asp:TextBox ID="Message" runat="server" Height="40" Width="650" />
            <asp:Button ID="btnSend" runat="server" Height="50" Width="50" Text="發送" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" OnClick="btnSend_Click" />
            <br />
           <asp:Button ID="btnBack" runat="server" Height="40" Width="700" Text="返回文章" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" OnClick="btnBack_Click" />
        </center>
    </form>
</body>
</html>
