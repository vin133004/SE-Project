<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project_Tpage.WebPage.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Height="75" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="我的首頁" /><br/>   
            <asp:TextBox ID="searchText" runat="server" Height="35" Width="800" />
            <asp:Button ID="btnSearch" runat="server" Height="40" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="查詢" OnClick="btnSearch_Click" /><br/>
            
            <asp:ListBox ID="ListOfBoard" runat="server" Font-Size="X-Large" AutoPostBack="true" Width="500" Rows="10" OnSelectedIndexChanged="SelectBoard" >
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
            </asp:ListBox>
            <asp:Image ID="AD" runat="server" Height="300" Width="500" ImageUrl="./pictures/2p3o0003noq07q391981.jpg" />
            <br />

            <asp:Button ID="btnNew" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="申請新版" OnClick="btnNew_Click"/>
            <asp:Label ID="blank1" runat="server" Width="100" Height="20" Text="" />
            <asp:Button ID="btnCard" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="抽卡" OnClick="btnCard_Click"/>
            <asp:Label ID="cardInfo" runat="server" Width="200" Height="50" Font-Names="微軟正黑體" Font-Size="X-Large" Text="" />
            <asp:Button ID="btnAD" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="購買廣告" OnClick="btnAD_Click"/>           
            <asp:Label ID="blank2" runat="server" Width="10" Height="20" Text="" />
            <asp:Label ID="StyleInfo" runat="server" Width="90" Height="20" Text="顯示模式" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Larger" />
            <asp:DropDownList ID="StyleList" runat="server"  AutoPostBack="true" Font-Names="微軟正黑體" Font-Size="Large" OnSelectedIndexChanged="StyleChanged">
                <asp:ListItem>日間模式</asp:ListItem>
                <asp:ListItem>夜間模式</asp:ListItem>
            </asp:DropDownList>
            <br /> 
            
            <asp:TextBox ID="boardText" runat="server" Height="35" Width="500" Visible="false" />
            <asp:Button ID="btnBoard" runat="server" Height="40" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="申請" OnClick="btnBoard_Click" Visible="false" />
            <asp:Label ID="boardInfo" runat="server" Width="100" Height="20" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="" />
        </center>
    </form>
</body>
</html>
