<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorIASpeech.aspx.cs" Inherits="WebMarketingCRM.VisorIASpeech" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <div id="mywaitmsg" style="display: none; width: 100%">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_themes/img/ajax-loader.gif" />Por favor espere se esta procesando su solicitud..
            </div>
            <asp:Button ID="btnConsultaIA" runat="server" Text="Consulta IA" OnClick="btnConsultaIA_Click" OnClientClick="mywaitdialog()" />
            <script>
                function mywaitdialog() {
                    var mywait = document.getElementById("mywaitmsg")
                    mywait.style.display = 'block';
                }
            </script>
            <br />
            <asp:TextBox ID="txtIASpeech" TextMode="MultiLine" Columns="100" Rows="6" runat="server"></asp:TextBox>
            
            
            
        </div>
    </form>
</body>
</html>
