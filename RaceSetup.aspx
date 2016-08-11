<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaceSetup.aspx.cs" Inherits="RaceSetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Подесувања за трка</title>
    <link href="style/Practice.css" type="text/css" rel="stylesheet" />
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
            <h3>Подесувања за трка</h3>
            <form id="form1" runat="server">
                <div>
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblNemaVozac" runat="server" Text="" Visible="false"></asp:Label>
                    
                    <div class="left">

                        <asp:GridView ID="gvVezbiSetup" runat="server" AutoGenerateColumns="false" CssClass="tabela">
                            <Columns>
                                <asp:BoundField DataField="ime" HeaderText="" />
                                <asp:BoundField DataField="nivo" HeaderText="Ниво" />
                                <asp:TemplateField HeaderText="Акција">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("istrosenost").ToString() + "%" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Подесување">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" MaxLength="3" runat="server"></asp:TextBox>(0-999)
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="right">
                        <asp:GridView ID="gvVezbiNonSetup" runat="server" AutoGenerateColumns="false" CssClass="tabela">
                            <Columns>
                                <asp:BoundField DataField="ime" HeaderText="" />
                                <asp:BoundField DataField="nivo" HeaderText="Ниво" />
                                <asp:TemplateField HeaderText="Акција">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("istrosenost").ToString() + "%" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <p>
                            Гуми:
                        <asp:DropDownList ID="ddlGumi" runat="server"></asp:DropDownList>
                        </p>
                        
                            <table style="font-size: 12px;">
                                <tr>
                                    <td style="text-align: right;">Гориво:</td>
                                    <td>
                                        <asp:TextBox ID="txtGorivo" runat="server"></asp:TextBox> (0-300)</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Ризик слободно: </td>
                                    <td>
                                        <asp:TextBox ID="txtSlobodno" runat="server"></asp:TextBox> (0-100)</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Ризик претекнување: </td>
                                    <td>
                                        <asp:TextBox ID="txtPreteknuvanje" runat="server"></asp:TextBox> (0-100)</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Ризик бранење: </td>
                                    <td>
                                        <asp:TextBox ID="txtBranenje" runat="server"></asp:TextBox> (0-100)</td>
                                </tr>
                            </table>
                            
                        
                        

                        <p>
                            <asp:Button ID="btnDrive" runat="server" Text="Вози" OnClick="btnDrive_Click" />
                        </p>
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
