<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserDropdownList.ascx.vb" Inherits="userctrl_UserDropdownList" %>

<asp:TextBox ID="Text" runat="server" 
onclick="InitDropdownList(this)"
onfocus="InitDropdownList(this)"
onkeydown="UnInitDropdowlistByKey(this)"
onpropertychange="IputText(this)"
oninput="IputText(this)"
onblur="ChkInputText(this)"
style="border:1px solid #ccc; background-color:#fff;"
cssclass="jq_dropdownlist_wuc"
></asp:TextBox>
<div id="divList" runat="server" 
    style="height:300px; overflow:auto; display:none; float:left; position:fixed;"
    onmousedown="DropdownListClick()">
    <asp:GridView ID="List" runat="server" ShowHeader="False" AutoGenerateColumns="false" 
        CssClass="wuc_dropdownlist">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#eval("text")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<asp:HiddenField ID="hidValue" runat="server" />
<asp:Button ID="btnRun" runat="server" Text="btnRun" style="display:none;" />
<script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
<script language="javascript" type="text/javascript">
/*
    function DropdownListClick () {
        cancelBubble();
    }

    //检查输入值是否存在， 不存在 清空

    function ChkInputText(e) {
        var list;
        var text;
        text = $(e);
        list = $(e).next();
        var key = $.trim($(e).val());
        var isKeyAru;
        isKeyAru = false;


        $(list).find('tr').each(function () {
            if ($.trim($(this).text()) == key) {
                isKeyAru = true;
                return false;
            }
        });

        if (!isKeyAru) {
            text.val("");
        }

        return false;
    }
    //下拉框筛选 Oninput

    function IputText(e) {
        var list;
        var text;
        text = $(e);
        list = $(e).next();
        var key = $.trim($(e).val());

        if (key == '') {
            $(list).find('tr').show();

        } else {
            $(list).find('tr').show();
            $(list).find('tr').each(function () {
                if ($(this).text().indexOf(key) == -1) {
                    $(this).hide();
                }
            });
        }
    }

    // 下拉框隐藏 Tab 键

    function UnInitDropdowlistByKey(e) {
        var keynum = (event.keyCode ? event.keyCode : event.which);
        if (keynum == '9') {
            var list;
            var text;
            text = $(e);
            list = $(e).next();
            $(list).hide();
            $(document).unbind('click', myFun1);
            $(list).find('tr').unbind();
            event.stopPropagation();
        }
    }

    function UnInitDropdowlistByBlur(e) {
        setTimeout(function () {
            var list;
            var text;
            text = $(e);
            list = $(e).next();
            $(list).hide();
            $(document).unbind('click', myFun1);
            $(list).find('tr').unbind();
        }, 100);
    }

    // 下拉框加载

    function InitDropdownList(e) {
        var list;
        var text;
        text = $(e);
        list = $(e).next();

        //相对位置
        //        var X = $(text).offset().top;
        //        var Y = $(text).offset().left;
        //绝对位置
        var T = $(text).position().top;
        var L = $(text).position().left;

        // $(list).offset({ top: T + $(text).height(), left: L });
        $(list).css('top', T + $(text).height() + 4);
        $(list).css('left', L);
        $(list).find('tr').css('background', '#fff');
        $(list).find('tr').show();

        //表示
        list.show();

        //阻止事件冒泡
        //event.stopPropagation();
        // cancelBubble(); //为了兼容火狐




        cancelBubble();

        //绑定隐藏事件
        setTimeout(function () {

            $(document).bind('mousedown', myFun1 = function () {

                //setTimeout(function () {
                $(list).hide();
                $(document).unbind('click', myFun1);
                $(list).find('tr').unbind();
                //}, 200);
            });

            //行选择
            $(list).find('tr').mousedown(function () {
                text.val($.trim($(this).text()));
                $(list).hide();
                $(document).unbind('click', myFun1);
                $(list).find('tr').unbind();

            });




            //行变色
            $(list).find('tr').mouseenter(function () {
                $(this).css('background', '#5CACEE');
            });
            //行变色 取消
            $(list).find('tr').mouseout(function () {
                $(this).css('background', '#fff');
            });

        }, 111);


        //        $(document).one('click', function () {
        //            
        //        })


        //绑定live事件(live事件只在jquery1.9以下才支持，高版本不支持)。
        //        $(document).live('click', function () {
        //            $(list).hide();
        //        })

    }



    //得到事件

    function getEvent() {
        if (window.event) {
            return window.event;
        }
        func = getEvent.caller;
        while (func != null) {
            var arg0 = func.arguments[0];
            if (arg0) {
                if ((arg0.constructor == Event || arg0.constructor == MouseEvent || arg0.constructor == KeyboardEvent) || (typeof (arg0) == 'object' && arg0.preventDefault && arg0.stopPropagation)) {
                    return arg0;
                }
            }
            func = func.caller;
        }
        return null;
    }
    //阻止冒泡

    function cancelBubble() {
        var e = getEvent();
        if (window.event) {
            //e.returnValue=false;//阻止自身行为
            e.cancelBubble = true; //阻止冒泡
        } else if (e.preventDefault) {
            //e.preventDefault();//阻止自身行为
            e.stopPropagation(); //阻止冒泡
        }
    }
    */
</script>