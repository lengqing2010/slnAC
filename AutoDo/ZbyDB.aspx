<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZbyDB.aspx.vb" Inherits="ZbyDB" %>

<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
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
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        
        
        <br />
        
        
        
    </div>
            
        
    
    </form>
</body>
</html>
