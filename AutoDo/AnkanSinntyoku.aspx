<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnkanSinntyoku.aspx.vb" Inherits="AnkanSinntyoku" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    
    body
    {
    	font-size:12px;
    }
    
td
{  
vertical-align:middle;
text-align:center;
}
th
{
	text-align:center;
	vertical-align:middle;

 white-space: nowrap;
  text-overflow: ellipsis;
  overflow:hidden;
width:20px;
/*
vertical-align:middle;
text-align:center;
letter-spacing:20px;
word-wrap:break-word;*/
}


.ms_left td
{
    height:16px;
    width:16px;
}
.ms_right td
{
    height:16px;
    width:16px;
}
.ms_header_right td
{
    height:16px;
    width:16px; 
 white-space: nowrap;
  text-overflow: ellipsis;
  overflow:hidden;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
                <div style="width:325px">
                </div>
            </td>
            <td>
                <div style="width:3000px;">
                    <asp:GridView ID="gvRightHeader" runat="server" CssClass="ms_header_right" ShowHeader="false" AutoGenerateColumns="false" BackColor="lightgreen">
      
                    </asp:GridView>
                </div>
            </td>
        </tr>
        </table>
<div style="width:1200px; height:600px; overflow:auto;">



        <table>
        <tr>
            <td>
                <div style="width:325px">
                    <asp:GridView ID="gvSintyoku" runat="server" CssClass="ms_left" ShowHeader="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="kinou_mei" ItemStyle-Width="80" />
                            <asp:BoundField DataField="pgm_bunrui_name" ItemStyle-Width="50"/>
                            <asp:BoundField DataField="pgm_name"  ItemStyle-Width="80"/>
                            <asp:BoundField DataField="percent"  ItemStyle-Width="40"/>
                            <asp:BoundField DataField="yotei_jisseki"  ItemStyle-Width="20"/>
                            <asp:BoundField DataField="tantousya"  ItemStyle-Width="55"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div>
                    <asp:GridView ID="gvMs" runat="server" CssClass="ms_right" ShowHeader="false" AutoGenerateColumns="false">
      
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</div>
<%--        <asp:GridView ID="gvSintyoku" runat="server" ShowHeader="true">
        </asp:GridView>--%>
    </div>
    </form>
</body>
</html>
