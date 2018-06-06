﻿<%@ page language="VB" autoeventwireup="false" inherits="ZsqlEdit, App_Web_kb05jwdc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    
        <asp:GridView ID="gv" runat="server" AutoGenerateDeleteButton="True" 
            AutoGenerateEditButton="True" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" DataKeyNames="edp_no,file_exp" 
            DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="edp_no" HeaderText="edp_no" ReadOnly="True" 
                    SortExpression="edp_no" />
                <asp:BoundField DataField="file_exp" HeaderText="file_exp" ReadOnly="True" 
                    SortExpression="file_exp" />
                <asp:BoundField DataField="txt" HeaderText="txt" SortExpression="txt" />
                <asp:BoundField DataField="user_cd" HeaderText="user_cd" 
                    SortExpression="user_cd" />
                <asp:BoundField DataField="db_type" HeaderText="db_type" 
                    SortExpression="db_type" />
                <asp:BoundField DataField="ins_date" HeaderText="ins_date" 
                    SortExpression="ins_date" />
            </Columns>
        </asp:GridView>
        
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%  C..tostring() %>" 
            SelectCommand="SELECT *  FROM [auto_code].[dbo].[m_siryoiu]">
        </asp:SqlDataSource>
        
    
    </div>
    </form>
</body>
</html>
