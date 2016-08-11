<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverMarket.aspx.cs" Inherits="user_DriverMarket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Маркет на возачи</title>
    <link href="style/DriverMarket.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#gvDriverMarket").one('click', function () {
     
                $("#gvDriverMarket").tablesorter();
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
            <div id="budget">Буџет:
                <asp:Label ID="lblBudzetStatus" runat="server" Text=""></asp:Label></div>
            <div id="loggedManager">Најaвен менаџер:
                <asp:Label ID="lblManagerStatus" runat="server" Text=""></asp:Label></div>
        </div>
        <div id="main">
            <h3>Маркет на возачи</h3>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvDriverMarket" runat="server" AutoGenerateColumns="False" DataKeyNames="id" CssClass="tabela" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Име">
                        <ItemTemplate>
                            <asp:HyperLink Text='<%# Eval("ime").ToString()  %>' NavigateUrl='<%# "DriverProfile.aspx?id=" + Eval("id").ToString() %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="vkupna_jacina" HeaderText="Вкупна јачина" />
                    <asp:BoundField DataField="godini" HeaderText="Години" />
                    <asp:TemplateField HeaderText="Награда при потпишување">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("nagradapripotpisuvanje").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Плата по трка">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("plata").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="brponudi" HeaderText="Број на понуди" />
                    <asp:BoundField DataField="id" HeaderText="Id" Visible="False" />
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
            <asp:Label ID="lblNemaDostapniVozaci" runat="server" Text="Нема возачи кои се слободни" Visible="false"></asp:Label>
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
