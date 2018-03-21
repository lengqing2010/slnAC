<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkannKanri.aspx.vb" Inherits="AnkannKanri" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
    <title></title>
    
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>


 
 
  <style>
/*! normalize.css v2.1.2 | MIT License | git.io/normalize */

/* ==========================================================================
   HTML5 display definitions
   ========================================================================== */

/**
 * Correct `block` display not defined in IE 8/9.
 */

article,
aside,
details,
figcaption,
figure,
footer,
header,
hgroup,
main,
nav,
section,
summary {
    display: block;
}

/**
 * Correct `inline-block` display not defined in IE 8/9.
 */

audio,
canvas,
video {
    display: inline-block;
}

/**
 * Prevent modern browsers from displaying `audio` without controls.
 * Remove excess height in iOS 5 devices.
 */

audio:not([controls]) {
    display: none;
    height: 0;
}

/**
 * Address styling not present in IE 8/9.
 */

[hidden] {
    display: none;
}

/* ==========================================================================
   Base
   ========================================================================== */

/**
 * 1. Set default font family to sans-serif.
 * 2. Prevent iOS text size adjust after orientation change, without disabling
 *    user zoom.
 */

html {
    font-family: sans-serif; /* 1 */
    -ms-text-size-adjust: 100%; /* 2 */
    -webkit-text-size-adjust: 100%; /* 2 */
}

/**
 * Remove default margin.
 */

body {
    margin: 0;
}

/* ==========================================================================
   Links
   ========================================================================== */

/**
 * Address `outline` inconsistency between Chrome and other browsers.
 */

a:focus {
    outline: thin dotted;
}

/**
 * Improve readability when focused and also mouse hovered in all browsers.
 */

a:active,
a:hover {
    outline: 0;
}

/* ==========================================================================
   Typography
   ========================================================================== */

/**
 * Address variable `h1` font-size and margin within `section` and `article`
 * contexts in Firefox 4+, Safari 5, and Chrome.
 */

h1 {
    font-size: 2em;
    margin: 0.67em 0;
}

/**
 * Address styling not present in IE 8/9, Safari 5, and Chrome.
 */

abbr[title] {
    border-bottom: 1px dotted;
}

/**
 * Address style set to `bolder` in Firefox 4+, Safari 5, and Chrome.
 */

b,
strong {
    font-weight: bold;
}

/**
 * Address styling not present in Safari 5 and Chrome.
 */

dfn {
    font-style: italic;
}

/**
 * Address differences between Firefox and other browsers.
 */

hr {
    -moz-box-sizing: content-box;
    box-sizing: content-box;
    height: 0;
}

/**
 * Address styling not present in IE 8/9.
 */

mark {
    background: #ff0;
    color: #000;
}

/**
 * Correct font family set oddly in Safari 5 and Chrome.
 */

code,
kbd,
pre,
samp {
    font-family: monospace, serif;
    font-size: 1em;
}

/**
 * Improve readability of pre-formatted text in all browsers.
 */

pre {
    white-space: pre-wrap;
}

/**
 * Set consistent quote types.
 */

q {
    quotes: "\201C" "\201D" "\2018" "\2019";
}

/**
 * Address inconsistent and variable font size in all browsers.
 */

small {
    font-size: 80%;
}

/**
 * Prevent `sub` and `sup` affecting `line-height` in all browsers.
 */

sub,
sup {
    font-size: 75%;
    line-height: 0;
    position: relative;
    vertical-align: baseline;
}

sup {
    top: -0.5em;
}

sub {
    bottom: -0.25em;
}

/* ==========================================================================
   Embedded content
   ========================================================================== */

/**
 * Remove border when inside `a` element in IE 8/9.
 */

img {
    border: 0;
}

/**
 * Correct overflow displayed oddly in IE 9.
 */

svg:not(:root) {
    overflow: hidden;
}

/* ==========================================================================
   Figures
   ========================================================================== */

/**
 * Address margin not present in IE 8/9 and Safari 5.
 */

figure {
    margin: 0;
}

/* ==========================================================================
   Forms
   ========================================================================== */

/**
 * Define consistent border, margin, and padding.
 */

fieldset {
    border: 1px solid #c0c0c0;
    margin: 0 2px;
    padding: 0.35em 0.625em 0.75em;
}

/**
 * 1. Correct `color` not being inherited in IE 8/9.
 * 2. Remove padding so people aren't caught out if they zero out fieldsets.
 */

legend {
    border: 0; /* 1 */
    padding: 0; /* 2 */
}

/**
 * 1. Correct font family not being inherited in all browsers.
 * 2. Correct font size not being inherited in all browsers.
 * 3. Address margins set differently in Firefox 4+, Safari 5, and Chrome.
 */

button,
input,
select,
textarea {
    font-family: inherit; /* 1 */
    font-size: 100%; /* 2 */
    margin: 0; /* 3 */
}

/**
 * Address Firefox 4+ setting `line-height` on `input` using `!important` in
 * the UA stylesheet.
 */

button,
input {
    line-height: normal;
}

/**
 * Address inconsistent `text-transform` inheritance for `button` and `select`.
 * All other form control elements do not inherit `text-transform` values.
 * Correct `button` style inheritance in Chrome, Safari 5+, and IE 8+.
 * Correct `select` style inheritance in Firefox 4+ and Opera.
 */

button,
select {
    text-transform: none;
}

/**
 * 1. Avoid the WebKit bug in Android 4.0.* where (2) destroys native `audio`
 *    and `video` controls.
 * 2. Correct inability to style clickable `input` types in iOS.
 * 3. Improve usability and consistency of cursor style between image-type
 *    `input` and others.
 */

button,
html input[type="button"], /* 1 */
input[type="reset"],
input[type="submit"] {
    -webkit-appearance: button; /* 2 */
    cursor: pointer; /* 3 */
}

/**
 * Re-set default cursor for disabled elements.
 */

button[disabled],
html input[disabled] {
    cursor: default;
}

/**
 * 1. Address box sizing set to `content-box` in IE 8/9.
 * 2. Remove excess padding in IE 8/9.
 */

input[type="checkbox"],
input[type="radio"] {
    box-sizing: border-box; /* 1 */
    padding: 0; /* 2 */
}

/**
 * 1. Address `appearance` set to `searchfield` in Safari 5 and Chrome.
 * 2. Address `box-sizing` set to `border-box` in Safari 5 and Chrome
 *    (include `-moz` to future-proof).
 */

input[type="search"] {
    -webkit-appearance: textfield; /* 1 */
    -moz-box-sizing: content-box;
    -webkit-box-sizing: content-box; /* 2 */
    box-sizing: content-box;
}

/**
 * Remove inner padding and search cancel button in Safari 5 and Chrome
 * on OS X.
 */

input[type="search"]::-webkit-search-cancel-button,
input[type="search"]::-webkit-search-decoration {
    -webkit-appearance: none;
}

/**
 * Remove inner padding and border in Firefox 4+.
 */

button::-moz-focus-inner,
input::-moz-focus-inner {
    border: 0;
    padding: 0;
}

/**
 * 1. Remove default vertical scrollbar in IE 8/9.
 * 2. Improve readability and alignment in all browsers.
 */

textarea {
    overflow: auto; /* 1 */
    vertical-align: top; /* 2 */
}

/* ==========================================================================
   Tables
   ========================================================================== */

/**
 * Remove most spacing between table cells.
 */

table {
    border-collapse: collapse;
    border-spacing: 0;
}
</style>

    <style>
@keyframes grow-padding-left {
  from {
    padding-left: 0;
  }
}
@keyframes grow-padding-right {
  from {
    padding-right: 0;
  }
}
h1 {
  text-align: center;
}



ul {
  list-style-type: none;
  padding: 0;
}
ul li {
  font-weight: bold;
  margin-bottom: 4px;
  border-radius: 4px;
  width: 400px;
}
ul em {
  display: inline-block;
  width: 180px;
  font-style: normal;
}
ul span {
  display: inline-block;
}


.style-3
{
	margin: 0px;
}

.style-3 li {
  color: white;
  background: orange;
  width:400px;
}
.style-3 em {
  padding: 4px;
  width:130px;
}
.style-3 span {
  float: right;
  background: rgba(0, 0, 0, 0.3);
  padding: 4px;
  width:30px;
  text-align: right;
  border-radius: 4px;
  animation: grow-padding-left 2s;
}


</style>

  <script src="js/prefixfree.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div>


        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>



        <table style="width: 1000px; margin-left:0px;">
            <tr>
                <td Width="200px">
                    edp_no
                </td>
                <td Width="600px">
                    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" FirstBlank = "true"  />
                </td>
                <td >
                    &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Edit" />
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    機能</td>
                <td Width="600px">
                    <uc1:UserDropdownList ID="ucKinouLst" runat="server" Width = "300" Height="20" JqName = "test" FirstBlank = "true" /></td>
                <td >
                    &nbsp;<asp:Button ID="btnUpdateKinou" runat="server" Text="Edit" /></td>
            </tr>
            <tr>
                <td Width="200px">
                    &nbsp;</td>
                <td Width="600px">
                    <asp:RadioButton ID="rbSinki" runat="server" GroupName="kinoukbn" Text = "新規" />
                    <asp:RadioButton ID="rbSyusei" runat="server"  GroupName="kinoukbn" Text = "修正" />
                </td>
                <td >

</td>
            </tr>
            <tr>
                <td Width="200px">
                    作成必要PGM</td>
                <td Width="600px">
                    &nbsp;</td>
                <td >
                    <asp:Button ID="btnPgmUpd" runat="server" Text="Edit" /></td>
            </tr>
            <tr>
                <td Width="200px">
                    <asp:GridView ID="gvPgm" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#Eval("pgm_bunrui_name")%>
                                    
                                    <asp:CheckBox ID="cbPgm" runat="server" Text='<%#Eval("pgm_name")%>' ToolTip = '<%#Eval("pgm_id")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td Width="800px" colspan="2">
<%--                                    <input type="button" value="20" class="jq_ritu" style="width:26px; text-align:center;" />
                                    <input type="button" value="40" class="jq_ritu" style="width:26px; text-align:center;" />
                                    <input type="button" value="50" class="jq_ritu" style="width:26px; text-align:center;" />
                                    <input type="button" value="70" class="jq_ritu" style="width:26px; text-align:center;" />
                                    <input type="button" value="80" class="jq_ritu" style="width:26px; text-align:center;" />
                                    <input type="button" value="100" class="jq_ritu" style="width:34px; text-align:center;" />--%>

                    <asp:GridView ID="gvPgmInfo" runat="server" AutoGenerateColumns="False" 
                        ShowHeader="False" Width="600px">
                        <Columns>

                            <asp:BoundField DataField="pgm_bunrui_name" />
<%--                            <asp:BoundField DataField="pgm_name" />--%>

                            <asp:TemplateField>
                                <ItemTemplate>
<ul class="style-3"><li><em><%#Eval("pgm_name")%></em><span><%#Eval("pgm_sinntyoku_retu")%></span></li></ul>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxRetu" runat="server"  
                                    Text='<%#Eval("pgm_sinntyoku_retu")%>' 
                                    pgm_id = '<%#Eval("pgm_id")%>'
                                    cssclass="jq_tbx_ritu"
                                    Width="40"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td Width="200px">
                    <asp:Button ID="btnPgmIns" runat="server" Text="New" />
                    <asp:Button ID="btnPgmSave" runat="server" Text="SAVE" />
                </td>
                <td Width="600px">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            </table>
    </div>

    <script type="text/javascript">


        $(document).ready(function () {



            //保存
            $(".jq_ritu").click(function () {

                $(this).parent().parent().find(".jq_tbx_ritu").val($(this).val());

            });


        });
    </script>


    
<div>
<section>
<ul class="style-3">
<li><em>Shopping</em><span>100</span></li>
<li><em>House</em><span>100</span></li>
<li><em>Phone</em><span>50</span></li>
<li><em>Travel</em><span>80</span></li>
</ul>
</section>

</div>
<div style="text-align:center;clear:both">
<script src="/gg_bd_ad_720x90.js" type="text/javascript"></script>
<script src="/follow.js" type="text/javascript"></script>
</div>
<script src='js/jquery.js'></script>

<script src="js/index.js"></script>


    </form>
</body>
</html>
