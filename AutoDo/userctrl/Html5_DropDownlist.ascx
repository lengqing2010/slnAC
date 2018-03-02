<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Html5_DropDownlist.ascx.vb" Inherits="userctrl_Html5_DropDownlist" %>
<asp:TextBox ID="h5_DropDownlist" runat="server" placeholder="热门电影排行"  onchange="alert()"></asp:TextBox>
<datalist runat="server" id="h5_DropDownlist_Datalist"  >
<option value="BMW">abbbc</option>
<option value="Ford">dddfe</option>
<option value="Volvo">fasdf</option>
</datalist>