<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editor.aspx.cs" Inherits="Project_Tpage.WebPage.editor" %>

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
                    <asp:TextBox ID="Tittle" Font-Size="Medium"  Width="500" Height="60" TextMode="SingleLine" runat="server" ></asp:TextBox>
                    <asp:Button ID="Setting" Text="文章設定" Width="100" Height="60" runat="server" />
                    <br />   
            </p>
            <p>
                文章內容 ： <br />
                <asp:TextBox ID="Content" runat="server" Width="1000" Height="500" TextMode="MultiLine" /><br />
                <asp:Button ID="Back" runat="server" Width="100" Height="60" Text="返回/放棄" />  
                <asp:Button ID="Send" runat="server" Width="100" Height="60" Text="送出"  />

            </p>
            </h1>
        </div>
    </form>
</body>
</html>
