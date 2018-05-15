<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" EnableEventValidation="false" validateRequest="false"%>

<%@ Register src="WucEditor.ascx" tagname="WucEditor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
        <link rel="stylesheet" type="text/css" href="css/Editor.css">
        <link rel="stylesheet" type="text/css" href="css/common.css">

        <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
        <!--导入js库-->

        <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
        <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    File/Dic Path</td>
                <td>
                    <asp:TextBox ID="tbxPath" runat="server" Width="600" Text="E:\fff\HattyuuSuuTourokuTenpo.aspx.vb"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="tbxKey1" runat="server" Width="600" Text="trim,lower|0tokey|onkeydown"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey2" runat="server" Width="600" Text="|all,no|me." 
                         style="margin-bottom: 0px"></asp:TextBox>
                     </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey3" runat="server" Width="600" Text="|0tokey|." 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey4" runat="server" Width="600" Text="save|r1|" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey5" runat="server" Width="600" Text="root||" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="tbxKey6" runat="server" Width="600" Text="trim,lower|0tokey|onchange"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey7" runat="server" Width="600" Text="|all,no|me." 
                         style="margin-bottom: 0px"></asp:TextBox>
                     </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey8" runat="server" Width="600" Text="|0tokey|." 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey9" runat="server" Width="600" Text="save|r2|" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>

            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey10" runat="server" Width="600" Text="in|r1|r2|r3" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey11" runat="server" Width="600" Text="show|r3" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey12" runat="server" Width="600" Text="root||" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey13" runat="server" Width="600" Text="trim,lower|all|onchange" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey14" runat="server" Width="600" Text="trim,lower|all|onkeydown" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey15" runat="server" Width="600" Text="save|r4|" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey16" runat="server" Width="600" Text="show|r4" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <asp:TextBox ID="tbxKey17" runat="server" Width="600" Text="" 
                         style="margin-bottom: 0px"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>


        </table>
    
    </div>
    <asp:Button ID="btnSerch" runat="server" Text="Serch" />
    <uc1:WucEditor ID="WucEditor" runat="server" EditType="vbscript" TEXT="検索" theme="" width="1000" height="500"/>
    </form>
</body>
</html>
