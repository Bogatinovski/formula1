<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bolid.aspx.cs" Inherits="user_Bolid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Болид</title>
    <link href="style/Bolid.css" type="text/css" rel="stylesheet" />
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
            <h3>Болид</h3>

            <form id="form1" runat="server">
            <div class="left" style="width: 50%; margin-right: 1%;" >
                <asp:GridView ID="gvBolid" CssClass="tabela" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ime" HeaderText="Дел од болид" />
                        <asp:BoundField DataField="nivo" HeaderText="Ниво" />
                        <asp:TemplateField HeaderText="Акција">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlNivo" DataSource='<%# LoadDDL(Eval("cena").ToString(), Eval("nivo").ToString()) %>' runat="server" AutoPostBack="True"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Акција">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("istrosenost").ToString() + "%" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
                <p>
                    Вкупно за плаќање:
                <asp:Label ID="lblVkupno" runat="server" Text="0$"></asp:Label>
                </p>
                <p><asp:Button ID="btnUpdate" runat="server" Text="Проемни ги деловите" OnClick="btnUpdate_Click" /></p>
                <p><asp:Label ID="lblIzvestuvanje" runat="server" Text="" Visible="false" ForeColor="#FFA500"></asp:Label></p>
                
                
            </div>
        </form>
            <div class="right" style="width: 45%;">
                <img src="images/car-2.png" style="width: 95%; max-height: 300px; margin: 0 auto;" />
                <p style="padding: 10px 5px; color: #FFA500;">Забелешки</p>
                <ul>
                    <li>Откако ќе кликнете на „Промени ги деловите“ нема да можете да ги менувате деловите се до следната трка</li>
                    <li>Цената за секој нов дел е специфицирана посебно за секое ниво</li>
                    <li>Може да надградите еден објект само едно ниво повеќе од моменталното</li>
                </ul>
            </div>
        </div>
        
        <div id="bottom">
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>
</body>
</html>
