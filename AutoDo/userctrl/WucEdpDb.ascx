<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucEdpDb.ascx.vb" Inherits="userctrl_WucEdpDb" %>
<%@ Register src="WucEdpList.ascx" tagname="WucEdpList" tagprefix="uc1" %>
<%@ Register src="WucDbList.ascx" tagname="WucDbList" tagprefix="uc2" %>

<link rel="stylesheet" type="text/css" href="css/db_control_panel.css">

<table>
    <tr>
        <td>
            
            <uc1:WucEdpList ID="WucEdpList1" runat="server" />
            
        </td>
        <td>
            <asp:Button ID="btnE" runat="server" Text="Edit" />
        </td>

        <td>
            
            <uc2:WucDbList ID="WucDbList1" runat="server" />
            
        </td>
        <td>
            <div runat="server" id="divDB" visible="false"  
                 class="db_panel"
                 style="">
                
                <asp:TextBox ID="tbxKey" CssClass="tbxKey" runat="server" Width="300"></asp:TextBox>
                <asp:TextBox ID="tbxKeyJP" CssClass="tbxKey" runat="server" Width="300"></asp:TextBox>
                <%--<asp:Button ID="btnSelTable" runat="server" Text="SelTable" />--%>
                <asp:Button ID="btnSaveTable" runat="server" Text="Use Table" />
                &nbsp;&nbsp;
                <asp:Button ID="btnShow" runat="server" Text="Show" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" />
                <hr />
                <table style="width:1000px;">
                <tr>
                    <td>
                        <div class="db_ms_div" style="width:520px">
                            <asp:GridView ID="GVDB" runat="server" AutoGenerateColumns="False" width="500px"
                                    ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField>
                      
                                        <ItemTemplate>
                                               <asp:CheckBox ID="cbx" runat="server" Height="15" />
                                               <asp:Label ID="LEN" CssClass="txtEN" runat="server" Text='<%#eval("table_en")%>' ToolTip='<%#eval("table_jp") %>' ></asp:Label>
                                               &nbsp;&nbsp;&nbsp;&nbsp;(
                                               <asp:Label ID="LJP" CssClass="txtJP" runat="server" Text='<%# eval("table_jp")%>' ToolTip='<%#eval("table_jp") %>' ></asp:Label>
                                               )
                                        </ItemTemplate>
                      
                                    </asp:TemplateField>                           
                                </Columns>
                            </asp:GridView>
                            <asp:TextBox ID="TextBox1" Width="0" CssClass="endline" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td >
                       <asp:TextBox ID="tbxNR" runat="server" Width="460" Height="200" 
                       TextMode="MultiLine"
                       rows="10">
                       
                       
                       </asp:TextBox>

                        
                    </td>
                </tr>
                </table>


                <div runat="server" id="divTable" visible="false"  
                     class=""
                     style=" background-color:#fff; width:1000px; height:800px; margin-top:5px; padding:5px;">
                </div>

             </div>


        </td>

        <td>
                <asp:Button ID="BtnDb" runat="server" Text="DB" />           
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript" src="js/db_control_panel.js">


</script>