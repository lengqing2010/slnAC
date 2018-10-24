/*===============================================================*/
/*    関数  ：Enterキーフォーカス移動 , Trim Right               */
/*                                                               */
/*                                                               */
/*    引数  ：                                                   */
/*    履歴  ：                                                   */
/*===============================================================*/
function KeyEnter() {
    //Enter　Key時	
    if (event.keyCode == 13) {
        //Text　AREA　OK
        if (event.srcElement.type == 'textarea') {
            return true;
        }
        //ボタン　　　OK
        if (event.srcElement.type == 'submit' || event.srcElement.type == 'button') {
            event.keyCode = 0;
            return true;
        }
        //Link　　　　OK
        if (event.srcElement.tagName == 'A') {
            event.keyCode = 0;
            return true;
        }
        //上記以外　Tab　Key           
        event.keyCode = 9;
        var e = event ? event : window.event;
        var form = document.getElementById('form1');
        if (e.keyCode == 13) {
            var tabindex = document.activeElement.getAttribute('tabindex');
            tabindex++;
            var inputs = form.getElementsByTagName('input');
            for (var i = 0, j = inputs.length; i < j; i++) {
                if (inputs[i].getAttribute('tabindex') == tabindex) {
                    inputs[i].focus();
                    break;
                }
            }

            inputs = form.getElementsByTagName('select');
            for (var i = 0, j = inputs.length; i < j; i++) {
                if (inputs[i].getAttribute('tabindex') == tabindex) {
                    inputs[i].focus();
                    break;
                }
            }
        }
        return false;
    }

    return true;
}

String.prototype.Trim = function () {
    var value = this.replace(/^\s+|\s+$/g, "");
    return value.replace(/(^　*)|(　*$)/g, "");
}

function right(mainStr, lngLen) {
    if (mainStr.length - lngLen >= 0 && mainStr.length >= 0 && mainStr.length - lngLen <= mainStr.length) {
        return mainStr.substring(mainStr.length - lngLen, mainStr.length)
    } else {
        return null
    }
}

function m_chkFoundSymbol(strInput) {
    var char1;
    for (i = 0; i < strInput.length; i++) {
        char1 = strInput.charAt(i);
        if (char1 == "'" || char1 == '"' || char1 == "&" || char1 == '<' || char1 == '>' || char1 == ',') {
            return true;
            break;
        }
    }
    return false;
}

function fncGetLengthB(data, keta) {
    var ch1;
    var ch2;
    var intLen = 0;
    var wkstr = data;
    for (i = 0; i < wkstr.length; i++) {
        ch1 = wkstr.charCodeAt(i);
        if (ch1 >= 0 && ch1 <= 255) {
            intLen = intLen + 1;
        } else {
            intLen = intLen + 2;
        }
    }
    if (parseInt(intLen, 10) > parseInt(keta, 10)) {
        return false;
    } else {
        return true
    }
}

/*===============================================================*/
/*    関数  ：JQUERY                                             */
/*                                                               */
/*                                                               */
/*    引数  ：                                                   */
/*    履歴  ：                                                   */
/*            2018/09/11 P-53354 李松濤 新規作成                 */
/*===============================================================*/
$("#divKM").hide();

var SelectRow;

$(document).ready(function () {

    /*===============================================================*/
    /*行選択                                 
    /*===============================================================*/
    $(".jq_ms tr").click(function () {
        $('.jq_upd').removeAttr("disabled");
        $('.jq_del').removeAttr("disabled");
        if (SelectRow == null) { } else {
            $(SelectRow).css("background", "#ffffff");
        }
        $(this).css("background", "#ffff66");
        SelectRow = $(this);

        $(this).find("td").each(function () {
            var className = $(this).attr("class");
            var ipt = className + '_ipt';
            $("." + ipt).val($(this).text())

        });
    })

    /*===============================================================*/
    /*明細部↑↓Key 押下                                 
    /*===============================================================*/
    //明細部↑↓
    $(".jq_ms_div").keydown(function (event) {
        if (SelectRow == null) {return true;}

        var keycode = event.which;
        if (keycode == 38) {
            if (SelectRow.prev()) {
                $(".jq_ms_div").scrollTop($(".jq_ms_div").scrollTop() - 21);
                SelectRow.prev().click();
                return false;
            }

        } else if (keycode == 40) {
            if (SelectRow.next()) {
                $(".jq_ms_div").scrollTop($(".jq_ms_div").scrollTop() + 21);
                SelectRow.next().click();
                return false;
            }

        }
    });

});



/*===============================================================*/
/*１１．チェック関数                                  
/*===============================================================*/
//数字
function chkHankakuSuuji(str) {
    var ch;
    var wkstr = str;
    var i;
    for (i = 0; i < wkstr.length; i++) {
        ch = wkstr.substring(i, i + 1);
        if ((ch >= '0' && ch <= '9')) { } else {
            return false;
        }
    }
    return true;
}

//桁数
function fncChkSiteinaiMoji(str, keta) {
    var iLength;
    iLength = str.length;
    if (parseInt(keta, 10) != parseInt(iLength, 10)) {
        return false;
    } else {
        return true;
    }
}


//日付チェック
function GetDateFormat(e) {
    var v;
    var Y;
    var M;
    var D;
    Y = "";
    M = "";
    D = "";
    v = SetDateNoSign(e.value, " ");

    if (v.split("/").length == 3) {
        Y = v.split("/")[0];
        M = v.split("/")[1];
        D = v.split("/")[2];

    } else if (v.split("-").length == 3) {
        Y = v.split("-")[0];
        M = v.split("-")[1];
        D = v.split("-")[2];
    } else {

        v = SetDateNoSign(v, "/");
        v = SetDateNoSign(v, "-");

        if (v.length == 6) { //6桁の場合
            if (v.substring(0, 2) > 70) {
                v = "19" + v;
            } else {
                v = "20" + v;
            }

        } else if (v.length == 4) { //4桁の場合
            dd = new Date();
            v = dd.getFullYear() + v;

        }

        if (v.length == 8) {
            Y = v.substring(0, 4);
            M = v.substring(4, 6);
            D = v.substring(6, 8);
        }
    }

    if (Y.length == 2 && Y.substring(0, 2) > 70) {
        Y = "19" + Y;
    }

    if (Y.length == 2 && Y.substring(0, 2) <= 70) {
        Y = "20" + Y;
    }

    if (Y == 'undefined' || Y == undefined || M == 'undefined' || M == undefined || D == 'undefined' || D == undefined || M.length > 2 || D.length > 2 || Y.length == 3) {
        return false;
    }


    if (M.length == 1) {
        M = "0" + M;
    }
    if (D.length == 1) {
        D = "0" + D;
    }

    if (!checkDateVali(Y, M, D) || Y < "1753") {
        return false;

    } else {
        e.value = Y + "/" + M + "/" + D;
        return Y + "/" + M + "/" + D;

    }
}

function SetDateNoSign(value, sign) {

    var arr;
    var strResult = "";
    arr = value.split(sign);

    var i;

    for (i = 0; i <= arr.length - 1; i++) {
        if (arr[i].length == 1) {
            arr[i] = "0" + arr[i];
        }
    }

    strResult = arr.join("");
    strResult = strResult == "" ? value : strResult;

    return strResult;
}

function checkDateVali(y, m, d) {
    var di = new Date(y, m - 1, d);
    if (di.getFullYear() == y && di.getMonth() == m - 1 && di.getDate() == d) {
        return true;
    }
    return false;
}



/*===============================================================*/
/*１２．ボタン使用不可関数                                  
/*===============================================================*/

var processing = false;
var isCsv = false;

function disableButton() {
    if (isCsv) {
        isCsv = false;

    } else {
        setTimeout('disableAfterTimeout()', 0);
        processing = true;
        return true;

    }

}

function disableAfterTimeout() {
    for (var i = 0; i < document.forms.length; i++) {
        c_form = document.forms[i];
        for (var j = 0; j < c_form.elements.length; j++) {
            if (c_form.elements[j].type == 'submit' || c_form.elements[j].type == 'button' || c_form.elements[j].type == 'radio') {
                c_form.elements[j].disabled = true;
            }
        }
    }
}
window.onactivate = function () {
    return disableButton();
}