<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucLink.ascx.vb" Inherits="userctrl_WucLink" %>
<div style=" font-size:12px; text-align:left;" class="header_link">
    <table style="width:100%">
    <tr>
    <td style="width:150px;">
        <div  class="logo">
            ATO
        </div>
    </td>

    <td>
        <asp:Panel ID="Panel1" runat="server">
            <asp:LinkButton ID="lbtn1" runat="server" CssClass="toplink" OnClientClick="return false">SQL</asp:LinkButton>
            <asp:LinkButton ID="lbtn2" runat="server" CssClass="toplink" OnClientClick="return false">漢字</asp:LinkButton>
            <asp:LinkButton ID="lbtn3" runat="server" CssClass="toplink" OnClientClick="return false">keyMs</asp:LinkButton>
            <asp:LinkButton ID="lbtn7" runat="server" CssClass="toplink" OnClientClick="return false">資料</asp:LinkButton>
            <asp:LinkButton ID="lbtn4" runat="server" CssClass="toplink" OnClientClick="return false">Fsql</asp:LinkButton>
            <asp:LinkButton ID="lbtn5" runat="server" CssClass="toplink" OnClientClick="return false">日本</asp:LinkButton>
            <asp:LinkButton ID="lbtn6" runat="server" CssClass="toplink" OnClientClick="return false">整理</asp:LinkButton>
         <%--   <asp:LinkButton ID="lbtn8" runat="server" CssClass="toplink" OnClientClick="return false">メール</asp:LinkButton>
--%>        </asp:Panel>
    </td>

    </tr>
    </table>

</div>
