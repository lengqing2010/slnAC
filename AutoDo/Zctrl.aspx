<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Zctrl.aspx.vb" Inherits="Zctrl" EnableEventValidation="false" validateRequest="false"  %>

<%@ Register src="userctrl/WucEdpList.ascx" tagname="WucEdpList" tagprefix="uc1" %>

<%@ Register src="userctrl/WucDbList.ascx" tagname="WucDbList" tagprefix="uc2" %>

<%@ Register src="userctrl/WucEdpDb.ascx" tagname="WucEdpDb" tagprefix="uc3" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc4" %>

<%@ Register src="userctrl/WucLink.ascx" tagname="WucLink" tagprefix="uc5" %>

<%@ Register src="userctrl/WucTopBar.ascx" tagname="WucTopBar" tagprefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>漢字⇒EN</title>
        <link rel="stylesheet" type="text/css" href="css/Editor.css">
        <link rel="stylesheet" type="text/css" href="css/common.css">
        <link rel="stylesheet" type="text/css" href="css/Button.css">
        <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
        <!--导入js库-->

        <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
        <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>
        
</head>
<body>
    <form id="form1" runat="server">
 <%--   <uc5:WucLink ID="WucLink1" runat="server" />--%>
    <div class="header">   
        <uc6:WucTopBar ID="WucTopBar1" runat="server" />
        <uc3:WucEdpDb ID="WucEdpDb1" runat="server" />
        Tables(空白：全部Tableで検索)
        <asp:TextBox ID="tbxTableNames" runat="server" BackColor="#EEE8AA" Width = "98%" TextMode="MultiLine" Rows="3"></asp:TextBox>

        <asp:Button ID="btnChToEng" runat="server" Text="漢字ToEng" CssClass="button" /> 
        <asp:Button ID="btnDim" runat="server" Text="DIM" CssClass="button" />   
        <asp:Button ID="btnControls" runat="server" Text="CONTROLS" CssClass="button" />   

    </div>
    <div class="divBody">
        <!--代码输入框（注意请务必设置高度，否则无法显示）-->
        <table>
        <tr>
        <td>
            <uc4:WucEditor ID="WucEditor1" runat="server" EditType="txt" TEXT="日付" theme="" height="530px" />
        </td>
        <td>

                
            <uc4:WucEditor ID="WucEditor2" runat="server" EditType="sql" TEXT="select"  height="530px"  />

        </td>
        </tr>
        
        </table>
    
    </div>
    </form>
</body>
</html>
