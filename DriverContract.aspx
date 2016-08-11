<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverContract.aspx.cs" Inherits="user_DriverContract" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Договор на возач</title>
    <link href="style/DriverContract.css" type="text/css" rel="stylesheet" />
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
            <h3>Договор на возач</h3>

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
                            <h4>Детали за моменталниот договор</h4>
                            <div class="part">
                                <table class="tabela">
                                    <tr>
                                        <td>Плата:</td>
                                        <td>
                                            <asp:Label ID="lblPlata" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Подиум бонус:</td>
                                        <td>
                                            <asp:Label ID="lblPodiumBonus" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Победа бонус:</td>
                                        <td>
                                            <asp:Label ID="lblPobedaBonus" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Преостанати трки:</td>
                                        <td>
                                            <asp:Label ID="lblDogovor" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            <h4>Понуди продолжување на договорот</h4>
                            <div class="part">
                                <table class="auto-style1">
                                    <tr>
                                        <td>Должина на продолжување</td>
                                        <td>Плаќање веднаш</td>
                                        <td>Нова плата</td>
                                    </tr>
                                    <tr>
                                        <td>1 трка:</td>
                                        <td>
                                            <asp:Label ID="lblSignFee1" runat="server" Text='<%# signingFeeProdolzvuanje(0).ToString() %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalary1" runat="server" Text='<%# salaryProdolzuvanje(0).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2 трки:</td>
                                        <td>
                                            <asp:Label ID="lblSignFee2" runat="server" Text='<%# signingFeeProdolzvuanje(1).ToString() %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalary2" runat="server" Text='<%# salaryProdolzuvanje(1).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2">4 трки:</td>
                                        <td class="auto-style2">
                                            <asp:Label ID="lblSignFee4" runat="server" Text='<%# signingFeeProdolzvuanje(2).ToString() %>'></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:Label ID="lblSalary4" runat="server" Text='<%# salaryProdolzuvanje(2).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>7 трки:</td>
                                        <td>
                                            <asp:Label ID="lblSignFee7" runat="server" Text='<%# signingFeeProdolzvuanje(3).ToString() %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalary7" runat="server" Text='<%# salaryProdolzuvanje(3).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>10 трки:</td>
                                        <td>
                                            <asp:Label ID="lblSignFee10" runat="server" Text='<%# signingFeeProdolzvuanje(4).ToString() %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalary10" runat="server" Text='<%# salaryProdolzuvanje(4).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>17 трки:</td>
                                        <td>
                                            <asp:Label ID="lblSignFee17" runat="server" Text='<%# signingFeeProdolzvuanje(5).ToString() %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSalary17" runat="server" Text='<%# salaryProdolzuvanje(5).ToString() %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Должина:</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlProdolzuvanje" runat="server">
                                                <asp:ListItem Value="0">1 трка</asp:ListItem>
                                                <asp:ListItem Value="1">2 трки</asp:ListItem>
                                                <asp:ListItem Value="2">4 трки</asp:ListItem>
                                                <asp:ListItem Value="3">7 трки</asp:ListItem>
                                                <asp:ListItem Value="4">10 трки</asp:ListItem>
                                                <asp:ListItem Value="5">17 трки</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3" colspan="3">
                                            <asp:Button ID="btnProdolzi" runat="server" Text="Продолжи го договорот" OnClick="btnProdolzi_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblNeMoziProdolzuvanje" runat="server" Text="" Visible="false" ForeColor="#FFA500"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
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
