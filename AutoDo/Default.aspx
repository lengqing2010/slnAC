<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register src="userctrl/msgui.ascx" tagname="msgui" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
    <meta name="viewport" content="width=device-width, initial-scale=1"> 
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="css/common.css">

    <link rel="stylesheet" type="text/css" href="css/login.style.css">
    <link rel="stylesheet" type="text/css" href="css/user_ctrl.msgui.style.css">

    <%--new alert--%>
    <link rel="stylesheet" type="text/css" href="css/new-alert.css">
    <script language="javascript" type="text/javascript" src="js/new-alert.js"></script>

    
</head>
<body>
<uc1:msgui ID="msgui1" runat="server" />
    <form id="form1" runat="server">

        <div style="text-align: center;"">
            <div id="content">
            <div class="login-header"  style="font: italic 3em Georgia, serif; background:#ccc; color:#fff; vertical-align:middle;">
                AUTO CODE
                &nbsp;

            </div>

                <div class="login-input-box T2">
                    USER&nbsp;:&nbsp;&nbsp;<asp:TextBox ID="tbxName" runat="server" text="lis6" ></asp:TextBox>
                 </div>
                <div class="login-input-box T2">
                    PASS&nbsp;:&nbsp;&nbsp; <asp:TextBox ID="btxPass" runat="server" TextMode="Password" text="19833130"></asp:TextBox> 
    
                </div>

            <div class="remember-box">
                <label>
                    &nbsp;
                </label>
            </div>
            <div class="login-button-box">

                <asp:Button ID="btnLogin" cssclass="loginbutton" runat="server" Text="Login"/>
  
            </div>
            <div class="logon-box">

            </div>
        </div>
        </div>

    </form>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            $("#btnLogin").click(function(){
                if ($("#tbxName").val() == '') {
                    $alert("Please input user name", $("#tbxName"));
                        return false;
                }
                if ($("#btxPass").val() == '') {
                    $alert("Please input user password", $("#btxPass"));
                    return false;
                }
            });

        });
    </script>


</body>
</html>
