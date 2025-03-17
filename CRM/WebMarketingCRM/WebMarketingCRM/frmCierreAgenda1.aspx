<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCierreAgenda1.aspx.cs" Inherits="WebCRM.frmCierreAgenda1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	<title>Cierre Directo Agenda</title>
  
        <style type="text/css">
            .auto-style1 {
                width: 273px;
            }
            .auto-style2 {
                width: 315px;
            }
            .auto-style3 {
                width: 291px;
            }
            .auto-style4 {
                width: 211px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table style="Z-INDEX: 101; LEFT: 23px; POSITION: absolute; TOP: 30px;" cellspacing="1"
		cellpadding="1" border="0">
		<tr>
        <td class="auto-style4">Respuesta Cliente</td>
        <td class="auto-style3">
            <asp:DropDownList ID="ddlEstado" runat="server" 
                CssClass="style10" Width="240px">
                <asp:ListItem Value="0">(Seleccione un valor)</asp:ListItem>
                <asp:ListItem Value="100000000">INTERESADO EN LA OFERTA</asp:ListItem>
                <asp:ListItem Value="100000001">NO INTERESADO EN LA OFERTA</asp:ListItem>
                <asp:ListItem Value="100000002">DATOS ERRONEOS</asp:ListItem>
                <asp:ListItem Value="100000003">NO SE LO PUDO CONTACTAR</asp:ListItem>
                <asp:ListItem Value="100000004">NO QUIERE RECIBIR MAS OFERTAS</asp:ListItem>
                <asp:ListItem Value="100000005">NO CALIFICA</asp:ListItem>
                <asp:ListItem Value="100000006">INTERESADO EN OTRO PRODUCTO</asp:ListItem>
            </asp:DropDownList>
            </td>
             
        </tr>
        <tr>
            <td class="auto-style1">Fecha de oportunidad (dd/mm/yy)</td>
        <td class="auto-style2">
            <asp:TextBox ID="txtFechaCierre" runat="server" 
                BorderColor="Black" CssClass="style10" 
                Width="133px" BorderStyle="Solid" ></asp:TextBox>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"  CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_themes/img/minical.gif" OnClick="ImageButton1_Click" />
        </td>
        </tr>
        <tr>
        <td class="auto-style4">Monto cierre Bs</td>
        <td class="auto-style3">
            <asp:TextBox ID="txtIngresosReales" runat="server" 
                BorderColor="Black" CssClass="style10" MaxLength=8
                Width="232px" BorderStyle="Solid"></asp:TextBox>
            
                </td>

        </tr>
        <tr>
        <td class="auto-style4">Probabilidad de cierre %</td>
        <td class="auto-style3">
            <asp:TextBox ID="txtProbabilidadCierre" runat="server" 
                BorderColor="Black" CssClass="style10" MaxLength=3
                Width="232px" BorderStyle="Solid"></asp:TextBox>

                </td>

        </tr>
       <tr>
        <td class="auto-style4">Descripcion</td>
        <td class="auto-style3">
            <asp:TextBox ID="txtDescripcion" runat="server" BorderColor="Black" 
                CssClass="style10" Height="64px" TextMode="MultiLine" 
                Width="290px" BorderStyle="Solid"></asp:TextBox>

                </td>

        </tr>
        <tr>
        
        <td class="auto-style4">
            <asp:Button ID="btnProcesar" runat="server" CssClass="style10" Text="PROCESAR" 
                onclick="btnProcesar_Click" />
                </td>
        <td class="auto-style3">
            <asp:Label ID="lblMensaje" runat="server" BorderColor="Black" 
                BorderStyle="Solid" BorderWidth="2px" CssClass="style10" Height="40px" 
                Width="289px"></asp:Label>
                </td>

        </tr>

	</table>
	<asp:textbox id="txtEntidadTipo" runat="server" Font-Size="8pt" Width="48px" Height="24px" Visible="False"/>
    <asp:textbox id="txtEntidadNombre" runat="server" Font-Size="8pt" Width="104px" Height="24px" Visible="False"/>
    <asp:textbox id="txtEntidadId" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtFuncionario" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtFechaFin" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtUsuarioConectado" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtPadre" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtUsuarioGestiona" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    <asp:textbox id="txtMatriculaConectado" runat="server" Font-Size="8pt" Width="288px" Height="24px" Visible="False"/>
    </div>		
</form>
</body>
</html>
