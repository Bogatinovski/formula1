<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaCekanje.aspx.cs" Inherits="ListaCekanje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Листа на чекање</title>
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
            <h3>Листа на чекање</h3>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="311px" CssClass="tabela" ValidateRequestMode="Enabled">
            <Columns>
                <asp:BoundField DataField="men_id" HeaderText="Ид" />
                <asp:BoundField DataField="men_ime" HeaderText="Име" />
                <asp:BoundField DataField="men_prezime" HeaderText="Презиме" />
                <asp:BoundField DataField="email" HeaderText="Емаил" />
                <asp:BoundField DataField="username" HeaderText="Корисничко име" />
                <asp:BoundField DataField="lozinka" HeaderText="Лозинка" />
                <asp:BoundField DataField="men_drzava" HeaderText="Држава" />
                <asp:BoundField DataField="datum" HeaderText="Датум" />
                <asp:CommandField DeleteText="Избриши" ShowDeleteButton="True" />
                <asp:CommandField HeaderText="Потврди" SelectText="Додади " ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Група број:"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Потребно е да внесете група во која ќе биде менаџерот " ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
        <p>
            <asp:Label ID="labelError" runat="server"></asp:Label>
        </p>
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
