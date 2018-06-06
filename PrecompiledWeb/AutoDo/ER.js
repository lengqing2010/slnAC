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

<<<<<<< HEAD
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
=======
    /**Line Color */
    var GetRandomColor_COLOR=new Array("#000000","#CE0000","#D200D2","#8600FF","#0000E3","#00A600","#FFD306");
    var GetRandomColor_INDEX = 0;
    var GetRandomColor = function(idx){ 
        
        if (idx==undefined){
            GetRandomColor_INDEX++;
            if (GetRandomColor_INDEX==7) {
                GetRandomColor_INDEX = 0;
            }
            return GetRandomColor_COLOR[GetRandomColor_INDEX];           
        } else{
            return GetRandomColor_COLOR[idx%6];   
        }
    }

    oER.GetTwoPointFlg = function(point1,point2){
        var tenFlg;
        if ($(point1).attr("class").indexOf("link_line_left")>=0 
         && $(point2).attr("class").indexOf("link_line_left")>=0){
            tenFlg = "left_left";

        }else  if ($(point1).attr("class").indexOf("link_line_right")>=0 
                && $(point2).attr("class").indexOf("link_line_right")>=0){
            tenFlg = "right_right";
        }else{
            tenFlg = "left_right";   
        }
        return tenFlg;
    }
    oER.text = function(txt){
        var text = oER.pub_draw.text(txt);
        text.move(20,20).font({ fill: '#f06', family: 'Inconsolata' })

        text.mousedown(function() { 
            var e = event || window.event;
            //拖动时鼠标样式
            //_moveDivTitle.css("cursor", "move");
            //获得鼠标指针离DIV元素左边界的距离
            var x = e.pageX;
            //获得鼠标指针离DIV元素上边界的距离 
            var y = e.pageY ;

            text.mousemove(function() { 
                text.fill('none').stroke({ width: 2});
            });
            
        });
        
        text.mouseover(function() { 
            //text.fill('none').stroke({ width: 2});
        });
        text.mouseout(function() { 
            //text.fill('none').stroke({ width: 1});
        });

    }
    oER.DrawLine = function(point1,point2){

        var px1 = parseInt($(point1).offset().left);
        var py1 = parseInt($(point1).offset().top)-$("#drawing").offset().top+10;
        var pw1 = $(point1).parent().parent().find("tr").index($(point1).parent()[0])*5;
        var px2 = parseInt($(point2).offset().left);
        var py2 = parseInt($(point2).offset().top)-$("#drawing").offset().top+10;
        var pw2 = $(point2).parent().parent().find("tr").index($(point2).parent()[0])*5;     
        var twoPointFlg = oER.GetTwoPointFlg(point1,point2);

        var rowIdx = $(point1).parent().parent().find("tr").index($(point1).parent()[0]);

        //$(obj).parent().parent().find("tr").index($(obj).parent()[0])
        //LineのPoints
        var linePos;
        if (px1<px2){
            linePos = GetLineFromTwoPoint(px1,py1,pw1,px2,py2,pw2,twoPointFlg);
        }else{
            linePos = GetLineFromTwoPoint(px2,py2,pw2,px1,py1,pw1,twoPointFlg);
        }
  
        //var linePos = GetLineFromTwoPoint(px1,py1,px2,py2);
        //LineのObject
        var line = oER.pub_draw.path(linePos);
        
        //Color
        line.fill('none').stroke({ width: 1, color:  GetRandomColor(rowIdx)});
        //Start Point
        line.marker('start', 10, 10, function (add) {
>>>>>>> 72ce6a2dd87e385d76b8d341c5a3a772601c03e6
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


       
        line.click(function() {  
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
            this.remove();
        });
        line.mouseover(function() { 
            line.fill('none').stroke({ width: 5});
        });
        line.mouseout(function() { 
            line.fill('none').stroke({ width: 1});
        });
        return line;
    }
    return oER;   
}

<<<<<<< HEAD
<<<<<<< HEAD
function GetLineFromTwoPoint(px1,py1,px2,py2){
=======
function GetLineFromTwoPoint(px1,py1,px2,py2,rcnt){
>>>>>>> parent of ca3c426... aa
=======
function GetLineFromTwoPoint(px1,py1,pw1,px2,py2,pw2,tenFlg){
>>>>>>> 72ce6a2dd87e385d76b8d341c5a3a772601c03e6

    var lpx,lpy,rpx,rpy,lpw,rpw;
    /** 左右点取得 */
    if(px1<px2){
        lpx = px1;
        lpy = py1;
        lpw = pw1;
        rpx = px2;
        rpy = py2;
        rpw = pw2;
    }else{
        rpx = px1;
        rpy = py1;
        rpw = pw1;
        lpx = px2;
        lpy = py2;
        lpw = pw2;
    }

    var line;
<<<<<<< HEAD
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
=======
>>>>>>> 72ce6a2dd87e385d76b8d341c5a3a772601c03e6

    if (tenFlg=="left_right"){
        line =        "M" + (lpx + 10) + " " + lpy + " ";
        line = line + "L" + (lpx + 20 + lpw) + " " + lpy + " ";
        line = line + "L" + (lpx + 20 + lpw) + " " + rpy + " ";
        line = line + "L" + (rpx - 10) + " " + rpy + " ";
        line = line + "M" + (rpx - 10) + " " + rpy + " ";
    }else if (tenFlg=="left_left"){
        line =        "M" + (lpx - 10) + " " + lpy + " ";
        line = line + "L" + (lpx - 20 - lpw) + " " + lpy + " ";
        line = line + "L" + (lpx - 20 - lpw) + " " + rpy + " ";
        line = line + "L" + (rpx - 10) + " " + rpy + " ";
        line = line + "M" + (rpx - 10) + " " + rpy + " ";
    }else if (tenFlg=="right_right"){
        var maxRight;
        maxRight = lpx;
        if (rpx>lpx){
            maxRight = rpx;
        }

        line =        "M" + (lpx + 10) + " " + lpy + " ";
        line = line + "L" + (maxRight + 20 + rpw) + " " + lpy + " ";
        line = line + "L" + (maxRight + 20 + rpw) + " " + rpy + " ";
        line = line + "L" + (maxRight + 10) + " " + rpy + " ";
        line = line + "M" + (rpx + 10) + " " + rpy + " ";
    }

    return line;

}


$(document).ready(function () {
 
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

        if (pub_select_cell_suu==0){
            pub_select_cell_suu = 1;
            pub_select_cell_one = obj;
        }else if (pub_select_cell_suu==1){
            pub_select_cell_suu = 0;
            pub_select_cell_two = obj;
<<<<<<< HEAD

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
=======
            $(pub_select_cell_one).attr("IsSelect","0");
            $(pub_select_cell_one).css("background-color",IsNotSelectColor); 
            $(pub_select_cell_two).attr("IsSelect","0");
            $(pub_select_cell_two).css("background-color",IsNotSelectColor); 
            var line = eEr.DrawLine($(pub_select_cell_one),$(pub_select_cell_two)) ;
           
>>>>>>> 72ce6a2dd87e385d76b8d341c5a3a772601c03e6
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
