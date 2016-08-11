<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal.aspx.cs" Inherits="user_Personal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Персонал</title>
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
            $("#gvPersonalAttributes tr").each(function (index) {
                var vred = parseInt($(this).children('td').eq(1).text(), 10);
                //vred += 50;
                var progressbar = $(this).children('td').eq(2);




                progressbar.progressbar({
                    value: vred,
                    max: 120
                });

                //var progressbar = $(".progressbar"),
                progressbarValue = progressbar.find(".ui-progressbar-value");

                if (vred <= 10) {
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
            <h3>Персонал</h3>
            <form id="form1" runat="server">
                <div>

                    <div class="left">
                        <h4>Вештини на персоналот</h4>
                        <div class="part">
                            <p>
                                Вкупна јачина:
                        <asp:Label ID="lblVkupno" runat="server" Text=""></asp:Label>
                            </p>
                            <asp:GridView ID="gvPersonalAttributes" runat="server" AutoGenerateColumns="False" ShowHeader="false" GridLines="None" CssClass="tabela">
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
                                            <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("trening_cena").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <p>
                                Изберете вид на тренинг:
                        <asp:DropDownList ID="ddlTrening" runat="server"></asp:DropDownList>
                            </p>
                            <p>
                                Вкупно плата на персонал:
                        <asp:Label ID="lblPlata" runat="server" Text=""></asp:Label>
                            </p>
                            <asp:Button ID="btnTreniraj" runat="server" Text="Тренирај" OnClick="btnUpdate_Click" />
                            <p> <asp:Label ID="lblIzvestuvanje" runat="server" Text="" Visible="false" ForeColor="#FFA500"></asp:Label></p>
                           
                        </div>
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
