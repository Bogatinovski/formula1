<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shortlist.aspx.cs" Inherits="user_Shortlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Листа на желби на возачи</title>
    <style>
        .skrieno {
            display: none;
        }
    </style>
    <link href="style/Shortlist.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#gvDostapniVozaci").one('click', function () {

                $("#gvDostapniVozaci").tablesorter();
            });
            $("#gvNedostapniVozaci").one('click', function () {
                $("#gvNedostapniVozaci").tablesorter();
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
            <h3>Листа на желби на возачи</h3>

            <form id="form1" runat="server">
                <div>
                    <h4>Возачи кои се достапни</h4>
                    <asp:GridView ID="gvDostapniVozaci" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowDeleting="gvDostapniVozaci_RowDeleting" CssClass="tabela">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id">
                                <HeaderStyle CssClass="skrieno" BackColor="#360000" />
                                <ItemStyle CssClass="skrieno" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Име на возач">
                                <ItemTemplate>
                                    <asp:HyperLink Text='<%# Eval("ime").ToString()  %>' NavigateUrl='<%# "DriverProfile.aspx?id=" + Eval("id").ToString() %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="vkupna_jacina" HeaderText="Вкупна јачина" ItemStyle-HorizontalAlign="Center">

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="koncentracija" HeaderText="Кон" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="talent" HeaderText="Тал" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="agresivnost" HeaderText="Агр" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="iskustvo" HeaderText="Иск" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="tehnika" HeaderText="Тех" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="izdrzlivost" HeaderText="Изд" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="harizma" HeaderText="Хар" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField />
                            <asp:BoundField DataField="motivacija" HeaderText="Мот" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="tezina" HeaderText="Теж" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="godini" HeaderText="Год" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Мин Награда Потпиш">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("NAGRADAPRIPOTPISUVANJE").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Мин Плата">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("plata").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:CommandField DeleteText="Отстрани" ShowDeleteButton="True" HeaderText="Отстрани" DeleteImageUrl="images/delete.png" ButtonType="Image" />

                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                    <asp:Label ID="lblNemaDostapniVozaci" runat="server" Text="Нема достапни возачои кои се на вашата листа на желби" Visible="False"></asp:Label>
                    <h4>Возачи кои не се достапни</h4>
                    <asp:GridView ID="gvNedostapniVozaci" CssClass="tabela" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvNedostapniVozaci_RowDeleting" HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id">
                                <HeaderStyle CssClass="skrieno" />
                                <ItemStyle CssClass="skrieno" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Име на возач">
                                <ItemTemplate>
                                    <asp:HyperLink Text='<%# Eval("ime").ToString()  %>' NavigateUrl='<%# "DriverProfile.aspx?id=" + Eval("id").ToString() %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="vkupna_jacina" HeaderText="Вкупна јачина" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Кон">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Тал">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Агр">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Иск">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Тех">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Изд">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Хар">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Мот">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tezina" HeaderText="Теж" />
                            <asp:BoundField DataField="godini" HeaderText="Год" />
                            <asp:TemplateField HeaderText="Мин Награда Потпиш">
                                <ItemTemplate>
                                    <asp:Label Text="?" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Мин Плата">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# convertToCurrency(Eval("plata").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField HeaderText="Отстрани" ShowDeleteButton="True" DeleteImageUrl="images/delete.png" DeleteText="Отстрани" ButtonType="Image" />

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
