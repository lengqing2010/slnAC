<%@ page language="VB" autoeventwireup="false" inherits="Default5, App_Web_kb05jwdc" %>

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
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
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
