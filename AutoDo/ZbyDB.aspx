<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZbyDB.aspx.vb" Inherits="ZbyDB" %>

<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        DBサーバ：
        <uc1:UserDropdownList ID="ucDbServLst" runat="server" Width = "220" Height="20" JqName = "test" />

        TABLES：
        <uc1:UserDropdownList ID="ucTableLst" runat="server" Width = "200" Height="20" JqName = "test"/>
        <br />
    </div>
            
        
    
    </form>
</body>
</html>
