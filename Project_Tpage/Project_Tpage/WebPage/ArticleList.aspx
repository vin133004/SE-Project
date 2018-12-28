<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Project_Tpage.WebPage.ArticleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Height="75" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="看板名稱" /><br/>
        
            <asp:ListBox ID="ListOfArticle" runat="server" Font-Size="X-Large" AutoPostBack="true" Width="700" Rows="10" OnSelectedIndexChanged="SelectArticle" >
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
            <asp:Button ID="btnBack" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Width="350" Height="75" BorderStyle="None" Text="返回看板" OnClick="btnBack_Click"/>
            <asp:Button ID="btnPo" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Width="350" Height="75" BorderStyle="None" Text="發文" OnClick="btnPo_Click"/>
            <br />
        </center>
    </form>
</body>
</html>
