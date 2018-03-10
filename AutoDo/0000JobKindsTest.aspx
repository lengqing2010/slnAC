<%@ Page Language="VB" AutoEventWireup="false" CodeFile="0000JobKindsTest.aspx.vb" Inherits="userctrl_0000JobKindsTest" %>

<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>フォルダ</title>
    <link rel="stylesheet" type="text/css" href="css/common.css" />
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <asp:DropDownList ID="ddlJob" runat="server" AutoPostBack="true" Width="200">
    </asp:DropDownList>

<hr />

<div style="height:600px; overflow:auto;">
    <asp:GridView ID="gvSerPaths" runat="server" AutoGenerateColumns="False" width="800px"
                                ShowHeader="False">
    <Columns>
        <asp:BoundField DataField="ser_name" ControlStyle-Width="400" />
        <asp:TemplateField ControlStyle-Width="200">
            <ItemTemplate>
                <div style="width:200px">
                    <a href="file:<%#Eval("JobSerPath")%>"  style="padding:4px;">Server</a> &nbsp;
                    <a href="file:<%#Eval("JobClientPath")%>"  style="padding:4px; visibility:<%#Eval("visibility")%>" >Client</a>                
                </div>

            </ItemTemplate>
        </asp:TemplateField>                           
        
    </Columns>
    </asp:GridView>
</div>
    <hr />
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 800px;  background-color:#fff;" class="com_table">
            <tr>
                <th style="width:250px;">
                    User　Name
                    <br />
                    &nbsp;
                </th>
                <td >                    
                    <asp:TextBox ID="tbxUserName" runat="server" MaxLength="100" Width="200" BackColor="Pink"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Job Edp</th>
                <td>
                    <asp:TextBox ID="tbxJobEdp" runat="server" MaxLength="20" Width="200" BackColor="Pink"></asp:TextBox> 简化名
                </td>
            </tr>
            <tr>
                <th>
                    Job　Server　Path
                    <br />
                    共有フォルダServerサーバ
                </th>
                <td>
                    <asp:TextBox ID="tbxJobSerPath" runat="server" Width="500" BackColor="YELLOW"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Job　Client　Path
                    <br />
                    共有フォルダ自分パス
                </th>
                <td>
                    <asp:TextBox ID="tbxJobClientPath" runat="server" Width="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Job　Backup　Path
                    <br />
                    共有フォルダバックアップパス
                </th>
                <td>
                    <asp:TextBox ID="tbxJobBackupPath" runat="server" Width="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <div style="padding:2px;">
                        <asp:Button ID="btnSel" runat="server" Text="SELECT" />
                        <asp:Button ID="btnInsUpd" runat="server" Text="INSERT" />
                        <asp:Button ID="btnDel" runat="server" Text="DELETE" />
                    </div>
                   
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GvMs" runat="server" AutoGenerateColumns="False" width="400px"
                                ShowHeader="False">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table cellpadding="0" cellspacing="0" style="width: 800px;  background-color:#fff; border-color:#000;" class="com_table">
                    <tr>
                        <td  style="width:250px;">
                            Job Edp
                            <br />
                            &nbsp;
                            </td>
                        <td>
                            <a href="#" onclick="SetUpdInfo('<%#Eval("job_edp")%>','<%#Eval("main_job_path_server")%>','<%#Eval("main_job_path_client")%>','<%#Eval("main_job_path_backup")%>');return false;">
                            <%#SetPathValue(Eval("job_edp"))%>                            
                            </a>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Job　Server　Path
                            <br />
                            共有フォルダServerサーバ
                        </td>
                        <td>
                         
                            <%#SetPathValue(Eval("main_job_path_server"))%>   
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Job　Client　Path
                            <br />
                            共有フォルダ自分パス
                        </td>
                        <td>
                           
                              <%#SetPathValue(Eval("main_job_path_client"))%>   
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Job　Backup　Path
                            <br />
                            共有フォルダバックアップパス
                        </td>
                        <td>
                           
                              <%#SetPathValue(Eval("main_job_path_backup"))%>   
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:TemplateField>                           
    </Columns>
    </asp:GridView>


    </form>

    <script language="javascript" type="text/javascript">


        function SetUpdInfo(tbxJobEdp, tbxJobSerPath, tbxJobClientPath, tbxJobBackupPath) {

            var i;

            $("#tbxJobEdp").val(tbxJobEdp);
            $("#tbxJobSerPath").val(tbxJobSerPath.replace('|', '\\'));
            $("#tbxJobClientPath").val(tbxJobClientPath.replace('|', '\\'));
            $("#tbxJobBackupPath").val(tbxJobBackupPath.replace('|', '\\'));
            
            for (i = 0; i <= 40; i++) {
                $("#tbxJobEdp").val($("#tbxJobEdp").val().replace('|', '\\'));
                $("#tbxJobSerPath").val($("#tbxJobSerPath").val().replace('|', '\\'));
                $("#tbxJobClientPath").val($("#tbxJobClientPath").val().replace('|', '\\'));
                $("#tbxJobBackupPath").val($("#tbxJobBackupPath").val().replace('|', '\\'));
            }

        }
    
    </script>
</body>
</html>
