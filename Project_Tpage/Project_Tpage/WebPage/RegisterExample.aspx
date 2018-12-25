<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterExample.aspx.cs" Inherits="Project_Tpage.WebPage.RegisterExample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
			<asp:Label runat="server" ID="lblTitle" Text="Register" Font-Names="微軟正黑體" Font-Size="Larger"></asp:Label>
        </div>
		<div>
			<asp:Label runat="server" ID="lbl1" Text="帳號" Width="200"></asp:Label><asp:TextBox runat="server" ID="tbx1"></asp:TextBox>
		</div>
		<div>
			<asp:Label runat="server" ID="lbl2" Text="密碼" Width="200"></asp:Label><asp:TextBox runat="server" ID="tbx2"></asp:TextBox>
		</div>
		<div>
			<asp:Label runat="server" ID="lbl3" Text="電子郵件" Width="200"></asp:Label><asp:TextBox runat="server" ID="tbx3"></asp:TextBox>
		</div>
		<div>
			<asp:Label runat="server" ID="lbl4" Text="學號" Width="200"></asp:Label><asp:TextBox runat="server" ID="tbx4"></asp:TextBox>
		</div>
		<div>
			<asp:Button runat="server" ID="btn1" Text="Register" Font-Names="微軟正黑體" Font-Size="Larger" OnClick="btn1_Click"/>
		</div>
    </form>
</body>
</html>
