<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkannTodayDo.aspx.vb" Inherits="AnkannTodayDo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
    <link rel="stylesheet" type="text/css" href="./AnkannTodayDo.css">
    <title></title>
    
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./AnkannTodayDo.js"></script>

</head>
<body ">
    <form id="form1" runat="server">

    <div class="div_aro" style= "vertical-align:middle; position:absolute; width:100%; height:16px; margin-top:400px ; ">
    ←重要</div>

    <div class="div_aro" style=" text-align:center; position:absolute;width:16px; height:100%; margin-left:600px ; ">
    ↑紧急</div>

    <div id="pl" style="width:100%; height:1000px; background-color:#ccc;">
        <asp:Label ID="lblYMD" runat="server" Text="Label"></asp:Label>


 <%--       <asp:GridView ID="gvMs" runat="server" ShowHeader="false" BackColor="White">
        </asp:GridView>--%>
    </div>
<%--    <div class='div_panel' style='position:absolute; z-index:10000; left:1px ; top:1px;'> 
        <textarea class="txt" id="TextArea1" cols="20" rows="5"></textarea>
    </div>
--%>
<%--    <input id="btnDel" type="button" value="Delete" style="position:fixed; z-index:100001" />
--%>
    <asp:HiddenField ID="hidUser" runat="server" />
    <asp:HiddenField ID="hidX" runat="server" />
    <asp:HiddenField ID="hidY" runat="server" />
    </form>

    <script type="text/javascript">

    </script>
</body>
</html>
