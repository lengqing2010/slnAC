<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TableEdit.aspx.vb" Inherits="TableEdit" enableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <table style="width: 100%;">
            <tr>
                <td>
                    EDP情報　
                </td>
                <td>
                    <asp:Button ID="btnEdpAdd" runat="server" Text="追加" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    edp_no
                </td>
                <td>
                    <asp:TextBox ID="edp_no" runat="server" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    edp_mei
                </td>
                <td>
                    <asp:TextBox ID="edp_mei" runat="server" MaxLength="200" Width="394px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    edp_exp
                </td>
                <td>
                    <asp:TextBox ID="edp_exp" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <hr />

        
        <table style="width: 100%;">
            <tr>
                <td>
                    DB情報　
                </td>
                <td>
                    <asp:Button ID="btnm_db_infoAdd" runat="server" Text="追加" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    data_source
                </td>
                <td>
                    <asp:TextBox ID="data_source" runat="server" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_name
                </td>
                <td>
                    <asp:TextBox ID="db_name" runat="server" MaxLength="200" Width="394px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_type
                </td>
                <td>
                    <asp:TextBox ID="db_type" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_user_id
                </td>
                <td>
                    <asp:TextBox ID="db_user_id" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_password
                </td>
                <td>
                    <asp:TextBox ID="db_password" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_enlist
                </td>
                <td>
                    <asp:TextBox ID="db_enlist" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_conn
                </td>
                <td>
                    <asp:TextBox ID="db_conn" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    db_exp
                </td>
                <td>
                    <asp:TextBox ID="db_exp" runat="server" MaxLength="1000" Width="640px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
           
        </table>
        <hr />
    </div>
    </form>
</body>
</html>
