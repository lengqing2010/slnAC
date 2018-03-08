<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ER.aspx.vb" Inherits="ER" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style type="text/css"> 
.wrapper{
    width: 600px;
    height :600px;
    background-color: lightgreen;
}
.er_table_content{
    width: 200px;
    height: 200px;
    background-color: #ccc;
    position: absolute;

}

/*ER Panel*/
/*table name*/
.table_name{
    background-color: blue;
    color: #fff;
    border:#000 1px solid;
}

/*table column*/
.column_ms{
    border:#000 1px solid;
    width:100%;
    
}
.column_ms td{
    border-right:#000 1px solid;
    border-bottom:#000 1px solid;
}




</style> 
    <script language="javascript" type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/SVG.js"></script>
    <script language="javascript" type="text/javascript" src="ER.js"></script>
    <script language="javascript" type="text/javascript">
/*
        $(document).ready(function () {
            //$('.er_table_content').dragDiv();

            //element.on('click', click)
            //element.off('click', click)
            if (SVG.supported) {
                
                SVG.on(window, 'click', function () {
                    //var point = path.point(e.screenX, e.screenY);

                   // alert(point)
                });


                var draw = SVG('drawing')
            
                //get()
                //has()

                //draw.clear()
                //draw.children()
                draw.each(function (i, children) {
                    this.fill({ color: '#f06' })
                })


                //                // e is some mouseevent 鼠标位置
                //                var point = path.point(e.screenX, e.screenY) // {x, y}

                var rect = draw.rect(100, 100).move(100, 50).fill('#fec');

                //link
                var link = draw.link('http://svgdotjs.github.io/')
                rect.linkTo(function (link) {
                    link.to('http://svgdotjs.github.io/').target('_blank')
                })


                var rect1 = link.rect(100, 100);
                var group = draw.group()

                rect.replace(draw.circle(100))

                // inserts circle after rect
                //                rect.after(circle)
                //                // inserts circle before rect
                //                rect.before(circle)


                group.add(rect)
                group.add(rect1)
                group.move(100, 500)

                //Copy
                var use = draw.use(rect1).move(200, 200)
                //remove
                //rect1.remove();



                //text
                var text = draw.plain('Lorem ipsum dolor sit amet consectetur.')
                text.move(20, 20).font({ fill: '#f06', family: 'Inconsolata' })
                //text.clear()

                // 不好用

                //The link url can be updated with the to() method:
                link.to('http://apple.com')
                //Furthermore, the link element has a show() method to create the xlink:show attribute:
                // link.show('replace')

                //**画线用这个不错 曲线
                var path = draw.path('M0 0 A50 50 0 0 1 50 50 A50 50 0 0 0 100 100')
                path.fill('none').move(20, 20).stroke({ width: 1, color: '#ccc' })
                path.marker('start', 10, 10, function (add) {
                    add.circle(10).fill('#f06')
                })
                path.marker('mid', 10, 10, function (add) {
                    add.rect(5, 10).cx(5).fill('#ccc')
                })
                path.marker('end', 20, 20, function (add) {
                    add.circle(6).center(4, 5)
                    add.circle(6).center(4, 15)
                    add.circle(6).center(12, 10)

                    this.fill('#0f9')
                })
                //path.marker.ref(2, 7)

                //rect.attr({
                //    fill: '#f06'
                //, 'fill-opacity': 0.5
                //, stroke: '#000'
                //, 'stroke-width': 10
                //})


                //rect.attr('x', 50, 'http://www.w3.org/2000/svg')

                //var x = rect.attr('x')



                //Positioning
                //                rect.attr({ x: 20, y: 60 })
                //                circle.attr({ cx: 50, cy: 40 })

                //                rect.cx(20).cy(60)
                //                circle.x(50).y(40)
                //                rect.move(200, 350)


                //Resizing
                //                rect.size(200, 300)
                //                rect.size(200)
                //                rect.size(null, 200)


                //fill
                //                rect.fill({ color: '#f06', opacity: 0.6 })
                //                rect.fill('#f06')
                //                rect.fill('images/shade.jpg')
                //                rect.fill(draw.image('images/shade.jpg', 20, 20))


                //stroke          The stroke() method is similar to fill():



                //opacity()
                //                rect.opacity(0.5)


                //Transforming  Transform转化
                //                element.transform({ rotation: 125 }).transform({ rotation: 37.5 })


                //flip() 反转
                //                element.flip('x')
                //                element.flip('y')
                //rotate() 旋转
                //                element.rotate(45)
                //                // rotate(degrees, cx, cy)
                //                element.rotate(45, 50, 50)


                //skew 斜
                //                // skew(x, y)
                //                element.skew(0, 45)


                //scale比例
                //                // scale(x, y)
                //                element.scale(0.5, -1)

                //translate() 位移 同margin

                //                // translate(x, y)
                //                element.translate(0.5, -1)

                //Styles
                //                element.style('cursor', 'pointer')
                //                element.style('cursor', null)
                //                element.style({ cursor: 'pointer', fill: '#f03' })
                //                element.style('cursor:pointer;fill:#f03;')
                //                element.style('cursor')
                //                element.style()
                //                // => 'cursor:pointer;fill:#f03;'


                //hide()  show()  element.visible()

                //hide()  show()  element.addClass() element.removeClass element.hasClass('purple-rain')
                //element.toggleClass('pink-flower') 切换



                //**********
                //var key = rect.data('key') //Get
                //rect.data('key', { value: { data: 0.3 }}) //Set
                //                rect.data({
                //                    forbidden: 'fruit'
                //                , multiple: {
                //                values: 'in'
                //                , an: 'object'
                //                }
                //                })



                //remember forget()  缓存到内存？
                //                rect.remember('oldBBox', rect.bbox())
                //                rect.remember({
                //                    oldFill: rect.attr('fill')
                //                    , oldStroke: rect.attr('stroke')
                //                                    })
                //                rect.forget('oldBBox')
                //                rect.forget()

            } else {
                alert('SVG not supported')
            }




        });

*/
        $(document).ready(function () {
            $('.er_table_content1').dragDiv();
            $('.er_table_content2').dragDiv();
        });

        ;   (function ($) {


            
            $.fn.dragDiv = function (options) {
                return this.each(function () {
                    var _moveDiv = $(this); //需要拖动的Div
                    var _moveArea = options ? $(options) : $(document); //限定拖动区域，默认为整个文档内
                    var isDown = false; //mousedown标记
                    //ie的事件监听，拖拽div时禁止选中内容，firefox与chrome已在css中设置过-moz-user-select: none; -webkit-user-select: none;
                    if (document.attachEvent) {
                        _moveDiv[0].attachEvent('onselectstart', function () {
                            return false;
                        });
                    }
                    _moveDiv.mousedown(function (event) {
                        var e = event || window.event;
                        //拖动时鼠标样式
                        _moveDiv.css("cursor", "move");
                        //获得鼠标指针离DIV元素左边界的距离
                        var x = e.pageX - _moveDiv.offset().left;
                        //获得鼠标指针离DIV元素上边界的距离 
                        var y = e.pageY - _moveDiv.offset().top;


                        _moveArea.on('mousemove', function (event) {
                            var ev = event || window.event;
                            //获得X轴方向移动的值 
                            var abs_x = ev.pageX - x;
                            //获得Y轴方向移动的值 
                            var abs_y = ev.pageY - y;

                            //div动态位置赋值
                            _moveDiv.css({ 'left': abs_x, 'top': abs_y });
                        })
                    });
                    _moveDiv.mouseup(function () {
                        _moveDiv.css('cursor', 'default');
                        //解绑拖动事件
                        _moveArea.off('mousemove');

                    });

                })
            }
        })(jQuery)
    
    </script>
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="drawing" style="position:absolute;width:100%; height:100%;  background-color:Silver;">
            <foreignobject x="0" y="0" >
                <body xmlns="http://www.w3.org/1999/xhtml">
                    
                    <div class="er_table_content1 er_table_content" style="left:300px">
                        <div class="table_name">m_job</div>
                        <table class="column_ms" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="column_name">edp_no</td>
                                <td class="column_type">nvarchar</td>
                                <td class="column_length">20</td>
                            </tr>
                            <tr>
                                <td class="column_name">edp_kj</td>
                                <td class="column_type">nvarchar</td>
                                <td class="column_length">220</td>
                            </tr>
                        </table>
                    </div>
            
                    <div class="er_table_content2 er_table_content">
                        <div class="table_name">m_job</div>
                        <table class="column_ms" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="column_name">edp_no</td>
                                <td class="column_type">nvarchar</td>
                                <td class="column_length">20</td>
                            </tr>
                            <tr>
                                <td class="column_name">edp_kj</td>
                                <td class="column_type">nvarchar</td>
                                <td class="column_length">220</td>
                            </tr>
                        </table>
                    </div>
                </body>
            </foreignobject>
        </div>
    </div>
    </form>
</body>
</html>
