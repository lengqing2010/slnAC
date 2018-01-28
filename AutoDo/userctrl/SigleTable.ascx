<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SigleTable.ascx.vb" Inherits="userctrl_SigleTable" %>


<div class="div_single">
<div style="background-color:blue; color:#fff; width:400px; padding-left:2px;border-radius: 2px;">
    <asp:Label ID="lblTableName" runat="server" Text='' Width="400"  Font-Bold="true" ></asp:Label>
</div>
<div class="" style="width:200px; overflow:auto; height:300px;">
    <asp:GridView ID="GvTable" runat="server" AutoGenerateColumns="False" width="400px"
                                ShowHeader="False">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    <asp:CheckBox ID="cbx" runat="server" Height="16" 
                    Text='<%#eval("item_en") & "|" & eval("item_jp") & ")"%>' 
                    ToolTip='<%#eval("item_en") & " " & eval("item_type")  & " " &  eval("item_keta")%>' />


            </ItemTemplate>
        </asp:TemplateField>                           
    </Columns>
    </asp:GridView>
</div>
</div>