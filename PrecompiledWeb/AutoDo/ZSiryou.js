$(document).ready(function () {
    //Ctrl + S
    $(window).keydown(function (e) {
        console.log(e);
        if (e.keyCode == 83 && e.ctrlKey) {
            e.preventDefault();
            //dosomething
            FncSaveData();
        }
    });
});


function FncSaveData() {
    alert(1);
    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        url: "ZSiryouAJAX.aspx/SaveData", //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
        dataType: "json",
        data: "{edpNo:'1111a'}", //username 为想问后台传的参数（这里的参数可有可无）
        success: function (result) {
            //alert(result.d); //result.d为后台返回的参数
        }
    });
    alert(2);
}