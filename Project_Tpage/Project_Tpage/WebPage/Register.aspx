<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Project_Tpage.WebPage.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="color" runat="server"style="background-color:lightgreen">
    <form id="form1" runat="server">
        <div style="width:48%; margin-left:16%; margin-right:36%; margin-top:5%;">
			<asp:Panel runat="server" ID="pnlMain" Width="100%" Height="800px" BackColor="#EFEFEF">
				<div style="height:100px;"></div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Label runat="server" ID="lbl1" Font-Names="微軟正黑體" Font-Size="18" Width="30%">帳號</asp:Label>
					<asp:TextBox runat="server" ID="tbx1" Width="50%" Height="20px"></asp:TextBox>
				</div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Label runat="server" ID="lbl2" Font-Names="微軟正黑體" Font-Size="18" Width="30%">密碼</asp:Label>
					<asp:TextBox runat="server" ID="tbx2" Width="50%" Height="20px" TextMode="Password"></asp:TextBox>
				</div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Label runat="server" ID="lbl3" Font-Names="微軟正黑體" Font-Size="18" Width="30%">電子郵件</asp:Label>
					<asp:TextBox runat="server" ID="tbx3" Width="50%" Height="20px"></asp:TextBox>
				</div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Label runat="server" ID="lbl4" Font-Names="微軟正黑體" Font-Size="18" Width="30%">學號</asp:Label>
					<asp:TextBox runat="server" ID="tbx4" Width="50%" Height="20px"></asp:TextBox>
				</div>
				<div style="height:100px; margin-left:10%; margin-top:30px;">
					<asp:Label runat="server" ID="lblError" Visible="false" Font-Names="微軟正黑體" Font-Size="18" ForeColor="#FF0000" Text=""></asp:Label>
				</div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Button runat="server" ID="btn1" Text="註冊" Width="30%" Font-Names="微軟正黑體" Font-Size="18" OnClick="btn1_Click"/>
				</div>
				<div style="margin-left:10%; margin-top:30px; margin-right:10%; margin-bottom:30px;">
					<asp:Button runat="server" ID="btn2" Text="返回" Width="30%" Font-Names="微軟正黑體" Font-Size="18" OnClick="btn2_Click"/>
				</div>
			</asp:Panel>
        </div>
    </form>
</body>
</html>
