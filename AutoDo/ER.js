/// <reference path="js/SVG.js" />
/// <reference path="js/jquery-1.10.2.min.js" />

function ER(panel_id){
    var oER = {};
    oER.pub_panel = panel_id;
    oER.pub_draw = "";
    oER.pub_tables= [];
    oER.pub_table_width= 200;
    oER.pub_table_height= 20;

    if (SVG.supported) {
        oER.pub_draw = SVG('drawing');
    }else{
        alert("not support SVG");
        return false;
    }

    oER.DrawTable = function(tableName,columnList,typeList,lengthList){
        var i ;
        var group = oER.pub_draw.group();

        //蓝色 表名追加
        //var rectTableName = oER.pub_draw.rect(oER.pub_table_width, oER.pub_table_height).fill('#0000EE');
       
        var tableRow;
        tableRow = oER.AddTableRow(tableName,0,0,'#fff','#0000EE');
       
  /*
        //var rectTableName = oER.pub_draw.viewbox(0,0,oER.pub_table_width, 20);
        //var text = draw.text('I know that eggs do well to stay out of frying pans.')
        //text.move(20,20).font({ fill: '#f06', family: 'Inconsolata' });
        rectTableName.attr({
            fill: '#0000EE'
          , 'fill-opacity': 1
          , stroke: '#000'
          , 'stroke-width': 0.5
          })

        group.add(rectTableName);

        var text = oER.pub_draw.text(tableName).fill('#fff');
        //text.build(true);
        
        text.attr({
            fill: '#fff'
            , 'stroke-width': 0.1
          })

        text.font({
            family:   'Helvetica'
            , size:     14
            , anchor:   'left'
            , leading:  '1.5em'
        })
        text.move(0,0);
*/

        group.add(tableRow.rect);
        group.add(tableRow.text);

        //淡黄色项目名追加
        for (i=0;i<=columnList.length-1;i++){
            /*
            var rect = oER.pub_draw.rect(oER.pub_table_width, oER.pub_table_height).move(0, i*oER.pub_table_height+oER.pub_table_height).fill('#F0E68C');          
                rect.attr({
                  fill: '#F0E68C'
                , 'fill-opacity': 1
                , stroke: '#000'
                , 'stroke-width': 0.5
                })
            group.add(rect);*/

            var txt;
            txt = columnList[i] + typeList[i] + lengthList[i];

            var kmTableRow;
            kmTableRow = oER.AddTableRow(txt,0,i*oER.pub_table_height+oER.pub_table_height,'#000','#F0E68C');
            group.add(kmTableRow.rect);
            group.add(kmTableRow.text);
            
            oER.pub_tables.push(group);
        }

        group.move(0,222);
        return group;

    }
    
    oER.AddTableRow = function(text,x,y,font_color,bg_lolor){
   
        var rect = oER.pub_draw.rect(oER.pub_table_width, oER.pub_table_height).fill(bg_lolor);

        rect.attr({
          'fill-opacity': 1
          , stroke: '#000'
          , 'stroke-width': 0.5
          })

        var text = oER.pub_draw.text(text).fill(font_color);

        text.attr({
            'stroke-width': 0.1
        })

        text.font({
            family:   'Helvetica'
            , size:     14
            , anchor:   'left'
            , leading:  '1.5em'
        })
        rect.move(x,y);
        text.move(x+2,y);

        
        this.rect = rect;
        this.text = text;

        return this;

    }

<<<<<<< HEAD
    oER.DrawLine = function(px1,py1,px2,py2){
        var line;
        line = GetLineFromTwoPoint(px1,py1,px2,py2)
=======
    var getRandomColor = function(){
        return '#'+('00000'+(Math.random()*0x1000000<<0).toString(16)).substr(-6);
    }

    var lineIdx;
    lineIdx = 0;
    oER.DrawLine = function(px1,py1,px2,py2,rcnt){
        lineIdx++;
        var lineColor;

        if(lineIdx%3==0){
            lineColor = '#FF7F24';
        }else if(lineIdx%3==1){
            lineColor = '#191970';
        }else{
            lineColor = '#B22222';
        }

        //lineColor = getRandomColor();

        var line;
        line = GetLineFromTwoPoint(px1,py1,px2,py2,rcnt)
>>>>>>> parent of ca3c426... aa
        var path = oER.pub_draw.path(line);
        path.fill('none').stroke({ width: 1, color: '#000ccc' });
        path.marker('start', 10, 10, function (add) {
            add.circle(10).fill('#f06');
        })
        path.marker('end', 10, 10, function (add) {
            add.circle(10).fill('#f06');
        })

        path.click(function() {
            //this.fill({ color: '#f06' })
            alert();
            this.remove();
        })

        return path;
    }
    return oER;   
}

<<<<<<< HEAD
function GetLineFromTwoPoint(px1,py1,px2,py2){
=======
function GetLineFromTwoPoint(px1,py1,px2,py2,rcnt){
>>>>>>> parent of ca3c426... aa

    var lpx,lpy,rpx,rpy;
    /** 左右点取得 */
    if(px1<px2){
        lpx = px1+5;
        lpy = py1;
        rpx = px2-5;
        rpy = py2;
    }else{
        rpx = px1+5;
        rpy = py1;
        lpx = px2-5;
        lpy = py2;
    }

    var line;
<<<<<<< HEAD
    line =        "M" + lpx + " " + lpy + " ";
    line = line + "L" + (lpx+10) + " " + lpy + " ";
    line = line + "L" + (lpx+10) + " " + rpy + " ";
    line = line + "L" + (rpx- 0) + " " + rpy + " ";
    line = line + "M" + (rpx- 0) + " " + rpy + " ";
=======
    line =        "M" + (lpx + 5) + " " + lpy + " ";
    line = line + "L" + (lpx + 10 + rcnt) + " " + lpy + " ";
    line = line + "L" + (lpx + 10 + rcnt) + " " + rpy + " ";
    line = line + "L" + (rpx - 5) + " " + rpy + " ";
    line = line + "M" + (rpx - 5) + " " + rpy + " ";
>>>>>>> parent of ca3c426... aa

    //alert(line);
    return line;
}


$(document).ready(function () {
    /*var eEr;
    eEr = ER("drawing");

    var tblName;
    tblName = "test";

    var columnList = [];
    var typeList = [];
    var lengthList = [];

    var i;
    for (i=0;i<=4;i++){
        columnList.push("aaa"+i);
        typeList.push(" nvarchar");
        lengthList.push("("+i+")");
    }

    eEr.DrawTable(tblName,columnList,typeList,lengthList);
*/

    var eEr;
    eEr = ER("drawing");
    eEr.DrawLine(0,0,100,200);


    /**column_name */
    $(".link_line_left,.link_line_right").click(function(){
        //$(this).hide();
        SelectCell(this);
    });

    //Cell 选择
    var pub_select_cell_suu = 0;
    var pub_select_cell_one;//第一个选择的项目
    var pub_select_cell_two;//第二个选择的项目

    var pub_arr_lines = [];

    function SelectCell(obj){
        var IsSelectColor = "yellow";
        var IsNotSelectColor = "";
        //未选择
        if($(obj).attr("IsSelect")==undefined || $(obj).attr("IsSelect")=="0"){
            //选择
            $(obj).attr("IsSelect","1");
            $(obj).css("background-color",IsSelectColor); 
        }else{
            //未选择
            $(obj).attr("IsSelect","0");
            $(obj).css("background-color",IsNotSelectColor); 
        }

        //alert($(obj).attr("LineIndex"));
        if ($(obj).attr("LineIndex") != undefined){

            //pub_arr_lines[$(obj).attr("LineIndex")].remove();
            //alert(pub_arr_lines[$(obj).attr("LineIndex")]);
        } 

        if (pub_select_cell_suu==0){
            pub_select_cell_suu = 1;
            pub_select_cell_one = obj;
        }else if (pub_select_cell_suu==1){
            pub_select_cell_suu = 0;
            pub_select_cell_two = obj;

            var e = event || window.event;
            var x1 = parseInt($(pub_select_cell_one).offset().left);
            var y1 = parseInt($(pub_select_cell_one).offset().top);

            var x2 = parseInt($(pub_select_cell_two).offset().left);
            var y2 = parseInt($(pub_select_cell_two).offset().top);

            //var eEr;
           // eEr = ER("drawing");
            var line = eEr.DrawLine(x1,y1,x2,y2);
            pub_arr_lines.push(line);

<<<<<<< HEAD
            $(pub_select_cell_one).attr("LineIndex",pub_arr_lines.length-1);
            $(pub_select_cell_two).attr("LineIndex",pub_arr_lines.length-1);
=======
            var connectLineObj=[];

            var line ;
            if (x1<x2){
                line = eEr.DrawLine(x1,y1,x2,y2,5);
            }else{
                line = eEr.DrawLine(x2,y2,x1,y1,5);
            }

            

            connectLineObj.push(line);
            connectLineObj.push(pub_select_cell_one);
            connectLineObj.push(pub_select_cell_two);

            pub_arr_lines.push(connectLineObj);

            var idxs1 = [];
            var idxs2 = [];

            if ($(pub_select_cell_one).attr("LineIndex") != undefined ) {
                idxs1 = $(pub_select_cell_one).attr("LineIndex").split(",");
            }
            
            if ($(pub_select_cell_two).attr("LineIndex") != undefined ) {
                idxs2 = $(pub_select_cell_two).attr("LineIndex").split(",");
            }
            
            //idxs2 = $(pub_select_cell_two).attr("LineIndex").split(",");

            idxs1.push(pub_arr_lines.length - 1);
            idxs2.push(pub_arr_lines.length - 1);

            $(pub_select_cell_one).attr("LineIndex",idxs1.join(","));
            $(pub_select_cell_two).attr("LineIndex",idxs2.join(","));


>>>>>>> parent of ca3c426... aa
//alert(x1+':'+y1+':'+x2+':'+y2);
        }else{
            pub_select_cell_suu = 0;
            pub_select_cell_one = null;
            pub_select_cell_two = null;

        }
    }

});




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