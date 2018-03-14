/// <reference path="js/SVG.js" />
/// <reference path="js/jquery-1.10.2.min.js" />

/**Lines Arr */
var pub_arr_lines = [];


function ER(panel_id){
    var oER = new Object;
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
    
        group.add(tableRow.rect);
        group.add(tableRow.text);

        //淡黄色项目名追加
        for (i=0;i<=columnList.length-1;i++){

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

    /**Line Color */
    var GetRandomColor_COLOR=new Array("#000000","#CE0000","#D200D2","#8600FF","#0000E3","#00A600","#FFD306");
    var GetRandomColor_INDEX = 0;
    var GetRandomColor = function(){  
        GetRandomColor_INDEX++;
        if (GetRandomColor_INDEX==7) {
            GetRandomColor_INDEX = 0;
        }
        return GetRandomColor_COLOR[GetRandomColor_INDEX];

    }


    oER.DrawLine = function(point1,point2){

        var px1 = parseInt($(point1).offset().left);
        var py1 = parseInt($(point1).offset().top);
        var px2 = parseInt($(point2).offset().left);
        var py2 = parseInt($(point2).offset().top);

        if ($(point1).attr("class").indexOf("link_line_left")>=0 
         && $(point2).attr("class").indexOf("link_line_left")>=0){
            tenFlg = "left_left";

        }else  if ($(point1).attr("class").indexOf("link_line_right")>=0 
                && $(point2).attr("class").indexOf("link_line_right")>=0){
            tenFlg = "right_right";
        }else{
            tenFlg = "left_right";   
        }

        //LineのPoints
        var linePos;
        if (px1<px2){
            linePos = GetLineFromTwoPoint(px1,py1,px2,py2,tenFlg);
        }else{
            linePos = GetLineFromTwoPoint(px2,py2,px1,py1,tenFlg);
        }
  
        //var linePos = GetLineFromTwoPoint(px1,py1,px2,py2);
        //LineのObject
        var line = oER.pub_draw.path(linePos);
        
        //Color
        line.fill('none').stroke({ width: 1, color:  GetRandomColor()});
        //Start Point
        line.marker('start', 10, 10, function (add) {
            add.circle(10).fill('#f06');
        })
        //End Point
        line.marker('end', 10, 10, function (add) {
            add.circle(10).fill('#f06');
        })

        line.point1 = $(point1)[0];
        line.point2 = $(point2)[0];

        if ( $(point1)[0].lines==undefined){
            $(point1)[0].lines = [];
        }
        if ( $(point2)[0].lines==undefined){
            $(point2)[0].lines = [];
        }
        $(point1)[0].lines.push(line);
        $(point2)[0].lines.push(line);


        var tmpPoint1 = $(point1)[0];
        var tmpPoint2 = $(point2)[0];
        line.click(function() {
            //this.fill({ color: '#f06' })
            //alert($(line).attr("LineIndex"));
            //alert($(line.point1).attr("class"));
            
            for (i=0;i<=this.point1.lines.length -1 ;i++){
                if (this.point1.lines[i]==this){
                    this.point1.lines.splice(i, 1);
                }
            }

            for (i=0;i<=this.point2.lines.length -1 ;i++){
                if (this.point2.lines[i]==this){
                    this.point2.lines.splice(i, 1);
                }
            }
            
           //this.point1.lines.remove(this);
           //point2.lines.remove(line);
            //alert();
            this.remove();
        })

        return line;
    }
    return oER;   
}

function GetLineFromTwoPoint(px1,py1,px2,py2,tenFlg){

    var lpx,lpy,rpx,rpy;
    /** 左右点取得 */
    if(px1<px2){
        lpx = px1;
        lpy = py1;
        rpx = px2;
        rpy = py2;
    }else{
        rpx = px1;
        rpy = py1;
        lpx = px2;
        lpy = py2;
    }

    var line;

    if (tenFlg=="left_right"){
        line =        "M" + (lpx + 10) + " " + lpy + " ";
        line = line + "L" + (lpx + 20) + " " + lpy + " ";
        line = line + "L" + (lpx + 20) + " " + rpy + " ";
        line = line + "L" + (rpx - 10) + " " + rpy + " ";
        line = line + "M" + (rpx - 10) + " " + rpy + " ";
    }else if (tenFlg=="left_left"){
        line =        "M" + (lpx - 10) + " " + lpy + " ";
        line = line + "L" + (lpx - 20) + " " + lpy + " ";
        line = line + "L" + (lpx - 20) + " " + rpy + " ";
        line = line + "L" + (rpx - 10) + " " + rpy + " ";
        line = line + "M" + (rpx - 10) + " " + rpy + " ";
    }else if (tenFlg=="right_right"){
        var maxRight;
        maxRight = lpx;
        if (rpx>lpx){
            maxRight = rpx;
        }

        line =        "M" + (lpx + 10) + " " + lpy + " ";
        line = line + "L" + (maxRight + 20) + " " + lpy + " ";
        line = line + "L" + (maxRight + 20) + " " + rpy + " ";
        line = line + "L" + (maxRight + 10) + " " + rpy + " ";
        line = line + "M" + (rpx + 10) + " " + rpy + " ";
    }


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
/*
    var eEr;
    eEr = ER("drawing");
    eEr.DrawLine(0,0,100,200);
*/

    /**column_name */
    $(".link_line_left,.link_line_right").click(function(){
        //$(this).hide();
        SelectCell(this);
    });

    //Cell 选择
    var pub_select_cell_suu = 0;
    var pub_select_cell_one;//第一个选择的项目
    var pub_select_cell_two;//第二个选择的项目



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

            //var eEr;
           // eEr = ER("drawing");

            var connectLineObj=[];
            
            var line = eEr.DrawLine($(pub_select_cell_one),$(pub_select_cell_two)) ;
           
/*
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

            idxs1.push(pub_arr_lines.length - 1);
            idxs2.push(pub_arr_lines.length - 1);

            $(pub_select_cell_one).attr("LineIndex",idxs1.join(","));
            $(pub_select_cell_two).attr("LineIndex",idxs2.join(","));
            $(line).attr("LineIndex",idxs2.join(","));
*/


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


/**
 * ERのJoinのLINE
 */
var ErJoinLine = {
    CreateNew: function(){
        var line = {};
        line.id = "大毛";
        line.makeSound = function(){ alert("喵喵喵"); };
        return line;
    }
};
