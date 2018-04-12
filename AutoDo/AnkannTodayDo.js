
$("#pl").dblclick(function (e) {
    e = e || window.event;
    __xx = parseInt(e.pageX || e.clientX + document.body.scroolLeft);
    __yy = parseInt(e.pageY || e.clientY + document.body.scrollTop);
    CreateDiv($("#hidUser").val(), "", __xx, __yy);

});

var CreateDiv = function (user, txt, x, y) {

    var X = x;
    var Y = y;

    //var parentdiv = $("<div class='div_panel' >1111</div>");        //创建一个父div
    var htmlSr = [];

    var id = (parseInt($(".div_panel").length) + 1) + '';

    htmlSr.push("<div id='pl" + id + "' class='div_panel' ondblclick='cancelBubble();return false;'");
    htmlSr.push("style='left:" + X + "px ; top:" + Y + "px;'");

    htmlSr.push(" X='" + X + "'");
    htmlSr.push(" Y='" + Y + "'");
    htmlSr.push(">");

    htmlSr.push(" <input id='btnDel" + id + "' type='button' value='Delete' />");
    htmlSr.push(" <input id='btnSave" + id + "' type='button' value='Save' />");

    htmlSr.push("<textarea id='txt" + id + "' class='txt' cols='20' rows='5'  style='z-index:100001'>");
    htmlSr.push(txt);
    htmlSr.push("</textarea>");

    /// htmlSr.push("<p contenteditable='true'>这是一段可编辑的段落。请试着编辑该文本。</p>");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("");
    htmlSr.push("</div>");
    var parentdiv = $(htmlSr.join(""));
    //parentdiv.attr('id', 'parent');        //给父div设置id
    $("#pl").append(parentdiv); //将父div添加到body中
    $("#btnDel" + id).click(function () {


        FncDelDataToday($("#hidUser").val(), $("#pl" + id).attr("X"), $("#pl" + id).attr("Y"));
        $(parentdiv).remove();


    });
    $("#txt" + id).blur(function () {

        var Y = parseInt($(this).parent().offset().top);
        var X = parseInt($(this).parent().offset().left);
        //     345435
        FncSaveDataToday($("#hidUser").val(), $(this).text(), X, Y);
    });

    var old_x, old_y, new_x, new_y;

    $("#pl" + id).mousedown(function (e) { //e鼠标事件 
        var old_y = parseInt($(this).offset().top);
        var old_x = parseInt($(this).offset().left);

        //                $(this).attr("Y", old_y)
        //                $(this).attr("X", old_x)
        //                $("#hidX").val(old_x);
        //                $("#hidY").val(old_y);
        $(this).css("cursor", "move"); //改变鼠标指针的形状  
        var offset = $(this).offset(); //DIV在页面的位置  
        var x = e.pageX - offset.left; //获得鼠标指针离DIV元素左边界的距离  
        var y = e.pageY - offset.top; //获得鼠标指针离DIV元素上边界的距离  
        $(document).bind("mousemove", function (ev) { //绑定鼠标的移动事件，因为光标在DIV元素外面也要有效果，所以要用doucment的事件，而不用DIV元素的事件  
            $("#pl" + id).stop(); //加上这个之后  
            var _x = ev.pageX - x; //获得X轴方向移动的值  
            var _y = ev.pageY - y; //获得Y轴方向移动的值  
            $("#pl" + id).animate({
                left: _x + "px",
                top: _y + "px"
            }, 10);
        });
    });

    $(document).mouseup(function () {
        if ($("#pl" + id).length > 0) {

            //                    var new_y = parseInt($("#pl" + id).offset().top);
            //                    var new_x = parseInt($("#pl" + id).offset().left);
            //                    FncDelDataToday($("#hidUser").val(), $("#pl" + id).attr("X"), $("#pl" + id).attr("Y"));
            //                    FncSaveDataToday($("#hidUser").val(), $("#txt" + id).text(), new_x, new_y);
            $("#pl" + id).css("cursor", "default");
            $(this).unbind("mousemove");

        }

    });


    $("#btnSave" + id).click(function () {
        if ($("#pl" + id).length > 0) {

            var new_y = parseInt($("#pl" + id).offset().top);
            var new_x = parseInt($("#pl" + id).offset().left);

            FncDelDataToday($("#hidUser").val(), $("#pl" + id).attr("X"), $("#pl" + id).attr("Y"));
            FncSaveDataToday($("#hidUser").val(), $("#txt" + id).text(), new_x, new_y);

            //                    $("#pl" + id).css("cursor", "default");
            //                    $(this).unbind("mousemove");
        }

    });

    //$(document.body).html(htmlSr.join(" "));
}


function FncSaveDataToday(user, txt, x, y) {

    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        url: "AnkanSinntyokuAjax.aspx/FncSaveDataToday",
        //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
        dataType: "json",
        data: "{user:'" + user + "',txt:'" + txt + "',x:'" + x + "',y:'" + y + "'}",
        //username 为想问后台传的参数（这里的参数可有可无）
        success: function (result) {
            //alert(result.d); //result.d为后台返回的参数
        }
    });

}

function FncDelDataToday(user, x, y) {

    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        url: "AnkanSinntyokuAjax.aspx/FncDelDataToday",
        //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
        dataType: "json",
        data: "{user:'" + user + "',x:'" + x + "',y:'" + y + "'}",
        //username 为想问后台传的参数（这里的参数可有可无）
        success: function (result) {
            //alert(result.d); //result.d为后台返回的参数
        }
    });

}

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