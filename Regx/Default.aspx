<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" EnableEventValidation="false" validateRequest="false"%>

<%@ Register src="WucEditor.ascx" tagname="WucEditor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
        <link rel="stylesheet" type="text/css" href="css/Editor.css">
        <link rel="stylesheet" type="text/css" href="css/common.css">

        <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
        <!--导入js库-->

        <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
        <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    File/Dic Path</td>
                <td>
                    <asp:TextBox ID="tbxPath" runat="server" Width="600" Text="H:\fff\1101F_TenburiDenpyouNyuuryoku.aspx.vb"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                     <uc1:WucEditor ID="WucKey" runat="server" EditType="vbscript"  theme="" width="800" height="300" 
TEXT=
"
'do 検索前操作|検索後操作②|検索Key③
'   検索前操作：COMMAND:save(save|変数名|) ,root||
'trim,lower,upper
'all,0tokey,keytoend,0tokeycontainskey,keytoendcontainskey
'onkeydownで検索
do trim,lower|0tokey|onkeydown
do replace|me.|
do |0tokey|.
do save|r1|
do root||
'onchangeで検索
do trim,lower|0tokey|onchange
do replace|me.|
do |0tokey|.
do save|r2|
'onkeydownとonchange検索結果比較（同じIdの場合r3に保存）
do in|r1|r2|r3
do show|r3|（同じIdの場合onkeydown、onchangeある）
'onkeydownとonchange一行に存在する場合
do root||
do trim,lower|all|onchange
do trim,lower|all|onkeydown
do save|r4|
do show|r4|（同じ行の場合onkeydown,onchangeある）
'this.onchangeある
do root||
do trim,lower|all|this.onchange
do save|r5|
do show|r5|（this.onchangeある）
"
/>
                </td>
                <td style="font-size:12px; color:Green; vertical-align:top;">
                    root|| ファイル内容取得<br />
                    in|変数1|変数2|変数3<br />
                    show|変数<br />
                    save|変数<br />
                    replace|文字1|文字2<br />
                    isnotfirst|文字1,文字2...<br />
                    ---------------------------------------------------<br />
                    do 検索前操作|検索後操作②|検索Key③<br />
                    検索前操作<br />
                    trim upper lower|文字<br />
                    検索後操作<br />
                    all|文字1 戻りall文字<br />
                    0tokey|文字1 戻り0桁～Key文字<br />
                    keytoend|文字1 戻りKey文字～最後<br />
                    0tokeycontainskey|文字1 戻り0桁～Key文字前<br />
                    keytoendcontainskey||文字1後 戻りKey文字～最後<br />

                    ---------------------------------------------------<br />

                    <br />

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    <asp:CheckBox ID="cbFilenomi" runat="server" /> 出力msg 只是文件名
    <asp:Button ID="btnSerch" runat="server" Text="Serch" />
    <uc1:WucEditor ID="WucEditor" runat="server" EditType="vbscript" TEXT="検索" theme="" width="1000" height="500"/>
    </form>
</body>
</html>
