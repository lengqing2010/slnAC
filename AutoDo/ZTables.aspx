<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZTables.aspx.vb" Inherits="ZTables" %>
<%@ Register src="userctrl/WucEdpList.ascx" tagname="WucEdpList" tagprefix="uc1" %>

<%@ Register src="userctrl/WucDbList.ascx" tagname="WucDbList" tagprefix="uc2" %>

<%@ Register src="userctrl/WucEdpDb.ascx" tagname="WucEdpDb" tagprefix="uc3" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc4" %>

<%@ Register src="userctrl/WucLink.ascx" tagname="WucLink" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link rel="stylesheet" type="text/css" href="css/Editor.css">
        <link rel="stylesheet" type="text/css" href="css/common.css">

        <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
        <!--导入js库-->

        <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
        <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>
        
</head>
<body>
    <form id="form1" runat="server">
<%--    <uc5:WucLink ID="WucLink1" runat="server" />--%>
    <div class="header">   
        <uc3:WucEdpDb ID="WucEdpDb1" runat="server" />
        TABLES:<br />
        <asp:TextBox ID="tbxTableNames" runat="server" Width = "800px" TextMode="MultiLine" Rows="3"></asp:TextBox>
        <hr />
        
        
    </div>
    <div>
    <br />
    <asp:Button ID="btnChToEng" runat="server" Text="SELECT" OnClientClick="" /> 
        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <asp:GridView ID="gv" runat="server">
            <RowStyle Height="20px" />
        </asp:GridView>
    
    </div>
        <!--代码输入框（注意请务必设置高度，否则无法显示）-->
        <table>
        <tr>
        <td>
            &nbsp;</td>
        <td>

                
            &nbsp;</td>
        </tr>
        
        </table>

    </form>
</body>
</html>
