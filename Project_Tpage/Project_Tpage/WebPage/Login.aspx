<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project_Tpage.WebPage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="color" runat="server" style="background-color:lightblue;">
    <form id="form1" runat="server">
		<div style="height:200px; margin-left:30%; margin-right:30%; width:40%;">
			<asp:Panel ID="pa1" runat="server" HorizontalAlign="Center">
				<asp:Label runat="server" ID="lblTitle" Text="Tpage" Font-Names="微軟正黑體" Font-Size="48" Font-Bold="true" ForeColor="#0000EF"></asp:Label>
			</asp:Panel>
		</div>
        <div style="width:20%; margin-left:40%; margin-right:40%;">
			<asp:Panel ID="pa2" runat="server" Width="100%" Height="600px" BackColor="#EFEFEF">
				<div style="height:130px;"></div>
				<div style="margin-left:10%;">
					<asp:Label runat="server" ID="lbl1" Font-Names="微軟正黑體" Font-Size="18" Height="60px">帳號</asp:Label>
				</div>
				<div style="width:60%; margin-left:20%; margin-right:20%; margin-bottom:30px;">
					<asp:TextBox runat="server" ID="tbx1" Width="100%" Height="20px"></asp:TextBox>
				</div>
				<div style="margin-left:10%;">
					<asp:Label runat="server" ID="lbl2" Font-Names="微軟正黑體" Font-Size="18" Height="60px">密碼</asp:Label>
				</div>
				<div style="width:60%; margin-left:20%; margin-right:20%; margin-bottom:30px;">
					<asp:TextBox runat="server" ID="tbx2" Width="100%" Height="20px" TextMode="Password"></asp:TextBox>
				</div>
				<div style="width:50%; margin-left:25%; margin-right:25%; margin-bottom:30px;">
					<asp:Button runat="server" ID="btn1" Text="登入" Width="100%" Font-Size="14" Font-Names="微軟正黑體" OnClick="btn1_Click"/>
				</div>
				<div style="width:50%; margin-left:25%; margin-right:25%; margin-bottom:30px;">
					<asp:Button runat="server" ID="btn2" Text="註冊" Width="100%" Font-Size="14" Font-Names="微軟正黑體" OnClick="btn2_Click"/>
				</div>

			</asp:Panel>
        </div>
    </form>
</body>
</html>