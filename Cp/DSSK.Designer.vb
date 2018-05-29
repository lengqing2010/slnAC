<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DSSK
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DSSK))
        Me.wb1 = New System.Windows.Forms.WebBrowser()
        Me.tbxUrl = New System.Windows.Forms.TextBox()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.wb2 = New System.Windows.Forms.WebBrowser()
        Me.cbPL = New System.Windows.Forms.CheckBox()
        Me.cbReadAg = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'wb1
        '
        Me.wb1.Location = New System.Drawing.Point(12, 81)
        Me.wb1.MinimumSize = New System.Drawing.Size(20, 22)
        Me.wb1.Name = "wb1"
        Me.wb1.Size = New System.Drawing.Size(924, 735)
        Me.wb1.TabIndex = 0
        '
        'tbxUrl
        '
        Me.tbxUrl.Location = New System.Drawing.Point(13, 43)
        Me.tbxUrl.Name = "tbxUrl"
        Me.tbxUrl.Size = New System.Drawing.Size(685, 20)
        Me.tbxUrl.TabIndex = 1
        Me.tbxUrl.Text = resources.GetString("tbxUrl.Text")
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(861, 43)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 25)
        Me.btnRead.TabIndex = 2
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = True
        '
        'wb2
        '
        Me.wb2.Location = New System.Drawing.Point(967, 81)
        Me.wb2.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb2.Name = "wb2"
        Me.wb2.Size = New System.Drawing.Size(575, 640)
        Me.wb2.TabIndex = 3
        '
        'cbPL
        '
        Me.cbPL.AutoSize = True
        Me.cbPL.Location = New System.Drawing.Point(93, 12)
        Me.cbPL.Name = "cbPL"
        Me.cbPL.Size = New System.Drawing.Size(74, 17)
        Me.cbPL.TabIndex = 4
        Me.cbPL.Text = "赔率读入"
        Me.cbPL.UseVisualStyleBackColor = True
        '
        'cbReadAg
        '
        Me.cbReadAg.AutoSize = True
        Me.cbReadAg.Location = New System.Drawing.Point(13, 12)
        Me.cbReadAg.Name = "cbReadAg"
        Me.cbReadAg.Size = New System.Drawing.Size(74, 17)
        Me.cbReadAg.TabIndex = 5
        Me.cbReadAg.Text = "重新读入"
        Me.cbReadAg.UseVisualStyleBackColor = True
        '
        'DSSK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1803, 829)
        Me.Controls.Add(Me.cbReadAg)
        Me.Controls.Add(Me.cbPL)
        Me.Controls.Add(Me.wb2)
        Me.Controls.Add(Me.btnRead)
        Me.Controls.Add(Me.tbxUrl)
        Me.Controls.Add(Me.wb1)
        Me.Name = "DSSK"
        Me.Text = "DSSK"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents wb1 As System.Windows.Forms.WebBrowser
    Friend WithEvents tbxUrl As System.Windows.Forms.TextBox
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents wb2 As System.Windows.Forms.WebBrowser
    Friend WithEvents cbPL As System.Windows.Forms.CheckBox
    Friend WithEvents cbReadAg As System.Windows.Forms.CheckBox
End Class
