<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office.aspx.cs" Inherits="Office" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Почетна</title>
    <link rel="stylesheet" href="style/office.css" />
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
            <form id="form1" runat="server">
                <div>
                    <h3>Следна трка:
                        <asp:HyperLink ID="slednaTrka" runat="server">
                            <asp:Label ID="lblSLednaTrka" runat="server" Text="Label"></asp:Label>
                        </asp:HyperLink></h3>

                    <div class="left" style="width: 320px;">
                        <div class="part-wrap" style="width: 320px; margin-right: 10px;">
                            <h4>
                                <asp:Label ID="lblMenIme" runat="server" Text="Label"></asp:Label></h4>
                            <div class="part">
                                <div class="left" style="width: 80px;">
                                    <asp:Image ID="imgProfile" runat="server" ImageUrl="images/unknown-person.gif" Width="80px" Height="80px" />
                                    <img src="images/bolid.jpg" width="80px" height="140px" />
                                </div>
                                <div class="right" style="width: 218px;">
                                    <table id="tabela1" class="tabela">
                                        <tr>
                                            <td>Корисничко:</td>
                                            <td>
                                                <asp:Label ID="lblKorisnicko" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Буџет:</td>
                                            <td>
                                                <asp:Label ID="lblBudzet" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Националност:</td>
                                            <td>
                                                <asp:Label ID="lblZemja" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Група:</td>
                                            <td>
                                                <asp:Label ID="lblGrupa" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Број на трки:</td>
                                            <td>
                                                <asp:Label ID="lblBrTrki" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Поени:</td>
                                            <td>
                                                <asp:Label ID="lblPoeni" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Победи:</td>
                                            <td>
                                                <asp:Label ID="lblPobedi" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Подиуми:</td>
                                            <td>
                                                <asp:Label ID="lblPodiumi" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Пол-позиции:</td>
                                            <td>
                                                <asp:Label ID="lblPolPozicii" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Просек поени:</td>
                                            <td>
                                                <asp:Label ID="lblAvgPoeni" runat="server" Text="0"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="part-wrap" style="width: 320px; margin-top: 15px;">
                            <h4>Подготви се за следната трка</h4>
                            <div class="part">

                                
                                        <table class="tabela">
                                            <tr>
                                                <td><a href="Kvalifikacii.aspx?level=1&id=1">Резултати Квалификации</a></td>
                                                <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td><a href="Standings.aspx?level=1&id=1">Поредок по групи</a></td>
                                                <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    </td>
                                            </tr>
                                        </table>
                                    </div>

                                
                        </div>
                        </div>
                        <div class="right" style="width: 620px; overflow: hidden">
                            <div class="part-wrap" style="width: 620px;">
                                <h4>Управување со вашиот персонал и објекти</h4>
                                <div class="part">

                                    <div class="left" width="300px">
                                        <div class="left" style="width: 100px; margin-right: 0;">
                                            <img src="images/helmet.png" width="80px" height="80px" style="border: 1px solid black; padding: 10px;" />

                                        </div>
                                        <div class="left" style="width: 200px; margin-right: 0;">
                                            <h5>
                                                <asp:HyperLink ID="navVozac" runat="server">
                                                    <asp:Label ID="lblVozacIme" runat="server" Text="немате ангажирано возач"></asp:Label>
                                                </asp:HyperLink></h5>
                                            <table id="tabela2" class="tabela">
                                                <tr>
                                                    <td>Вкупна јачина:</td>
                                                    <td>
                                                        <asp:Label ID="lblVozacJacina" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Плата:</td>
                                                    <td>
                                                        <asp:Label ID="lblVozacPlata" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Преостанти трки:</td>
                                                    <td>
                                                        <asp:Label ID="lblVozacDogovor" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="right" style="width: 298px">
                                        <h5>Менаџирај го возачот и Маркет на возачи</h5>
                                        <table id="tabela3" class="tabela">
                                            <tr>
                                                <td><a href="DriverTraining.aspx">Тренирај возач</a></td>
                                                <td><a href="DriverMarket.aspx">Маркет на возачи</a></td>
                                            </tr>
                                            <tr>
                                                <td><a href="DriverContract.aspx">Договор на возач</a></td>
                                                <td><a href="Shortlist.aspx">Листа на желби</a></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td><a href="YourCurrOffers.aspx">Ваши тековни понуди</a></td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="left" width="300px" style="margin-top: 15px;">
                                        <div class="left" style="width: 100px; margin-right: 0;">
                                            <img src="images/staff.png" width="80px" height="80px" style="border: 1px solid black; padding: 10px;" />

                                        </div>
                                        <div class="left" style="width: 200px; margin-right: 0;">
                                            <h5>
                                                <asp:HyperLink ID="navStaff" runat="server" NavigateUrl="Personal.aspx">Персонал</asp:HyperLink>
                                            </h5>
                                            <table id="tabela2" class="tabela">
                                                <tr>
                                                    <td>Вкупна јачина:</td>
                                                    <td>
                                                        <asp:Label ID="lblStaffVkupno" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Плата:</td>
                                                    <td>
                                                        <asp:Label ID="lblStaffPlata" runat="server" Text=""></asp:Label></td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>

                                    <div class="right" style="width: 298px; margin-top: 15px;">
                                        <div class="left" style="width: 100px; margin-right: 0;">
                                            <img src="images/building.png" width="80px" height="80px" style="border: 1px solid black; padding: 10px;" />

                                        </div>
                                        <div class="left" style="width: 198px; margin-right: 0;">
                                            <h5>
                                                <asp:HyperLink ID="navObjekti" runat="server" NavigateUrl="Objekti.aspx">Објекти</asp:HyperLink></h5>
                                            <table id="tabela2" class="tabela">
                                                <tr>
                                                    <td>Вкупна јачина:</td>
                                                    <td>
                                                        <asp:Label ID="lblObjektiVkupno" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Оддржување:</td>
                                                    <td>
                                                        <asp:Label ID="lblObjektiOdrzuvanje" runat="server" Text=""></asp:Label></td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>

                                </div>
                            </div>


                            <div class="part-wrap" style="width: 620px; margin-top: 15px;">
                                <h4>Подготви се за следната трка</h4>
                                <div class="part">

                                    <div class="left" width="300px">
                                        <div class="left" style="width: 100px; margin-right: 0;">
                                            <img src="images/car-4.png" width="80px" height="80px" style="border: 1px solid black; padding: 10px;" />

                                        </div>
                                        <div class="left" style="width: 200px; margin-right: 15px;">
                                            <h5><a href="Bolid.aspx">Болид</a></h5>
                                            <table id="tabela2" class="tabela">
                                                <tr>
                                                    <td>Моќност:</td>
                                                    <td>
                                                        <asp:Label ID="lblMoknost" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Управување:</td>
                                                    <td>
                                                        <asp:Label ID="lblUpravuvanje" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Забрзување:</td>
                                                    <td>
                                                        <asp:Label ID="lblZabrzuvanje" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="left" style="width: 200px; margin-right: 0;">
                                            <h5>Подесувања</h5>
                                            <table class="tabela">
                                                <tr>
                                                    <td><a href="Practice.aspx">Вежби и Квалификации</a>   </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td><a href="RaceSetup.aspx">Подесувања за Трка</a> </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>





                                </div>
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
