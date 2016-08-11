<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenadzerPodatoci.aspx.cs" Inherits="MenadzerPodatoci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/Objekti.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <style>
        #Button1 {
    background-image: url(../images/bg1.gif);
    width: 220px;
    height: 30px;
    border: 1px solid #333;
    text-transform: uppercase;
    color: #fff;
    padding: 5px 5px;
    font-weight: bold;
    margin: 15px auto;
}

    #Button1:active {
        border: 1px solid #fff;
    }

    #Button1:disabled {
        background: #333;
        border: 1px solid #000;
    }
    .auto-style1 tr td:nth-child(1){
        text-align: right;
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
            <div id="budget">
                Буџет:
                <asp:Label ID="lblBudzetStatus" runat="server" Text=""></asp:Label>
            </div>
            <div id="loggedManager">
                Најaвен менаџер:
                <asp:Label ID="lblManagerStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div id="main">
            <h3>Профил на возач</h3>
            <form id="form1" runat="server">
                <div>

                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style3">&nbsp;</td>
                            <td class="auto-style4">Податоци за менаџер</td>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label1" runat="server" Text="Име:"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label2" runat="server" Text="Презиме:"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label3" runat="server" Text="Слика:"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label4" runat="server" Text="Лозинка"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox4" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label7" runat="server" Text="Потврда за лозинка:"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox7" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </td>
                            <td class="auto-style5">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox4" ControlToValidate="TextBox7" ErrorMessage="Различни лозинки"></asp:CompareValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label5" runat="server" Text="Е-маил"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label6" runat="server" Text="Место на живеење:"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="labelError" runat="server" Text="Label" Visible="false"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Потврди ги податоците" />
                            </td>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </div>
                <p>
                    &nbsp;
                </p>
            </form>
        </div>



        <div id="bottom">
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>
</body>
</html>
