<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_edp.aspx.vb" Inherits="P_TableEditor_m_edp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    edp_no
                </td>
                <td>
                    <asp:TextBox ID="tbxEdpNo" runat="server"></asp:TextBox>
                </td>
                <td>
                    varchar(20)
                </td>
            </tr>
            <tr>
                <td>
                    edp_mei
                </td>
                <td>
                    <asp:TextBox ID="tbxEdpMei" runat="server"></asp:TextBox>
                </td>
                <td>
                    varchar(200)
                </td>
            </tr>
            <tr>
                <td>
                    edp_exp
                </td>
                <td>
                    <asp:TextBox ID="tbxEdpExp" runat="server"></asp:TextBox>
                </td>
                <td>
                    varchar(1000)
                </td>
            </tr>
        </table>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
        <asp:GridView ID="gvMs" runat="server"
        autogenerateselectbutton="True"
        >
            <SelectedRowStyle BackColor="#FFFF99" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
