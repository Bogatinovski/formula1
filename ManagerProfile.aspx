<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerProfile.aspx.cs" Inherits="ManagerProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Профил на менаџер</title>
    <link href="style/ManagerProfile.css" type="text/css" rel="stylesheet" />
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
            <h3>Профил на менаџер</h3>
            <form id="form1" runat="server">
                <div>
                    <h5 class="column-30">Информации за менаџерот</h5>
                    <h5 class="column-30">Историја на возачи</h5>
                    <div class="column-30 part" >
                        
                        <div class="left" style="width: 80px;">
                            <asp:Image ID="imgProfile" runat="server" ImageUrl="~/images/unknown-person.gif" Height="80px" Width="80px" />
                            <img src="images/bolid.jpg" width="80px" height="140px" />
                        </div>
                        <div class="right" style="width: 218px;">
                            <table id="tabela1" class="tabela">
                                <tr>
                                    <td>Име:</td>
                                    <td>
                                        <asp:Label ID="lblIme" runat="server" Text="0"></asp:Label></td>
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

                    
                    <div class="column-30 part" >
                        <asp:GridView ID="gvHistory" HorizontalAlign="Center" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Возач">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# pecatiVozac(Eval("id").ToString(), Eval("ime").ToString())  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Старт">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# pecatiIstorija(Eval("start_contract_season").ToString(), Eval("start_contract_race").ToString())  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Крај">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# pecatiIstorija(Eval("end_contract_season").ToString(), Eval("end_contract_race").ToString())  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                      
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
