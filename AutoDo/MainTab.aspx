<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MainTab.aspx.vb" Inherits="MainTab" %>

<%@ Register src="userctrl/WucLink.ascx" tagname="WucLink" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <link rel="stylesheet" type="text/css" href="css/common.css">
            <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <uc1:WucLink ID="WucLink1" runat="server" />

    <div class="divBody" style="overflow:hidden; width:1054px; height:750px"> 
   <%-- 1.--%>
        <iframe id = "fractrl" class="fra" runat="server" src="Zsql.aspx" width="100%" height="800px">
        </iframe>
   <%-- 2.--%>
        <iframe id = "frasql" class="fra" runat="server" src="Zctrl.aspx" width="100%" height="800px">
        </iframe>
   <%-- 3.--%>
        <iframe id = "fraZsqlEdit" class="fra" runat="server" src="ZTables.aspx" width="100%" height="800px">
        </iframe>
   <%-- 3.--%>
        <iframe id = "fraZSiryou" class="fra" runat="server" src="ZSiryou.aspx" width="100%" height="800px">
        </iframe>
   <%-- 4.--%>
        <iframe id = "fraSqlSeiri" class="fra" runat="server" src="http://www.dpriver.com/pp/sqlformat.htm" width="100%" height="800px">
        </iframe> 
   <%-- 5.--%>
        <iframe id = "fraNihon" class="fra" runat="server" src="https://www.excite.co.jp/world/chinese/" width="100%" height="800px">
        </iframe>
   <%-- 6.--%>
        <iframe id = "fraBeaul" class="fra" runat="server" src="http://tools.jb51.net/code/css" width="100%" height="800px">
        </iframe>
   <%-- 7.--%>
<%--        <iframe id = "Iframe1" class="fra" runat="server" src="https://www.office.com/?auth=2&home=1" width="100%" height="800px">
        </iframe>--%>

        
    </div>

    </form>
    <script language="javascript">
        $(document).ready(function () {

            //$(".fra").hide();

            $(".toplink").click(function () {
                $(".toplink").css("background", "#000");
                $(".toplink").css("color", "#fff");  

                $(this).css("background", "#fff");
                $(this).css("color", "#000"); 
                var pIdx = $(this).index();

                $(".fra").hide();

                $(".fra").each(function () {
                    if ($(this).index() == pIdx) {
                        $(this).show(100);
                        return false;
                    }

                });
                return false;
            });

        });
    
    </script>

</body>
</html>
