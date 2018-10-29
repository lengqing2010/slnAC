<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test.aspx.vb" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="tmp.css" rel="stylesheet" type="text/css" />
    <title></title>
<script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
<script language="javascript" type="text/javascript" src="JidouTemp.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
<table class='ms_title' style="width:2172px" cellpadding="0" cellspacing="0">
   <tr>
       <td style="width:138px;">
           登録日時
       </td>
       <td style="width:138px;">
           更新日時
       </td>
       <td style="width:90px;">
           連番
       </td>
       <td style="width:90px;">
           －ＵＲＩＫＡＫＥ－ＫＩＮＧＡＫＵ
       </td>
       <td style="width:90px;">
           金額
       </td>
       <td style="width:90px;">
           消費税
       </td>
       <td style="width:90px;">
           －ＵＴＩＫＩＮ
       </td>
       <td style="width:90px;">
           ＣＡＴ件数
       </td>
       <td style="width:74px;">
           －ＴＯＫＵＩＳＡＫＩ－ＫＢＮ
       </td>
       <td style="width:202px;">
           －ＫＯＫＹＫＵ－ＭＥＩ－ＫＡＮＡ
       </td>
       <td style="width:210px;">
           登録者
       </td>
       <td style="width:210px;">
           更新者
       </td>
       <td style="width:74px;">
           店コード
       </td>
       <td style="width:138px;">
           最終売上日
       </td>
       <td style="width:30px;">
           取消区分
       </td>
       <td style="width:74px;">
           レジNO
       </td>
       <td style="width:30px;">
           オンライン区分
       </td>
       <td style="width:30px;">
           －ＵＲＩＫＡＫＥ－ＫＢＮ
       </td>
       <td style="width:30px;">
           －ＴＯＲＩＡＴＵＫＡＩ－ＫＢＮ
       </td>
       <td style="width:42px;">
           －ＢＵＮＫＡＴＵ－ＫＡＩＳＵＵ
       </td>
       <td style="width:30px;">
           －Ｇ－ＣＡＴ－ＦＬＧ
       </td>
       <td style="">
           データ作成日
       </td>
   </tr>
   <tr>
       <td style="width:138px;">
<asp:TextBox ID="tbxtourokuDate" class="jq_touroku_date_ipt" runat="server" style="width:128px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:138px;">
<asp:TextBox ID="tbxkousinDate" class="jq_kousin_date_ipt" runat="server" style="width:128px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxrenNo" class="jq_ren_no_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxurikakeKingaku" class="jq_urikake_kingaku_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxkingaku" class="jq_kingaku_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxsyouhizei" class="jq_syouhizei_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxutikin" class="jq_utikin_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:90px;">
<asp:TextBox ID="tbxcatKensuu" class="jq_cat_kensuu_ipt" runat="server" style="width:80px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:74px;">
<asp:TextBox ID="tbxtokuisakiKbn" class="jq_tokuisaki_kbn_ipt" runat="server" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:202px;">
<asp:TextBox ID="tbxkokykuMeiKana" class="jq_kokyku_mei_kana_ipt" runat="server" style="width:192px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:210px;">
<asp:TextBox ID="tbxtourokuUser" class="jq_touroku_user_ipt" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:210px;">
<asp:TextBox ID="tbxkousinUser" class="jq_kousin_user_ipt" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:74px;">
<asp:TextBox ID="tbxtenpoCd" class="jq_tenpo_cd_ipt" runat="server" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:138px;">
<asp:TextBox ID="tbxsaisyuuUriageDate" class="jq_saisyuu_uriage_date_ipt" runat="server" style="width:128px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxtorikesiKbn" class="jq_torikesi_kbn_ipt" runat="server" style="width:20px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:74px;">
<asp:TextBox ID="tbxregiNo" class="jq_regi_no_ipt" runat="server" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxonlineKbn" class="jq_online_kbn_ipt" runat="server" style="width:20px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxurikakeKbn" class="jq_urikake_kbn_ipt" runat="server" style="width:20px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxtoriatukaiKbn" class="jq_toriatukai_kbn_ipt" runat="server" style="width:20px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:42px;">
<asp:TextBox ID="tbxbunkatuKaisuu" class="jq_bunkatu_kaisuu_ipt" runat="server" style="width:32px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxgCatFlg" class="jq_g_cat_flg_ipt" runat="server" style="width:20px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="">
<asp:TextBox ID="tbxdataSakuseiDate" class="jq_data_sakusei_date_ipt" runat="server" style="width:128px;background-color: #FFAA00;"></asp:TextBox>
       </td>
   </tr>
</table>
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:auto ; height:294px;margin-left:0px; width:2192px; margin-top :0px; border-collapse :collapse ;">
   <asp:GridView CssClass ="jq_ms" Width="2172px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" CellPadding="0" CellSpacing ="0" style=" margin-top :-1px; " TabIndex="-1" >
<Columns>
<asp:TemplateField><ItemTemplate ><%#Eval("touroku_date")%></ItemTemplate><ItemStyle Height="20px" Width="138px" HorizontalAlign="Left" CssClass="jq_touroku_date" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("kousin_date")%></ItemTemplate><ItemStyle Height="20px" Width="138px" HorizontalAlign="Left" CssClass="jq_kousin_date" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("ren_no")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_ren_no" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("urikake_kingaku")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_urikake_kingaku" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("kingaku")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_kingaku" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("syouhizei")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_syouhizei" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("utikin")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_utikin" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("cat_kensuu")%></ItemTemplate><ItemStyle Height="20px" Width="90px" HorizontalAlign="Left" CssClass="jq_cat_kensuu" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("tokuisaki_kbn")%></ItemTemplate><ItemStyle Height="20px" Width="74px" HorizontalAlign="Left" CssClass="jq_tokuisaki_kbn" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("kokyku_mei_kana")%></ItemTemplate><ItemStyle Height="20px" Width="202px" HorizontalAlign="Left" CssClass="jq_kokyku_mei_kana" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("touroku_user")%></ItemTemplate><ItemStyle Height="20px" Width="210px" HorizontalAlign="Left" CssClass="jq_touroku_user" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("kousin_user")%></ItemTemplate><ItemStyle Height="20px" Width="210px" HorizontalAlign="Left" CssClass="jq_kousin_user" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("tenpo_cd")%></ItemTemplate><ItemStyle Height="20px" Width="74px" HorizontalAlign="Left" CssClass="jq_tenpo_cd" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("saisyuu_uriage_date")%></ItemTemplate><ItemStyle Height="20px" Width="138px" HorizontalAlign="Left" CssClass="jq_saisyuu_uriage_date" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("torikesi_kbn")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_torikesi_kbn" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("regi_no")%></ItemTemplate><ItemStyle Height="20px" Width="74px" HorizontalAlign="Left" CssClass="jq_regi_no" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("online_kbn")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_online_kbn" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("urikake_kbn")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_urikake_kbn" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("toriatukai_kbn")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_toriatukai_kbn" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("bunkatu_kaisuu")%></ItemTemplate><ItemStyle Height="20px" Width="42px" HorizontalAlign="Left" CssClass="jq_bunkatu_kaisuu" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("g_cat_flg")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_g_cat_flg" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("data_sakusei_date")%></ItemTemplate><ItemStyle Height="20px"  HorizontalAlign="Left" CssClass="jq_data_sakusei_date" /></asp:TemplateField>
</Columns>
   </asp:GridView>
</div>
<asp:TextBox ID="hidtourokuDate" runat="server" class="jq_touroku_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidkousinDate" runat="server" class="jq_kousin_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidrenNo" runat="server" class="jq_ren_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidurikakeKingaku" runat="server" class="jq_urikake_kingaku_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidkingaku" runat="server" class="jq_kingaku_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidsyouhizei" runat="server" class="jq_syouhizei_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidutikin" runat="server" class="jq_utikin_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidcatKensuu" runat="server" class="jq_cat_kensuu_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidtokuisakiKbn" runat="server" class="jq_tokuisaki_kbn_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidkokykuMeiKana" runat="server" class="jq_kokyku_mei_kana_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidtourokuUser" runat="server" class="jq_touroku_user_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidkousinUser" runat="server" class="jq_kousin_user_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidtenpoCd" runat="server" class="jq_tenpo_cd_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidsaisyuuUriageDate" runat="server" class="jq_saisyuu_uriage_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidtorikesiKbn" runat="server" class="jq_torikesi_kbn_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidregiNo" runat="server" class="jq_regi_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidonlineKbn" runat="server" class="jq_online_kbn_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidurikakeKbn" runat="server" class="jq_urikake_kbn_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidtoriatukaiKbn" runat="server" class="jq_toriatukai_kbn_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidbunkatuKaisuu" runat="server" class="jq_bunkatu_kaisuu_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidgCatFlg" runat="server" class="jq_g_cat_flg_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hiddataSakuseiDate" runat="server" class="jq_data_sakusei_date_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="jq_del" />
    </div>
    </form>
</body>
</html>
