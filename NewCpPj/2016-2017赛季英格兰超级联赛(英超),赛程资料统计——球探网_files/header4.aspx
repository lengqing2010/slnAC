
function changeCsDiv(cna)
{
    var items = document.getElementById(cna).getElementsByTagName("span");
           var clsName = "b_bb";
            for (var i = 0; i < items.length; i++)
               {items[i].className = clsName; }
}
function showDiv(cna) {
    document.getElementById(cna).style.display = "block";
}
function hideDiv(cna,event)
{
 
  try{
   if (window.event)
       ev = window.event.toElement;
   else
       ev = event.relatedTarget;
   if (ev.id != cna && ev.id.indexOf("aa") == -1 ) {
        document.getElementById(cna).style.display = "none";
        var items = document.getElementById(cna.replace("div", "")).getElementsByTagName("span");
        for (var i = 0; i < items.length; i++)
          { 
            if(cna=="b_divL9") 
               items[i].className =  "b_s9"; 
          }
      }
  } catch (e) { }
}
 function check(theForm) {
        if (theForm.UserName.value == '') {
            alert("请输入球探帐号");
            theForm.UserName.focus();
            return false;
        }
        if (theForm.Password.value == '') {
            alert("请输入密码");
            theForm.Password.focus();
            return false;
        }
    }
    function showhideul(kind)
    {
        document.getElementById("ulselect").style.display = kind == 0 ? "none" : "block";
    }
    function showhideul2(kind) {
        document.getElementById("ulselect2").style.display = kind == 0 ? "none" : "block";
    }
    function showhideul3(kind) {
        document.getElementById("ulselect3").style.display = kind == 0 ? "none" : "block";
    }
	document.write('<link href="http://guess2.win007.com/Styles/head_pubic2.css" rel="stylesheet" type="text/css" />');
document.write('<div id="stop"><div id="toplogin">');

    document.write('<div style="float:left;">');
       document.write('<form name="formlogin" method="post" action="http://guess2.win007.com/users/login.aspx?g=0" onsubmit="return check(this);">');
   document.write('<div class="ms1">球探帐号：</div>');
    document.write('<div class="ms2">');
     document.write('<input type="text" name="UserName" id="UserName" class="userint" />');
   document.write('</div>');
   document.write('<div class="ms3">密码：</div>');
   document.write('<div class="ms2"><input type="password" name="Password" id="Password" class="userint" /></div>');
   document.write('<div  class="ms4">');
    document.write('<input type="submit" name="button" id="button" value="" class="userbtn" />');
document.write('</div></form></div>');
    document.write('<div style="float: left;"><a href="http://users.win007.com/users/register.aspx"><font color="Red">免费注册</font></a>');
    document.write(' | <a href="http://users.win007.com/users/forgetpassword.aspx">忘记密码</a>');
    document.write(' | <a href="http://ba2.win007.com/61/3688856.html" target="_blank">帮助</a>');
    document.write(' </div>');

document.write('<div style="float:right;" onmousemove="showhideul2(1)" onmouseout="showhideul2(0)">&nbsp;|&nbsp;<a href="javascript:void(0);">关注收藏</a><ul id="ulselect2" class="kops2" style="display:none;"><li><a href="http://weibo.com/bet007com" target="_blank" >微博关注</a></li><li><a href="javascript:AddFavorite(\'http://www.win007.com/\',\'球探网-专业体育彩票数据\');">主网收藏</a></li><li><a href="javascript:AddFavorite(\'http://www.titan007.com\',\'球探网-专业体育彩票数据\');">备用收藏</a></li></ul></div>');            
document.write('<div style="float: right;">');
document.write('&nbsp;|&nbsp;<a href="http://www.win007.com/about/ads.html" target="_blank">业务合作</a>');
document.write('</div>');
    document.write('<div style="float:right;" onmousemove="showhideul3(1)" onmouseout="showhideul3(0)"><a href="javascript:void(0);">手机比分&nbsp;</a><ul id="ulselect3" class="kops3" style="display:none;"><li><a href="http://www.ewin007.com/app/" target="_blank" >数据大师下载</a></li><li><a href="http://www.win007.com/app/" target="_blank" >球探体育下载</a></li></ul></div>'); 
document.write('</div></div>');

function AddFavorite(sURL, sTitle)
{
    try
    {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e)
    {
        try
        {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e)
        {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}