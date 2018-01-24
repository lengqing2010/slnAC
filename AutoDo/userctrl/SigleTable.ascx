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
                    <asp:CheckBox ID="cbx" runat="server" Height="16" Text='<%#eval("item_en") & "&nbsp;&nbsp&nbsp&nbsp;(" & eval("item_jp") & ")"%>' ToolTip='<%#eval("item_en")%>' />
          <%--          <asp:Label ID="LEN" CssClass="txtEN" runat="server" Text='<%#eval("item_en")%>'  ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;(
                    <asp:Label ID="LJP" CssClass="txtJP" runat="server" Text='<%# eval("item_jp")%>' ></asp:Label>
                    )--%>
            </ItemTemplate>
        </asp:TemplateField>                           
    </Columns>
    </asp:GridView>
</div>
</div>