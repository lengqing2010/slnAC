/* ************************************************************************************************
                                  YYYY/MM/DD 期日自動作成
作成者 : 李松涛(大連)＿LIS6
作成日 : 2007/12/12
重点：期日初始値　　　YYYY/MM/DD
************************************************************************************************ */
var userLisNum1="";
var userLisNum2="";
var userLisNum3="";
var userLisNum4="";
var userLisNum5="";
var userLisNum6="";
var userLisNum7="";
var userLisNum8="";
var intXPos = 1;
var strdate = "";
var onfocusflg = false;
var maxYear = 2100;
var minYear = 1988;
var mouseDownKbn = false;
var strDataValue;
var strOldDataValue;
/* ************************************************************************************************
	関数名 setFocusOut
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : false
		概要   : 期日ﾁｪｯｸ
************************************************************************************************ */


function setFocusOut(obj){
    if (getMaxDay(userLisNum5*10+userLisNum6*1) == (userLisNum7+"")){
        if (userLisNum8>getLastDayNum(userLisNum5*10+userLisNum6*1)){
            strdate = userLisNum1 + userLisNum2 + userLisNum3 + userLisNum4 + "/" + userLisNum5 + userLisNum6 + "/" + (--userLisNum7) + userLisNum8;
            obj.value = strdate;
        }
    }

//    if (obj.value.indexOf("_") >= 0){    
//        obj.value = strOldDataValue; 
//    }  
}

function setouseDownKbn(){
    mouseDownKbn = true;
}
/* ************************************************************************************************
	関数名 checkDate
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 4番目 -> N1,N2,N3,N4
		戻り値 : false
		概要   : 期日ﾁｪｯｸ
************************************************************************************************ */
function checkDate(N1,N2,N3,N4){
    var str;
    var intValue;
    if (N1=="" || N1=="_"){
        str = "0";
    }else{
        str = N1 + ""
    }    
    if (N2=="" || N2=="_"){
        str = str + "0";
    }else{
        str = str + N2 + ""
    }
    if (N3=="" || N3=="_"){
        str = str + "0";
    }else{
        str = str + N3 + ""
    }
    if (N4=="" || N4=="_"){
        str = str + "0";
    }else{
        str = str + N4 + ""
    }    
    intValue = parseInt(str,10);    
    if (intValue>maxYear || intValue<minYear){    
        return false;
    }
    return true;    
}

/* ************************************************************************************************
	関数名 getDateValue
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : false
		概要   : 期日設定に値します
************************************************************************************************ */
function getDateValue(obj){

    
    var objtime = document.getElementById(obj.id);
    strOldDataValue = obj.value;
    strDataValue = objtime.value;
    if (strDataValue.length == 10){
        userLisNum1 = strDataValue.substring(0,1);
        userLisNum2 = strDataValue.substring(1,2);
        userLisNum3 = strDataValue.substring(2,3);
        userLisNum4 = strDataValue.substring(3,4);
        userLisNum5 = strDataValue.substring(5,6);
        userLisNum6 = strDataValue.substring(6,7);
        userLisNum7 = strDataValue.substring(8,9);
        userLisNum8 = strDataValue.substring(9,10);

        if (!mouseDownKbn){
            selectLength(obj,0,1);

        }else{
            mouseDownKbn = false;        
        }
        return false;        
    }
}
/* ************************************************************************************************
	関数名 getMaxDay
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> intI
		戻り値 : 最大の日
		概要   : 期日　最大の日
************************************************************************************************ */
function getMaxDay(intI){
    if (intI==2){return 2;}else{return 3;}    
}
/* ************************************************************************************************
	関数名 getKeyValue
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> value
		戻り値 : 入力の値
		概要   : 値を入力します
************************************************************************************************ */
function getKeyValue(value){
    if (48<=value && value<=57){
        return value - 48    ;
    }

    if (96<=value && value<=105){
        return value - 96    ;
    }
    return -1;    
}
/* ************************************************************************************************
	関数名 getLastDayNum
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> intI
		戻り値 : 最後の日
		概要   : 期日　最後の日
************************************************************************************************ */
function getLastDayNum(i){
    var intYear;
    var intI;

    if ((i+"").length>2){return 0;}
    intI = i;
    intYear = parseInt(userLisNum1 + userLisNum2 + userLisNum3 + userLisNum4 +'',10);
    if (intI==1){return 1;}
    if (intI==2){    
        if(new Date(intYear , 2 , 0).getDate()==29){
            return 9;
        }else{
            return 8;
        }
    }
    if (intI==3){return 1;}
    if (intI==4){return 0;}
    if (intI==5){return 1;}
    if (intI==6){return 0;}
    if (intI==7){return 1;}
    if (intI==8){return 1;}
    if (intI==9){return 0;}
    if (intI==10){return 1;}
    if (intI==11){return 0;}
    if (intI==12){return 1;}
}
/* ************************************************************************************************
	関数名 setValueKeydown
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : 期日 設定「keydown」
************************************************************************************************ */
function setValueKeydown(obj){

var selLength = getSelectedText().length;
var outLength = selLength;  
var date = new Date();
var keyValue ;

keyValue = getKeyValue(event.keyCode);
if (event.keyCode==9){return}
if (event.keyCode==13){event.keyCode=9;return}


if (selLength <2){            
} else {  
    
    for (i=intXPos;i<=intXPos+selLength-1;i++){
        if (intXPos+selLength == 5 || intXPos+selLength == 7){
            outLength--;
        }                
    }     

    for (i=intXPos;i<=intXPos + outLength-1;i++){
        eval("userLisNum" + i + "='" + "_" + "';");             
    } 
    
} 
    
if (keyValue != -1) {
 



    if (intXPos == 1 ){ 

        if (keyValue>0){  

            if (userLisNum2 != "_" || userLisNum3 != "_" || userLisNum4 != "_"){
                if (!checkDate(keyValue , userLisNum2 , userLisNum3 , userLisNum4 )){
                    if (keyValue>(maxYear+"").substring(0,1)){
                    	return false;
                    }else{
	                    userLisNum2 = "_";
	                    userLisNum3 = "_";
	                    userLisNum4 = "_";            
                    }
                }
            } else {
 
                if (keyValue>parseInt((maxYear+"").substring(0,1),10) || keyValue<parseInt((minYear+"").substring(0,1),10)){
                    return false;
                }            
            }   
        } else {        
            return false;
        }  
    }
    
    
    if (intXPos == 2 ){    
        if (userLisNum3 != "_" || userLisNum4 != "_"){  
            if (!checkDate(userLisNum1 , keyValue , userLisNum3 , userLisNum4 )){return false;}
        }else{
            if ((userLisNum1 * 10 +keyValue)>parseInt((maxYear+"").substring(0,2),10) || (userLisNum1 * 10 +keyValue)<parseInt((minYear+"").substring(0,2),10)){
                  return false;
            }        
        }        
    }
    if (intXPos == 3 ){  
        if (userLisNum1 == "_" && userLisNum2 == "_" && userLisNum4 == "_"){    
            if (!checkDate((date.getYear() + "").substring(0,1), (date.getYear() + "").substring(1,2) , keyValue , userLisNum4 )){
            return false;
            }else{
                if (userLisNum1 == "_" && userLisNum2 == "_" && userLisNum4 == "_"){
                    userLisNum1 = (date.getYear() + "").substring(0,1);
                    userLisNum2 = (date.getYear() + "").substring(1,2);   
                }         
            }
        }else{
            if (!checkDate(userLisNum1, userLisNum2 , keyValue , userLisNum4 )){
                return false;
            }else{
            }           
        }
    }
    if (intXPos == 4 ){      
        if (userLisNum1 == "_" && userLisNum2 == "_" && userLisNum3 == "_"){
        
            if (!checkDate((date.getYear() + "").substring(0,1), (date.getYear() + "").substring(1,2) , (date.getYear() + "").substring(2,3) , keyValue )){
            return false;
            }else{
                if (userLisNum1 == "_" && userLisNum2 == "_" && userLisNum3 == "_"){
                    userLisNum1 = (date.getYear() + "").substring(0,1);
                    userLisNum2 = (date.getYear() + "").substring(1,2);   
                    userLisNum3 = (date.getYear() + "").substring(2,3);          
                }
            }
        }else{
        
            if (!checkDate(userLisNum1, userLisNum2 , userLisNum3 , keyValue )){
                return false;
            }else{
            }        
        }        
    }    
    if (intXPos == 5 ){      
        if ( keyValue > 1){
            eval("userLisNum" + intXPos + "='" + 0 + "';");
        intXPos ++;      }
        if ( keyValue == 1) {
            eval("userLisNum" + (intXPos+1) + "='" + "_" + "';");        
        }
    } 
    if (intXPos == 6 ){    
        if(userLisNum5==1){
            if ( keyValue > 2){return false;}   
        }else{        
        }
        if (keyValue==0 && userLisNum5==0){return false;}
    }           
    if (intXPos == 7){
        if (keyValue > getMaxDay(userLisNum5*10+userLisNum6*1)){          
            eval("userLisNum" + intXPos + "='" + 0 + "';");
            intXPos ++;}             
        if (keyValue == getMaxDay(userLisNum5*10+userLisNum6*1)){          
            eval("userLisNum" + (intXPos+1) + "='" + "_" + "';");
        }               
    }      
    if (intXPos == 8){

        if (keyValue==0 && userLisNum7==0){return false;}
                  
        if (getMaxDay(userLisNum5*10+userLisNum6*1) == (userLisNum7+"")){
            if (keyValue>getLastDayNum(userLisNum5*10+userLisNum6*1)){
                return false;
            }
        }


    }      
        
    eval("userLisNum" + intXPos + "='" + (keyValue) + "';");
      
      
      if (intXPos==8){
        intXPos=8;
      }else{ 
        intXPos ++;
      } 
}   
  if (event.keyCode==37){

      if (intXPos==1){
        intXPos=1;
      }else{ 
        intXPos --;
      }    
  }    
    if (39==event.keyCode){

      if (intXPos==8){
        intXPos=8;
      }else{ 
        intXPos++;
      }    
    } 
    if (8==event.keyCode){

        var i ;


            if (intXPos<5){
                for (i=intXPos;i<=4;i++){
                    eval("userLisNum" + i + "='" + "_" + "';");
                }
            }
            if (intXPos>4 && intXPos<7){
                for (i=intXPos;i<=6;i++){
                    eval("userLisNum" + i + "='" + "_" + "';");
                }
            }
            if (intXPos>6 && intXPos<9){
                for (i=intXPos;i<=8;i++){
                    eval("userLisNum" + i + "='" + "_" + "';");
                }
            }
     
            
        eval("userLisNum" + intXPos + "='" + "_" + "';");
        if (intXPos==1){
            intXPos=1;
        }else{ 
            intXPos --;
        }    

    }  
    if (46==event.keyCode){
        var i ;
        if (intXPos<5){
            for (i=intXPos;i<=4;i++){
                eval("userLisNum" + i + "='" + "_" + "';");
            }
        }
        if (intXPos>4 && intXPos<7){
            for (i=intXPos;i<=6;i++){
                eval("userLisNum" + i + "='" + "_" + "';");
            }
        }
        if (intXPos>6 && intXPos<9){
            for (i=intXPos;i<=8;i++){
                eval("userLisNum" + i + "='" + "_" + "';");
            }
        }
        eval("userLisNum" + intXPos + "='" + "_" + "';");
        if (intXPos==1){
            intXPos=1;
        }else{ 
        }    
    }    
    strdate = userLisNum1 + userLisNum2 + userLisNum3 + userLisNum4 + "/" + userLisNum5 + userLisNum6 + "/" + userLisNum7 + userLisNum8;
    setvalue(obj);

    return false;    
}
function setvalue(obj){
    var objtime = document.getElementById(obj.id);
    
    if (event.keyCode==27){
        mouseDownKbn = false;    
        objtime.value = strDataValue;        
        getDateValue(obj);
        intXPos=1;
        return
    }
    
    if (strdate != ''){
    objtime.value = strdate;
    getPos(obj);
    }
}
/* ************************************************************************************************
	関数名 getPos
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : マウスポインタの位置を獲得します
************************************************************************************************ */
function getPos(obj)
{
    var workRange=document.selection.createRange();
    obj.select();
    var allRange=document.selection.createRange();
    workRange.setEndPoint("StartToStart",allRange);
    var len=workRange.text.length;
    workRange.collapse(false);
    workRange.select(); 
    var intN;
    intN = intXPos;
    if (intN>=5){intN=intN+1;}
    if (intN>=8){intN=intN+1;}
    selectLength(obj,intN-1,1) ;    
    return len;
}
/* ************************************************************************************************
	関数名 setPos
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : マウスポインタの位置を設定します
************************************************************************************************ */
var tLength = 0;
function setPos(obj){
try{
    
    var selText=""; 
    mouseDownKbn = false;  
    
	if(isSelected){
		selText =getSelectedText();
		isSelected = false;
		if(selText.length>1){}
	}
	tLength = selText.length;
	
    var workRange=document.selection.createRange();
    obj.select();
    var allRange=document.selection.createRange();
    workRange.setEndPoint("StartToStart",allRange);
    var len=workRange.text.length;
    workRange.collapse(false);
    workRange.select(); 
    var rn;

    rn = len;
    if (len == 4){rn = 5;}
    if (len == 7){rn = 8;}
    if (len == 10){rn = 9;} 
    
    rn = len-selText.length;
    if (tLength==0){tLength=1;}

    if (rn == 4 && tLength==1){rn = 5;}
    if (rn == 7 && tLength==1){rn = 8;}
    if (rn == 10 && tLength==1){rn = 9;} 
    
    if (rn == 4 && tLength>1){rn = 5;tLength--;}
    if (rn == 7 && tLength>1){rn = 8;tLength--;}
    if (rn == 10 && tLength>1){rn = 9;tLength--;} 
    
    selectLength(obj,rn,tLength); 
    
    intXPos = rn+1;
    
    
    if (rn+1>= 5){intXPos--;}
    if (rn+1>= 7){intXPos--;}
    if (intXPos>8){intXPos=8;}
    return len;
 }catch(e){}
 
}
/* ************************************************************************************************
	関数名 getSelectedText
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : テキストを獲得します
************************************************************************************************ */
function getSelectedText() {
  if (window.getSelection) {
    return window.getSelection().toString();
  }
  else if (document.getSelection) {
    return document.getSelection();
  }
  else if (document.selection) {
    return document.selection.createRange().text;
  }
}
/* ************************************************************************************************
	関数名 setObjNull
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 
		戻り値 : 
		概要   : 変換を選びます
************************************************************************************************ */
var isSelected = false;
document.onselectionchange = function(){
  isSelected = true
}
/* ************************************************************************************************
	関数名 setObjNull
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : ヌルを設けます
************************************************************************************************ */
function setObjNull(){
    intXPos = 1;
}
/* ************************************************************************************************
	関数名 selectLength
	    作成者 : 李松涛(大連)＿LIS6
		作成日 : 2007/12/04
		引数   : 1番目 -> obj
		戻り値 : 
		概要   : テキスト選択
************************************************************************************************ */
function selectLength(textbox,start,len) 
{ 
    try 
    { 
        var r =textbox.createTextRange(); 
        r.moveEnd('character',len-(textbox.value.length-start)); 
        r.moveStart('character',start); 
        r.select(); 
    } 
    catch(e) 
    {
    } 
}



//日付妥当性チェック
var objDataColor;
objDataColor = '';
//-----------------------------

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

        if (v.length == 6) {  //6桁の場合
            if (v.substring(0, 2) > 70) {
                v = "19" + v;
            } else {
                v = "20" + v;
            }

        } else if (v.length == 4) {  //4桁の場合
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


    if (M.length == 1) { M = "0" + M; }
    if (D.length == 1) { D = "0" + D; }

    if (!checkDateVali(Y, M, D) || Y < "1753") {
        return false;

    } else {
        e.value = Y + "/" + M + "/" + D;
        return Y + "/" + M + "/" + D;

    }
}

/**
* 日付用スラッシュ付与

* @param str:付与対象値
* @return val:付与後値 or false
*/
function addSlash(str) {
    var val = "";
    str = removeSlash(str); //スラッシュ除去
    if (str.length != 8) return false; //8桁で無ければfalse
    val = str.substring(0, 4) + "/" + str.substring(4, 6) + "/" + str.substring(6, 8);  //yyyy/mm/dd
    return (val);
}

/**
* スラッシュ除去
* @param val:除去対象値
* @return num:除去後値
*/
function removeSlash(val) {
    return new String(val).replace(/\//g, '');  //カンマを削除
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

/**
* 日付妥当性チェック関数
* @param y:年
* @param m:月

* @param d:日
* @return boolean
*/
function checkDateVali(y, m, d) {
    var di = new Date(y, m - 1, d);
    if (di.getFullYear() == y && di.getMonth() == m - 1 && di.getDate() == d) {
        return true;
    }
    return false;
}