﻿<%@ page language="VB" autoeventwireup="false" inherits="P_TableEditor_m_default_info, App_Web_kb05jwdc" %>

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
                    user_id
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxUserId" runat="server" Width="600" BackColor="Yellow"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    data_source
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDataSource" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_name
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbName" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    edp_no
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxEdpNo" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    varchar(20)
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
