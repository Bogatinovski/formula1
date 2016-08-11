<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverTraining.aspx.cs" Inherits="user_DriverTraining" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Тренинг на возач</title>
    <link href="style/DriverTraining.css" type="text/css" rel="stylesheet" />
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
            <h3>Тренинг на возач</h3>
            <form id="form1" runat="server">
                <div id="vozacInfo" runat="server">
                    <div id="goren-del">
                        <div class="column-30">
                            <h4>Информации за возачот</h4>
                            <div class="part">
                                <table class="tabela">
                                    <tr>

                                        <td><span class="attribute">Име:</span></td>
                                        <td>
                                            <asp:Label ID="lblIme" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Нациналност:</span></td>
                                        <td>
                                            <asp:Label ID="lblNacionalnost" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                </table>
                            </div>

                            <h4>Информации за кариерата</h4>

                            <div class="part">
                                <table class="tabela">
                                    <tr>
                                        <td><span class="attribute">Титули:</span></td>
                                        <td>
                                            <asp:Label ID="lblTituli" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Број на трки:</span></td>
                                        <td>
                                            <asp:Label ID="lblBrTrki" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Победи:</span></td>
                                        <td>
                                            <asp:Label ID="lblPobedi" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Подиуми:</span></td>
                                        <td>
                                            <asp:Label ID="lblPodiumi" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Освоени поени:</span></td>
                                        <td>
                                            <asp:Label ID="lblOsvoeniPoeni" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Пол-позиции:</span></td>
                                        <td>
                                            <asp:Label ID="lblPolPozicii" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Најбрз круг:</span></td>
                                        <td>
                                            <asp:Label ID="lblNajbrzKurg" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Просечно поени по трка:</span></td>
                                        <td>
                                            <asp:Label ID="lblAvgPtsPerRace" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </div>


                        </div>
                        <div class="column-30">
                            <h4>Карактеристики на возачот</h4>

                            <div class="part">
                                <table class="tabela">
                                    <tr>
                                        <td><span class="attribute">Вкупна јачина:</span></td>
                                        <td>
                                            <asp:Label ID="lblOA" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Концентрација:</span></td>
                                        <td>
                                            <asp:Label ID="lblConcentration" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Талент:</span></td>
                                        <td>
                                            <asp:Label ID="lblTalent" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Агресивност:</span></td>
                                        <td>
                                            <asp:Label ID="lblAggresiveness" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Искуство:</span></td>
                                        <td>
                                            <asp:Label ID="lblExperience" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Техничко познавање:</span></td>
                                        <td>
                                            <asp:Label ID="lblTI" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Издржливост:</span></td>
                                        <td>
                                            <asp:Label ID="lblStamina" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Харизма:</span></td>
                                        <td>
                                            <asp:Label ID="lblCharisma" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Мотивација:</span></td>
                                        <td>
                                            <asp:Label ID="lblMotivation" runat="server" Text="?"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Тежина:</span></td>
                                        <td>
                                            <asp:Label ID="lblWeight" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Возраст:</span></td>
                                        <td>
                                            <asp:Label ID="lblAge" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="column-30" style="margin-right: 0;">
                            <h4>Можни типови на тренинзи</h4>
                            <div class="part">

                                <asp:GridView ID="gvTrening" GridLines="None" AutoGenerateColumns="False" Width="100%" runat="server" CssClass="tabela1">
                                    <Columns>
                                        <asp:BoundField DataField="ime" HeaderText="Тип на тренинг" />
                                        <asp:TemplateField HeaderText="Цена">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# convertToCurrency(Eval("cena").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <p>
                                    Избери тренинг:
                                <asp:DropDownList ID="ddlTreninzi" runat="server"></asp:DropDownList>
                                </p>
                                <p>
                                    <asp:Button ID="btnTreninrajVozac" runat="server" Text="Тренирај го возачот" OnClick="btnTreninrajVozac_Click" />
                                </p>
                                <p>
                                    <asp:Label ID="lblNeMoziTreniranje" runat="server" Text="" Visible="false"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="nemaVozac" runat="server" visible="false">
                    Немате ангажирано возач.
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
