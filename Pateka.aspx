<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pateka.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Патека</title>
    <link href="style/ManagerProfile.css" type="text/css" rel="stylesheet" />
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
            <h3>Патека</h3>
            <form id="form1" runat="server">
                <div>
                    <h5 class="column-50" style="margin-bottom: 0;">Изглед на патеката</h5>
                    <h5 class="column-30">Информации за патеката</h5>
                    <div class="column-50 part" >
                        
                        <div class="left">
                            <asp:Image ID="imgProfile" runat="server" Height="350px" Width="450px" />

                        </div>
                        
                    </div>

                     <div class="column-30 part" >
                        
                        
                            <table id="tabela1" class="tabela">
                                <tr>
                                    <td>Име:</td>
                                    <td>
                                        <asp:Label ID="lblIme" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td>Должина:</td>
                                    <td>
                                        <asp:Label ID="lblDolzina" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Број на кругови:</td>
                                    <td>
                                        <asp:Label ID="lblBKruga" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Должина на круг:</td>
                                    <td>
                                        <asp:Label ID="lblDolzKrug" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Претекнување:</td>
                                    <td>
                                        <asp:Label ID="lblPreteknuvanje" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Потрошувачка гориво:</td>
                                    <td>
                                        <asp:Label ID="lblGorivo" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Потрошувачка на гуми:</td>
                                    <td>
                                        <asp:Label ID="lblGumi" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                 
                            </table>
                        
                    </div>

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
