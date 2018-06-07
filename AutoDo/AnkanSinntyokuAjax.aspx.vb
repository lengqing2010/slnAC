Imports System.Web.Services
Partial Class AnkanSinntyokuAjax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function SaveData(ByVal kinou_no As String, ByVal pgm_id As String, ByVal yotei_jisseki As String, ByVal mi_ymd As String, ByVal mx_ymd As String) As String
        Dim sb As New StringBuilder
        If yotei_jisseki = "0" Then

            With sb
                .AppendLine("UPDATE m_ankan_pgm_info")
                .AppendLine("SET")
                .AppendLine("yotei_start_date  = '" & mi_ymd & "'   ")
                .AppendLine(",yotei_end_date  = '" & mx_ymd & "'   ")
                .AppendLine("WHERE")
                .AppendLine(" kinou_no = '" & kinou_no & "'   ")
                .AppendLine("AND pgm_id = '" & pgm_id & "'   ")
            End With

        Else
            With sb
                .AppendLine("UPDATE m_ankan_pgm_info")
                .AppendLine("SET")
                .AppendLine("jisseki_start_date  = '" & mi_ymd & "'   ")
                .AppendLine(",jisseki_end_date  = '" & mx_ymd & "'   ")
                .AppendLine("WHERE")
                .AppendLine(" kinou_no = '" & kinou_no & "'   ")
                .AppendLine("AND pgm_id = '" & pgm_id & "'   ")
            End With

        End If

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)

        Return ""

    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function SavePercentData(ByVal kinou_no As String, ByVal pgm_id As String, ByVal per As String) As String
        Dim sb As New StringBuilder

        With sb
            .AppendLine("UPDATE m_ankan_pgm_info")
            .AppendLine("SET")
            .AppendLine("pgm_sinntyoku_retu  = '" & per & "'   ")
            .AppendLine("WHERE")
            .AppendLine(" kinou_no = '" & kinou_no & "'   ")
            .AppendLine("AND pgm_id = '" & pgm_id & "'   ")
        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)

        Return ""

    End Function
    'user, txt, x, y
    <System.Web.Services.WebMethod()>
    Public Shared Function FncSaveDataToday(ByVal pkey As String, ByVal user As String, ByVal txt As String, ByVal x As String, ByVal y As String) As String
        Dim sb As New StringBuilder

        With sb
            .AppendLine("DELETE FROM t_today_do")
            .AppendLine("WHERE")
            .AppendLine(" user_id = '" & user & "'   ")
            .AppendLine("AND pkey = '" & pkey & "'   ")


            .AppendLine("INSERT INTO t_today_do")
            .AppendLine("SELECT")
            .AppendLine("  '" & Now.ToString("yyyyMMddHHmmss") & "'   ")
            .AppendLine(", '" & user & "'   ")
            .AppendLine(", '" & x & "'   ")
            .AppendLine(", '" & Int(y) & "'   ")
            .AppendLine(", N'" & txt & "'   ")
            .AppendLine(", ''   ")
        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)

        Return ""

    End Function


    <System.Web.Services.WebMethod()>
    Public Shared Function FncUpdPos(ByVal pkey As String, ByVal user As String, ByVal txt As String, ByVal x As String, ByVal y As String) As String
        Dim sb As New StringBuilder

        With sb
            .AppendLine("Update t_today_do")
            .AppendLine("SET")
            .AppendLine(" x = '" & x & "'   ")
            .AppendLine(" ,y = '" & y & "'   ")

            .AppendLine("WHERE")
            .AppendLine(" user_id = '" & user & "'   ")
            .AppendLine("AND pkey = '" & pkey & "'   ")


            '.AppendLine("INSERT INTO t_today_do")
            '.AppendLine("SELECT")
            '.AppendLine("  '" & Now.ToString("yyyyMMddHHmmss") & "'   ")
            '.AppendLine(", '" & user & "'   ")
            '.AppendLine(", '" & x & "'   ")
            '.AppendLine(", '" & Int(y) & "'   ")
            '.AppendLine(", N'" & txt & "'   ")
            '.AppendLine(", ''   ")
        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)

        Return ""

    End Function


    <System.Web.Services.WebMethod()>
    Public Shared Function FncDelDataToday(ByVal pkey As String, ByVal user As String, ByVal x As String, ByVal y As String) As String
        Dim sb As New StringBuilder

        With sb
            .AppendLine("DELETE FROM t_today_do")
            .AppendLine("WHERE")
            .AppendLine(" user_id = '" & user & "'   ")
            .AppendLine("AND pkey = '" & pkey & "'   ")


        End With

        Dim DbResult As DbResult = DefaultDB.RunIt(sb.ToString)

        Return ""

    End Function
End Class
