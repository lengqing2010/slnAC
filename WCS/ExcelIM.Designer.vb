<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelIM
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
        Me.tbxExcelPath = New System.Windows.Forms.TextBox()
        Me.tbxDataSource = New System.Windows.Forms.TextBox()
        Me.tbxDbName = New System.Windows.Forms.TextBox()
        Me.btnImportTable = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tbxExcelPath
        '
        Me.tbxExcelPath.Location = New System.Drawing.Point(28, 12)
        Me.tbxExcelPath.Name = "tbxExcelPath"
        Me.tbxExcelPath.Size = New System.Drawing.Size(561, 21)
        Me.tbxExcelPath.TabIndex = 0
        Me.tbxExcelPath.Text = "F:\ILIKEMAKE2017\AutoMakeCode\tabletest"
        '
        'tbxDataSource
        '
        Me.tbxDataSource.Location = New System.Drawing.Point(28, 39)
        Me.tbxDataSource.Name = "tbxDataSource"
        Me.tbxDataSource.Size = New System.Drawing.Size(561, 21)
        Me.tbxDataSource.TabIndex = 1
        Me.tbxDataSource.Text = "tbxDataSource"
        '
        'tbxDbName
        '
        Me.tbxDbName.Location = New System.Drawing.Point(28, 66)
        Me.tbxDbName.Name = "tbxDbName"
        Me.tbxDbName.Size = New System.Drawing.Size(561, 21)
        Me.tbxDbName.TabIndex = 2
        Me.tbxDbName.Text = "tbxDbName"
        '
        'btnImportTable
        '
        Me.btnImportTable.Location = New System.Drawing.Point(610, 64)
        Me.btnImportTable.Name = "btnImportTable"
        Me.btnImportTable.Size = New System.Drawing.Size(123, 23)
        Me.btnImportTable.TabIndex = 3
        Me.btnImportTable.Text = "btnImportTable"
        Me.btnImportTable.UseVisualStyleBackColor = True
        '
        'ExcelIM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 261)
        Me.Controls.Add(Me.btnImportTable)
        Me.Controls.Add(Me.tbxDbName)
        Me.Controls.Add(Me.tbxDataSource)
        Me.Controls.Add(Me.tbxExcelPath)
        Me.Name = "ExcelIM"
        Me.Text = "ExcelIM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbxExcelPath As System.Windows.Forms.TextBox
    Friend WithEvents tbxDataSource As System.Windows.Forms.TextBox
    Friend WithEvents tbxDbName As System.Windows.Forms.TextBox
    Friend WithEvents btnImportTable As System.Windows.Forms.Button
End Class
