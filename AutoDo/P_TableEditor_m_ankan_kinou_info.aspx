<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_ankan_kinou_info.aspx.vb" Inherits="P_TableEditor_m_ankan_kinou_info" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
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
                    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test"  />
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
                    kinou_mei
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxKinouMei" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    kinou_kbn
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxKinouKbn" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    nvarchar(2)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    yotei_kousuu
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxYoteiKousuu" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    numeric(5)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    yotei_start_date
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxYoteiStartDate" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    datetime(8)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    yotei_end_date
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxYoteiEndDate" runat="server" Width="600"></asp:TextBox>
                </td>
                <td >
                    datetime(8)
                </td>
            </tr>
        </table>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
        <asp:Button ID="btnBack" runat="server" Text="Back" />
        <asp:GridView ID="gvMs" runat="server"
        autogenerateselectbutton="True"
        >
            <SelectedRowStyle BackColor="#FFFF99" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
