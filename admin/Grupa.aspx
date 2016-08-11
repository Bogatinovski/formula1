<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grupa.aspx.cs" Inherits="TreningVozac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Групи</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 125px;
        }
        .auto-style3 {
            width: 125px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
    </style>
    <link href="style/primer.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
</head>
<body>
    <div id="container">
        <div id="banner">
            <img id="logo" src="images/logo.jpg" />
            <div id="ime">Формула <span style="color: red;">1</span> Менаџер</div>
            <img id="car" src="images/car-1.jpg" />
        </div>
        <div class="menu2">
            <a href="Office.aspx">Почетна</a>
            <a href="#2">Правила на играта</a>
            <a href="#3">За Нас</a>
            <a href="MenadzerPodatoci.aspx">Профил</a>
            <a href="Login.aspx">Одјави се</a>
            <a class="dummy"></a>
        </div>
        <div id="status-bar">
            
        </div>
        <div id="main">
            <h3>Групи</h3>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"  Width="237px" CssClass="tabela">
            <Columns>
                <asp:BoundField DataField="grupa_broj" HeaderText="Група број" />
                <asp:BoundField DataField="grupa_nivo" HeaderText="Група ниво" />
                <asp:CommandField DeleteText="Избриши" HeaderText="Избриши" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
   
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Група број:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Група ниво:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Додади" />
            <asp:Label ID="labelError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
 
            </div>
    </form>
      </div>
        <div id="bottom">
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>
</body>
</html>
