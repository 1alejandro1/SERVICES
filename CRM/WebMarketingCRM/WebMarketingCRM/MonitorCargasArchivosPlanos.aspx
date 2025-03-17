<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorCargasArchivosPlanos.aspx.cs" Inherits="WebMarketingCRM.MonitorCargasArchivosPlanos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Monitoreo de Carga Archivos Planos</title>
    <style type="text/css">
        .style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #000099;
            width: 122px;
        }

        .style3 {
            width: 132px;
        }

        .style7 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #990000;
            width: 121px;
        }

        .style9 {
            width: 218px;
            font-size: 11px;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style10 {
        }

        .style11 {
            width: 83px;
        }

        .style12 {
            width: 218px;
        }

        .auto-style2 {
            width: 152px;
        }

        .auto-style3 {
            width: 188px;
        }

        .auto-style4 {
            width: 257px;
            font-size: 11px;
            font-family: Arial, Helvetica, sans-serif;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td class="auto-style4">
                    <asp:RadioButtonList ID="rdbOpciones" runat="server"
                        OnSelectedIndexChanged="rdbOpciones_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1" Selected="True">Información de campaña</asp:ListItem>
                        <asp:ListItem Value="2">Actualización Propietarios</asp:ListItem>
                        <asp:ListItem Value="3">Información Segmentación</asp:ListItem>
                        <asp:ListItem Value="4">Registra Productos Preaprobados</asp:ListItem>
                        <asp:ListItem Value="5">Cierre de Actividades de Marketing</asp:ListItem>
                        <asp:ListItem Value="6">Cambio de Propietario por Cobranza</asp:ListItem>
                        <asp:ListItem Value="7">Actualización de Direcciones</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style1"><strong>
                    <asp:Label ID="lblCodigo" runat="server" Text="Código de Campaña:"></asp:Label>&nbsp;</strong>

                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtCodigoCampania" runat="server" Width="164px"></asp:TextBox>

                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnVerLog" runat="server"  Text="Ver Logs"
                        OnClick="btnVerLog_Click" Width="130px" Font-Size="X-Small" ForeColor="White" BackColor="#f26f28" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="style7" align="right">
                    <asp:Label ID="lblMensaje" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <asp:GridView ID="grvDatos1" runat="server" Font-Names="Tahoma" Font-Size="X-Small" BackColor="White" BorderColor="Gray"
                    BorderWidth="1px" GridLines="Vertical" CellPadding="3" AutoGenerateColumns="True">
                    <HeaderStyle BackColor="#CCCCCC" />
                    <AlternatingRowStyle BackColor="Gainsboro" />
                </asp:GridView>
                <br />
            </tr>
            <tr>
                <asp:GridView ID="grvDatos2" runat="server" Font-Names="Tahoma" Font-Size="X-Small" BackColor="White" BorderColor="Gray"
                    BorderWidth="1px" GridLines="Vertical" CellPadding="3" AutoGenerateColumns="True">
                    <HeaderStyle BackColor="#CCCCCC" />
                    <AlternatingRowStyle BackColor="Gainsboro" />
                </asp:GridView>
            </tr>
        </table>
    </form>
</body>
</html>
