<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="Project_Tpage.WebPage.Article" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Height="100" Width="500" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="文章主題" />
            <asp:Label ID="blank3" runat="server" Width="50"/>
            <asp:Button ID="btnDel" runat="server" Height="50" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" BorderStyle="None" Text="刪除文章" Enabled="false" OnClick="btnDel_Click" />
            <br/>
            <asp:Label ID="Content" runat="server" Height="250" Width="700" Font-Names="微軟正黑體" Font-Size="Larger" Text="文章內容" />
            <br/>
            <asp:Label ID="Blank1" runat="server" Height="75"  />
            <br/>
            <asp:ListBox ID="allMessage" runat="server" Height="200" Width="700" Font-Names="微軟正黑體" Font-Size="Large" >
                <asp:ListItem>留言aaa</asp:ListItem>
                <asp:ListItem>留言bbb</asp:ListItem>
                <asp:ListItem>留言ccc</asp:ListItem>
                <asp:ListItem>留言ddd</asp:ListItem>
            </asp:ListBox>
            <br />
            <asp:Image ID="picLike" runat="server" Height="50" Width="50" ImageUrl="./pictures/Like.jpg" /> 
            <asp:Label ID="numLike" runat="server" Height="50" Width="50" Text="X 15"/>
            <asp:Label ID="Blank2" runat="server" Height="50" Width="500"  />
            <asp:ImageButton ID="btnLike" runat="server" Height="50" Width="50" OnClick="btnLike_Click" ImageUrl="./pictures/Like.jpg" /> 
            <br />     
            <asp:TextBox ID="Message" runat="server" Height="40" Width="600" />
            <asp:Button ID="btnSend" runat="server" Height="50" Width="50" Text="發送" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" OnClick="btnSend_Click" />
             
            <br />
           <asp:Button ID="btnBack" runat="server" Height="40" Width="230" Text="返回看板" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnBack_Click" />
           <asp:Button ID="btnHome" runat="server" Height="40" Width="230" Text="返回首頁" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnHome_Click" />
           <asp:Button ID="btnEdit" runat="server" Height="40" Width="230" Text="編輯文章" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnEdit_Click" Enabled="false"/>
        </center>
    </form>
</body>
</html>
