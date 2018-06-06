var arrPgm = [];
arrPgm.push(new Array("000000020", "調査", "調査"));
arrPgm.push(new Array("000000021", "調査", "調査_単体テスト仕様書"));
arrPgm.push(new Array("000000001", "外部仕様設計", "ER図"));
arrPgm.push(new Array("000000002", "外部仕様設計", "テーブル定義書"));
arrPgm.push(new Array("000000003", "外部仕様設計", "ビュー定義書"));
arrPgm.push(new Array("000000101", "内部仕様設計", "機能定義書（詳細）"));
arrPgm.push(new Array("000000102", "内部仕様設計", "関数定義書"));
arrPgm.push(new Array("000000103", "内部仕様設計", "単体テスト仕様書"));
arrPgm.push(new Array("000000104", "内部仕様設計", "結合テスト仕様書"));
arrPgm.push(new Array("000000201", "製造 ", "機能定義書（詳細）"));
arrPgm.push(new Array("000000202", "製造 ", "ソース"));
arrPgm.push(new Array("000000203", "製造 ", "単体テスト仕様書"));
arrPgm.push(new Array("000000204", "製造 ", "結合テスト仕様書"));


var objPgmBunruiMst = {
    "c000000020": "調査",
    "c000000021": "調査",
    "c000000001": "外部仕様設計",
    "c000000002": "外部仕様設計",
    "c000000003": "外部仕様設計",
    "c000000101": "内部仕様設計",
    "c000000102": "内部仕様設計",
    "c000000103": "内部仕様設計",
    "c000000104": "内部仕様設計",
    "c000000201": "製造 ",
    "c000000202": "製造 ",
    "c000000203": "製造 ",
    "c000000204": "製造 "
};

var objPgmMst = {
    "c000000020": "調査",
    "c000000021": "調査_単体テスト仕様書",
    "c000000001": "ER図",
    "c000000002": "テーブル定義書",
    "c000000003": "ビュー定義書",
    "c000000101": "機能定義書（詳細）",
    "c000000102": "関数定義書",
    "c000000103": "単体テスト仕様書",
    "c000000104": "結合テスト仕様書",
    "c000000201": "機能定義書（詳細）",
    "c000000202": "ソース",
    "c000000203": "単体テスト仕様書",
    "c000000204": "結合テスト仕様書"
};

var user_id = "lis6";
var arrMsMain = [];
//                       0                  1           2            3            4     5             6           7
//                       kinou_no           pgm_id      retu  kinou_mei     bunrui pgm_name          start_date   end_date     
/*
arrMsMain.push(new Array("R14239_000000015", "000000020", "100", "001_伝票検索", "調査", "単体テスト仕様書", "2018/02/18", "2018/05/18", "2018/02/19", "2018/04/18"));
arrMsMain.push(new Array("R14239_000000015", "000000021", "100", "001_伝票検索", "調査", "単体テスト仕様書", "2018/03/18", "2018/04/18", "2018/03/18", "2018/04/18"));
arrMsMain.push(new Array("R14239_000000015", "000000021", "100", "002_伝票検索", "調査", "単体テスト仕様書", "2018/03/18", "2018/04/18", "2018/03/18", "2018/04/18"));
*/

var arrMsLeftRows = [];
var arrMsRightRow = [];
var arrColsDay = [];


$(document).ready(function () {

    InitVBData();

    var tblLeft = $("#tblMsLeft");
    var tblRight = $("#tblMsRight");
    var tblTitleRight = $("#tblTitleRight");

    var i, j;
    var rMs = [];

    //データ作成
    for (i = 0; i <= arrMsMain.length - 1; i++) {

        if (i == 0) {
            start_ymd_yotei = arrMsMain[i][6];
            end_ymd_yotei = arrMsMain[i][7];
            start_ymd_jisseki = arrMsMain[i][8];
            end_ymd_jisseki = arrMsMain[i][9];
        } else {
            if (arrMsMain[i][6] != "" && start_ymd_yotei > arrMsMain[i][6]) {
                start_ymd_yotei = arrMsMain[i][6]
            }
            if (arrMsMain[i][7] != "" && end_ymd_yotei < arrMsMain[i][7]) {
                end_ymd_yotei = arrMsMain[i][7]
            }
        }

        arrMsLeftRows.push(new Array(arrMsMain[i][3], arrMsMain[i][4], arrMsMain[i][5], arrMsMain[i][2].replace("%", "") + "%", "予", user_id));
        arrMsLeftRows.push(new Array(arrMsMain[i][3], arrMsMain[i][4], arrMsMain[i][5], arrMsMain[i][2].replace("%", "") + "%", "実", user_id));
        //                       0                  1                   2                 3            4     5             6           7
        //                       kinou_no           pgm_id             start_date       end_date     
        arrMsRightRow.push(new Array(arrMsMain[i][0], arrMsMain[i][1], arrMsMain[i][6], arrMsMain[i][7]));
        arrMsRightRow.push(new Array(arrMsMain[i][0], arrMsMain[i][1], arrMsMain[i][8], arrMsMain[i][9]));

    }

    var days = GetDateDiff(start_ymd_yotei, end_ymd_yotei, "day");

    //右側のTITLE
    SetRightTitle(tblTitleRight, arrMsRightRow, start_ymd_yotei, end_ymd_yotei);

    function SetRightTitle(tbl, ms, minDate, maxDate) {



        var i, j;

        var Week = ['日', '月', '火', '水', '木', '金', '土'];

        var nowDate = (new Date()).Format("yyyyMMdd");

        for (i = 0; i <= 5; i++) {
            var rcell = [];
            for (j = 0; j <= days; j++) {
                var today = DateAdd("d", j, minDate);
                if (i == 0) {
                    arrColsDay.push(today.getDay());
                }

                if (i == 0) {
                    rcell.push(today.Format("yy"));
                } else if (i == 1) {
                    rcell.push(today.Format("MM"));
                } else if (i == 2) {
                    rcell.push(today.Format("dd"));
                } else if (i == 3) {
                    rcell.push(Week[today.getDay()]);
                } else if (i == 4) {
                    rcell.push(today.Format("yyyyMMdd") == nowDate);
                } else if (i == 5) {
                    rcell.push(today.Format("yyyy/MM/dd"));
                }
            }

            rMs.push(rcell);
        }

        var cCnt = rMs[0].length;



        for (i = 0; i <= 3; i++) {
            var ttr = [];
            var $trTemp = $("<tr></tr>");

            var isTodayStyle = "";
            //ttr.push("<tr>");
            for (j = 0; j <= days; j++) {

                if (rMs[4][j]) {
                    isTodayStyle = "tdc";
                } else {
                    isTodayStyle = "";
                }

                var newKey = rMs[i][j];

                if (j > 0) {
                    var oldKey = rMs[i][j - 1];


                    if (oldKey != newKey) {

                        var colspan = GetColSpan(i, j, cCnt, rMs);

                        if (arrColsDay[j] == 6 || arrColsDay[j] == 0 && i >= 2) {
                            //$trTemp.append("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + " yasumi_cell'><div>" + newKey + "</div></td>");
                            ttr.push("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + " yasumi_cell'><div>" + newKey + "</div></td>");
                        } else {
                            //$trTemp.append("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                            ttr.push("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                        }
                    }
                } else {
                    var colspan = GetColSpan(i, j, cCnt, rMs);
                    if (arrColsDay[j] == 6 || arrColsDay[j] == 0 && i >= 2) {
                        //$trTemp.append("<td colspan='" + colspan + "' class='r" + i + " c" + j + " yasumi_cell " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                        ttr.push("<td colspan='" + colspan + "' class='r" + i + " c" + j + " yasumi_cell " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                    } else {
                        //$trTemp.append("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                        ttr.push("<td colspan='" + colspan + "' class='r" + i + " c" + j + " " + isTodayStyle + "'><div>" + newKey + "</div></td>");
                    }
                }

            }

            //ttr.push("</tr>");
            $trTemp.append($(ttr.join("")));
            //$trTemp.appendTo(tbl);
            $trTemp.appendTo(tbl);
        }

    }


    //右側の明細
    SetRightMs(tblRight, arrMsRightRow, start_ymd_yotei, end_ymd_yotei);

    function SetRightMs(tbl, ms, minDate, maxDate) {

        //var days = GetDateDiff(minDate, maxDate, "day");
        var i, j;
        var rMs = [];

        var ht = [];
        var ttr = [];

        for (i = 0; i <= arrMsRightRow.length - 1; i++) {

            var mark;
            if (i % 2 == 0) {
                mark = "□";
            } else {
                mark = "■";
            }

            var rcell = [];
            for (j = 0; j <= days; j++) {
                var today = DateAdd("d", j, minDate).Format("yyyy/MM/dd");
                //if (today>=DateAdd("d",0,arrMsRightRow[i][2]) && today<=DateAdd("d",0,arrMsRightRow[i][3])){
                if (DaysBetween(today, arrMsRightRow[i][2]) >= 0 && DaysBetween(today, arrMsRightRow[i][3]) <= 0) {
                    rcell.push(mark);
                } else {
                    rcell.push('&nbsp;');
                }

            }
            rMs.push(rcell);
        }

        for (i = 0; i <= arrMsRightRow.length - 1; i++) {
            var cellType;
            if (i % 2 == 0) {
                cellType = "PRE"
            } else {
                cellType = "JIS"
            }
            //var $trTemp = $("<tr kinou_no='" + arrMsRightRow[i][0] + "' pgm_id='" + arrMsRightRow[i][1] + "' cellType='" + cellType + "'></tr>");
            ht.push("<tr kinou_no='" + arrMsRightRow[i][0] + "' pgm_id='" + arrMsRightRow[i][1] + "' cellType='" + cellType + "'>");

            for (j = 0; j <= days; j++) {
                //CreateCell(1, i, j, $trTemp, rMs);
                if (arrColsDay[j] == 6 || arrColsDay[j] == 0) {
                    //$trTemp.append("<td  class='r" + i + " c" + j + " yasumi_cell'><div>" + rMs[i][j] + "</div></td>");
                    ht.push("<td  class='r" + i + " c" + j + " yasumi_cell'><div>" + rMs[i][j] + "</div></td>");
                } else {
                    //$trTemp.append("<td  class='r" + i + " c" + j + "'><div>" + rMs[i][j] + "</div></td>");
                    ht.push("<td  class='r" + i + " c" + j + "'><div>" + rMs[i][j] + "</div></td>");
                }
                //$trTemp.append("<td  class='r" + i + " c" + j + "'>"+rMs[i][j]+"</td>");
            }
            ht.push("</tr>");

        }

        var $trTemp = $(ht.join(""));
        $trTemp.appendTo(tbl);
    }




    //左側の明細
    SetLeftMs(tblLeft, arrMsLeftRows);

    //左側の明細

    function SetLeftMs(tbl, ms) {

        var rCnt;
        var cCnt;

        rCnt = ms.length;
        if (rCnt > 0) {
            cCnt = ms[0].length;
        }

        var i, j;

        //Row
        for (i = 0; i <= rCnt - 1; i++) {

            var $trTemp = $("<tr></tr>");

            //Columns
            for (j = 0; j <= cCnt - 1; j++) {

                var rspan = GetRowSpan(i, j, rCnt, ms);
                //USER　列
                if (j == 5) {
                    if (i % 2 == 0) {




                        CreateCell(2, i, j, $trTemp, ms);

                    } else {

                    }
                } else if (j == 3) {
                    //CreateCell(1, i, j, $trTemp, ms);
                    if (i % 2 == 0) {
                        if (ms[i][j] == "100%" || ms[i][j] == "100") {
                            $trTemp.append("<td RowSpan='2' class='r" + i + " c" + j + "' style='background-color:Green;'>" + ms[i][j] + "</td>");
                        } else {
                            $trTemp.append("<td RowSpan='2' class='r" + i + " c" + j + "' style='background-color:#fff;'>" + ms[i][j] + "</td>");
                        }
                    }



                } else {

                    if (i > 0) {
                        var oldKey = GetCellsValueByColIdx(ms[i - 1], j);
                        var newKey = GetCellsValueByColIdx(ms[i], j);
                        if (oldKey == newKey) { } else {
                            //$trTemp.append("<td RowSpan='" + rspan + "' class='r" + i + " c" + j + "'>" + ms[i][j] + "</td>");
                            CreateCell(rspan, i, j, $trTemp, ms);
                        }
                    } else {
                        //$trTemp.append("<td RowSpan='" + rspan + "' class='r" + i + " c" + j + "'>" + ms[i][j] + "</td>");
                        CreateCell(rspan, i, j, $trTemp, ms);
                    }
                }

            }

            $trTemp.appendTo(tbl);
        }

    }



    function CreateCell(rspan, i, j, $trTemp, ms) {
        //$trTemp.append("<td RowSpan='" + rspan + "' class='r" + i + " c" + j + "'><div>" + ms[i][j] + "</div></td>");
        var height = "";
        height = " height='" + (rspan * 20) + "px'"
        if (rspan > 1) {

        }
        $trTemp.append("<td  RowSpan=" + rspan + " class='r" + i + " c" + j + "' " + height + ">" + ms[i][j] + "</td>");
    }

    //Row

    function GetRowSpan(startRowIdx, endColIdx, rCnt, ms) {

        var i, j;
        var rowSpan = 1;
        var oldKey = GetCellsValueByColIdx(ms[startRowIdx], endColIdx);

        for (i = startRowIdx + 1; i <= rCnt - 1; i++) {

            var keys = GetCellsValueByColIdx(ms[i], endColIdx); ;

            if (oldKey == keys) {
                rowSpan = rowSpan + 1;
            } else {
                return rowSpan;
            }
        }
        return rowSpan;
    }

    function GetColSpan(rowIdx, StartColIdx, cCnt, ms) {
        var i, j;
        var colSpan = 1;
        var oldKey = ms[rowIdx][StartColIdx];
        var key;
        for (i = StartColIdx + 1; i <= cCnt - 1; i++) {
            key = ms[rowIdx][i];

            if (oldKey == key) {
                colSpan = colSpan + 1;
            } else {
                return colSpan;
            }

        }

        return colSpan;

    }




    function GetCellsValueByColIdx(cells, endColIdx) {
        var keys = [];
        for (j = 0; j <= endColIdx; j++) {
            keys.push(cells[j]);
        }
        return keys.join("");
    }



    $("#divMsRight").scroll(function () {


        $("#divMsLeft").scrollTop($(this).scrollTop());
        $("#divTitleRight").scrollLeft($(this).scrollLeft());

    });


    $("#divMsLeft").scroll(function () {


        $("#divMsRight").scrollTop($(this).scrollTop());
        //$("#divTitleRight").scrollLeft($(this).scrollLeft());
    });


    var old_kinou_no_pgm_id = "";
    var old_ymd;
    var old_cell = "";
    var old_text;
    $(".msRight").find("td").click(function () {

        var tr = $(this).parent();
        var td = $(this);
        var div = $(this).find("div");

        var kinou_no = $(tr).attr("kinou_no");
        var pgm_id = $(tr).attr("pgm_id");

        //var ymd = $(this).attr("ymd");
        var cls = $(this).attr("class");


        var r, c;

        r = parseInt(cls.split(" ")[0].replace("r", ""));
        c = parseInt(cls.split(" ")[1].replace("c", ""));

        //rMs[5][c]
        // alert(tr.attr("cellType"));
        //rMs
        var mark;
        if (r % 2 == 0) {
            mark = "□";
            yotei_jisseki = "0";
        } else {
            mark = "■";
            yotei_jisseki = "1";
        }

        if (old_kinou_no_pgm_id != kinou_no + ',' + pgm_id) {
            old_kinou_no_pgm_id = kinou_no + ',' + pgm_id;
            old_ymd = rMs[5][c];
            if (old_cell != "") {
                $(old_cell).text(old_text);
            }
            old_cell = $(td);
            old_text = $(div).text();

            $(div).text("⇒");

        } else {

            old_cell = "";

            var new_ymd, mx_ymd, mi_ymd;
            new_ymd = rMs[5][c];
            if (new_ymd > old_ymd) {
                mx_ymd = new_ymd;
                mi_ymd = old_ymd;
            } else {
                mx_ymd = old_ymd;
                mi_ymd = new_ymd;
            }


            $(this).parent().find("td").each(function () {

                var rr, cc;
                var ccls = $(this).attr("class");
                rr = parseInt(ccls.split(" ")[0].replace("r", ""));
                cc = parseInt(ccls.split(" ")[1].replace("c", ""));

                var ddiv = $(this).find("div");

                if (rMs[5][cc] >= mi_ymd && rMs[5][cc] <= mx_ymd) {
                    $(ddiv).text(mark);
                } else {
                    $(ddiv).text("");
                }

            });

            old_kinou_no_pgm_id = "";


            FncSaveData(kinou_no, pgm_id, yotei_jisseki, mi_ymd, mx_ymd);

        }


    });

    $(".msLeft").find(".c3").click(function () {
        var tr = $(this).parent();
        var td = $(this);
        var cls = $(this).attr("class");
        var r, c;
        r = parseInt(cls.split(" ")[0].replace("r", ""));
        c = parseInt(cls.split(" ")[1].replace("c", ""));

        var kinou_no = $(".msRight").find(".r" + r).parent().attr("kinou_no");
        var pgm_id = $(".msRight").find(".r" + r).parent().attr("pgm_id");



        if (r % 2 == 0) {
            yotei_jisseki = "0";
            var per = parseInt($(td).text().replace("%", ""));
            if (per == 100) {
                per = 0;
            } else {
                per = per + 20;
            }
            if (per == 100) {
                $(this).css("background-color", "green");
            } else {
                $(this).css("background-color", "#fff");
            }

            FncPercentData(kinou_no, pgm_id, per);
            $(td).text(per + "%");

        } else {
            yotei_jisseki = "1";
        }

    });

});




function FncSaveData(kinou_no, pgm_id, yotei_jisseki, mi_ymd, mx_ymd) {
    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        url: "AnkanSinntyokuAjax.aspx/SaveData",
        //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
        dataType: "json",
        data: "{kinou_no:'" + kinou_no + "',pgm_id:'" + pgm_id + "',yotei_jisseki:'" + yotei_jisseki + "',mi_ymd:'" + mi_ymd + "',mx_ymd:'" + mx_ymd + "'}",
        //username 为想问后台传的参数（这里的参数可有可无）
        success: function (result) {
            //alert(result.d); //result.d为后台返回的参数
        }
    });

}

function FncPercentData(kinou_no, pgm_id, per) {

    $.ajax({
        type: "post",
        contentType: "application/json;charset=utf-8",
        url: "AnkanSinntyokuAjax.aspx/SavePercentData",
        //WebAjaxForMe.aspx为目标文件，GetValueAjax为目标文件中的方法
        dataType: "json",
        data: "{kinou_no:'" + kinou_no + "',pgm_id:'" + pgm_id + "',per:'" + per + "'}",
        //username 为想问后台传的参数（这里的参数可有可无）
        success: function (result) {
            //alert(result.d); //result.d为后台返回的参数
        }
    });

}




function GetDateDiff(startTime, endTime, diffType) {
    //将xxxx-xx-xx的时间格式，转换为 xxxx/xx/xx的格式 
    startTime = startTime.replace(/\-/g, "/");
    endTime = endTime.replace(/\-/g, "/");

    if (startTime == "" || endTime == "") {
        return 0;
    }
    //将计算间隔类性字符转换为小写
    diffType = diffType.toLowerCase();
    var sTime = new Date(startTime); //开始时间
    var eTime = new Date(endTime); //结束时间
    //作为除数的数字
    var timeType = 1;
    switch (diffType) {
        case "second":
            timeType = 1000;
            break;
        case "minute":
            timeType = 1000 * 60;
            break;
        case "hour":
            timeType = 1000 * 3600;
            break;
        case "day":
            timeType = 1000 * 3600 * 24;
            break;
        default:
            break;
    }
    return parseInt((eTime.getTime() - sTime.getTime()) / parseInt(timeType));
}



function DateAdd(interval, value, dateValue) {
    var myDate = new Date(dateValue);
    value *= 1;
    if (isNaN(value)) {
        value = 0;
    }
    switch (interval) {
        case "y":
            myDate.setUTCFullYear(myDate.getUTCFullYear() + value);
            break;
        case "m":
            myDate.setUTCMonth(myDate.getUTCMonth() + value);
            break;
        case "d":
            myDate.setUTCDate(myDate.getUTCDate() + value);
            break;
        case "h":
            myDate.setUTCHours(myDate.getUTCHours() + value);
            break;
        case "n":
            myDate.setUTCMinutes(myDate.getUTCMinutes() + value);
            break;
        case "s":
            myDate.setUTCSeconds(myDate.getUTCSeconds() + value);
            break;
        default:
    }
    return myDate;

}

//TimeCom对象


function TimeCom(dateValue) {
    var newCom;
    if (dateValue == "") {
        newCom = new Date();
    } else {
        newCom = new Date(dateValue);
    }
    this.year = newCom.getFullYear();
    this.month = newCom.getMonth() + 1;
    this.day = newCom.getDate();
    this.hour = newCom.getHours();
    this.minute = newCom.getMinutes();
    this.second = newCom.getSeconds();
    this.msecond = newCom.getMilliseconds();
    this.week = newCom.getDay();
}

function DaysBetween(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('/'));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('/') + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf('/'));
    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('/'));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('/') + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf('/'));
    var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
    return cha;
    //return Math.abs(cha); 
}

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