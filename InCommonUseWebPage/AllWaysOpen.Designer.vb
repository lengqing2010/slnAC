<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainFraN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFraN))
        Me.TabCtrl = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabFile = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.url_tbx_url = New System.Windows.Forms.TextBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.TabCtrl.SuspendLayout()
        Me.TabMain.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabCtrl
        '
        Me.TabCtrl.Controls.Add(Me.TabPage2)
        Me.TabCtrl.Location = New System.Drawing.Point(0, 4)
        Me.TabCtrl.Name = "TabCtrl"
        Me.TabCtrl.SelectedIndex = 0
        Me.TabCtrl.Size = New System.Drawing.Size(666, 371)
        Me.TabCtrl.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(658, 345)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me.TabPage3)
        Me.TabMain.Controls.Add(Me.TabPage1)
        Me.TabMain.Location = New System.Drawing.Point(2, 24)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 0
        Me.TabMain.Size = New System.Drawing.Size(667, 502)
        Me.TabMain.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TabCtrl)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(659, 476)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "IE"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TabFile)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(659, 476)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "File"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabFile
        '
        Me.TabFile.Controls.Add(Me.TabPage4)
        Me.TabFile.Location = New System.Drawing.Point(0, 3)
        Me.TabFile.Name = "TabFile"
        Me.TabFile.SelectedIndex = 0
        Me.TabFile.Size = New System.Drawing.Size(200, 100)
        Me.TabFile.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(192, 74)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'url_tbx_url
        '
        Me.url_tbx_url.Location = New System.Drawing.Point(4, 1)
        Me.url_tbx_url.Name = "url_tbx_url"
        Me.url_tbx_url.Size = New System.Drawing.Size(876, 20)
        Me.url_tbx_url.TabIndex = 1
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(886, 1)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(33, 23)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "←"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'MainFra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1147, 613)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.url_tbx_url)
        Me.Controls.Add(Me.TabMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainFra"
        Me.Text = "All Ways Open"
        Me.TabCtrl.ResumeLayout(False)
        Me.TabMain.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabFile.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabCtrl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabFile As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents url_tbx_url As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As System.Windows.Forms.Button

End Class
