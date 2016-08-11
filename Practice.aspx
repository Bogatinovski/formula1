<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Practice.aspx.cs" Inherits="user_Practice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Вежби и Квалификации</title>
    <link href="style/Practice.css" type="text/css" rel="stylesheet" />
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
            <h3>Вежби и Квалификации</h3>
            <form id="form1" runat="server">
                <div>
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblNemaVozac" runat="server" Text="" Visible="false"></asp:Label>
                    <h4>Имате извозено
                        <asp:Label ID="lblIzvozeniKrugovi" runat="server" Text=""></asp:Label>/5</h4>
                    <asp:GridView ID="GridView1" runat="server" CssClass="tabela" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField SelectText="Прикажи" ShowSelectButton="True" HeaderText="Коментари" />
                        </Columns>
                    </asp:GridView>
                    <div id="comments" runat="server"></div>
                    <div class="left">

                        <asp:GridView ID="gvVezbiSetup" runat="server" AutoGenerateColumns="false" CssClass="tabela">
                            <Columns>
                                <asp:BoundField DataField="ime" HeaderText="" />
                                <asp:BoundField DataField="nivo" HeaderText="Ниво" />
                                <asp:TemplateField HeaderText="Акција">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("istrosenost").ToString() + "%" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Подесување">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" MaxLength="3" runat="server"></asp:TextBox>(0-999)
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="right">
                        <asp:GridView ID="gvVezbiNonSetup" runat="server" AutoGenerateColumns="false" CssClass="tabela">
                            <Columns>
                                <asp:BoundField DataField="ime" HeaderText="" />
                                <asp:BoundField DataField="nivo" HeaderText="Ниво" />
                                <asp:TemplateField HeaderText="Акција">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("istrosenost").ToString() + "%" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <p>
                            Гуми:
                        <asp:DropDownList ID="ddlGumi" runat="server"></asp:DropDownList>
                        </p>
                        <p>
                            Ризик:
                        <asp:DropDownList ID="ddlRizik" runat="server" Enabled="false"></asp:DropDownList>
                        </p>
                        <p>
                            <asp:RadioButtonList ID="radioType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radioType_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Вежби</asp:ListItem>
                                <asp:ListItem Value="1">Квалификација 1</asp:ListItem>
                                <asp:ListItem Value="2">Квалификација 2</asp:ListItem>
                            </asp:RadioButtonList>
                        </p>
                        <p>
                            <asp:Button ID="btnDrive" runat="server" Text="Вози" OnClick="btnDrive_Click" />
                        </p>
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
