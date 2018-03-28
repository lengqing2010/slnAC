<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkannTodayDo.aspx.vb" Inherits="AnkannTodayDo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
    <title></title>
    
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>

<style>
td
{
	width:22px;
	height:22px;
	text-align:center;
	vertical-align:middle;
	border-color:#fff;
	border-width:0px;
	border-radius:25px;
	color:Blue;
}

.div_panel
{
	width:250px;
	height:100px;
	background-color:Yellow;
	opacity:0.6;
filter:alpha(opacity=60);
	border-radius:25px;
	border:1px solid;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvMs" runat="server" ShowHeader="false" BackColor="White">
        </asp:GridView>
    </div>

    <div style="position:absolute; z-index:10000; left: ; top;">
    </div>
    </form>

    <script type="text/javascript">

        $(document).ready(function () {
            $("td").click(function () {
                CreateDiv(this);
            });
        });


        var CreateDiv = function (e) {

            var Y = $(e).offset().top;
            var X = $(e).offset().left;

            var parentdiv = $("<div class='div_panel' style='position:absolute; z-index:10000; left:" + X + "px ; top:" + Y + "px;'>1111</div>");        //创建一个父div
            parentdiv.attr('id', 'parent');        //给父div设置id

            $(e).append(parentdiv);            //将父div添加到body中
        }
    </script>
</body>
</html>
