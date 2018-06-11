<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cp.aspx.vb" Inherits="Cp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/common_body.css" />
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="tbxTeamName" runat="server" Text = "米内罗竞技"></asp:TextBox>
        <asp:TextBox ID="tbxVistName" runat="server" Text = "弗拉门戈"></asp:TextBox>


        <asp:DropDownList ID="ddlLeague_name" runat="server">

        <asp:ListItem>巴甲</asp:ListItem>
        <asp:ListItem>美职联</asp:ListItem>
        <asp:ListItem>挪超</asp:ListItem>
        <asp:ListItem>日职联</asp:ListItem>
        <asp:ListItem>日职乙</asp:ListItem>
        </asp:DropDownList>

        场数：
        <asp:DropDownList ID="ddlTop" runat="server">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
        <asp:ListItem>40</asp:ListItem>
        </asp:DropDownList>

        <asp:DropDownList ID="ddlZKQ" runat="server">
        <asp:ListItem>主</asp:ListItem>
        <asp:ListItem>客</asp:ListItem>
        <asp:ListItem>全</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnSel" runat="server" Text="Sel" />
        <asp:Button ID="btnSelAll" runat="server" Text="SelAll" />
        <asp:Button ID="btnKeisan" runat="server" Text="计算" />
        <hr />
         <asp:Label ID="lblKeisanResult" runat="server" Text="Label"></asp:Label>
       <hr />
        <asp:Label ID="lblHalf" runat="server" Text="Label"></asp:Label>
        <hr />
        <asp:Label ID="lblWhole" runat="server" Text="Label"></asp:Label>
        <br />
        主<hr />
        <asp:GridView ID="gvHome" runat="server">
        </asp:GridView>
        客<hr />
        <asp:GridView ID="gvVist" runat="server">
        </asp:GridView>

     
    <asp:GridView ID="gvAll" runat="server">
    </asp:GridView>
    </div>
    </form>
</body>
</html>
