<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project_Tpage.WebPage.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
    <body id="color" runat="server" style="background-color:lightblue;">
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="Title" runat="server" Height="75" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="XX-Large" Text="我的首頁" /><br/>   
            <asp:Button ID="btnSet" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Height="40" BorderStyle="None" Text="設定" OnClick="btnset_Click"/>
            <asp:TextBox ID="searchText" runat="server"  Font-Names="微軟正黑體" Height="35" Width="400" Font-Size="Large" Font-Bold="true" />
            <asp:Button ID="btnSearch" runat="server" Height="40" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="查詢" OnClick="btnSearch_Click" />
             <asp:Label ID="searchresult" Font-Bold="true" Font-Names="微軟正黑體" runat="server" Height="35" Width="100" Font-Size="Large" Text="查無此人" Visible="false" /><br/>
            <asp:Button ID="btnBoard" runat="server" Width="75" Height="75" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="進入" OnClick="btnboard_Click" />
            <asp:ListBox ID="ListOfBoard" runat="server" Font-Size="X-Large" AutoPostBack="false" Width="500" Rows="10" OnSelectedIndexChanged="SelectBoard">
            </asp:ListBox>
            <asp:Image ID="ADimage" runat="server" Height="270" Width="500" ImageUrl="./pictures/2p3o0003noq07q391981.jpg" />
            
            <br />
            
            <asp:Button ID="btnNew" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="申請新版" OnClick="btnNew_Click"/>
            <asp:Label ID="blank1" runat="server" Width="100" Height="20" Text="" />
            <asp:Button ID="btnCard" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="抽卡" OnClick="btnCard_Click"/>
            <asp:Label ID="cardInfo" runat="server" Width="200" Height="50" Font-Names="微軟正黑體" Font-Size="X-Large" Text="" />
            <asp:Button ID="btnAD" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="購買廣告" OnClick="btnAD_Click" />           
            <asp:Label ID="blank2" runat="server" Width="10" Height="20" Text="" />
            <asp:ListBox ID="inviteList" runat="server" Width="100" Font-Names="微軟正黑體" Font-Size="Large" OnSelectedIndexChanged="inviteList_SelectedIndexChanged"></asp:ListBox>
            <asp:Button ID="btnYes" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Height="40" BorderStyle="None" Text="接受" OnClick="btnYes_Click"/>
            <asp:Button ID="btnNo" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Height="40" BorderStyle="None" Text="拒絕" OnClick="btnNo_Click"/>  
        </center>
    </form>
</body>
</html>
