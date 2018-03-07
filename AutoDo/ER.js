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

    return oER;   
}


$(document).ready(function () {
    var eEr;
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


});