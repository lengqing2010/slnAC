<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZbyDB.aspx.vb" Inherits="ZbyDB" %>

<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc2" %>

<%@ Register src="userctrl/WucTopBar.ascx" tagname="WucTopBar" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <link rel="stylesheet" type="text/css" href="css/new_common.css">

    <link rel="stylesheet" type="text/css" href="css/Editor.css" />
    <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
    <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    <uc3:WucTopBar ID="WucTopBar1" runat="server" />
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width:150px;">
                    Edp no/Db name：</td>
                <td>
                    
                    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" />
                    <uc1:UserDropdownList ID="ucDbServLst" runat="server" Width = "300" Height="20" JqName = "test" />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td>
                    Table key</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Rows="3" TextMode="MultiLine" 
                        Width="795px"></asp:TextBox>


                </td>
                <td>
                    &nbsp;</td>
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
                    Vb Code:
                </td>
                <td>
                    <asp:Button ID="btnMkSelDim" runat="server" Text="Dim " />
                    <asp:DropDownList ID="ddlParamType" runat="server">
                        <asp:ListItem Value="SqlParam">SqlParam</asp:ListItem>
                        <asp:ListItem Value="NoParam">NoParam</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="cbNote" runat="server" Checked="true" Text="Note it" />
                    <asp:Button ID="btnMkSelSql" runat="server" Text="Select" />
                    <asp:Button ID="btnMkInsSql" runat="server" Text="Insert" />
                    <asp:Button ID="btnMkUpdSql" runat="server" Text="Update" />
                    <asp:Button ID="btnMkBulkcopy" runat="server" Text="BulkCopy" />

                    <asp:Button ID="btnAspControls" runat="server" Text=".net Controls" />
                    <br />
                    <asp:TextBox ID="tbxSavePagePath" runat="server" Width="354px" Text ="E:\案件\AutoMakeCode\AutoCode\slnAC\AutoDo\App_Code\"></asp:TextBox>

                    <asp:Button ID="btnMKPage" runat="server" Text="Make Page" />
                    <asp:Button ID="btnMKPageReal" runat="server" Text="Make Page vb.net2005" />
                    <asp:Button ID="btnMKPageRealNew" runat="server" Text="Make Page vb.net2005New" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    Sql Code:
                </td>
                <td>
                    <asp:Button ID="t" runat="server" Text="Xxx " />
                    <asp:Button ID="btnSelectSql" runat="server" Text="Select" />
                    <asp:Button ID="btnInsSql" runat="server" Text="Insert" />
                    <asp:Button ID="btnUpdSql" runat="server" Text="Update" />
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
