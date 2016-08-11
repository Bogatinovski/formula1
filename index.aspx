<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Почетна</title>
    <!-- bjqs.css contains the *essential* css needed for the slider to work -->
    <link rel="stylesheet" href="style/bjqs.css">
    <link rel="stylesheet" href="style/demo.css">
    <!-- some pretty fonts for this demo page - not required for the slider -->
    <link href='http://fonts.googleapis.com/css?family=Source+Code+Pro|Open+Sans:300' rel='stylesheet' type='text/css'>

    <!-- load jQuery and the plugin -->
    <script src="js/jquery-1.7.1.min.js"></script>
    <script src="js/bjqs-1.3.js"></script>
    <link rel="stylesheet" href="style/index.css">
    <link rel="stylesheet" href="style/menu.css">
</head>
<body>
    <div id="container">
        <div id="banner">
            <img id="logo" src="images/logo.jpg" />
            <div id="ime">Формула <span style="color: red;">1</span> Менаџер</div>
            <img id="car" src="images/car-1.jpg" />
        </div>
        <div class="menu2">
            <a href="index.aspx" class="current">Почетна</a>
            <a href="#2">Правила на играта</a>
            <a href="#3">Слики од играта</a>
            <a href="#3">За Нас</a>
            <a href="Register.aspx">Регистрирај се</a>
            <a href="Login.aspx">Најави се</a>
            <a class="dummy"></a>
        </div>
        <div id="status-bar">
        </div>
        <div id="main">
            <blockquote class="style1">

                <span>Водете го својот тим до успехот преку развивање на добар болид, правење на добри стратегии во трката, ангажирање на вистинските возачи и персонал, како и планирање на иднината. Но сите овие работи треба да се направени во рамките на вашиот финансиски буџет и не заборавајте ја конкуренцијата, која постојано ќе ви дишe во вратот!
                </span>

            </blockquote>
            <div id="login-form-wrap">
                <form id="form1" runat="server">
                    <div>
                        <h4><span style="display: inline-block; vertical-align: middle;">Најави се</span></h4>
                        <div id="login-form">
                            <label>Корисничко име:</label>
                            <asp:TextBox CssClass="text-pole" ID="txtUsername" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValidUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Внесете го вашето корисничко име"></asp:RequiredFieldValidator>
                            <label>Лозинка:</label>
                            <asp:TextBox ID="txtPassword" CssClass="text-pole" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValidPassword" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Внесете ја вашата лозинка"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Најави се" />
                            <asp:Label ID="lblIzvestuvanje" runat="server" Font-Size="14px" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                            <span style="font-size: 0.7em; color: #ccc;">Ако немаш корисничка сметка, тогаш</span>
                            <br />
                            <input type="button" id="register-button" value="Регистрирај се" onclick="window.location = 'Register.aspx';"  />
                        </div>
                    </div>
                </form>

            </div>
            <div id="slider">
                <div id="banner-fade">

                    <!-- start Basic Jquery Slider -->
                    <ul class="bjqs">
                        <li>
                            <img src="images/banner1.jpg" title="Уживај во преку 40 различни патеки, секоја крие ново возбудување!"></li>
                        <li>
                            <img src="images/banner2.jpg" title="Вложувај во твојот персонал за да добиваш подобри перформанси за време на трките!"></li>
                        <li>
                            <img src="images/banner3.jpg" title="Добро менаџирај го болидот, навремено заменувај ги деловите, прави мудри финансиски одлуки!"></li>
                        <li>
                            <img src="images/banner4.jpg" title="Ангажирај го најдобриот возач кој нема да знае за пораз!"></li>
                        <li>
                            <img src="images/banner5.jpg" title="Целиот труд е најсладок кога ќе се осети победничкото чувство!"></li>
                    </ul>
                    <!-- end Basic jQuery Slider -->

                </div>
                <!-- End outer wrapper -->
            </div>
            <script>
                jQuery(document).ready(function ($) {

                    $('#banner-fade').bjqs({
                        height: 320,
                        width: 620,
                        responsive: true
                    });

                });
            </script>

        </div>
        <div id="bottom">
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>

</body>
</html>
