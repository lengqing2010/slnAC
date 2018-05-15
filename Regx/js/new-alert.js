//弹出自定义提示窗口
var showAlert = function (msg, e) {
    //弹框存在
    if ($("#alert_box").length > 0) {
        $('#pop_box_msg').html(msg);
    } else {
        var alertHtml = '<div id="alert_box">'
                    + '<div class="cover"  id="cover_alert"  onclick="closeAlert()"></div>'
                    + '     <div class="pop_box  " id="pop_box_alert" onclick="closeAlert()">'
                    + '         <div class="pop_img">'
                    + '         </div>'
                    + '         <div class="pop_center">'
                    + '         !:' + msg + ''
                    + '         </div>'
                    + '         <div class="pop_img">'
                    + '         </div>'
                    + '     </div>'
                    + '</div>';
        $("body").append(alertHtml);
    }

    setTimeout(function () {
        if (!e || e == null) {
            $(".pop_box").width(300);
            $(".pop_box").height(200);
            $("#alert_box").offset({ top: 200, left: $(window).width() / 2 - $(".pop_box").width()/2 });

            $("#alert_box").show();
        } else {
            showPanel(e);
        }
    }, 100);
}

//重定义alert
window.$alert = showAlert;

//点击遮罩关闭
function closeAlert() {
    $("#alert_box").hide();
    $("#alert_box").remove();
}

function showPanel(target) {
    $("#alert_box").offset({ top: $(target).offset().top-$(".pop_box").height() -4 , left: $(target).offset().left });
    $("#alert_box").show();
    $(target).focus();
    //+ $(target).height()
}