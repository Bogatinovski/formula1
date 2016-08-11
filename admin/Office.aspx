<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Админ</title>
    <link href="style/primer.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 165px;
        }
    </style>
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
            <h3>Админ</h3>
    <form id="form1" runat="server">
    <div>
        
        <table class="auto-style1 tabela">
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="DodadiDelovi.aspx">Додади делови</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="Grupa.aspx">Група</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="Komentari.aspx">Коментари</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="ListaCekanje.aspx">Листа на чекање</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="NivoIme.aspx">Ниво име</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="Objekt_tip.aspx">Објект тип</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="Personal_tip.aspx">Персонал тип</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="SetupDelovi.aspx">Сетап делови</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="TipoviGumi.aspx">Типови гуми</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="TreningVozac.aspx">Тренинг возач</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="Trka.aspx">Трка</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="Vozaci.aspx">Возачи</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton13" runat="server" PostBackUrl="VremenskiUslovi.aspx">Временски услови</asp:LinkButton>
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
