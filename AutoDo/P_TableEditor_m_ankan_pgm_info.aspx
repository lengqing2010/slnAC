﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_ankan_pgm_info.aspx.vb" Inherits="P_TableEditor_m_ankan_pgm_info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="css/new_common.css">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
	<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <table style="width: 1000px;">
            <tr>
                <td Width="200px">
                    edp_no
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxEdpNo" runat="server" Width="600" BackColor="Yellow"></asp:TextBox>
                </td>
                <td >
                    nvarchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    kinou_no
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxKinouNo" runat="server" Width="600" BackColor="Yellow"></asp:TextBox>
                </td>
                <td >
                    nvarchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_id
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmId" runat="server" Width="600" BackColor="Yellow"></asp:TextBox>
                </td>
                <td >
                    nvarchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_name
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmName" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_level
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmLevel" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(2)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_santaku_flg
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmSantakuFlg" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(2)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_sinntyoku_retu
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmSinntyokuRetu" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    numeric(5)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_last_upd_date
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmLastUpdDate" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(2)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    pgm_staus
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxPgmStaus" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    datetime(8)
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
