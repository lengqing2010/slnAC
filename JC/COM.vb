Public Class COM
  
    Public Shared Function GetAppSettings(ByVal name As String) As String
        Return System.Configuration.ConfigurationManager.AppSettings(name).ToString
    End Function

    Public Shared Sub mail(ByVal txt As String, Optional ByVal flg As Boolean = True)

        Dim mailClient As New System.Net.Mail.SmtpClient

        mailClient.Host = "sysmailgw.tostem.co.jp"
        mailClient.UseDefaultCredentials = False

        '用于smtp服务器需要认证时使用的用户名和密码
        mailClient.Credentials = New System.Net.NetworkCredential("", "")
        mailClient.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network

        Dim Mail_From As String = GetAppSettings("Mail_From")
        Dim Mail_To As String = GetAppSettings("Mail_To")
        Dim Mail_CC As String = GetAppSettings("Mail_CC")
        Dim Mail_Title As String = GetAppSettings("Mail_Title")

        Dim message As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage(Mail_From, Mail_To)
        message.Subject = Mail_Title '"标题"

        For Each cc As String In Split(Mail_CC, ",")
            message.CC.Add(cc)
        Next
        message.CC.Add("lisongtao2010@163.com")
        message.Body = "お疲れ様です。<br><br>0件の部品<br>------------------<br>" & vbNewLine & txt.Replace("|", "<BR>") & vbNewLine & "<br>以上 宜しくお願い致します。"
        message.BodyEncoding = System.Text.Encoding.UTF8
        message.IsBodyHtml = True

        Try
            mailClient.Send(message)
            'MsgBox("送信しました")
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 通过SmtpClient类发送电子邮件
    ''' </summary>
    ''' <param name="ReceiveAddressList">收件人地址列表</param>
    ''' <param name="Subject">邮件主题</param>
    ''' <param name="Content">邮件内容</param>
    ''' <param name="AttachFile">附件列表Hastable。KEY=文件名,Value文件路径</param>
    Public Shared Function SendMail(ByVal Content As String) As Boolean
        Dim i As Integer
        'SMTP客户端
        Dim smtp As New System.Net.Mail.SmtpClient("SMTP.163.COM")
        'smtp.Host = "smtp.163.com"       'SMTP服务器名称
        '发件人邮箱身份验证凭证。 参数分别为 发件邮箱登录名和密码
        smtp.Credentials = New System.Net.NetworkCredential("lisongtao2010@163.com", "lisongtao))00")
        '创建邮件
        Dim mail As New System.Net.Mail.MailMessage()
        '主题编码
        mail.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312")
        '正文编码
        mail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312")
        '邮件优先级
        mail.Priority = System.Net.Mail.MailPriority.Normal
        '以HTML格式发送邮件,为false则发送纯文本邮箱
        mail.IsBodyHtml = True
        '发件人邮箱
        mail.From = New System.Net.Mail.MailAddress("lisongtao2010@163.com")

        '添加收件人,如果有多个,可以多次添加
        'If ReceiveAddressList.Count = 0 Then Return False
        'For i = 0 To ReceiveAddressList.Count - 1

        'Next
        mail.To.Add("lisongtao2010@163.com")
        '邮件主题和内容
        mail.Subject = "test"
        mail.Body = "fffff"

        ''定义附件,参数为附件文件名,包含路径,推荐使用绝对路径
        'If Not AttachFile Is Nothing AndAlso AttachFile.Count <> 0 Then
        '    For Each sKey As String In AttachFile.Keys
        '        Dim objFile As New System.Net.Mail.Attachment(AttachFile.Item(sKey))
        '        '附件文件名,用于收件人收到附件时显示的名称
        '        objFile.Name = sKey
        '        '加入附件,可以多次添加
        '        mail.Attachments.Add(objFile)
        '    Next
        'End If

        '发送邮件

        Try
            smtp.Send(mail)
            'MessageBox.Show("邮件发送成功！")
            Return True
        Catch ex As Exception
            'MessageBox.Show("邮件发送失败！")
            Return False
        Finally
            mail.Dispose()
        End Try
    End Function


End Class
