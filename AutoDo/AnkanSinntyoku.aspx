<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkanSinntyoku.aspx.vb" Inherits="AnkanSinntyoku" %>
<%@ Register src="userctrl/UserDropdownList.ascx" tagname="UserDropdownList" tagprefix="uc1" %>
<%@ Register src="userctrl/WucTopBar.ascx" tagname="WucTopBar" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1">
    <link rel="stylesheet" type="text/css" href="css/new_common.css">
    <link rel="stylesheet" type="text/css" href="AnkanSinntyoku.css">
    <title>案件進捗</title>

    <script language="javascript" type="text/javascript" src="js/jquery-1.4.1.min.js"></script>

    <script language="javascript" type="text/javascript" src="AnkanSinntyoku.js"></script>

    <script language="javascript" type="text/javascript">


        function InitVBData(){
            <% 
            response.write(sintyoukuSb.toString())
            %>
            start_ymd_yotei='<%response.write(miKinouStartDate.toString())%>';
            end_ymd_yotei='<%response.write(mxKinouEndDate.toString())%>';
        }

        $(document).ready(function () {
        });
</script>



</head>
<body>
    <form id="form1" runat="server">
    <uc2:WucTopBar ID="WucTopBar1" runat="server" />
    <div>

    <uc1:UserDropdownList ID="ucEdpLst" runat="server" Width = "300" Height="20" JqName = "test" FirstBlank="true"  />
    <asp:Button ID="btnSintyoku" runat="server" Text="進捗詳細" CssClass="small green button" />&nbsp;
    <asp:Button ID="btnToday" runat="server" Text="今日予定" CssClass="small green button" />&nbsp;

<table>
        <tr>
            <td colspan="1">
                <div id="divTitleLeft" class="divTitleLeft">
                    <table id="tblTitleLeft" class="TitleLeft" cellpadding="0" cellspacing="0" >
                    
                        
                    
                    </table>       
                </div>
            </td>
            <td>
                <div id="divTitleRight" class="divTitleRight">
                    <table id="tblTitleRight" class="TitleRight" cellpadding="0" cellspacing="0" >
                    
                    </table>       
                </div>     
            </td>
        </tr>
    <tr>
        <td style="vertical-align:top;">
            <div id="divMsLeft" class="divMsLeft">
                <table id="tblMsLeft" class="msLeft" cellpadding="0" cellspacing="0" >
                
                </table>       
            </div>
        </td>
        <td style="vertical-align:top;">
            <div id="divMsRight" class="divMsRight">
                <table id="tblMsRight" class="msRight" cellpadding="0" cellspacing="0" >
                
                </table>       
            </div>     
        </td>
    </tr>
</table>
</div>

    </form>
</body>
</html>
