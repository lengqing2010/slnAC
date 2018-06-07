<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkannForuda.aspx.vb" Inherits="AnkannForuda" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
<%@ Register src="userctrl/WucTopBar.ascx" tagname="WucTopBar" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/default.css">
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>

    <script language="javascript" type="text/javascript">
        function Tongbu(idx) {
            //alert(idx);
            document.getElementById("hidIdx").value = idx;
            document.getElementById("btnTongbu").click();
            
            
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:WucTopBar ID="WucTopBar1" runat="server" />
        <table style="width: 100%;">

            <tr>
                <td style="width:150px; vertical-align:bottom;">
                   Edp No:
                </td>
                <td style="width:150px; vertical-align:bottom;">
                      <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "800" Height="20" JqName = "test" FirstBlank = "true"  />

                </td>
                <td style="width:150px; vertical-align:bottom;">
                   
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    Client&nbsp; Path:</td>
                <td>
                    <asp:TextBox ID="tbxClientPath" runat="server" Width="800" Text="\\ot5600\案件\test_client\"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnKihonToClient" runat="server" Text="基本  ⇒クライアント(一括Copy)" Width="200" />
                </td>
            </tr>
            <tr>
                <td>
                    Server Path</td>
                <td>
                    <asp:TextBox ID="tbxClientToServer" runat="server" Width="800" Text="\\ot5600\案件\test_server\"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnClientToServer" runat="server" Text="クライアント⇒サーバ(一括Copy)" Width="200"/>
                </td>
            </tr>
            </table>
            <table style="width: 100%; height:50px">
            <tr>
                <td style="width:150px; vertical-align:bottom;">
                    <asp:Button ID="btnKihonCompClient" runat="server" Text="基本<>クライアント(一覧)" Width="200" />
                </td>
                <td style="width:150px; vertical-align:bottom;">
                    <asp:Button ID="btnClientCompServer" runat="server" Text="クライアント<>サーバ(一覧)" Width="200"/>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:DropDownList ID="ddlLevel" runat="server">
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="cbForuda" runat="server" Text="フォルダのみ" />
        
        <asp:HiddenField ID="hidIdx" runat="server" />
        <asp:Button ID="btnTongbu" runat="server" Text="同步" Width="150" style="display:none;"/>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div style="width:400px">
                        <asp:HiddenField ID="hid_from_path" runat="server" Value='<%#eval("from_path") %>' />
                        <asp:HiddenField ID="hid_to_path" runat="server" Value='<%#eval("to_path") %>' />
                        <asp:HiddenField ID="hidType" runat="server" Value='<%#eval("Type") %>' />
                        <%#eval("link_mae") %><img alt="" src='<%#eval("link_pic") %>' height="20" />
                        <%#Eval("link_1")%>
                        </div>
                    </ItemTemplate>
                    <ItemStyle  />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <div style="width:400px">
                        <%#eval("link_mae") %><img alt="" src='<%#eval("link_pic") %>' height="20" />
                        <%#Eval("link_2")%>
                        </div>
                    </ItemTemplate>
                    <ItemStyle  />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="同步">
                    <ItemTemplate>
                        <div style="width:50px">                  
                            <asp:Button ID="btnCopyIt" runat="server" Text='<%#eval("fileCompare") %>' Visible='<%#eval("copy_btn_vis") %>' OnClientClick='<%#eval("tongbuFnc")%>' />
                        </div>
                    </ItemTemplate>
                    <ItemStyle  />
                </asp:TemplateField>

            </Columns>

        </asp:GridView>
    </div>
    
    </form>
</body>
</html>
