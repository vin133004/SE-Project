<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Project_Tpage.WebPage.Editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="color" style="background-color:lightblue" runat="server">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="la1" Font-Bold="true" Font-Size="X-Large" Text="文章標題：" Font-Names="微軟正黑體" runat="server"></asp:Label>
                    <asp:TextBox ID="Tittle" Font-Size="Medium"  Width="500" Height="30" TextMode="SingleLine" runat="server" ></asp:TextBox>
                 
                    <br />   
            <br />
         <asp:Label ID="la2" Font-Bold="true" Font-Size="X-Large" Text="文章內容：" Font-Names="微軟正黑體" runat="server"></asp:Label>
   <br />
             
                <asp:TextBox ID="Content" runat="server" Width="1000" Height="500" TextMode="MultiLine" /><br />
            <br />
             <br />
                <asp:Button ID="Back" runat="server" Width="200" Height="60" Font-Size="X-Large" Font-Bold="true" Text="返回/放棄" Font-Names="微軟正黑體" OnClick="Back_Click"/>  
             <asp:Label ID="blank2" runat="server" Width="50" Height="20" Text="" />
                <asp:Button ID="Send" runat="server" Width="200" Height="60" Font-Size="X-Large" Font-Bold="true" Text="發布" Font-Names="微軟正黑體"  OnClick="Send_Click"/>

          
        </div>
    </form>
</body>
</html>