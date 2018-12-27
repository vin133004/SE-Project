<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="article.aspx.cs" Inherits="Project_Tpage.WebPage.article" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>
            <p>
                文章標題 ： 
                    <asp:label ID="Tittle" Font-Size="Medium"  Width="500" Height="60" TextMode="SingleLine" runat="server" ></asp:label>
                    <asp:Button ID="Setting" Text="點讚" Width="100" Height="60" runat="server" OnClick="Good_Click"/>
                
                    <br />   
            </p>
            <p>
                文章內容 ： <br />
                <asp:label ID="Content" runat="server" Width="1000" Height="500" /><br />
                <asp:Button ID="Back" runat="server" Width="100" Height="60" Text="返回" OnClick="Back_Click"/>  
               <br /> 
                <h3>留言:</h3>
                 <asp:TextBox ID="TextBox1" runat="server" Width="500" Height="60" TextMode="MultiLine" /><br />
                <asp:Button ID="Send" runat="server" Width="100" Height="60" Text="送出留言"  OnClick="Send_Click"/>
            </p>
            </h1>
        </div>
    </form>
</body>
</html>
