<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chose.aspx.cs" Inherits="Project_Tpage.WebPage.chose" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Button ID="Button1" Text="星座" Width="1000" Height="60" runat="server" OnClick="Board"/> 
           
                    <br />   
            <asp:Button ID="Button2" Text="有趣" Width="1000" Height="60" runat="server" OnClick="Board"/>
                    <br />   
            <asp:Button ID="Button3" Text="黑特" Width="1000" Height="60" runat="server" OnClick="Board"/>
                    <br />   
       <asp:Image ID="Image1" runat="server" Width="1000" Height="100"  />
            <br />
            <asp:Button ID="Button4" Text="群組" Width="333" Height="60" runat="server" OnClick="group"/>
                  <asp:Button ID="Button5" Text="發文" Width="333" Height="60" runat="server" OnClick="artic"/>
                    <asp:Button ID="Button6" Text="設定" Width="333" Height="60" runat="server" OnClick="setting"/>               
    </div>
        <div>
            asdfasfsa
        </div>
    </form>
</body>
</html>
