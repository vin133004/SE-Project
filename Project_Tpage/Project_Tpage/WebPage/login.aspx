<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Project_Tpage.WebPage.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:Image ID="Image1" runat="server" Width="800" Height="800"/>
    <h3>Account</h3>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br />   
    <h3>password</h3>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br />   
     <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click"/>
     <asp:Button ID="Button2" runat="server" Text="Register" OnClick="Button2_Click"/>
        </div>
    </form>
</body>
</html>
