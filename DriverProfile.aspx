<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverProfile.aspx.cs" Inherits="user_DriverProfile" ResponseEncoding="utf-8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Профил на возач</title>
    <link href="style/DriverProfile.css" type="text/css" rel="stylesheet" />
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
            $("#karakteristiki tr").each(function (index) {
                var pom = $(this).children('td').eq(1).text();
                if(pom === '?')
                    return true;
                var vred = parseInt(pom, 10);
                //vred += 50;
                var progressbar = $(this).children('td').eq(2);




                progressbar.progressbar({
                    value: vred,
                    max: 250
                });

                //var progressbar = $(".progressbar"),
                progressbarValue = progressbar.find(".ui-progressbar-value");

                if (vred < 50) {
                    $(progressbarValue).css({ 'background': 'Red' });
                } else if (vred < 10) {
                    $(progressbarValue).css({ 'background': 'Orange' });
                } else if (vred < 200) {
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
		    <a class="dummy"> </a>
        </div>
        <div id="status-bar">
			<div id="budget">Буџет: <asp:Label ID="lblBudzetStatus" runat="server" Text=""></asp:Label></div>
			<div id="loggedManager">Најaвен менаџер: <asp:Label ID="lblManagerStatus" runat="server" Text=""></asp:Label></div>
		</div>
        <div id="main">
            <h3>Профил на возач</h3>
            
            <form id="form1" runat="server">
                <asp:Label ID="lblNemaVozac" runat="server" Text="Не постои возачот кој го побравте" Visible="false" ForeColor="#FFA500"></asp:Label>
                <div id="id1" runat="server">
                    <div id="goren-del">

                        <div class="column-30">
                            <h4>Информации за возачот</h4>
                            <div class="part">
                                <table class="tabela">
                                    <tr>

                                        <td><span class="attribute">Име:</span></td>
                                        <td>
                                            <asp:Label ID="lblIme" runat="server" Text=""></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Нациналност:</span></td>
                                        <td>
                                            <asp:Label ID="lblNacionalnost" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center; font-weight: bold;">

                                            <asp:Button ID="btnShortlist" runat="server" Text="Додај во листа на желби" OnClick="btnShortlist_Click"></asp:Button></td>
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
                                <table class="tabela" id="karakteristiki">
                                    <tr>
                                        <td><span class="attribute">Вкупна јачина:</span></td>
                                        <td>
                                            <asp:Label ID="lblOA" runat="server" Text=""></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Концентрација:</span></td>
                                        <td>
                                            <asp:Label ID="lblConcentration" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Талент:</span></td>
                                        <td>
                                            <asp:Label ID="lblTalent" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Агресивност:</span></td>
                                        <td>
                                            <asp:Label ID="lblAggresiveness" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Искуство:</span></td>
                                        <td>
                                            <asp:Label ID="lblExperience" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Техничко познавање:</span></td>
                                        <td>
                                            <asp:Label ID="lblTI" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Издржливост:</span></td>
                                        <td>
                                            <asp:Label ID="lblStamina" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Харизма:</span></td>
                                        <td>
                                            <asp:Label ID="lblCharisma" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Мотивација:</span></td>
                                        <td>
                                            <asp:Label ID="lblMotivation" runat="server" Text="?"></asp:Label></td>
                                        <td><div class="progressbar"></div></td>
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
                            <h4>
                                <asp:Label ID="lblCurrContract" runat="server" Text="Детали за договор на возачот"></asp:Label></h4>
                            <div class="part">
                                <p id="noCurrOwner" runat="server" visible="false">
                                    Нема понуди за возачот во моментов
                                </p>
                                <asp:GridView ID="gvCurrOffers" Visible="false" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" Width="100%" runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label Text='<%# pecatiImeMenadzerIstorija(Eval("id").ToString(), Eval("menadzer").ToString())  %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label Text='<%# pecatiGrupa(Eval("grupa_nivo").ToString(), Eval("grupa_broj").ToString(), Eval("grupa").ToString())  %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table class="tabela" id="currOwner" runat="server">
                                    <tr>
                                        <td><span class="attribute">Моментален сопственик:</span></td>
                                        <td>
                                            <asp:HyperLink ID="linkManager" runat="server">
                                                <asp:Label ID="lblOwner" runat="server" Text=""></asp:Label>
                                            </asp:HyperLink></td>

                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Група:</span></td>
                                        <td>
                                            <asp:HyperLink ID="linkGroup" runat="server">
                                                <asp:Label ID="lblGroup" runat="server" Text=""></asp:Label>
                                            </asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Плата:</span></td>
                                        <td>
                                            <asp:Label ID="lblSalary" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span class="attribute">Должина на договор:</span></td>
                                        <td>
                                            <asp:Label ID="lblContractDuration" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="column-50">
                        <h4>Работна историја на возачот</h4>
                        <div class="part">
                            <asp:Label ID="lblIzvestuvanje" runat="server" Text="Досега немал никакви ангажмани"></asp:Label>

                            <asp:GridView ID="gvHistory" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Менаџер">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# pecatiImeMenadzerIstorija(Eval("id").ToString(), Eval("menadzer").ToString())  %>' runat="server" />
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
                    <div class="column-50" style="margin-right: 0;">
                        <h4>Постави ја твојата понуда
                        </h4>
                        <div class="part">
                            <asp:Label ID="lblIzvestuvanjePonuda" runat="server" Text="" Visible="false" ForeColor="#FFA500"></asp:Label>
                            <table class="tabela">
                                <tr>
                                    <td>Плата по трка*</td>
                                    <td>
                                        <asp:TextBox ID="txtPonudaPlata" runat="server" CssClass="text-pole"></asp:TextBox>$
                            <asp:RequiredFieldValidator ID="reqValidPonudaPlata" runat="server" ControlToValidate="txtPonudaPlata" Display="Dynamic" ErrorMessage="Мора да ја внесете платата по трка на возачот" ForeColor="#CC0000" ValidationGroup="ponudaGroup"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rangeValidPonudaPlata" runat="server" ControlToValidate="txtPonudaPlata" Display="Dynamic" ErrorMessage="Најмалку може да биде 0" ForeColor="#CC0000" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ponudaGroup"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Награда при потпишување*</td>
                                    <td>
                                        <asp:TextBox ID="txtPonudaPotpisuvanje" runat="server" CssClass="text-pole"></asp:TextBox>$
                            <asp:RequiredFieldValidator ID="reqValidPonudaPotpisuvanje" runat="server" ControlToValidate="txtPonudaPotpisuvanje" Display="Dynamic" ErrorMessage="Мора да внесете вредност за награда при потпишување" ForeColor="#CC0000" ValidationGroup="ponudaGroup"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rangeValidPonudaPotpisuvanje" runat="server" ControlToValidate="txtPonudaPotpisuvanje" Display="Dynamic" ErrorMessage="Најмалку може да биде 0" ForeColor="#CC0000" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ponudaGroup"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Бонус за победа</td>
                                    <td>
                                        <asp:TextBox ID="txtPonudaPobeda" runat="server" CssClass="text-pole"></asp:TextBox>$
                            <asp:RangeValidator ID="rangeValidPonudaPobeda" runat="server" ControlToValidate="txtPonudaPobeda" Display="Dynamic" ErrorMessage="Најмалку може да биде 0" ForeColor="#CC0000" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ponudaGroup"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Бонус за подиум</td>
                                    <td>
                                        <asp:TextBox ID="txtPonudaPodium" runat="server" CssClass="text-pole"></asp:TextBox>$
                            <asp:RangeValidator ID="rangeValidPonudaPodium" runat="server" ControlToValidate="txtPonudaPodium" Display="Dynamic" ErrorMessage="Најмалку може да биде 0" ForeColor="#CC0000" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ponudaGroup"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Должина на договор*</td>
                                    <td>
                                        <asp:TextBox ID="txtPonudaDogovor" runat="server" CssClass="text-pole"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqValidDogovor" runat="server" ControlToValidate="txtPonudaDogovor" Display="Dynamic" ErrorMessage="Мора да внесете должина на договор" ForeColor="#CC0000" ValidationGroup="ponudaGroup"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rangeValidPonudaDogovor" runat="server" ControlToValidate="txtPonudaDogovor" Display="Dynamic" ErrorMessage="Најмалку може да биде 0" ForeColor="#CC0000" MaximumValue="99999999" MinimumValue="0" ValidationGroup="ponudaGroup"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnOfferContact" runat="server" Text="Понуди договор" OnClick="btnOfferContact_Click" ValidationGroup="ponudaGroup" /></td>
                                </tr>
                            </table>
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
