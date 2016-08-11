<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DodadiDelovi.aspx.cs" Inherits="DodadiDelovi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Додади делови</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
        }
        .auto-style3 {
            width: 118px;
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
            <h3>Додади делови</h3>
    <form id="form1" runat="server">
    <div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Нов дел"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label4" runat="server" Text="Ид:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="Име:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label3" runat="server" Text="Цена:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="labelError" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Додади го делот" Width="128px" />
                </td>
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
