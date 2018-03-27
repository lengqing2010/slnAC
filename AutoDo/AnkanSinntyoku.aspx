<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkanSinntyoku.aspx.vb" Inherits="AnkanSinntyoku" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    
    body
    {
    	font-size:12px;
    }
    
td
{
        	

white-space: nowrap;

vertical-align:middle;
text-align:center;
}
th
{
	text-align:center;
	vertical-align:middle;
        	
/*
vertical-align:middle;
text-align:center;
letter-spacing:20px;
word-wrap:break-word;*/
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvSintyoku" runat="server" ShowHeader="true">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
