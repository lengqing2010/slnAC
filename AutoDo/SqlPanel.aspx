<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SqlPanel.aspx.vb" Inherits="SqlPanel" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc1" %>

<%@ Register src="userctrl/WucEdpDb.ascx" tagname="WucEdpDb" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/common.css">
    <link rel="stylesheet" type="text/css" href="css/SqlPanel.aspx.css">

    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc2:WucEdpDb ID="WucEdpDb1" runat="server" />
        <div class="divBtnPanel">
            <asp:TextBox ID="tbxTitle" runat="server" Width="705px"  BackColor="#EEE8AA" CssClass="txt"></asp:TextBox>
            <br />
            <asp:Button ID="btnSel" runat="server" Text="SELECT" CssClass="button" />
            <asp:Button ID="btnRun" runat="server" Text="RUN" CssClass="button" />
             <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Blue"></asp:Label>
        </div>    
        <uc1:WucEditor ID="WucEditor1" runat="server" EditType="sql" width="1000" height="200" />
        <div class="divTitle">
        
        </div>
        <div class="divMsPanel">

            <asp:GridView ID="MS" runat="server">
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="Freezing"/>
                <RowStyle Height="20px" Wrap="False" />
            </asp:GridView>
        </div>
    </div>
  
    <script language="javascript">

        $(document).ready(function () {
        /*
            $(".divTitle").html("<table class='tblTitle'><tr >" + $(".Freezing").html() + "</tr></table>");

            // $(".divTitle").find("th").css('background-color', 'red');
            $(".Freezing").hide();
            var i = 0;
            $(".divTitle").width($(".divMsPanel").width() - 18);
            $($(".divTitle").find("table").get(0)).width($(".Freezing").width());
    
            $(".Freezing").find("th").each(function () {

                if ($.browser.msie) {
                    $($(".divTitle").find("th").eq(i)).width($(this).width() + 0);
                } else {
                    $($(".divTitle").find("th").eq(i)).width($(this).width() + 1);
                }


                i++;
            });

            $(".divMsPanel").scroll(function () {
                // $(".divTitle").scrollTop($(this).scrollTop()); // 纵向滚动条
                $(".divTitle").scrollLeft($(this).scrollLeft()); // 横向滚动条
            });
            */

        });


    </script>
    </form>
</body>
</html>
