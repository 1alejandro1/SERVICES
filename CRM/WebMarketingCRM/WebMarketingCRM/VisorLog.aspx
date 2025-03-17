<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorLog.aspx.cs" Inherits="WebMarketingCRM.VisorLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 304px;
            height: 50px;
            float: right;
        }

        .style2 {
            color: #003875;
        }

        .style4 {
            width: 160px;
            height: 45px;
            float: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width: 100%;">
                <tr>
                    <td style="background-color: #002876" colspan="2" class="auto-style1"></td>
                </tr>
                <tr>
                    <td style="text-align: center" class="style2" style="color: #002876">
                        <strong>Logs Proceso Agenda</strong></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GvwLog" runat="server" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            GridLines="Vertical" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:BoundField DataField="lejProceso" HeaderText="Proceso">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lejEstado" HeaderText="Estado">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lejMensaje" HeaderText="Mensaje">
                                    <ItemStyle Width="500px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lejFecha" HeaderText="Fecha">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#002876" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
