<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2DragControl1 = New Guna.UI2.WinForms.Guna2DragControl(Me.components)
        Me.Guna2GradientPanel1 = New Guna.UI2.WinForms.Guna2GradientPanel()
        Me.Guna2ControlBox1 = New Guna.UI2.WinForms.Guna2ControlBox()
        Me.StartButton = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.YoutubeButton = New Guna.UI2.WinForms.Guna2ImageButton()
        Me.TelegramButton = New Guna.UI2.WinForms.Guna2ImageButton()
        Me.DiscordButton = New Guna.UI2.WinForms.Guna2ImageButton()
        Me.BloggerButton = New Guna.UI2.WinForms.Guna2ImageButton()
        Me.KeyResoutTextbox = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2GradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.TargetControl = Me
        '
        'Guna2DragControl1
        '
        Me.Guna2DragControl1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2DragControl1.TargetControl = Me.Guna2GradientPanel1
        Me.Guna2DragControl1.UseTransparentDrag = True
        '
        'Guna2GradientPanel1
        '
        Me.Guna2GradientPanel1.Controls.Add(Me.Guna2ControlBox1)
        Me.Guna2GradientPanel1.Controls.Add(Me.StartButton)
        Me.Guna2GradientPanel1.Controls.Add(Me.Label2)
        Me.Guna2GradientPanel1.Controls.Add(Me.YoutubeButton)
        Me.Guna2GradientPanel1.Controls.Add(Me.TelegramButton)
        Me.Guna2GradientPanel1.Controls.Add(Me.DiscordButton)
        Me.Guna2GradientPanel1.Controls.Add(Me.BloggerButton)
        Me.Guna2GradientPanel1.Controls.Add(Me.KeyResoutTextbox)
        Me.Guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.Guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Guna2GradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2GradientPanel1.Name = "Guna2GradientPanel1"
        Me.Guna2GradientPanel1.Size = New System.Drawing.Size(428, 147)
        Me.Guna2GradientPanel1.TabIndex = 0
        '
        'Guna2ControlBox1
        '
        Me.Guna2ControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2ControlBox1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Guna2ControlBox1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2ControlBox1.IconColor = System.Drawing.Color.White
        Me.Guna2ControlBox1.Location = New System.Drawing.Point(391, 3)
        Me.Guna2ControlBox1.Name = "Guna2ControlBox1"
        Me.Guna2ControlBox1.Size = New System.Drawing.Size(34, 18)
        Me.Guna2ControlBox1.TabIndex = 23
        '
        'StartButton
        '
        Me.StartButton.Animated = True
        Me.StartButton.BackColor = System.Drawing.Color.Transparent
        Me.StartButton.BorderRadius = 5
        Me.StartButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.StartButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.StartButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.StartButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.StartButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.StartButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.StartButton.FillColor = System.Drawing.Color.FromArgb(CType(CType(78, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.StartButton.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.StartButton.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.StartButton.ForeColor = System.Drawing.Color.DarkGray
        Me.StartButton.Location = New System.Drawing.Point(110, 112)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(212, 25)
        Me.StartButton.TabIndex = 22
        Me.StartButton.Text = "Login"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.DarkGray
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "SMTP Checker Login"
        '
        'YoutubeButton
        '
        Me.YoutubeButton.BackColor = System.Drawing.Color.Transparent
        Me.YoutubeButton.CheckedState.ImageSize = New System.Drawing.Size(64, 64)
        Me.YoutubeButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.YoutubeButton.HoverState.ImageSize = New System.Drawing.Size(14, 14)
        Me.YoutubeButton.Image = Global.SMTP_Checker.My.Resources.Resources.icons8_youtube_15
        Me.YoutubeButton.ImageOffset = New System.Drawing.Point(0, 0)
        Me.YoutubeButton.ImageRotate = 0!
        Me.YoutubeButton.ImageSize = New System.Drawing.Size(15, 15)
        Me.YoutubeButton.Location = New System.Drawing.Point(184, 6)
        Me.YoutubeButton.Name = "YoutubeButton"
        Me.YoutubeButton.PressedState.ImageSize = New System.Drawing.Size(16, 16)
        Me.YoutubeButton.Size = New System.Drawing.Size(15, 15)
        Me.YoutubeButton.TabIndex = 19
        '
        'TelegramButton
        '
        Me.TelegramButton.BackColor = System.Drawing.Color.Transparent
        Me.TelegramButton.CheckedState.ImageSize = New System.Drawing.Size(64, 64)
        Me.TelegramButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TelegramButton.HoverState.ImageSize = New System.Drawing.Size(14, 14)
        Me.TelegramButton.Image = Global.SMTP_Checker.My.Resources.Resources.icons8_telegram_15
        Me.TelegramButton.ImageOffset = New System.Drawing.Point(0, 0)
        Me.TelegramButton.ImageRotate = 0!
        Me.TelegramButton.ImageSize = New System.Drawing.Size(15, 15)
        Me.TelegramButton.Location = New System.Drawing.Point(163, 6)
        Me.TelegramButton.Name = "TelegramButton"
        Me.TelegramButton.PressedState.ImageSize = New System.Drawing.Size(16, 16)
        Me.TelegramButton.Size = New System.Drawing.Size(15, 15)
        Me.TelegramButton.TabIndex = 18
        '
        'DiscordButton
        '
        Me.DiscordButton.BackColor = System.Drawing.Color.Transparent
        Me.DiscordButton.CheckedState.ImageSize = New System.Drawing.Size(64, 64)
        Me.DiscordButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DiscordButton.HoverState.ImageSize = New System.Drawing.Size(14, 14)
        Me.DiscordButton.Image = Global.SMTP_Checker.My.Resources.Resources.icons8_discord_15
        Me.DiscordButton.ImageOffset = New System.Drawing.Point(0, 0)
        Me.DiscordButton.ImageRotate = 0!
        Me.DiscordButton.ImageSize = New System.Drawing.Size(15, 15)
        Me.DiscordButton.Location = New System.Drawing.Point(142, 6)
        Me.DiscordButton.Name = "DiscordButton"
        Me.DiscordButton.PressedState.ImageSize = New System.Drawing.Size(16, 16)
        Me.DiscordButton.Size = New System.Drawing.Size(15, 15)
        Me.DiscordButton.TabIndex = 17
        '
        'BloggerButton
        '
        Me.BloggerButton.BackColor = System.Drawing.Color.Transparent
        Me.BloggerButton.CheckedState.ImageSize = New System.Drawing.Size(64, 64)
        Me.BloggerButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BloggerButton.HoverState.ImageSize = New System.Drawing.Size(14, 14)
        Me.BloggerButton.Image = Global.SMTP_Checker.My.Resources.Resources.icons8_blogger_15
        Me.BloggerButton.ImageOffset = New System.Drawing.Point(0, 0)
        Me.BloggerButton.ImageRotate = 0!
        Me.BloggerButton.ImageSize = New System.Drawing.Size(15, 15)
        Me.BloggerButton.Location = New System.Drawing.Point(121, 6)
        Me.BloggerButton.Name = "BloggerButton"
        Me.BloggerButton.PressedState.ImageSize = New System.Drawing.Size(16, 16)
        Me.BloggerButton.Size = New System.Drawing.Size(15, 15)
        Me.BloggerButton.TabIndex = 16
        '
        'KeyResoutTextbox
        '
        Me.KeyResoutTextbox.Animated = True
        Me.KeyResoutTextbox.BackColor = System.Drawing.Color.Transparent
        Me.KeyResoutTextbox.BorderColor = System.Drawing.Color.FromArgb(CType(CType(147, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KeyResoutTextbox.BorderRadius = 6
        Me.KeyResoutTextbox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.KeyResoutTextbox.DefaultText = ""
        Me.KeyResoutTextbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.KeyResoutTextbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.KeyResoutTextbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.KeyResoutTextbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.KeyResoutTextbox.FillColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.KeyResoutTextbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(183, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KeyResoutTextbox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.KeyResoutTextbox.ForeColor = System.Drawing.Color.DarkGray
        Me.KeyResoutTextbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(183, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KeyResoutTextbox.Location = New System.Drawing.Point(12, 78)
        Me.KeyResoutTextbox.Name = "KeyResoutTextbox"
        Me.KeyResoutTextbox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.KeyResoutTextbox.PlaceholderForeColor = System.Drawing.Color.Gray
        Me.KeyResoutTextbox.PlaceholderText = "Your Key"
        Me.KeyResoutTextbox.SelectedText = ""
        Me.KeyResoutTextbox.Size = New System.Drawing.Size(404, 28)
        Me.KeyResoutTextbox.TabIndex = 4
        Me.KeyResoutTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 147)
        Me.Controls.Add(Me.Guna2GradientPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.Guna2GradientPanel1.ResumeLayout(False)
        Me.Guna2GradientPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2GradientPanel1 As Guna.UI2.WinForms.Guna2GradientPanel
    Friend WithEvents Guna2DragControl1 As Guna.UI2.WinForms.Guna2DragControl
    Friend WithEvents KeyResoutTextbox As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents YoutubeButton As Guna.UI2.WinForms.Guna2ImageButton
    Friend WithEvents TelegramButton As Guna.UI2.WinForms.Guna2ImageButton
    Friend WithEvents DiscordButton As Guna.UI2.WinForms.Guna2ImageButton
    Friend WithEvents BloggerButton As Guna.UI2.WinForms.Guna2ImageButton
    Friend WithEvents Label2 As Label
    Friend WithEvents StartButton As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents Guna2ControlBox1 As Guna.UI2.WinForms.Guna2ControlBox
End Class
