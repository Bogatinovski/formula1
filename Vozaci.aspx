<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vozaci.aspx.cs" Inherits="Vozaci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="348px" DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:BoundField DataField="IME" HeaderText="Име" />
                <asp:BoundField DataField="NACIONALNOST" HeaderText="Националност" />
                <asp:BoundField DataField="TITULI" HeaderText="Титули" />
                <asp:BoundField DataField="VKUPNA_JACINA" HeaderText="Вкупна јачина" />
                <asp:BoundField DataField="KONCENTRACIJA" HeaderText="Концентрација" />
                <asp:BoundField DataField="TALENT" HeaderText="Талент" />
                <asp:BoundField DataField="AGRESIVNOST" HeaderText="Агресивност" />
                <asp:BoundField DataField="ISKUSTVO" HeaderText="Искуство" />
                <asp:BoundField DataField="TEHNIKA" HeaderText="Техника" />
                <asp:BoundField DataField="IZDRZLIVOST" HeaderText="Издржливост" />
                <asp:BoundField DataField="MOTIVACIJA" HeaderText="Мотивација" />
                <asp:BoundField DataField="HARIZMA" HeaderText="Харизма" />
                <asp:BoundField DataField="GODINI" HeaderText="Години" />
                <asp:BoundField DataField="PLATA" HeaderText="Плата" />
                <asp:BoundField DataField="TEZINA" HeaderText="Тежина" />
                <asp:BoundField DataField="NAGRADAPRIPOTPISUVANJE" HeaderText="Награда при потпушување" />
                <asp:CommandField HeaderText="Уредување" ShowEditButton="True" CancelText="Откажи" DeleteText="Избриши" EditText="Уреди" />
            </Columns>
        </asp:GridView>
           <p > 
               <table class="auto-style1">
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label18" runat="server" Text="Ид:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server" Height="22px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox17" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Име:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Националност:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Титули:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label5" runat="server" Text="Вкупна Јачина"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label6" runat="server" Text="Концентрација:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label7" runat="server" Text="Талент"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label8" runat="server" Text="Агресивност:"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label9" runat="server" Text="Искуство:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label10" runat="server" Text="Техника:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label11" runat="server" Text="Издржливост:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label12" runat="server" Text="Мотивација:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label13" runat="server" Text="Харизма:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label14" runat="server" Text="Години:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label15" runat="server" Text="Плата:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label16" runat="server" Text="Тежина:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label17" runat="server" Text="Награда:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Додади" />
                </td>
            </tr>
        </table>

           </p>
    </div>
        <asp:Label ID="labelError" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
