<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AD.aspx.cs" Inherits="Project_Tpage.WebPage.AD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="color" runat="server">
    <form id="form1" runat="server">
        <center>
            <asp:Label ID="IDLabel" runat="server" Width="150" Height="30" Text="用戶ID：" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" />
            <asp:Label ID="IDInfo" runat="server" Width="250" Height="30" Text="" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" />
            <br />
            <asp:Label ID="moneyLabel" runat="server" Width="150" Height="30" Text="用戶金額：" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" />
            <asp:Label ID="moneyInfo" runat="server" Width="250" Height="30" Text="" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="Large" />
            <br />
            <asp:Panel ID="panelInfo" runat="server" >
                <h3>若想要置放廣告請依照下列步驟：</h3>
                <h3>1. 選擇想廣告的圖片</h3>
                <h3>2. 挑喜歡的廣告模式</h3>
                <h3>3. 點選確認購買即可</h3>
                <h3>！！返回並不扣除任何手續！！</h3>
                <h3>自動安排播出時間不可決定時段</h3>
            </asp:Panel>        
            <br />
            <asp:Label ID="upInfo" runat="server" Text="上傳檔案" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" /><br/>
            <asp:FileUpload ID="FileTmp" runat="server" Width="300" Height="50" /><br />
            
            <asp:Label ID="costInfo" runat="server" Text="付費方式" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" />
            <asp:RadioButtonList ID="btnList" runat="server" TextAlign="Right">
                <asp:ListItem>5     分鐘/1$</asp:ListItem>
                <asp:ListItem>15    分鐘/2$</asp:ListItem>
                <asp:ListItem>30    分鐘/3$</asp:ListItem>
                <asp:ListItem>1     小時/5$</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnHome" runat="server" Height="40" Width="175" Text="返回首頁" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnHome_Click" />
            <asp:Button ID="btnSend" runat="server" Height="40" Width="175" Text="確認購買" BorderStyle="None" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" OnClick="btnSend_Click" />
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Names="微軟正黑體" Font-Size="X-Large" Visible="false" />
        </center>
    </form>
</body>
</html>
