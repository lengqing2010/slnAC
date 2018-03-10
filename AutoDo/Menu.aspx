<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MENU</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <h1>MASTER</h1> 
        <asp:LinkButton ID="lbtnEdp" runat="server" OnClientClick="window.open('P_TableEditor_m_edp.aspx')">EDP</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lbtnJobFile" runat="server" OnClientClick="window.open('0000JobKindsTest.aspx')">仕事フォルダ</asp:LinkButton>
         <hr />
       　<h1>ツール</h1>
        <asp:LinkButton ID="lbtnSiryou" runat="server" OnClientClick="window.open('MainTab.aspx')">資料</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lbtnJidouCode" runat="server" OnClientClick="window.open('ZbyDB.aspx')">自動ソース</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lbtnJidouCode2" runat="server" OnClientClick="window.open('Zctrl.aspx')">自動ソース２</asp:LinkButton>
    </div>
    </form>
</body>
</html>
