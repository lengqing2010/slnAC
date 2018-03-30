<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkanSinntyoku.aspx.vb" Inherits="AnkanSinntyoku" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <title></title>

    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <style>
    body {
	font-size: 12px;
}

td {
	vertical-align: middle;
	text-align: center;
}

th {
	text-align: center;
	vertical-align: middle;
	white-space: nowrap;
	text-overflow: ellipsis;
	overflow: hidden;
	width: 20px;
/*
vertical-align:middle;
text-align:center;
letter-spacing:20px;
word-wrap:break-word;*/
}

.ms_left td {
	height: 16px;
	width: 16px;
}

.ms_right td {
	height: 16px;
	width: 16px;
}

.ms_header_right td {
	height: 16px;
	width: 16px;
	white-space: nowrap;
	overflow: hidden;
	text-align: center;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" FirstBlank="true"  />
    <asp:Button ID="btnSintyoku" runat="server" Text="進捗詳細" Width="86px" />

        <asp:Button ID="btnToday" runat="server" Text="今日予定" />
    <table>
        <tr>
            <td>
                <div style="width:425px">
                </div>
            </td>
            <td>
                <div style="width:3000px; text-align:left;">
                    <asp:GridView ID="gvRightHeader" runat="server" CssClass="ms_header_right" ShowHeader="false" AutoGenerateColumns="false" BackColor="lightgreen" style="text-align:center">
      
                    </asp:GridView>
                </div>
            </td>
        </tr>
        </table>
<div style="width:1200px; height:600px; overflow:auto;">



        <table>
        <tr>
            <td>
                <div style="width:425px">
                    <asp:GridView ID="gvSintyoku" runat="server" CssClass="ms_left" ShowHeader="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="kinou_mei" ItemStyle-Width="180" />
                            <asp:BoundField DataField="pgm_bunrui_name" ItemStyle-Width="50"/>
                            <asp:BoundField DataField="pgm_name"  ItemStyle-Width="80"/>
                            <asp:BoundField DataField="percent"  ItemStyle-Width="40"/>
                            <asp:BoundField DataField="yotei_jisseki"  ItemStyle-Width="20"/>
                            <asp:BoundField DataField="tantousya"  ItemStyle-Width="55"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div>
                    <asp:GridView ID="gvMs" runat="server" CssClass="ms_right" ShowHeader="false" AutoGenerateColumns="false">
      
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</div>
    <script type="text/javascript">

        $(document).ready(function () {

            var old_kinou_no_pgm_id = "";
            var old_ymd;
            var old_cell = "";
            var old_text;

            $(document).click(function () {
                if (old_cell != "") {
                    //$(old_cell).text(old_text);
                }

            });

            //保存
            $(".ms_right").find("td").click(function () {

                var kinou_no = $(this).parent().attr("kinou_no");
                var pgm_id = $(this).parent().attr("pgm_id");
                var ymd = $(this).attr("ymd");

                var yotei_jisseki = $(this).parent().attr("yotei_jisseki");
                var mark;
                if (yotei_jisseki == "0") {
                    mark = "囗";
                } else {
                    mark = "■";
                }



                if (old_kinou_no_pgm_id != kinou_no + ',' + pgm_id) {
                    old_kinou_no_pgm_id = kinou_no + ',' + pgm_id;
                    old_ymd = ymd;
                    if (old_cell != "") {
                        $(old_cell).text(old_text);
                    }
                    old_cell = $(this);
                    old_text = $(this).text();

                    $(this).text("⇒");

                } else {

                    old_cell = "";

                    var new_ymd, mx_ymd, mi_ymd;
                    new_ymd = $(this).attr("ymd")
                    if (new_ymd > old_ymd) {
                        mx_ymd = new_ymd;
                        mi_ymd = old_ymd;
                    } else {
                        mx_ymd = old_ymd;
                        mi_ymd = new_ymd;
                    }


                    $(this).parent().find("td").each(function () {
                        if ($(this).attr("ymd") >= mi_ymd && $(this).attr("ymd") <= mx_ymd) {
                            $(this).text(mark);
                        } else {
                            $(this).text("");
                        }
                    });

                    old_kinou_no_pgm_id = "";

                    FncSaveData(kinou_no, pgm_id, yotei_jisseki, mi_ymd, mx_ymd);

                }

            });

            var TimeFn = null;
            //保存
            $(".ms_left").find("td").click(function () {

                //if ($(this).parents("tr").find("td").index($(this)) == 3) {
                if ($(this).attr("cellType")=="PER"){
                    
                  
                    var kinou_no = $(this).parent().attr("kinou_no");
                    var pgm_id = $(this).parent().attr("pgm_id");
                    var per = parseInt($(this).text().replace("%", ""));
                    var e;
                    e = $(this);





                        if (per == 100) {
                            per = 0;
                        } else {
                            per = per + 20;
                        }

                        //alert(1);
                        FncPercentData(kinou_no, pgm_id, per);
                        $(e).text(per + "%");
                        //alert(2);
       
                }
            });
//            $(".ms_left").find("td").dblclick(function () {
//                clearTimeout(TimeFn);

//                if ($(this).parents("tr").find("td").index($(this)) == 3) {

//                    var kinou_no = $(this).parent().attr("kinou_no");
//                    var pgm_id = $(this).parent().attr("pgm_id");
//                    var per = parseInt($(this).text().replace("%", ""));

//                    FncPercentData(kinou_no, pgm_id, per);
//                    $(this).text(per + "%");

//                }
//            });
        });

        function FncSaveData(kinou_no, pgm_id, yotei_jisseki, mi_ymd, mx_ymd) {
          
            $.ajax({
                type: "post",
                contentType: "application/json;charset=utf-8",
                url: "AnkanSinntyokuAjax.aspx/SaveData", //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
                dataType: "json",
                data: "{kinou_no:'" + kinou_no + "',pgm_id:'" + pgm_id + "',yotei_jisseki:'" + yotei_jisseki + "',mi_ymd:'" + mi_ymd + "',mx_ymd:'" + mx_ymd + "'}", //username 为想问后台传的参数（这里的参数可有可无）
                success: function (result) {
                    //alert(result.d); //result.d为后台返回的参数
                }
            });

        }

        function FncPercentData(kinou_no, pgm_id, per) {

            $.ajax({
                type: "post",
                contentType: "application/json;charset=utf-8",
                url: "AnkanSinntyokuAjax.aspx/SavePercentData", //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
                dataType: "json",
                data: "{kinou_no:'" + kinou_no + "',pgm_id:'" + pgm_id + "',per:'" + per + "'}", //username 为想问后台传的参数（这里的参数可有可无）
                success: function (result) {
                    //alert(result.d); //result.d为后台返回的参数
                }
            });

        }
    </script>
    </div>
    </form>
</body>
</html>
