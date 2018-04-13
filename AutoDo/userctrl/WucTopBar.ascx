<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucTopBar.ascx.vb" Inherits="userctrl_WucTopBar" %>
<div style="width:100%; background-color:#fff; font-family:MS UI Gothic; font-size:12px; padding:4px 0 4px 0;">
<asp:LinkButton ID="lbtnSintyoku" runat="server">進捗管理</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAnnkenKanri" runat="server">案件管理</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnToday" runat="server">今日仕事</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnKanjiToEng" runat="server">漢字⇒Eng</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAnkanSiryou" runat="server">案件資料</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAutoCode" runat="server">ソース自動作成</asp:LinkButton>&nbsp;|&nbsp;
</div>