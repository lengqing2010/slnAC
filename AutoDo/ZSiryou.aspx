﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ZSiryou.aspx.vb" Inherits="ZSiryou" %>

<%@ Register src="userctrl/WucEdpDb.ascx" tagname="WucEdpDb" tagprefix="uc1" %>

<%@ Register src="userctrl/WucEditor.ascx" tagname="WucEditor" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Editor.css" />
    <link rel="stylesheet" type="text/css" href="css/common.css" />

    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <!--TEXT 导入js库-->
    <script src="src-noconflict/ace.js" type="text/javascript" charset="utf-8"></script>
    <script src="src-noconflict/ext-language_tools.js" type="text/javascript" charset="utf-8"></script>

<script src="jquery-ui-1.12.1/external/jquery/jquery.js"></script>
<script src="jquery-ui-1.12.1/jquery-ui.js"></script>

<link href="jquery-ui-1.12.1/jquery-ui.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <uc1:WucEdpDb ID="WucEdpDb1" runat="server" />
        
    </div>

    <div class="header2"> 
        TITLE : 
        <asp:TextBox ID="tbxGroupNm" runat="server" Width="150px"  BackColor="#EEE8AA" CssClass="txt"></asp:TextBox>
        <asp:TextBox ID="tbxTitleNm" runat="server" Width="500px"  BackColor="#EEE8AA" CssClass="txt"></asp:TextBox>
        <asp:DropDownList ID="ddlType" runat="server" Width="100px" BackColor="#EEE8AA" CssClass="txt">
            <asp:ListItem Value="text">Text</asp:ListItem>
            <asp:ListItem Value="sqlserver">SQLServer</asp:ListItem>
            <asp:ListItem Value="sql">SQL</asp:ListItem>
            <asp:ListItem Value="mysql">MySQL</asp:ListItem>
            <asp:ListItem Value="javascript">JavaScript</asp:ListItem>
            <asp:ListItem Value="vbscript">VBScript</asp:ListItem>
            <asp:ListItem Value="html">HTML</asp:ListItem>
            <asp:ListItem Value="rhtml">RHTML</asp:ListItem>
            <asp:ListItem Value="svg">SVG</asp:ListItem>
            <asp:ListItem Value="xml">XML</asp:ListItem>
            <asp:ListItem Value="pgsql">pgSQL</asp:ListItem>
            <asp:ListItem Value="php">PHP</asp:ListItem>
            <asp:ListItem Value="abap">ABAP</asp:ListItem>
            <asp:ListItem Value="abc">ABC</asp:ListItem>
            <asp:ListItem Value="css">CSS</asp:ListItem>
            <asp:ListItem Value="csharp">C#</asp:ListItem>
            <asp:ListItem Value="actionscript">ActionScript</asp:ListItem>
            <asp:ListItem Value="ada">ADA</asp:ListItem>
            <asp:ListItem Value="apache_conf">Apache Conf</asp:ListItem>
            <asp:ListItem Value="asciidoc">AsciiDoc</asp:ListItem>
            <asp:ListItem Value="assembly_x86">Assembly x86</asp:ListItem>
            <asp:ListItem Value="autohotkey">AutoHotKey</asp:ListItem>
            <asp:ListItem Value="batchfile">BatchFile</asp:ListItem>
            <asp:ListItem Value="bro">Bro</asp:ListItem>
            <asp:ListItem Value="c_cpp">C and C++</asp:ListItem>
            <asp:ListItem Value="c9search">C9Search</asp:ListItem>
            <asp:ListItem Value="cirru">Cirru</asp:ListItem>
            <asp:ListItem Value="clojure">Clojure</asp:ListItem>
            <asp:ListItem Value="cobol">Cobol</asp:ListItem>
            <asp:ListItem Value="coffee">CoffeeScript</asp:ListItem>
            <asp:ListItem Value="coldfusion">ColdFusion</asp:ListItem>
            <asp:ListItem Value="csound_document">Csound Document</asp:ListItem>
            <asp:ListItem Value="csound_orchestra">Csound</asp:ListItem>
            <asp:ListItem Value="csound_score">Csound Score</asp:ListItem>
            <asp:ListItem Value="curly">Curly</asp:ListItem>
            <asp:ListItem Value="d">D</asp:ListItem>
            <asp:ListItem Value="dart">Dart</asp:ListItem>
            <asp:ListItem Value="diff">Diff</asp:ListItem>
            <asp:ListItem Value="dockerfile">Dockerfile</asp:ListItem>
            <asp:ListItem Value="dot">Dot</asp:ListItem>
            <asp:ListItem Value="drools">Drools</asp:ListItem>
            <asp:ListItem Value="dummy">Dummy</asp:ListItem>
            <asp:ListItem Value="dummysyntax">DummySyntax</asp:ListItem>
            <asp:ListItem Value="eiffel">Eiffel</asp:ListItem>
            <asp:ListItem Value="ejs">EJS</asp:ListItem>
            <asp:ListItem Value="elixir">Elixir</asp:ListItem>
            <asp:ListItem Value="elm">Elm</asp:ListItem>
            <asp:ListItem Value="erlang">Erlang</asp:ListItem>
            <asp:ListItem Value="forth">Forth</asp:ListItem>
            <asp:ListItem Value="fortran">Fortran</asp:ListItem>
            <asp:ListItem Value="ftl">FreeMarker</asp:ListItem>
            <asp:ListItem Value="gcode">Gcode</asp:ListItem>
            <asp:ListItem Value="gherkin">Gherkin</asp:ListItem>
            <asp:ListItem Value="gitignore">Gitignore</asp:ListItem>
            <asp:ListItem Value="glsl">Glsl</asp:ListItem>
            <asp:ListItem Value="gobstones">Gobstones</asp:ListItem>
            <asp:ListItem Value="golang">Go</asp:ListItem>
            <asp:ListItem Value="graphqlschema">GraphQLSchema</asp:ListItem>
            <asp:ListItem Value="groovy">Groovy</asp:ListItem>
            <asp:ListItem Value="haml">HAML</asp:ListItem>
            <asp:ListItem Value="handlebars">Handlebars</asp:ListItem>
            <asp:ListItem Value="haskell">Haskell</asp:ListItem>
            <asp:ListItem Value="haskell_cabal">Haskell Cabal</asp:ListItem>
            <asp:ListItem Value="haxe">haXe</asp:ListItem>
            <asp:ListItem Value="hjson">Hjson</asp:ListItem>
            <asp:ListItem Value="html_elixir">HTML (Elixir)</asp:ListItem>
            <asp:ListItem Value="html_ruby">HTML (Ruby)</asp:ListItem>
            <asp:ListItem Value="ini">INI</asp:ListItem>
            <asp:ListItem Value="io">Io</asp:ListItem>
            <asp:ListItem Value="jack">Jack</asp:ListItem>
            <asp:ListItem Value="jade">Jade</asp:ListItem>
            <asp:ListItem Value="java">Java</asp:ListItem>
            <asp:ListItem Value="json">JSON</asp:ListItem>
            <asp:ListItem Value="jsoniq">JSONiq</asp:ListItem>
            <asp:ListItem Value="jsp">JSP</asp:ListItem>
            <asp:ListItem Value="jssm">JSSM</asp:ListItem>
            <asp:ListItem Value="jsx">JSX</asp:ListItem>
            <asp:ListItem Value="julia">Julia</asp:ListItem>
            <asp:ListItem Value="kotlin">Kotlin</asp:ListItem>
            <asp:ListItem Value="latex">LaTeX</asp:ListItem>
            <asp:ListItem Value="less">LESS</asp:ListItem>
            <asp:ListItem Value="liquid">Liquid</asp:ListItem>
            <asp:ListItem Value="lisp">Lisp</asp:ListItem>
            <asp:ListItem Value="livescript">LiveScript</asp:ListItem>
            <asp:ListItem Value="logiql">LogiQL</asp:ListItem>
            <asp:ListItem Value="lsl">LSL</asp:ListItem>
            <asp:ListItem Value="lua">Lua</asp:ListItem>
            <asp:ListItem Value="luapage">LuaPage</asp:ListItem>
            <asp:ListItem Value="lucene">Lucene</asp:ListItem>
            <asp:ListItem Value="makefile">Makefile</asp:ListItem>
            <asp:ListItem Value="markdown">Markdown</asp:ListItem>
            <asp:ListItem Value="mask">Mask</asp:ListItem>
            <asp:ListItem Value="matlab">MATLAB</asp:ListItem>
            <asp:ListItem Value="maze">Maze</asp:ListItem>
            <asp:ListItem Value="mel">MEL</asp:ListItem>
            <asp:ListItem Value="mushcode">MUSHCode</asp:ListItem>
            <asp:ListItem Value="nix">Nix</asp:ListItem>
            <asp:ListItem Value="nsis">NSIS</asp:ListItem>
            <asp:ListItem Value="objectivec">Objective-C</asp:ListItem>
            <asp:ListItem Value="ocaml">OCaml</asp:ListItem>
            <asp:ListItem Value="pascal">Pascal</asp:ListItem>
            <asp:ListItem Value="perl">Perl</asp:ListItem>
            <asp:ListItem Value="pig">Pig</asp:ListItem>
            <asp:ListItem Value="powershell">Powershell</asp:ListItem>
            <asp:ListItem Value="praat">Praat</asp:ListItem>
            <asp:ListItem Value="prolog">Prolog</asp:ListItem>
            <asp:ListItem Value="properties">Properties</asp:ListItem>
            <asp:ListItem Value="protobuf">Protobuf</asp:ListItem>
            <asp:ListItem Value="python">Python</asp:ListItem>
            <asp:ListItem Value="r">R</asp:ListItem>
            <asp:ListItem Value="razor">Razor</asp:ListItem>
            <asp:ListItem Value="rdoc">RDoc</asp:ListItem>
            <asp:ListItem Value="red">Red</asp:ListItem>
            <asp:ListItem Value="rst">RST</asp:ListItem>
            <asp:ListItem Value="ruby">Ruby</asp:ListItem>
            <asp:ListItem Value="rust">Rust</asp:ListItem>
            <asp:ListItem Value="sass">SASS</asp:ListItem>
            <asp:ListItem Value="scad">SCAD</asp:ListItem>
            <asp:ListItem Value="scala">Scala</asp:ListItem>
            <asp:ListItem Value="scheme">Scheme</asp:ListItem>
            <asp:ListItem Value="scss">SCSS</asp:ListItem>
            <asp:ListItem Value="sh">SH</asp:ListItem>
            <asp:ListItem Value="sjs">SJS</asp:ListItem>
            <asp:ListItem Value="smarty">Smarty</asp:ListItem>
            <asp:ListItem Value="snippets">snippets</asp:ListItem>
            <asp:ListItem Value="soy_template">Soy Template</asp:ListItem>
            <asp:ListItem Value="space">Space</asp:ListItem>
            <asp:ListItem Value="stylus">Stylus</asp:ListItem>
            <asp:ListItem Value="swift">Swift</asp:ListItem>
            <asp:ListItem Value="tcl">Tcl</asp:ListItem>
            <asp:ListItem Value="tex">Tex</asp:ListItem>
            <asp:ListItem Value="textile">Textile</asp:ListItem>
            <asp:ListItem Value="toml">Toml</asp:ListItem>
            <asp:ListItem Value="tsx">TSX</asp:ListItem>
            <asp:ListItem Value="twig">Twig</asp:ListItem>
            <asp:ListItem Value="typescript">Typescript</asp:ListItem>
            <asp:ListItem Value="vala">Vala</asp:ListItem>
            <asp:ListItem Value="velocity">Velocity</asp:ListItem>
            <asp:ListItem Value="verilog">Verilog</asp:ListItem>
            <asp:ListItem Value="vhdl">VHDL</asp:ListItem>
            <asp:ListItem Value="wollok">Wollok</asp:ListItem>
            <asp:ListItem Value="xquery">XQuery</asp:ListItem>
            <asp:ListItem Value="yaml">YAML</asp:ListItem>
            <asp:ListItem Value="django">Django</asp:ListItem>
        </asp:DropDownList>

        <asp:DropDownList ID="ddlShareType" runat="server" Width="100px" BackColor="#EEE8AA" CssClass="txt">
            <asp:ListItem Value="PUBLIC">PUBLIC</asp:ListItem>
            <asp:ListItem Value="PRIVATE">PRIVATE</asp:ListItem>
            <asp:ListItem Value="EDP">EDP</asp:ListItem>
            <asp:ListItem Value="PERSON">自分</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="btnNew" runat="server" Text="NEW" />

        <hr />

        <table>
        <tr>
        
        <td style="width:200px">
            <input type="button" value="<" id="hidTV"  />
            <input type="button" value=">" id="showTV"/>
        </td>
        <td  style="width:800px; text-align:right;">
            <asp:Button ID="btnAdd" runat="server" Text="SAVE" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDel" runat="server" Text="DEL" style="height: 21px" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSel1" runat="server" Text="SEL" style="height: 21px" />&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        
        </tr>
        
        </table>



        
    </div>

    <hr />
    <table cellspacing="0" cellpadding="0" style=" height:600px;">
    <tr>
    
        <td>
            <div id="tabs_L">
	            <ul>
		            <li><a href="#tabs_L-1">First</a></li>
		            <li><a href="#tabs_L-2">Second</a></li>
	            </ul>
	            <div id="tabs_L-1" style="padding:0;">

		           <div style="vertical-align:top; margin-top:2px; 
		                height:580px; 
		                width:200px;
		                overflow:auto;
		                 background-color:#EEE8AA;border:1px solid #000;"
                         class="divL_Tv" >


                       <div style="width:100%; margin:2px;">
                            <asp:Button ID="btnExp1" runat="server" Text="Exp" Width="25%" />
                            <asp:TextBox ID="tbxEKey1" runat="server" Width="70%"></asp:TextBox>
                           
                       </div>

		                <asp:TreeView ID="tv" runat="server" Width="100" CssClass="treeview">
		                    <SelectedNodeStyle BackColor="AliceBlue"  BorderStyle="Solid" BorderWidth="1px" 
		                        Font-Bold="True" ForeColor="White" />
		                </asp:TreeView>
		            </div>
                </div>
	            <div id="tabs_L-2" style="padding:0">
		           <div style="vertical-align:top; margin-top:0; 
		                height:555px; 
		                width:200px;
		                overflow:auto;
		                 background-color:#EEE8AA;
		                 border:1px solid #000;"
                         class="divL_Tv" >
                       <div style="width:100%; margin:2px;">
                           <asp:Button ID="btnExp2" runat="server" Text="Exp" Width="25%" />
                           <asp:TextBox ID="tbxEKey2" runat="server" Width="70%"></asp:TextBox>
                           
                       </div>
		                <asp:TreeView ID="tv2" runat="server" Width="100">
		                    <SelectedNodeStyle BackColor="AliceBlue" BorderStyle="Solid" BorderWidth="1px" 
		                        Font-Bold="True" ForeColor="White" />
		                </asp:TreeView>
		            </div>
                </div>
            </div>

        </td>
        <td>
            <div id="tabs">
	            <ul>
		            <li><a href="#tabs-1">First</a></li>
		            <li><a href="#tabs-2">Second</a></li>
		            <li><a href="#tabs-3">Third</a></li>
	            </ul>
	            <div id="tabs-1">
                    <div style="border:1px solid #000;width:820px; height:555px;">
                        <uc2:WucEditor ID="WucEditor1" runat="server" 
                        width="800px"
                        height="540px"
                        />
                    </div>
                </div>
	            <div id="tabs-2">
                    <asp:Label ID="lblMsg1" runat="server" Text="" ForeColor="Blue"></asp:Label>
                    <div id="div_ms1" style="border:1px solid #000;width:820px; height:555px; overflow:auto;">
                        <asp:GridView ID="MS1" runat="server">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="Freezing"/>
                            <RowStyle Height="20px" Wrap="False" />
                        </asp:GridView>
                    </div>
                </div>
	            <div id="tabs-3">
                    <asp:Label ID="lblMsg2" runat="server" Text="" ForeColor="Blue"></asp:Label>
                    <div id="div_ms2" style="border:1px solid #000;width:820px; height:555px;">
<%--                        <asp:GridView ID="MS2" runat="server">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="Freezing"/>
                            <RowStyle Height="20px" Wrap="False" />
                        </asp:GridView>--%>
                        <asp:TextBox ID="tbxData" runat="server" Rows="100" TextMode="MultiLine" Width="800" Height="500"></asp:TextBox>
                    </div>
                </div>
            </div>
        </td>
    </tr>
    </table>




    </form>
    <script type="text/javascript">        $(document).ready(function () {

            ArrEditors[0].session.setMode('ace/mode/' + $("#ddlType").val());

            $("#ddlType").change(function () {
                ArrEditors[0].session.setMode('ace/mode/' + $(this).val());
            });

            $("#tabs_L").tabs();
            $("#tabs").tabs();
            /*
            $(".divL_Tv").mouseover(function () {
                $(this).width(400);
            });
            $(".divL_Tv").mouseenter(function () {
                $(this).width(400);
            });

            $(".divL_Tv").mouseout(function () {
                $(this).width(200);
            });
            */

            $("#hidTV").click(function () {
                $(".divL_Tv").width(200);
            });
            $("#showTV").click(function () {
                $(".divL_Tv").width(400);
            });

            

//            $("#tabs_L").tabs({
//                active: 0
//            });
            //http://api.jqueryui.com/tabs/#option-active
            //$("#tabs_L-2").active();
            //$("#tabs_L-1").hide();
            //$("#tabs_L-2").trigger("onclick");
            //$("#tabs_L-2").trigger("focus");


            /*
            NEW ボタン
            */
            $("#btnNew").click(function () {

                var ajaxRtv = false;
                var jqxhr = $.post("ZSiryouAJAX1.aspx", function () {
                //success
                })
                .success(function () { ajaxRtv = true;  })
                .error(function ()   { ajaxRtv = false; })
                .complete(function () {});

                return false;
            });


        });
    </script>
</body>
</html>