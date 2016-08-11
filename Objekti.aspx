<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Objekti.aspx.cs" Inherits="user_Objekti" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Објекти</title>
    <link href="style/Objekti.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>

    <style>
        .progressbar {
            width: 120px;
            overflow: hidden;
            background-color: #0BA1B5 !important;
        }

        .ui-progressbar {
            height: 23px;
            padding: 0 !important;
        }
        .ui-widget-content{
            padding: 0 !important;
            margin: 0 !important;
            border: 1px solid black;
            background: none;
        }
    </style>
    <script>
        $(function () {
            $("#gvFacilitiesLevel tr").each(function (index) {
                var vred = parseInt($(this).children('td').eq(1).text(), 10);
                //vred += 50;
                var progressbar = $(this).children('td').eq(2);




                progressbar.progressbar({
                    value: vred,
                    max: 100
                });

                //var progressbar = $(".progressbar"),
                progressbarValue = progressbar.find(".ui-progressbar-value");

                if (vred < 10) {
                    $(progressbarValue).css({ 'background': 'Red' });
                } else if (vred < 30) {
                    $(progressbarValue).css({ 'background': 'Orange' });
                } else if (vred < 50) {
                    $(progressbarValue).css({ 'background': 'Yellow' });
                } else {
                    $(progressbarValue).css({ 'background': 'LightGreen' });
                }
            });

        });
    </script>
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
            <h3>Објекти</h3>
            <form id="form1" runat="server">
                <div>

                    <div class="left">
                        <h4>Нивоа на објектите</h4>
                        <div class="part">
                            <p>
                                Вкупна јачина:
                            <asp:Label ID="lblVkupno" runat="server" Text="" Font-Bold="true" ForeColor="GreenYellow"></asp:Label>
                            </p>
                            <asp:GridView ID="gvFacilitiesLevel" runat="server" AutoGenerateColumns="False" ShowHeader="false" GridLines="None" CssClass="tabela">
                                <Columns>
                                    <asp:BoundField DataField="ime" />
                                    <asp:BoundField DataField="vrednost" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="progressbar"></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="right">
                        <h4>Тренинг на персоналот</h4>
                        <div class="part">
                            <asp:GridView ID="gvTreninzi" runat="server" AutoGenerateColumns="False" ShowHeader="false" GridLines="None" CssClass="tabela">
                                <Columns>
                                    <asp:BoundField DataField="ime" />
                                    <asp:TemplateField HeaderText="Акција">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlNivo" DataSource='<%# LoadDDL(Eval("trening_cena").ToString(), Eval("vrednost").ToString()) %>' runat="server" AutoPostBack="True"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <p>
                                Вкупно за плаќање:
                        <asp:Label ID="lblVkupnoPlakjanje" runat="server" Text="0$" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </p>
                            <p>
                                Вкупна цена за одржување на објектите:
                        <asp:Label ID="lblPlata" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </p>
                            <asp:Button ID="btnTreniraj" runat="server" Text="Тренирај" OnClick="btnUpdate_Click" />
                            <p>
                                <asp:Label ID="lblIzvestuvanje" runat="server" Text="" Visible="false" ForeColor="#FFA500"></asp:Label>
                            </p>

                        </div>
                    </div>
                </div>
            </form>
        </div>



        <div id="bottom">
            <div class="progressbar"></div>
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>
</body>
</html>
