$(document).ready(function () {
    $('.tbxKey').bind('input propertychange', function() {
        var key;
        var keyObj = $(this);
        key = $(this).val();

        $(".txtEN").each(function(){
            if ($(this).text().indexOf(key)!=-1){
                $(".endline").focus();
                $(this).prev().children().focus();
                keyObj.focus();
                return false ;
            }
          
        });
    });
    $('.tbxKeyJP').bind('input propertychange', function() {
        var key;
        var keyObj = $(this);
        key = $(this).val();

        $(".txtEN").each(function(){
            if ($(this).text().indexOf(key)!=-1){
                $(".endline").focus();
                $(this).prev().children().focus();
                keyObj.focus();
                return false ;
            }
          
        });
    });
});