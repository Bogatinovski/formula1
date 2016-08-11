<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kvalifikacii.aspx.cs" Inherits="Kvalifikacii" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Резултати од квалификации</title>
    <link href="style/Shortlist.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="style/menu.css" />
    <style>
        #menu{
            text-align: center;
        }
        .dropdown{
             display: block;
             text-align: center;
            width: 200px;
            margin: 0 auto;
        }
    </style>
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
            <h3>Резултати од квалификации</h3>
            <form id="form1" runat="server">
                <div>
                    <asp:DropDownList ID="ddlGrupi" runat="server" AutoPostBack="true" OnTextChanged="ddlGrupi_TextChanged" CssClass="dropdown" ></asp:DropDownList>
                
                <div class="column-30">
                    <asp:GridView ID="gvPrvaKval" runat="server" CssClass="tabela" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Менаџер">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ime").ToString()  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Време">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# prikaziVreme(Eval("vreme").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        

                    </asp:GridView>
                </div>
                <div class="column-30">
                    <asp:GridView ID="gvVtoraKval" runat="server" CssClass="tabela" AutoGenerateColumns="false">
                         <Columns>
                            <asp:TemplateField HeaderText="Менаџер">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ime").ToString()  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Време">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# prikaziVreme(Eval("vreme").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                    <div class="column-30" style="margin-right: 0">
                    <asp:GridView ID="gvVkupno" runat="server" CssClass="tabela" AutoGenerateColumns="false">
                         <Columns>
                            <asp:TemplateField HeaderText="Менаџер">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ime").ToString()  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Време">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# prikaziVreme(Eval("vreme").ToString())  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Време">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# prikaziVreme(Eval("zaostanuvanje").ToString())  %>'></asp:Label>
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
