<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trka.aspx.cs" Inherits="TreningVozac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Трка</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 116px;
        }
        .auto-style3 {
            width: 116px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
    </style>
    <link href="style/primer.css" type="text/css" rel="stylesheet" />
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
            
        </div>
        <div id="main">
            <h3>Трка</h3>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="237px" CssClass="tabela">
            <Columns>
                <asp:BoundField DataField="sezonabroj" HeaderText="Сезона број" />
                <asp:BoundField DataField="trkabroj" HeaderText="Трка број" />
                <asp:BoundField DataField="pateka" HeaderText="Патека" />
                <asp:BoundField DataField="vreme" HeaderText="Време" />
                <asp:BoundField DataField="sledna" HeaderText="Следна" />
                <asp:CommandField DeleteText="Избриши" EditText="Уреди" HeaderText="Уреди" InsertText="Додади" ShowEditButton="True" />
                <asp:CommandField DeleteText="Избриши" HeaderText="Избриши" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Сезона број:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Трка број:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Патека:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label4" runat="server" Text="Време:"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label5" runat="server" Text="Следна:"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Додади" />
            <asp:Label ID="labelError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
           
        </table>
    </form>
            </div>
        <div id="bottom">
            <p>&copy; Сите права се задржани</p>
            <p><a href="">Услови на користење</a> | <a href="">Политика на приватност</a></p>

        </div>
    </div>
</body>
</html>
