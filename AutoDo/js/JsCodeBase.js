/*-----------------------------------String-----------------------------------*/
/**
* 删除左右两端的空格
*/
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, '');
}
/**
* 删除左边的空格
*/
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, '');
}
/**
* 删除右边的空格
*/
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, '');
}
/**
* Right
*/
String.prototype.right = function (length) {
    if (this.length - length >= 0 && this.length >= 0 && this.length - length <= this.length) {
        return this.substring(this.length - length, this.length);
    }
    else { return this }
}
/**
* Left
*/
String.prototype.left = function (length) {
    if (this.length - length >= 0 && this.length >= 0 && this.length - length <= this.length) {
        return this.substring(0, length);
    }
    else { return this }
}

/**
* ToDate
*/
String.prototype.toDate = function () {
    return new Date(this); 
}

/*-----------------------------------StringBuilder-----------------------------------*/
function StringBuilder() {
    this.strings = new Array;
};
StringBuilder.prototype.append = function (str) {
    this.strings.push(str);
};
StringBuilder.prototype.appendLine = function (str) {
    this.strings.push(str+'\n');
};
StringBuilder.prototype.toString = function () {
    return this.strings.join('');
};


/*-----------------------------------Date-----------------------------------*/
Date.prototype.Format = function (formatStr) {
    var str = formatStr;
    var Week = ['日', '一', '二', '三', '四', '五', '六'];

    str = str.replace(/yyyy|YYYY/, this.getFullYear());
    str = str.replace(/yy|YY/, (this.getFullYear() % 100) > 9 ? (this.getFullYear() % 100).toString() : '0' + (this.getFullYear() % 100));

    str = str.replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1));
    str = str.replace(/M/g, (this.getMonth() + 1));

    str = str.replace(/w|W/g, Week[this.getDay()]);

    str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
    str = str.replace(/d|D/g, this.getDate());

    str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
    str = str.replace(/h|H/g, this.getHours());
    str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
    str = str.replace(/m/g, this.getMinutes());

    str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
    str = str.replace(/s|S/g, this.getSeconds());

    return str;


}

/*-----------------------------------64解码-----------------------------------*/
//JS实现base64解码
function base64_decode(data) {
    var b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var o1, o2, o3, h1, h2, h3, h4, bits, i = 0, ac = 0, dec = "", tmp_arr = [];
    if (!data) { return data; }
    data += '';
    do {
        h1 = b64.indexOf(data.charAt(i++));
        h2 = b64.indexOf(data.charAt(i++));
        h3 = b64.indexOf(data.charAt(i++));
        h4 = b64.indexOf(data.charAt(i++));
        bits = h1 << 18 | h2 << 12 | h3 << 6 | h4;
        o1 = bits >> 16 & 0xff;
        o2 = bits >> 8 & 0xff;
        o3 = bits & 0xff;
        if (h3 == 64) {
            tmp_arr[ac++] = String.fromCharCode(o1);
        } else if (h4 == 64) {
            tmp_arr[ac++] = String.fromCharCode(o1, o2);
        } else {
            tmp_arr[ac++] = String.fromCharCode(o1, o2, o3);
        }
    } while (i < data.length);
    dec = tmp_arr.join('');
    dec = utf8_decode(dec);
    return dec;
}

//JS实现utf8解码
function utf8_decode(str_data) {
    var tmp_arr = [], i = 0, ac = 0, c1 = 0, c2 = 0, c3 = 0; str_data += '';
    while (i < str_data.length) {
        c1 = str_data.charCodeAt(i);
        if (c1 < 128) {
            tmp_arr[ac++] = String.fromCharCode(c1);
            i++;
        } else if (c1 > 191 && c1 < 224) {
            c2 = str_data.charCodeAt(i + 1);
            tmp_arr[ac++] = String.fromCharCode(((c1 & 31) << 6) | (c2 & 63));
            i += 2;
        } else {
            c2 = str_data.charCodeAt(i + 1);
            c3 = str_data.charCodeAt(i + 2);
            tmp_arr[ac++] = String.fromCharCode(((c1 & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
            i += 3;
        }
    }
    return tmp_arr.join('');
}


/*-----------------------------------JS固定在网页顶部不随浏览滚动而消失的DIV层-----------------------------------*/

/*
*滚动条滑动，位置不变的DIV层
*div_id：DIV的ID属性值，必填参数
*offsetTop：滚动条滑动时DIV层距顶部的高度，可选参数
*/
function fixDiv(div_id, offsetTop) {
    var Obj = $('#' + div_id);
    if (Obj.length != 1) { return false; }
    var offsetTop = arguments[1] ? arguments[1] : 0;
    var ObjTop = Obj.offset().top;
    var isIE6 = $.browser.msie && $.browser.version == '6.0';
    if (isIE6) {
        $(window).scroll(function () {
            if ($(window).scrollTop() <= ObjTop) {
                Obj.css({
                    'position': 'relative',
                    'top': 0
                });
            } else {
                Obj.css({
                    'position': 'absolute',
                    'top': $(window).scrollTop() + offsetTop + 'px',
                    'z-index': 1
                });
            }
        });
    } else {
        $(window).scroll(function () {
            if ($(window).scrollTop() <= ObjTop) {
                Obj.css({
                    'position': 'relative',
                    'top': 0
                });
            } else {
                Obj.css({
                    'position': 'fixed',
                    'top': 0 + offsetTop + 'px',
                    'z-index': 1
                });
            }
        });
    }
}


/*-----------------------------------Check-----------------------------------*/
//半角英字チェック（a～zとA～Z）
String.prototype.IsHalfEng = function () {
    if (this.match(/[^a-z\^A-Z]/) != null) {
        return false;
    }
    else {
        return true;
    }
    return true;
};

//半角英数字チェック（a～zとA～Zと0～9）
String.prototype.IsHalfEngNum = function () {
    if (this.match(/[^a-z\^A-Z\^0-9]/) != null) {
        return false;
    }
    else {
        return true;
    }
    return true;
};

//半角英数字チェック（a～zとA～Zと0～9と-）
String.prototype.IsHalfEngNegative = function () {
    if (this.match(/[^a-z\^A-Z\^0-9\^-]/) != null) {
        return false;
    }
    else {
        return true;
    }
    return true;
};

//半角数字チェック（0～9）
String.prototype.IsHalfNumber = function () {
    if (this != '') {
        if (this.match(/[^0-9]/) != null) {
            return false;
        }
    }
    return true;
};


/*-----------------------------------Dictionary-----------------------------------*/
//キーと値のコレクション対象を生成する
function Dictionary() {
    this.dic    = {};
    this.dic.Keys = []; //キー配列
    this.dic.Values = []; //値配列
};
Dictionary.prototype.AddItem = function (key, value) {
    var kcnt = this.dic.Keys.length;
    if (kcnt > 0) {
        for (var i = 0; i < kcnt; i++) {
            //キーが存在する場合、値を更新する
            if (this.dic.Keys[i] == key) {
                this.dic.Values[i] = value;
                return ;
            }
        }
        //キーが存在しない場合、値を追加する
        this.dic.Keys.push(key);
        this.dic.Values.push(value);

    } else {
        //キーが存在しない場合、値を追加する
        this.dic.Keys.push(key);
        this.dic.Values.push(value);
    }

};
Dictionary.prototype.GetValue = function (key) {
    var kcnt = this.dic.Keys.length;
    if (kcnt > 0) {
        //コレクション対象からキーを検索する
        for (var i = 0; i < kcnt; i++) {
            if (dic.Keys[i] == key) {
                return dic.Values[i];
            }
        }
    }
    return null;
};
function IsPC() {
    var userAgentInfo = navigator.userAgent;
    var Agents = ["Android", "iPhone",
        "SymbianOS", "Windows Phone",
        "iPad", "iPod"];
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    return flag;
}
