<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourCurrOffers.aspx.cs" Inherits="user_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Вашите тековни понуди кон возачи</title>
    <link href="style/YourCurrOffers.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#gvCurrOffers").one('click', function () {
                $("#gvCurrOffers").tablesorter();
               
            });
        }
);
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
            <h3>Вашите тековни понуди кон возачи</h3>
            <form id="form1" runat="server">
                <div>
                    <asp:GridView ID="gvCurrOffers" runat="server" AutoGenerateColumns="False" CssClass="tabela" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Име">
                                <ItemTemplate>
                                    <asp:HyperLink Text='<%# Eval("ime").ToString()  %>' NavigateUrl='<%# "DriverProfile.aspx?id=" + Eval("vozac_id").ToString() %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Плата">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("plata").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Награда при Потпишување">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("NAGRADAPOTPISUVANJE").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Бонус за победа">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("POBEDABONUS").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Бонус за подиум">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("PODIUMBONUS").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="brponudi" HeaderText="Бр. на понуди" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                    <asp:Label ID="lblAktivniPonudi" runat="server" Text="Немате активна понуда до ниту еден возач"></asp:Label>
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
