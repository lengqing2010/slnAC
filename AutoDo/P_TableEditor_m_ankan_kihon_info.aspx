<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_ankan_kihon_info.aspx.vb" Inherits="P_TableEditor_m_ankan_kihon_info" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
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
                    server_siryou_path
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxServerSiryouPath" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    client_siryou_path
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxClientSiryouPath" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    code_path1
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxCodePath1" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    code_path2
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxCodePath2" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    code_path3
                </td>
                <td Width="600px">
                    <asp:TextBox ID="tbxCodePath3" runat="server" cssClass="txtbox"></asp:TextBox>
                </td>
                <td >
                    nvarchar(1000)
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
