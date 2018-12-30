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
            <asp:TextBox ID="searchText" runat="server"  Font-Names="微軟正黑體" Height="35" Width="400" Font-Size="Large" Font-Bold="true" />
            <asp:Button ID="btnSearch" runat="server" Height="40" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Text="查詢" OnClick="btnSearch_Click" />
             <asp:Label ID="searchresult" Font-Bold="true" Font-Names="微軟正黑體" runat="server" Height="35" Width="100" Font-Size="Large" Text="查無此人" Visible="true" /><br/>
            <asp:ListBox ID="ListOfBoard" runat="server" Font-Size="X-Large" AutoPostBack="true" Width="500" Rows="10" OnSelectedIndexChanged="SelectBoard">
                <asp:ListItem Value="aaaa">aaa</asp:ListItem>
                <asp:ListItem >bbb</asp:ListItem>
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
            
            <asp:Image ID="AD" runat="server" Height="270" Width="500" ImageUrl="./pictures/2p3o0003noq07q391981.jpg" />
            
            <br />

            <asp:Button ID="btnNew" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="申請新版" OnClick="btnNew_Click"/>
            <asp:Label ID="blank1" runat="server" Width="100" Height="20" Text="" />
            <asp:Button ID="btnCard" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="抽卡" OnClick="btnCard_Click"/>
            <asp:Label ID="cardInfo" runat="server" Width="200" Height="50" Font-Names="微軟正黑體" Font-Size="X-Large" Text="dfds" />
            <asp:Button ID="btnAD" runat="server"  Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Width="150" Height="75" BorderStyle="None" Text="購買廣告" OnClick="btnAD_Click" />           
            <asp:Label ID="blank2" runat="server" Width="10" Height="20" Text="" />
            <asp:Label ID="StyleInfo" runat="server" Width="90" Height="20" Text="顯示模式" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Larger" />
            <asp:DropDownList ID="StyleList" runat="server"  AutoPostBack="true" Font-Names="微軟正黑體" Font-Size="Large" OnSelectedIndexChanged="StyleChanged">
                <asp:ListItem >日間模式</asp:ListItem>
                <asp:ListItem >夜間模式</asp:ListItem>
                 <asp:ListItem  >模式</asp:ListItem>
            </asp:DropDownList>
             <div style="height:0%; margin-left:80%; margin-right:0%; width:20%;">
            <asp:Panel ID="Panel0" runat="server" HorizontalAlign="Center"  BackColor="#EFEFEF">
				<asp:Label runat="server" ID="lblTitle" Text="邀請來自:" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#0000EF"></asp:Label>
                <asp:Label runat="server" ID="who" Text="XXX" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                <br />
                <asp:Button runat="server" ID="yes" Text="接受" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" OnClick="yes_Click" />
                <asp:Button runat="server" ID="no" Text="拒絕" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" />
			</asp:Panel>
               <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center"  BackColor="#EFEFEF" Visible="false">
				<asp:Label runat="server" ID="Label1" Text="邀請來自:" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#0000EF"></asp:Label>
                <asp:Label runat="server" ID="Label2" Text="XXX" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                <br />
                <asp:Button runat="server" ID="Button1" Text="接受" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" OnClick="yes_Click" />
                <asp:Button runat="server" ID="Button2" Text="拒絕" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" />
			</asp:Panel>
                 <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center"  BackColor="#EFEFEF" Visible="false">
				<asp:Label runat="server" ID="Label3" Text="邀請來自:" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#0000EF"></asp:Label>
                <asp:Label runat="server" ID="Label4" Text="XXX" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                <br />
                <asp:Button runat="server" ID="Button3" Text="接受" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" OnClick="yes_Click" />
                <asp:Button runat="server" ID="Button4" Text="拒絕" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" />
			</asp:Panel>
                 <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center"  BackColor="#EFEFEF" Visible="false">
				<asp:Label runat="server" ID="Label5" Text="邀請來自:" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#0000EF"></asp:Label>
                <asp:Label runat="server" ID="Label6" Text="XXX" Font-Names="微軟正黑體" Font-Size="25" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                <br />
                <asp:Button runat="server" ID="Button5" Text="接受" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" OnClick="yes_Click" />
                <asp:Button runat="server" ID="Button6" Text="拒絕" Font-Names="微軟正黑體" Font-Size="12" Font-Bold="true" />
			</asp:Panel>
                </div>        
            <br />  
            <asp:TextBox ID="boardText" runat="server" Height="35" Width="500" Visible="false" />     
        </center>
    </form>
</body>
</html>
