<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkannKanri.aspx.vb" Inherits="AnkannKanri" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
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
                    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" />
                </td>
                <td >
                    &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Edit" />
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    機能</td>
                <td Width="600px">
                    <uc1:UserDropdownList ID="ucKinouLst" runat="server" Width = "300" Height="20" JqName = "test" /></td>
                <td >
                    &nbsp;<asp:Button ID="btnUpdateKinou" runat="server" Text="Edit" /></td>
            </tr>
            <tr>
                <td Width="200px">
                    &nbsp;</td>
                <td Width="600px">
                    <asp:RadioButton ID="rbSinki" runat="server" GroupName="kinoukbn" Text = "新規" />
                    <asp:RadioButton ID="rbSyusei" runat="server"  GroupName="kinoukbn" Text = "修正" />
                </td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td Width="200px">
                    作成必要PGM</td>
                <td Width="600px">
                    &nbsp;</td>
                <td >
                    <asp:Button ID="btnPgmUpd" runat="server" Text="Edit" /></td>
            </tr>
            <tr>
                <td Width="200px">
                    &nbsp;</td>
                <td Width="600px">
                    <asp:Panel ID="PLPgm" runat="server">
                    </asp:Panel>
                </td>
                <td >
                    &nbsp;</td>
            </tr>
            </table>
    </div>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
