<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_TableEditor_m_edp_test_temp.aspx.vb" Inherits="P_TableEditor_m_edp_test_temp" %>

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
<table class='ms_title' style="width:732px" cellpadding="0" cellspacing="0">
   <tr>
       <td style="width:210px;">
           EDP NUMBER
       </td>
       <td style="width:210px;">
           EDP 名
       </td>
       <td style="width:210px;">
           EDP 説明
       </td>
       <td style="width:30px;">
           ｉｎｄｅｘ
       </td>
       <td style="width:30px;">
           ステータス       
       </td>
       <td style="">
           －ＳＴＡＴＵＳ２
       </td>
   </tr>
   <tr>
       <td style="width:210px;">
<asp:TextBox ID="tbxEdpNo" class="jq_edp_no_ipt" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
       </td>
       <td style="width:210px;">
<asp:TextBox ID="tbxEdpMei" class="jq_edp_mei_ipt" runat="server" style="width:200px;"></asp:TextBox>
       </td>
       <td style="width:210px;">
<asp:TextBox ID="tbxEdpExp" class="jq_edp_exp_ipt" runat="server" style="width:200px;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxIdx" class="jq_idx_ipt" runat="server" style="width:20px;"></asp:TextBox>
       </td>
       <td style="width:30px;">
<asp:TextBox ID="tbxStatus" class="jq_status_ipt" runat="server" style="width:20px;"></asp:TextBox>
       </td>
       <td style="">
<asp:TextBox ID="tbxStatus2" class="jq_status2_ipt" runat="server" style="width:20px;"></asp:TextBox>
       </td>
   </tr>
</table>
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:auto ; height:294px;margin-left:0px; width:752px; margin-top :0px; border-collapse :collapse ;">
   <asp:GridView CssClass ="jq_ms" Width="732px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" CellPadding="0" CellSpacing ="0" style=" margin-top :-1px; " TabIndex="-1" >
<Columns>
<asp:TemplateField><ItemTemplate ><%#Eval("edp_no")%></ItemTemplate><ItemStyle Height="20px" Width="210px" HorizontalAlign="Left" CssClass="jq_edp_no" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("edp_mei")%></ItemTemplate><ItemStyle Height="20px" Width="210px" HorizontalAlign="Left" CssClass="jq_edp_mei" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("edp_exp")%></ItemTemplate><ItemStyle Height="20px" Width="210px" HorizontalAlign="Left" CssClass="jq_edp_exp" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("idx")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_idx" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("status")%></ItemTemplate><ItemStyle Height="20px" Width="30px" HorizontalAlign="Left" CssClass="jq_status" /></asp:TemplateField>
<asp:TemplateField><ItemTemplate ><%#Eval("status2")%></ItemTemplate><ItemStyle Height="20px"  HorizontalAlign="Left" CssClass="jq_status2" /></asp:TemplateField>
</Columns>
   </asp:GridView>
</div>
<asp:TextBox ID="hidEdpNo" runat="server" class="jq_edp_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidEdpMei" runat="server" class="jq_edp_mei_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidEdpExp" runat="server" class="jq_edp_exp_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidIdx" runat="server" class="jq_idx_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidStatus" runat="server" class="jq_status_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidStatus2" runat="server" class="jq_status2_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="jq_del" />
    </div>
    </form>
</body>
</html>
