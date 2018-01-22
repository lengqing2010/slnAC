﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Zsql.aspx.vb" Inherits="Zsql"  EnableEventValidation="false" ValidateRequest = "false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="userctrl/WucEdpList.ascx" tagname="WucEdpList" tagprefix="uc1" %>

<%@ Register src="userctrl/WucDbList.ascx" tagname="WucDbList" tagprefix="uc2" %>

<%@ Register src="userctrl/WucEdpDb.ascx" tagname="WucEdpDb" tagprefix="uc3" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc4" %>

<%@ Register src="userctrl/msgui.ascx" tagname="msgui" tagprefix="uc5" %>

<%@ Register src="userctrl/WucLink.ascx" tagname="WucLink" tagprefix="uc6" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" type="text/css" href="css/Editor.css">
        <link rel="stylesheet" type="text/css" href="css/common.css">
    <title></title>
    
        <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
        <!--导入js库-->

        <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
        <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>

        

</head>
<body>

    <form id="form1" runat="server">

    <uc6:WucLink ID="WucLink1" runat="server" />

    <div class="header">   
        <uc3:WucEdpDb ID="WucEdpDb1" runat="server" />
            <hr />
  
    </div>
    <div class="header2">  
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClientClick="" />
        <asp:Button ID="btnSelect" runat="server" Text="SELECT" OnClientClick="" /> 
        <asp:Button ID="btnSQLRUN" runat="server" Text="SQLRUN" OnClientClick="" />
 
        <hr />
        TITLE : <asp:TextBox ID="tbxTitle" runat="server" Width="705px"></asp:TextBox>
                &nbsp;|&nbsp;
        <asp:Button ID="btnNew" runat="server" Text="NEW" OnClientClick="" />
        <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClientClick="" /> 
        <asp:Button ID="btnDel" runat="server" Text="DELETE" OnClientClick="" /> 
        <hr />
    </div>
    <div>
        <table s>
        <tr>
        <td style="width:200px">
   
            <input id="Button1" type="button" value="+" onclick="$('#lstFile').width(500);" />
            <input id="Button2" type="button" value="-" onclick="$('#lstFile').width(200);"/>
            <%--     <input id="Button3" type="button" value="--" onclick="$('#lstFile').width(100);"/>--%>
            </td>
        <td style="width:800px">
            &nbsp;</td>
        <td>

                
           

            &nbsp;</td>
        </tr>
        
        <tr>
        <td style="">
   
            <asp:ListBox ID="lstFile" runat="server" Width="200" Height="400" AutoPostBack="true" style="margin-top:0; "></asp:ListBox>
        </td>
        <td style="width:790px">
            <uc4:WucEditor ID="WucEditor1" runat="server" EditType="sql" TEXT="" width="685px" />
        </td>
        <td>

                
           

        </td>
        </tr>
        
        </table>

    </div>
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gv" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true">
        <HeaderStyle BackColor="#0099FF" />
    </asp:GridView>
    </form>
</body>
</html>
