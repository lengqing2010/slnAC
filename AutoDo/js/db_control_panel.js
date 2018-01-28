$(document).ready(function () {
    $('.tbxKey').bind('input propertychange', function () {
        var key;
        var keyObj = $(this);
        key =  $.trim($(this).val());


        var tab = $('.db_ms_div')

        $(".txtEN,.txtJP").each(function () {


            if ($(this).text().indexOf(key) != -1) {
                //$(this).parent().hide();
                //$(".endline").focus();
                //$(this).prev().children().focus();
                //keyObj.focus().val(key);
                //return false ;
                scrollTo = $(this); //获取指定行的元素

                tab.scrollTop(
                    scrollTo.offset().top - tab.offset().top + tab.scrollTop()
                );

            } else {

            }

        });
    });
});


