<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserDropdownList.ascx.vb" Inherits="userctrl_UserDropdownList" %>

<asp:TextBox ID="Text" runat="server" 
cssclass="jq_dropdownlist_wuc txtbox"
AutoCompleteType="None"
autocomplete="off"
></asp:TextBox>
<div id="divList" runat="server" 
    style="
    overflow:auto; 
    display:none; float:left; position:fixed;
    z-index:99999999; 
    background-color:#fff;
    width:260px;"

    onmousedown="DropdownListClick()">
    <asp:GridView ID="List" runat="server" ShowHeader="False" AutoGenerateColumns="false" 
        CssClass="wuc_dropdownlist">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#eval("text")%>                 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<asp:HiddenField ID="hidValue" runat="server" />
<asp:Button ID="btnRun" runat="server" Text="btnRun" style="display:block; position:absolute; top:-100px;" />
<script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
