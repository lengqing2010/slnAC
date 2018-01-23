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
        <td>
            <div  style=" visibility:hidden; position :fixed   ; margin-top:20px;  z-index:10000; width:400px; height:400px; overflow:auto; background-color:White;">
            <asp:GridView ID="GVDB" runat="server" Width="1000px" AutoGenerateColumns="False" 
                    ShowHeader="False">
                <Columns>
                    <asp:TemplateField>
                      
                        <ItemTemplate>
                               <asp:CheckBox ID="cbx" runat="server" />
                               <asp:Label ID="lblEn" runat="server" Text='<%#eval("table_en") & eval("table_jp")%>' ToolTip='<%#eval("table_jp") %>' ></asp:Label>
                        </ItemTemplate>
                      
                    </asp:TemplateField>
                    <asp:TemplateField></asp:TemplateField>
                </Columns>
            </asp:GridView>
             </div>
        </td>

        <td>
            
                <asp:Button ID="BtnDb" runat="server" Text="DB" />
           
            
           
        </td>
    </tr>
</table>