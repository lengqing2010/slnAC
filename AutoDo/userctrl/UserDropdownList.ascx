<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserDropdownList.ascx.vb" Inherits="userctrl_UserDropdownList" %>

<asp:TextBox ID="Text" runat="server" 
onclick="InitDropdownList(this)"
onfocus="InitDropdownList(this)"
onkeydown="UnInitDropdowlistByKey(this)"
onpropertychange="IputText(this)"
oninput="IputText(this)"
onblur="ChkInputText(this)"
style="border:1px solid #ccc; background-color:#fff;"
cssclass="jq_dropdownlist_wuc"
AutoCompleteType="None"
></asp:TextBox>
<div id="divList" runat="server" 
    style="height:300px; overflow:auto; display:none; float:left; position:fixed;"
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
<script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
<script language="javascript" type="text/javascript">

</script>