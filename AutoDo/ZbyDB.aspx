﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZbyDB.aspx.vb" Inherits="ZbyDB" %>

<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/new_common.css">

    <link rel="stylesheet" type="text/css" href="css/Editor.css" />
    <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
    <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width:150px;">
                    Edp no:</td>
                <td>
                    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width:150px;">
                    Database name：
                </td>
                <td>
                    <uc1:UserDropdownList ID="ucDbServLst" runat="server" Width = "300" Height="20" JqName = "test" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Tables：
                </td>
                <td>
                    <uc1:UserDropdownList ID="ucTableLst" runat="server" Width = "600" Height="20" JqName = "test"/>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Action:
                </td>
                <td>
                    <asp:Button ID="btnMkSelDim" runat="server" Text="Dim " />
                    <asp:Button ID="btnMkSelSql" runat="server" Text="Select" />
                    <asp:Button ID="btnMkInsSql" runat="server" Text="Insert" />
                    <asp:Button ID="btnMkUpdSql" runat="server" Text="Update" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Result:</td>
                <td>
                    <uc2:WucEditor ID="WucEditor1" runat="server" EditType="vbscript"
                        width="800px"
                        height="540px"
                         />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
        
        <br />
        
        
        
    </div>
            
        
    
    </form>
</body>
</html>
