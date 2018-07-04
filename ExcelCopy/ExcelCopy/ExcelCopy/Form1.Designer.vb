<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelCopy
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExcelCopy))
        Me.btnRun = New System.Windows.Forms.Button()
        Me.tbxCmd = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(763, 12)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'tbxCmd
        '
        Me.tbxCmd.Location = New System.Drawing.Point(12, 47)
        Me.tbxCmd.Name = "tbxCmd"
        Me.tbxCmd.Size = New System.Drawing.Size(826, 355)
        Me.tbxCmd.TabIndex = 1
        Me.tbxCmd.Text = resources.GetString("tbxCmd.Text")
        '
        'ExcelCopy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 437)
        Me.Controls.Add(Me.tbxCmd)
        Me.Controls.Add(Me.btnRun)
        Me.Name = "ExcelCopy"
        Me.Text = "ExcelCopy"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents tbxCmd As System.Windows.Forms.RichTextBox

End Class
