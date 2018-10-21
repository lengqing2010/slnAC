Public Class Menu

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click


    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        MainFraN.Show()
        MainFraN.Location = New Point(Me.Location)
        MainFraN.WindowState = FormWindowState.Maximized
    End Sub
End Class