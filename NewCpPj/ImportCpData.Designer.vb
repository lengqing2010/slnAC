<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportCpData
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.WB1 = New System.Windows.Forms.WebBrowser()
        Me.WB2 = New System.Windows.Forms.WebBrowser()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.tbxUrl = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'WB1
        '
        Me.WB1.Location = New System.Drawing.Point(12, 92)
        Me.WB1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB1.Name = "WB1"
        Me.WB1.ScriptErrorsSuppressed = True
        Me.WB1.Size = New System.Drawing.Size(944, 475)
        Me.WB1.TabIndex = 0
        '
        'WB2
        '
        Me.WB2.Location = New System.Drawing.Point(978, 92)
        Me.WB2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB2.Name = "WB2"
        Me.WB2.ScriptErrorsSuppressed = True
        Me.WB2.Size = New System.Drawing.Size(818, 475)
        Me.WB2.TabIndex = 1
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(881, 50)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 23)
        Me.btnRead.TabIndex = 4
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = True
        '
        'tbxUrl
        '
        Me.tbxUrl.Location = New System.Drawing.Point(12, 51)
        Me.tbxUrl.Name = "tbxUrl"
        Me.tbxUrl.Size = New System.Drawing.Size(863, 21)
        Me.tbxUrl.TabIndex = 3
        '
        'ImportCpData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1808, 579)
        Me.Controls.Add(Me.btnRead)
        Me.Controls.Add(Me.tbxUrl)
        Me.Controls.Add(Me.WB2)
        Me.Controls.Add(Me.WB1)
        Me.Name = "ImportCpData"
        Me.Text = "ImportCpData"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WB1 As System.Windows.Forms.WebBrowser
    Friend WithEvents WB2 As System.Windows.Forms.WebBrowser
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents tbxUrl As System.Windows.Forms.TextBox
End Class
