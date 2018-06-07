<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucTopBar.ascx.vb" Inherits="userctrl_WucTopBar" %>
<div style="width:100%; background-color:#fff; font-family:MS UI Gothic; font-size:12px; padding:4px 0 4px 0;">
<asp:LinkButton ID="lbtnSintyoku" runat="server"  CssClass="small red button">進捗管理</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAnnkenKanri" runat="server" CssClass="small red button">案件管理</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAnnkenForuda" runat="server" CssClass="small red button">案件フォルダ</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnToday" runat="server" CssClass="small red button">今日仕事</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnKanjiToEng" runat="server" CssClass="small red button">漢字⇒Eng</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAnkanSiryou" runat="server" CssClass="small red button">案件資料</asp:LinkButton>&nbsp;|&nbsp;
<asp:LinkButton ID="lbtnAutoCode" runat="server" CssClass="small red button">ソース自動作成</asp:LinkButton>&nbsp;|&nbsp;
</div>