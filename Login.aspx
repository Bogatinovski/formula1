<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="user_style_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>најави се</title>
    <link rel="stylesheet" href="style/login.css" />
    <link rel="stylesheet" href="style/menu.css" />

</head>
<body>
    <div id="container">
		<div id="banner">
			<img id="logo" src="images/logo.jpg" />
			<div id="ime">Формула <span style="color: red; ">1</span> Менаџер</div>
			<img id="car" src="images/car-1.jpg" />
		</div>
		<div class="menu2">
		    <a href="index.aspx">Почетна</a>
		    <a href="#2">Правила на играта</a>
		    <a href="#3">Слики од играта</a>
		    <a href="#3">За Нас</a>
		    <a href="Register.aspx">Регистрирај се</a>
		    <a href="Login.aspx" class="current" >Најави се</a>
		    <a class="dummy"> </a>
		</div>
		<div id="status-bar">
		</div>
		<div id="main">
			<h3>Најави се</h3>	
            <form id="form1" runat="server">
    <div>
        
        <table class="tabela">
            <tr>
                <td colspan="2">Внесете го вашето корисникчко име и лозинка за да се најавите</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Text="Корисникчко име:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox CssClass="text-pole" ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValidUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Внесете го вашето корисничко име"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Лозинка:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" CssClass="text-pole" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValidPassword" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Внесете ја вашата лозинка"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Најави се" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblIzvestuvanje" runat="server"></asp:Label>
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
