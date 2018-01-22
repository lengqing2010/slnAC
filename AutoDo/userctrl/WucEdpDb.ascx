<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucEdpDb.ascx.vb" Inherits="userctrl_WucEdpDb" %>
<%@ Register src="WucEdpList.ascx" tagname="WucEdpList" tagprefix="uc1" %>
<%@ Register src="WucDbList.ascx" tagname="WucDbList" tagprefix="uc2" %>
<table>
    <tr>
        <td>
            
            <uc1:WucEdpList ID="WucEdpList1" runat="server" />
            
        </td>
        <td>
            
            <uc2:WucDbList ID="WucDbList1" runat="server" />
            
        </td>
    </tr>
</table>