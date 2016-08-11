<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Форма за регистрирање</title>
    <link href="style/register.css" type="text/css" rel="stylesheet" />
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
            <a href="index.aspx">Почетна</a>
		    <a href="#2">Правила на играта</a>
		    <a href="#3">Слики од играта</a>
		    <a href="#3">За Нас</a>
		    <a href="Register.aspx" class="current">Регистрирај се</a>
		    <a href="Login.aspx" >Најави се</a>
		    <a class="dummy"> </a>
        </div>
        <div id="status-bar">
        </div>
        <div id="main">
            <h3>Регистрирај Се</h3>

            <form id="form1" runat="server">
                <div>
                    <div id="left">
                        <h4>Форма за регистрација</h4>
                        <p>Сите полиња на формуларот за регистрација се задолжителни. Ве молиме да користите латинични букви, бидејќи нема да одобриме имиња со кирилични, азиски или арапски букви.</p>
                        <table align="left" class="tabela">
                            <tr>
                                <td>Корисничко име:</td>
                                <td>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="text-pole"></asp:TextBox></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="reqValidatorUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Потребно е да внесте корисничко име кое ќе го користите за најавување" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Лозинка:</td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="text-pole" TextMode="Password"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="regexValidatorPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Лозинката мора да е најмалку 4 карактери и не може да е подолга од 25 карактери" ForeColor="Red" ValidationExpression="^[\s\S]{4,25}$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Потврди лозинка:</td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="text-pole" TextMode="Password"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="cmpValidatorPassword" runat="server" ControlToCompare="txtConfirmPassword" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Двете лозинки не се совпаѓаат" ForeColor="Red"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Е-маил:</td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="text-pole" TextMode="Email"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Внесете емаил" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexValidatorEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Внесете е-маил" ValidationExpression="\S+@\S+\.\S+" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Име:</td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="text-pole" TextMode="SingleLine"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="reqValidatorName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Внесете го вашето име" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Презиме:</td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="text-pole"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="reqValidatorLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="Внесете го вашето презиме" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Држава:</td>
                                <td>
                                    <asp:DropDownList name="Country" ID="ddlCountry" Style="max-width: 150px;" runat="server">
                                        <asp:ListItem Value="">Изберете држава</asp:ListItem>
                                        <asp:ListItem>Авганистан</asp:ListItem>
                                        <asp:ListItem>Албанија</asp:ListItem>
                                        <asp:ListItem>Алжир</asp:ListItem>
                                        <asp:ListItem>Американска Самоа</asp:ListItem>
                                        <asp:ListItem>Андора</asp:ListItem>
                                        <asp:ListItem>Ангола</asp:ListItem>
                                        <asp:ListItem>Ангилја</asp:ListItem>
                                        <asp:ListItem>Антарктика</asp:ListItem>
                                        <asp:ListItem>Антигва и Барбуда</asp:ListItem>
                                        <asp:ListItem>Аргентина</asp:ListItem>
                                        <asp:ListItem>Ерменија</asp:ListItem>
                                        <asp:ListItem>Аруба</asp:ListItem>
                                        <asp:ListItem>Австралија</asp:ListItem>
                                        <asp:ListItem>Австрија</asp:ListItem>
                                        <asp:ListItem>Азербејџан</asp:ListItem>
                                        <asp:ListItem>Бахами</asp:ListItem>
                                        <asp:ListItem>Бахреин</asp:ListItem>
                                        <asp:ListItem>Бангладеш</asp:ListItem>
                                        <asp:ListItem>Барбадос</asp:ListItem>
                                        <asp:ListItem>Белорусија</asp:ListItem>
                                        <asp:ListItem>Белгија</asp:ListItem>
                                        <asp:ListItem>Белизе</asp:ListItem>
                                        <asp:ListItem>Бенин</asp:ListItem>
                                        <asp:ListItem>Бермуда</asp:ListItem>
                                        <asp:ListItem>Бутан</asp:ListItem>
                                        <asp:ListItem>Боливија</asp:ListItem>
                                        <asp:ListItem>Босна и Херцеговина</asp:ListItem>
                                        <asp:ListItem>Боцвана</asp:ListItem>
                                        <asp:ListItem>Остров Бувет</asp:ListItem>
                                        <asp:ListItem>Бразил</asp:ListItem>
                                        <asp:ListItem>Британска Територија на Индиски Океан</asp:ListItem>
                                        <asp:ListItem>Британски Девствени Острови</asp:ListItem>
                                        <asp:ListItem>Брунеи Дарусалам</asp:ListItem>
                                        <asp:ListItem>Бугарија</asp:ListItem>
                                        <asp:ListItem>Буркина Фасо</asp:ListItem>
                                        <asp:ListItem>Бурунди</asp:ListItem>
                                        <asp:ListItem>Камбоџа</asp:ListItem>
                                        <asp:ListItem>Камерун</asp:ListItem>
                                        <asp:ListItem>Канада</asp:ListItem>
                                        <asp:ListItem>Зелено&amp;#39;ртски Острови</asp:ListItem>
                                        <asp:ListItem>Кајмански Острови</asp:ListItem>
                                        <asp:ListItem>Централна Африканска Република</asp:ListItem>
                                        <asp:ListItem>Чад</asp:ListItem>
                                        <asp:ListItem>Чиле</asp:ListItem>
                                        <asp:ListItem>Кина</asp:ListItem>
                                        <asp:ListItem>Божиќни Острови</asp:ListItem>
                                        <asp:ListItem>Кокосови (Keeling) Острови</asp:ListItem>
                                        <asp:ListItem>Колумбија</asp:ListItem>
                                        <asp:ListItem>Коморос</asp:ListItem>
                                        <asp:ListItem>Демократска Република Конго</asp:ListItem>
                                        <asp:ListItem>Република Конго</asp:ListItem>
                                        <asp:ListItem>Куковите Острови</asp:ListItem>
                                        <asp:ListItem>Костарика</asp:ListItem>
                                        <asp:ListItem>Брегот на Слоновата Коска</asp:ListItem>
                                        <asp:ListItem>Хрватска</asp:ListItem>
                                        <asp:ListItem>Куба</asp:ListItem>
                                        <asp:ListItem>Кипар</asp:ListItem>
                                        <asp:ListItem>Чешка Република</asp:ListItem>
                                        <asp:ListItem>Данска</asp:ListItem>
                                        <asp:ListItem>Џибути</asp:ListItem>
                                        <asp:ListItem>Доминика</asp:ListItem>
                                        <asp:ListItem>Доминиканска Република</asp:ListItem>
                                        <asp:ListItem>Еквадор</asp:ListItem>
                                        <asp:ListItem>Египет</asp:ListItem>
                                        <asp:ListItem>Ел Салвадор</asp:ListItem>
                                        <asp:ListItem>Англија</asp:ListItem>
                                        <asp:ListItem>Екваторска Гвинеја</asp:ListItem>
                                        <asp:ListItem>Еритреја</asp:ListItem>
                                        <asp:ListItem>Естонија</asp:ListItem>
                                        <asp:ListItem>Етиопија</asp:ListItem>
                                        <asp:ListItem>Фолкландски Острови</asp:ListItem>
                                        <asp:ListItem>Фарски Острови</asp:ListItem>
                                        <asp:ListItem>Фиџи</asp:ListItem>
                                        <asp:ListItem>Финска</asp:ListItem>
                                        <asp:ListItem>Франција</asp:ListItem>
                                        <asp:ListItem>Францускa Гвајана</asp:ListItem>
                                        <asp:ListItem>Француски јужни територии</asp:ListItem>
                                        <asp:ListItem>Македонија</asp:ListItem>
                                        <asp:ListItem>Габон</asp:ListItem>
                                        <asp:ListItem>Гамбија</asp:ListItem>
                                        <asp:ListItem>Грузија</asp:ListItem>
                                        <asp:ListItem>Германија</asp:ListItem>
                                        <asp:ListItem>Гана</asp:ListItem>
                                        <asp:ListItem>Гибралтар</asp:ListItem>
                                        <asp:ListItem>Грција</asp:ListItem>
                                        <asp:ListItem>Гренланд</asp:ListItem>
                                        <asp:ListItem>Гренада</asp:ListItem>
                                        <asp:ListItem>Гвадалупе</asp:ListItem>
                                        <asp:ListItem>Гуам</asp:ListItem>
                                        <asp:ListItem>Гватемала</asp:ListItem>
                                        <asp:ListItem>Гернзи</asp:ListItem>
                                        <asp:ListItem>Гвинеја</asp:ListItem>
                                        <asp:ListItem>Гвинеа-Биса</asp:ListItem>
                                        <asp:ListItem>Гвајана</asp:ListItem>
                                        <asp:ListItem>Хаити</asp:ListItem>
                                        <asp:ListItem>Херд и Мекдоналд Острови</asp:ListItem>
                                        <asp:ListItem>Хондурас</asp:ListItem>
                                        <asp:ListItem>Хонг Конг</asp:ListItem>
                                        <asp:ListItem>Унгарија</asp:ListItem>
                                        <asp:ListItem>Исланд</asp:ListItem>
                                        <asp:ListItem>Индија</asp:ListItem>
                                        <asp:ListItem>Индонезија</asp:ListItem>
                                        <asp:ListItem>Иран</asp:ListItem>
                                        <asp:ListItem>Ирак</asp:ListItem>
                                        <asp:ListItem>Ирска</asp:ListItem>
                                        <asp:ListItem>Isle Of Man</asp:ListItem>
                                        <asp:ListItem>Израел</asp:ListItem>
                                        <asp:ListItem>Италија</asp:ListItem>
                                        <asp:ListItem>Јамајка</asp:ListItem>
                                        <asp:ListItem>Јапонија</asp:ListItem>
                                        <asp:ListItem>Џерси</asp:ListItem>
                                        <asp:ListItem>Јордан</asp:ListItem>
                                        <asp:ListItem>Казахстан</asp:ListItem>
                                        <asp:ListItem>Кенија</asp:ListItem>
                                        <asp:ListItem>Кирибати</asp:ListItem>
                                        <asp:ListItem>Кувајт</asp:ListItem>
                                        <asp:ListItem>Киргистан</asp:ListItem>
                                        <asp:ListItem>Лаос</asp:ListItem>
                                        <asp:ListItem>Латвија</asp:ListItem>
                                        <asp:ListItem>Либан</asp:ListItem>
                                        <asp:ListItem>Лешото</asp:ListItem>
                                        <asp:ListItem>Либериа</asp:ListItem>
                                        <asp:ListItem>Либија</asp:ListItem>
                                        <asp:ListItem>Лихтенштајн</asp:ListItem>
                                        <asp:ListItem>Литванија</asp:ListItem>
                                        <asp:ListItem>Луксембург</asp:ListItem>
                                        <asp:ListItem>Макао</asp:ListItem>
                                        <asp:ListItem>Мадагаскар</asp:ListItem>
                                        <asp:ListItem>Малави</asp:ListItem>
                                        <asp:ListItem>Малезија</asp:ListItem>
                                        <asp:ListItem>Малдивите</asp:ListItem>
                                        <asp:ListItem>Мали</asp:ListItem>
                                        <asp:ListItem>Малта</asp:ListItem>
                                        <asp:ListItem>Маршалски Острови</asp:ListItem>
                                        <asp:ListItem>Мартиник</asp:ListItem>
                                        <asp:ListItem>Мавританија</asp:ListItem>
                                        <asp:ListItem>Мауритус</asp:ListItem>
                                        <asp:ListItem>Мајоте</asp:ListItem>
                                        <asp:ListItem>Мексико</asp:ListItem>
                                        <asp:ListItem>Микронезија</asp:ListItem>
                                        <asp:ListItem>Молдавија</asp:ListItem>
                                        <asp:ListItem>Монако</asp:ListItem>
                                        <asp:ListItem>Монголија</asp:ListItem>
                                        <asp:ListItem>Црна Гора</asp:ListItem>
                                        <asp:ListItem>Монтсерат</asp:ListItem>
                                        <asp:ListItem>Мароко</asp:ListItem>
                                        <asp:ListItem>Мозамбе</asp:ListItem>
                                        <asp:ListItem>Мјанмар</asp:ListItem>
                                        <asp:ListItem>Намибија</asp:ListItem>
                                        <asp:ListItem>Науру</asp:ListItem>
                                        <asp:ListItem>Непал</asp:ListItem>
                                        <asp:ListItem>Холандија</asp:ListItem>
                                        <asp:ListItem>Холандски Антили</asp:ListItem>
                                        <asp:ListItem>Нова Каледонија</asp:ListItem>
                                        <asp:ListItem>Нов Зеланд</asp:ListItem>
                                        <asp:ListItem>Никарагва</asp:ListItem>
                                        <asp:ListItem>Niger</asp:ListItem>
                                        <asp:ListItem>Нигерија</asp:ListItem>
                                        <asp:ListItem>Ниуе</asp:ListItem>
                                        <asp:ListItem>Остров Норфолк</asp:ListItem>
                                        <asp:ListItem>Северна Кореја</asp:ListItem>
                                        <asp:ListItem>Северна Ирска</asp:ListItem>
                                        <asp:ListItem>Северни Маријански Острови</asp:ListItem>
                                        <asp:ListItem>Норвешка</asp:ListItem>
                                        <asp:ListItem>Не се наведени</asp:ListItem>
                                        <asp:ListItem>Оман</asp:ListItem>
                                        <asp:ListItem>Пакистан</asp:ListItem>
                                        <asp:ListItem>Палау</asp:ListItem>
                                        <asp:ListItem>Палестина</asp:ListItem>
                                        <asp:ListItem>Панама</asp:ListItem>
                                        <asp:ListItem>Папуа Нова Гвинеја</asp:ListItem>
                                        <asp:ListItem>Парагвај</asp:ListItem>
                                        <asp:ListItem>Перу</asp:ListItem>
                                        <asp:ListItem>Филипини</asp:ListItem>
                                        <asp:ListItem>Питкерн остров</asp:ListItem>
                                        <asp:ListItem>Полска</asp:ListItem>
                                        <asp:ListItem>Полинезија (Француска)</asp:ListItem>
                                        <asp:ListItem>Португалија</asp:ListItem>
                                        <asp:ListItem>Порторико</asp:ListItem>
                                        <asp:ListItem>Катар</asp:ListItem>
                                        <asp:ListItem>Реунион</asp:ListItem>
                                        <asp:ListItem>Романија</asp:ListItem>
                                        <asp:ListItem>Руска Федерација</asp:ListItem>
                                        <asp:ListItem>Руанда</asp:ListItem>
                                        <asp:ListItem>Свети Бартелеми</asp:ListItem>
                                        <asp:ListItem>Света Елена</asp:ListItem>
                                        <asp:ListItem>Свети Китс и Невис</asp:ListItem>
                                        <asp:ListItem>Света Луција</asp:ListItem>
                                        <asp:ListItem>Свети Мартин</asp:ListItem>
                                        <asp:ListItem>Сент Пјер и Микелон</asp:ListItem>
                                        <asp:ListItem>Свети Винсент и Гренадини</asp:ListItem>
                                        <asp:ListItem>Самоа</asp:ListItem>
                                        <asp:ListItem>Сан Марино</asp:ListItem>
                                        <asp:ListItem>Сао Томе и Принципе</asp:ListItem>
                                        <asp:ListItem>Саудиска Арабија</asp:ListItem>
                                        <asp:ListItem>Шкотска</asp:ListItem>
                                        <asp:ListItem>Сенегал</asp:ListItem>
                                        <asp:ListItem>Србија</asp:ListItem>
                                        <asp:ListItem>Сејшелени</asp:ListItem>
                                        <asp:ListItem>Сиера Леоне</asp:ListItem>
                                        <asp:ListItem>Сингапур</asp:ListItem>
                                        <asp:ListItem>Словачка</asp:ListItem>
                                        <asp:ListItem>Словенија</asp:ListItem>
                                        <asp:ListItem>Соломонови Острови</asp:ListItem>
                                        <asp:ListItem>Сомалија</asp:ListItem>
                                        <asp:ListItem>Јужна Африка</asp:ListItem>
                                        <asp:ListItem>Јужна Џорџија и Островите</asp:ListItem>
                                        <asp:ListItem>Јужна Кореја</asp:ListItem>
                                        <asp:ListItem>Шпанија</asp:ListItem>
                                        <asp:ListItem>Шри Ланка</asp:ListItem>
                                        <asp:ListItem>Судан</asp:ListItem>
                                        <asp:ListItem>Суринам</asp:ListItem>
                                        <asp:ListItem>Свалбард</asp:ListItem>
                                        <asp:ListItem>Свазиленд</asp:ListItem>
                                        <asp:ListItem>Шведска</asp:ListItem>
                                        <asp:ListItem>Швајцарија</asp:ListItem>
                                        <asp:ListItem>Сирија</asp:ListItem>
                                        <asp:ListItem>Тајван</asp:ListItem>
                                        <asp:ListItem>Таџикистан</asp:ListItem>
                                        <asp:ListItem>Танзанија</asp:ListItem>
                                        <asp:ListItem>Тајланд</asp:ListItem>
                                        <asp:ListItem>Тимор-Лесте</asp:ListItem>
                                        <asp:ListItem>Того</asp:ListItem>
                                        <asp:ListItem>Токелау</asp:ListItem>
                                        <asp:ListItem>Тонга</asp:ListItem>
                                        <asp:ListItem>Тринидад и Тобаго</asp:ListItem>
                                        <asp:ListItem>Тунис</asp:ListItem>
                                        <asp:ListItem>Турција</asp:ListItem>
                                        <asp:ListItem>Туркменистан</asp:ListItem>
                                        <asp:ListItem>Турк и Каикос Острови</asp:ListItem>
                                        <asp:ListItem>Тувалу</asp:ListItem>
                                        <asp:ListItem>Уганда</asp:ListItem>
                                        <asp:ListItem>Украина</asp:ListItem>
                                        <asp:ListItem>Обединетите Арапски Емирати</asp:ListItem>
                                        <asp:ListItem>САД</asp:ListItem>
                                        <asp:ListItem>Уругвај</asp:ListItem>
                                        <asp:ListItem>САД Мали далечни острови</asp:ListItem>
                                        <asp:ListItem>Узбекистан</asp:ListItem>
                                        <asp:ListItem>Вануату</asp:ListItem>
                                        <asp:ListItem>Ватикан</asp:ListItem>
                                        <asp:ListItem>Венецуела</asp:ListItem>
                                        <asp:ListItem>Виетнам</asp:ListItem>
                                        <asp:ListItem>Девствени Острови</asp:ListItem>
                                        <asp:ListItem>Велс</asp:ListItem>
                                        <asp:ListItem>Валис и Футуна</asp:ListItem>
                                        <asp:ListItem>Западна Сахара</asp:ListItem>
                                        <asp:ListItem>Јемен</asp:ListItem>
                                        <asp:ListItem>Замбија</asp:ListItem>
                                        <asp:ListItem>Зимбабве</asp:ListItem>
                                        <asp:ListItem>Аланд Острови</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Дата на раѓање:</td>
                                <td>
                                    <asp:DropDownList ID="ddlDen" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMesec" runat="server">
                                        <asp:ListItem Value="Месец">Месец</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem Value="2"></asp:ListItem>
                                        <asp:ListItem Value="3"></asp:ListItem>
                                        <asp:ListItem Value="4"></asp:ListItem>
                                        <asp:ListItem Value="5"></asp:ListItem>
                                        <asp:ListItem Value="6"></asp:ListItem>
                                        <asp:ListItem Value="7"></asp:ListItem>
                                        <asp:ListItem Value="8"></asp:ListItem>
                                        <asp:ListItem Value="9"></asp:ListItem>
                                        <asp:ListItem Value="10"></asp:ListItem>
                                        <asp:ListItem Value="11"></asp:ListItem>
                                        <asp:ListItem Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlGodina" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Регистрирај се" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <div id="right">
                        <h4>Информации околу регистрирањето</h4>
                        <p><b>Добредојдовте на Формула 1 Менаџер!</b></p>
                        <p>Пред да бидете во можност да играте постојат неколку работи што треба да направите:</p>
                        <ul id="rules">
                            <li>За да избегнете одбивање на вашето барање за креирање на профил, ве молиме користете го вашето реално име и презиме.</li>
                            <li>Препорачливо е да се користи уникатна лозинка која не ја користите на други веб страни.</li>
                            <li>Може да има само еден профил по еден играч и еден играч по компјутер. Двојни акаунти не се дозволени!</li>
                        </ul>
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
