<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_db_info.aspx.vb" Inherits="P_TableEditor_m_db_info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="css/new_common.css">
        <link rel="stylesheet" type="text/css" href="css/Button.css">
    <link rel="stylesheet" type="text/css" href="css/Text.css">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
	<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <table style="width: 1000px;">
            <tr>
                <td Width="200px">
                    data_source
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDataSource" runat="server" cssClass="txtbox" BackColor="Yellow"></asp:TextBox>
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
                    <asp:TextBox ID="tbxDbName" runat="server" cssClass="txtbox" BackColor="Yellow"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_type
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbType" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(20)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_user_id
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbUserId" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_password
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbPassword" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(100)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_enlist
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbEnlist" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(5)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_conn
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbConn" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(500)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    db_exp
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxDbExp" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    varchar(1000)
                </td>
            </tr>
        </table>
        <div class="btn_panel">
        
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
            <asp:Button ID="btnInsert" runat="server" Text="Insert" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" />
            <asp:Button ID="btnBack" runat="server" Text="Back" />

        </div>
        <asp:GridView ID="gvMs" runat="server"
        autogenerateselectbutton="True"
        >
            <SelectedRowStyle BackColor="#FFFF99" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
