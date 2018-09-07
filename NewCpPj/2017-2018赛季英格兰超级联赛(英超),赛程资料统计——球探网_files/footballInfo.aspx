
var ShowAd=true;
if(window.location.href.indexOf("my=ad")>0)  ShowAd=true;
var ShowOut =true;
if (window.location.href.indexOf("my=ad1") > 0) ShowOut = true;
var headAdArr = new Array();
headAdArr[0] = "$";
headAdArr[1] = "$";
headAdArr[2] = "<!--div class='item' style='width:950px;'><a href='http://mp.win007.com/145' target='_blank'><img src='http://img2.titan007.com/image/aaddd54.gif' width='950' height='45' border='0' /></a><i></i></div-->$";
headAdArr[3] = "$";

function showHead1() {
    return getAdHtml(headAdArr[0]) + getAdHtml(headAdArr[1]);
}

function showHead2() {
    return getAdHtml(headAdArr[2]);
}
function showHead3() {
return getAdHtml(headAdArr[3]);
}
function getAdHtml(html) {
    var retVal = "";
    if (ShowAd) {
        var arr = html.split('$');
        if (arr.length == 2 && arr[1] != '' && ShowOut)
            retVal = arr[1];
        else
            retVal = arr[0];
    }
    return retVal;
}
