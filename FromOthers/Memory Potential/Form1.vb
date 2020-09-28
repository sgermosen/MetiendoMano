Public Class Form1
    Inherits System.Windows.Forms.Form
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents imgHidden1 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice1 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice2 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice3 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice4 As System.Windows.Forms.PictureBox
    Friend WithEvents imgBack As System.Windows.Forms.PictureBox
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents lblGuesses As System.Windows.Forms.Label
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tmrTime As System.Windows.Forms.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblMinutes As System.Windows.Forms.Label
    Friend WithEvents lblSeconds As System.Windows.Forms.Label
    Friend WithEvents dot As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents imgChoice5 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice6 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice7 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice8 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice9 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden2 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden3 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden4 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden5 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden10 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden6 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden8 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden7 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden9 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden14 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden12 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden13 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden11 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden15 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden19 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden17 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden18 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden16 As System.Windows.Forms.PictureBox
    Friend WithEvents imgHidden20 As System.Windows.Forms.PictureBox
    Friend WithEvents imgChoice10 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdPlay As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblStat As System.Windows.Forms.Label
    Friend WithEvents lblSkipGame As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.cmdNew = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.imgHidden1 = New System.Windows.Forms.PictureBox
        Me.imgChoice1 = New System.Windows.Forms.PictureBox
        Me.imgChoice2 = New System.Windows.Forms.PictureBox
        Me.imgChoice3 = New System.Windows.Forms.PictureBox
        Me.imgChoice4 = New System.Windows.Forms.PictureBox
        Me.imgBack = New System.Windows.Forms.PictureBox
        Me.lblGuesses = New System.Windows.Forms.Label
        Me.lblScore = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.tmrTime = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblMinutes = New System.Windows.Forms.Label
        Me.dot = New System.Windows.Forms.Label
        Me.lblSeconds = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label6 = New System.Windows.Forms.Label
        Me.imgChoice5 = New System.Windows.Forms.PictureBox
        Me.imgChoice6 = New System.Windows.Forms.PictureBox
        Me.imgChoice7 = New System.Windows.Forms.PictureBox
        Me.imgChoice8 = New System.Windows.Forms.PictureBox
        Me.imgChoice9 = New System.Windows.Forms.PictureBox
        Me.imgHidden2 = New System.Windows.Forms.PictureBox
        Me.imgHidden3 = New System.Windows.Forms.PictureBox
        Me.imgHidden4 = New System.Windows.Forms.PictureBox
        Me.imgHidden5 = New System.Windows.Forms.PictureBox
        Me.imgHidden10 = New System.Windows.Forms.PictureBox
        Me.imgHidden6 = New System.Windows.Forms.PictureBox
        Me.imgHidden8 = New System.Windows.Forms.PictureBox
        Me.imgHidden7 = New System.Windows.Forms.PictureBox
        Me.imgHidden9 = New System.Windows.Forms.PictureBox
        Me.imgHidden14 = New System.Windows.Forms.PictureBox
        Me.imgHidden12 = New System.Windows.Forms.PictureBox
        Me.imgHidden13 = New System.Windows.Forms.PictureBox
        Me.imgHidden11 = New System.Windows.Forms.PictureBox
        Me.imgHidden15 = New System.Windows.Forms.PictureBox
        Me.imgChoice10 = New System.Windows.Forms.PictureBox
        Me.imgHidden19 = New System.Windows.Forms.PictureBox
        Me.imgHidden17 = New System.Windows.Forms.PictureBox
        Me.imgHidden18 = New System.Windows.Forms.PictureBox
        Me.imgHidden16 = New System.Windows.Forms.PictureBox
        Me.imgHidden20 = New System.Windows.Forms.PictureBox
        Me.cmdPause = New System.Windows.Forms.Button
        Me.cmdPlay = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblStat = New System.Windows.Forms.Label
        Me.lblSkipGame = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.DarkGreen
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(664, 584)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(152, 48)
        Me.cmdNew.TabIndex = 0
        Me.cmdNew.Text = "&Start Game"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.DarkGreen
        Me.cmdExit.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.Color.White
        Me.cmdExit.Location = New System.Drawing.Point(664, 640)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(152, 40)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "E&xit"
        '
        'imgHidden1
        '
        Me.imgHidden1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden1.Image = CType(resources.GetObject("imgHidden1.Image"), System.Drawing.Image)
        Me.imgHidden1.Location = New System.Drawing.Point(24, 16)
        Me.imgHidden1.Name = "imgHidden1"
        Me.imgHidden1.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden1.TabIndex = 2
        Me.imgHidden1.TabStop = False
        '
        'imgChoice1
        '
        Me.imgChoice1.Image = CType(resources.GetObject("imgChoice1.Image"), System.Drawing.Image)
        Me.imgChoice1.Location = New System.Drawing.Point(376, 520)
        Me.imgChoice1.Name = "imgChoice1"
        Me.imgChoice1.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice1.TabIndex = 6
        Me.imgChoice1.TabStop = False
        Me.imgChoice1.Visible = False
        '
        'imgChoice2
        '
        Me.imgChoice2.Image = CType(resources.GetObject("imgChoice2.Image"), System.Drawing.Image)
        Me.imgChoice2.Location = New System.Drawing.Point(328, 520)
        Me.imgChoice2.Name = "imgChoice2"
        Me.imgChoice2.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice2.TabIndex = 7
        Me.imgChoice2.TabStop = False
        Me.imgChoice2.Visible = False
        '
        'imgChoice3
        '
        Me.imgChoice3.Image = CType(resources.GetObject("imgChoice3.Image"), System.Drawing.Image)
        Me.imgChoice3.Location = New System.Drawing.Point(424, 520)
        Me.imgChoice3.Name = "imgChoice3"
        Me.imgChoice3.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice3.TabIndex = 7
        Me.imgChoice3.TabStop = False
        Me.imgChoice3.Visible = False
        '
        'imgChoice4
        '
        Me.imgChoice4.Image = CType(resources.GetObject("imgChoice4.Image"), System.Drawing.Image)
        Me.imgChoice4.Location = New System.Drawing.Point(280, 520)
        Me.imgChoice4.Name = "imgChoice4"
        Me.imgChoice4.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice4.TabIndex = 7
        Me.imgChoice4.TabStop = False
        Me.imgChoice4.Visible = False
        '
        'imgBack
        '
        Me.imgBack.Image = CType(resources.GetObject("imgBack.Image"), System.Drawing.Image)
        Me.imgBack.Location = New System.Drawing.Point(472, 520)
        Me.imgBack.Name = "imgBack"
        Me.imgBack.Size = New System.Drawing.Size(56, 32)
        Me.imgBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgBack.TabIndex = 7
        Me.imgBack.TabStop = False
        Me.imgBack.Visible = False
        '
        'lblGuesses
        '
        Me.lblGuesses.AutoSize = True
        Me.lblGuesses.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuesses.ForeColor = System.Drawing.Color.White
        Me.lblGuesses.Location = New System.Drawing.Point(144, 648)
        Me.lblGuesses.Name = "lblGuesses"
        Me.lblGuesses.Size = New System.Drawing.Size(18, 23)
        Me.lblGuesses.TabIndex = 8
        Me.lblGuesses.Text = "0"
        Me.lblGuesses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.ForeColor = System.Drawing.Color.White
        Me.lblScore.Location = New System.Drawing.Point(144, 616)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(18, 23)
        Me.lblScore.TabIndex = 8
        Me.lblScore.Text = "0"
        Me.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(24, 616)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 23)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Score:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(24, 648)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 23)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Mistake(s):"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Image = CType(resources.GetObject("lblTitle.Image"), System.Drawing.Image)
        Me.lblTitle.Location = New System.Drawing.Point(0, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(848, 520)
        Me.lblTitle.TabIndex = 11
        Me.lblTitle.Text = "MEMORY GAME"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'tmrTime
        '
        Me.tmrTime.Interval = 1000
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(24, 584)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 23)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Time:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMinutes
        '
        Me.lblMinutes.AutoSize = True
        Me.lblMinutes.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinutes.ForeColor = System.Drawing.Color.Red
        Me.lblMinutes.Location = New System.Drawing.Point(144, 584)
        Me.lblMinutes.Name = "lblMinutes"
        Me.lblMinutes.Size = New System.Drawing.Size(29, 23)
        Me.lblMinutes.TabIndex = 13
        Me.lblMinutes.Text = "00"
        Me.lblMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dot
        '
        Me.dot.AutoSize = True
        Me.dot.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dot.ForeColor = System.Drawing.Color.White
        Me.dot.Location = New System.Drawing.Point(176, 584)
        Me.dot.Name = "dot"
        Me.dot.Size = New System.Drawing.Size(12, 23)
        Me.dot.TabIndex = 14
        Me.dot.Text = ":"
        Me.dot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSeconds
        '
        Me.lblSeconds.AutoSize = True
        Me.lblSeconds.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeconds.ForeColor = System.Drawing.Color.Red
        Me.lblSeconds.Location = New System.Drawing.Point(192, 584)
        Me.lblSeconds.Name = "lblSeconds"
        Me.lblSeconds.Size = New System.Drawing.Size(29, 23)
        Me.lblSeconds.TabIndex = 15
        Me.lblSeconds.Text = "00"
        Me.lblSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(256, 528)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(8, 24)
        Me.ProgressBar1.TabIndex = 18
        Me.ProgressBar1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(8, 536)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 17)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Borla"
        '
        'imgChoice5
        '
        Me.imgChoice5.Image = CType(resources.GetObject("imgChoice5.Image"), System.Drawing.Image)
        Me.imgChoice5.Location = New System.Drawing.Point(624, 520)
        Me.imgChoice5.Name = "imgChoice5"
        Me.imgChoice5.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice5.TabIndex = 7
        Me.imgChoice5.TabStop = False
        Me.imgChoice5.Visible = False
        '
        'imgChoice6
        '
        Me.imgChoice6.Image = CType(resources.GetObject("imgChoice6.Image"), System.Drawing.Image)
        Me.imgChoice6.Location = New System.Drawing.Point(528, 520)
        Me.imgChoice6.Name = "imgChoice6"
        Me.imgChoice6.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice6.TabIndex = 7
        Me.imgChoice6.TabStop = False
        Me.imgChoice6.Visible = False
        '
        'imgChoice7
        '
        Me.imgChoice7.Image = CType(resources.GetObject("imgChoice7.Image"), System.Drawing.Image)
        Me.imgChoice7.Location = New System.Drawing.Point(576, 520)
        Me.imgChoice7.Name = "imgChoice7"
        Me.imgChoice7.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice7.TabIndex = 7
        Me.imgChoice7.TabStop = False
        Me.imgChoice7.Visible = False
        '
        'imgChoice8
        '
        Me.imgChoice8.Image = CType(resources.GetObject("imgChoice8.Image"), System.Drawing.Image)
        Me.imgChoice8.Location = New System.Drawing.Point(776, 520)
        Me.imgChoice8.Name = "imgChoice8"
        Me.imgChoice8.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice8.TabIndex = 7
        Me.imgChoice8.TabStop = False
        Me.imgChoice8.Visible = False
        '
        'imgChoice9
        '
        Me.imgChoice9.Image = CType(resources.GetObject("imgChoice9.Image"), System.Drawing.Image)
        Me.imgChoice9.Location = New System.Drawing.Point(728, 520)
        Me.imgChoice9.Name = "imgChoice9"
        Me.imgChoice9.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice9.TabIndex = 7
        Me.imgChoice9.TabStop = False
        Me.imgChoice9.Visible = False
        '
        'imgHidden2
        '
        Me.imgHidden2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden2.Image = CType(resources.GetObject("imgHidden2.Image"), System.Drawing.Image)
        Me.imgHidden2.Location = New System.Drawing.Point(184, 16)
        Me.imgHidden2.Name = "imgHidden2"
        Me.imgHidden2.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden2.TabIndex = 2
        Me.imgHidden2.TabStop = False
        '
        'imgHidden3
        '
        Me.imgHidden3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden3.Image = CType(resources.GetObject("imgHidden3.Image"), System.Drawing.Image)
        Me.imgHidden3.Location = New System.Drawing.Point(344, 16)
        Me.imgHidden3.Name = "imgHidden3"
        Me.imgHidden3.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden3.TabIndex = 2
        Me.imgHidden3.TabStop = False
        '
        'imgHidden4
        '
        Me.imgHidden4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden4.Image = CType(resources.GetObject("imgHidden4.Image"), System.Drawing.Image)
        Me.imgHidden4.Location = New System.Drawing.Point(504, 16)
        Me.imgHidden4.Name = "imgHidden4"
        Me.imgHidden4.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden4.TabIndex = 2
        Me.imgHidden4.TabStop = False
        '
        'imgHidden5
        '
        Me.imgHidden5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden5.Image = CType(resources.GetObject("imgHidden5.Image"), System.Drawing.Image)
        Me.imgHidden5.Location = New System.Drawing.Point(664, 16)
        Me.imgHidden5.Name = "imgHidden5"
        Me.imgHidden5.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden5.TabIndex = 2
        Me.imgHidden5.TabStop = False
        '
        'imgHidden10
        '
        Me.imgHidden10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden10.Image = CType(resources.GetObject("imgHidden10.Image"), System.Drawing.Image)
        Me.imgHidden10.Location = New System.Drawing.Point(664, 144)
        Me.imgHidden10.Name = "imgHidden10"
        Me.imgHidden10.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden10.TabIndex = 2
        Me.imgHidden10.TabStop = False
        '
        'imgHidden6
        '
        Me.imgHidden6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden6.Image = CType(resources.GetObject("imgHidden6.Image"), System.Drawing.Image)
        Me.imgHidden6.Location = New System.Drawing.Point(24, 144)
        Me.imgHidden6.Name = "imgHidden6"
        Me.imgHidden6.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden6.TabIndex = 2
        Me.imgHidden6.TabStop = False
        '
        'imgHidden8
        '
        Me.imgHidden8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden8.Image = CType(resources.GetObject("imgHidden8.Image"), System.Drawing.Image)
        Me.imgHidden8.Location = New System.Drawing.Point(344, 144)
        Me.imgHidden8.Name = "imgHidden8"
        Me.imgHidden8.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden8.TabIndex = 2
        Me.imgHidden8.TabStop = False
        '
        'imgHidden7
        '
        Me.imgHidden7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden7.Image = CType(resources.GetObject("imgHidden7.Image"), System.Drawing.Image)
        Me.imgHidden7.Location = New System.Drawing.Point(184, 144)
        Me.imgHidden7.Name = "imgHidden7"
        Me.imgHidden7.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden7.TabIndex = 2
        Me.imgHidden7.TabStop = False
        '
        'imgHidden9
        '
        Me.imgHidden9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden9.Image = CType(resources.GetObject("imgHidden9.Image"), System.Drawing.Image)
        Me.imgHidden9.Location = New System.Drawing.Point(504, 144)
        Me.imgHidden9.Name = "imgHidden9"
        Me.imgHidden9.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden9.TabIndex = 2
        Me.imgHidden9.TabStop = False
        '
        'imgHidden14
        '
        Me.imgHidden14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden14.Image = CType(resources.GetObject("imgHidden14.Image"), System.Drawing.Image)
        Me.imgHidden14.Location = New System.Drawing.Point(504, 272)
        Me.imgHidden14.Name = "imgHidden14"
        Me.imgHidden14.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden14.TabIndex = 2
        Me.imgHidden14.TabStop = False
        '
        'imgHidden12
        '
        Me.imgHidden12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden12.Image = CType(resources.GetObject("imgHidden12.Image"), System.Drawing.Image)
        Me.imgHidden12.Location = New System.Drawing.Point(184, 272)
        Me.imgHidden12.Name = "imgHidden12"
        Me.imgHidden12.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden12.TabIndex = 2
        Me.imgHidden12.TabStop = False
        '
        'imgHidden13
        '
        Me.imgHidden13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden13.Image = CType(resources.GetObject("imgHidden13.Image"), System.Drawing.Image)
        Me.imgHidden13.Location = New System.Drawing.Point(344, 272)
        Me.imgHidden13.Name = "imgHidden13"
        Me.imgHidden13.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden13.TabIndex = 2
        Me.imgHidden13.TabStop = False
        '
        'imgHidden11
        '
        Me.imgHidden11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden11.Image = CType(resources.GetObject("imgHidden11.Image"), System.Drawing.Image)
        Me.imgHidden11.Location = New System.Drawing.Point(24, 272)
        Me.imgHidden11.Name = "imgHidden11"
        Me.imgHidden11.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden11.TabIndex = 2
        Me.imgHidden11.TabStop = False
        '
        'imgHidden15
        '
        Me.imgHidden15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden15.Image = CType(resources.GetObject("imgHidden15.Image"), System.Drawing.Image)
        Me.imgHidden15.Location = New System.Drawing.Point(664, 272)
        Me.imgHidden15.Name = "imgHidden15"
        Me.imgHidden15.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden15.TabIndex = 2
        Me.imgHidden15.TabStop = False
        '
        'imgChoice10
        '
        Me.imgChoice10.Image = CType(resources.GetObject("imgChoice10.Image"), System.Drawing.Image)
        Me.imgChoice10.Location = New System.Drawing.Point(672, 520)
        Me.imgChoice10.Name = "imgChoice10"
        Me.imgChoice10.Size = New System.Drawing.Size(48, 32)
        Me.imgChoice10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgChoice10.TabIndex = 7
        Me.imgChoice10.TabStop = False
        Me.imgChoice10.Visible = False
        '
        'imgHidden19
        '
        Me.imgHidden19.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden19.Image = CType(resources.GetObject("imgHidden19.Image"), System.Drawing.Image)
        Me.imgHidden19.Location = New System.Drawing.Point(504, 400)
        Me.imgHidden19.Name = "imgHidden19"
        Me.imgHidden19.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden19.TabIndex = 2
        Me.imgHidden19.TabStop = False
        '
        'imgHidden17
        '
        Me.imgHidden17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden17.Image = CType(resources.GetObject("imgHidden17.Image"), System.Drawing.Image)
        Me.imgHidden17.Location = New System.Drawing.Point(184, 400)
        Me.imgHidden17.Name = "imgHidden17"
        Me.imgHidden17.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden17.TabIndex = 2
        Me.imgHidden17.TabStop = False
        '
        'imgHidden18
        '
        Me.imgHidden18.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden18.Image = CType(resources.GetObject("imgHidden18.Image"), System.Drawing.Image)
        Me.imgHidden18.Location = New System.Drawing.Point(344, 400)
        Me.imgHidden18.Name = "imgHidden18"
        Me.imgHidden18.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden18.TabIndex = 2
        Me.imgHidden18.TabStop = False
        '
        'imgHidden16
        '
        Me.imgHidden16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden16.Image = CType(resources.GetObject("imgHidden16.Image"), System.Drawing.Image)
        Me.imgHidden16.Location = New System.Drawing.Point(24, 400)
        Me.imgHidden16.Name = "imgHidden16"
        Me.imgHidden16.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden16.TabIndex = 2
        Me.imgHidden16.TabStop = False
        '
        'imgHidden20
        '
        Me.imgHidden20.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgHidden20.Image = CType(resources.GetObject("imgHidden20.Image"), System.Drawing.Image)
        Me.imgHidden20.Location = New System.Drawing.Point(664, 400)
        Me.imgHidden20.Name = "imgHidden20"
        Me.imgHidden20.Size = New System.Drawing.Size(152, 120)
        Me.imgHidden20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHidden20.TabIndex = 2
        Me.imgHidden20.TabStop = False
        '
        'cmdPause
        '
        Me.cmdPause.BackColor = System.Drawing.Color.DarkGreen
        Me.cmdPause.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPause.ForeColor = System.Drawing.Color.White
        Me.cmdPause.Location = New System.Drawing.Point(496, 640)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(152, 40)
        Me.cmdPause.TabIndex = 0
        Me.cmdPause.Text = "&Pause"
        Me.cmdPause.Visible = False
        '
        'cmdPlay
        '
        Me.cmdPlay.BackColor = System.Drawing.Color.DarkGreen
        Me.cmdPlay.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPlay.ForeColor = System.Drawing.Color.White
        Me.cmdPlay.Location = New System.Drawing.Point(496, 584)
        Me.cmdPlay.Name = "cmdPlay"
        Me.cmdPlay.Size = New System.Drawing.Size(152, 48)
        Me.cmdPlay.TabIndex = 0
        Me.cmdPlay.Text = "P&lay"
        Me.cmdPlay.Visible = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.ForeColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(16, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(808, 8)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.ForeColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(16, 264)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(808, 8)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Label7"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.ForeColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(16, 520)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(808, 8)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Label7"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.ForeColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(16, 392)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(808, 8)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Label7"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.ForeColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(176, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(8, 504)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Label7"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.ForeColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(336, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(8, 504)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Label7"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.ForeColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(496, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(8, 504)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "Label7"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.ForeColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(656, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 504)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Label7"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.ForeColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(16, 8)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(808, 8)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Label7"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.ForeColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(8, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(8, 520)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Label7"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.ForeColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(824, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(8, 520)
        Me.Label17.TabIndex = 20
        Me.Label17.Text = "Label7"
        '
        'lblStat
        '
        Me.lblStat.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStat.ForeColor = System.Drawing.Color.White
        Me.lblStat.Location = New System.Drawing.Point(264, 584)
        Me.lblStat.Name = "lblStat"
        Me.lblStat.Size = New System.Drawing.Size(216, 96)
        Me.lblStat.TabIndex = 21
        Me.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSkipGame
        '
        Me.lblSkipGame.Location = New System.Drawing.Point(736, 560)
        Me.lblSkipGame.Name = "lblSkipGame"
        Me.lblSkipGame.Size = New System.Drawing.Size(80, 16)
        Me.lblSkipGame.TabIndex = 22
        Me.lblSkipGame.TabStop = True
        Me.lblSkipGame.Text = "&Surrender"
        Me.lblSkipGame.Visible = False
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(842, 696)
        Me.Controls.Add(Me.lblSkipGame)
        Me.Controls.Add(Me.lblStat)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmdPlay)
        Me.Controls.Add(Me.cmdPause)
        Me.Controls.Add(Me.lblSeconds)
        Me.Controls.Add(Me.dot)
        Me.Controls.Add(Me.lblMinutes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblGuesses)
        Me.Controls.Add(Me.imgChoice2)
        Me.Controls.Add(Me.imgChoice1)
        Me.Controls.Add(Me.imgHidden1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdNew)
        Me.Controls.Add(Me.imgChoice3)
        Me.Controls.Add(Me.imgChoice4)
        Me.Controls.Add(Me.imgBack)
        Me.Controls.Add(Me.lblScore)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.imgChoice5)
        Me.Controls.Add(Me.imgChoice6)
        Me.Controls.Add(Me.imgChoice7)
        Me.Controls.Add(Me.imgChoice8)
        Me.Controls.Add(Me.imgChoice9)
        Me.Controls.Add(Me.imgHidden2)
        Me.Controls.Add(Me.imgHidden3)
        Me.Controls.Add(Me.imgHidden4)
        Me.Controls.Add(Me.imgHidden5)
        Me.Controls.Add(Me.imgHidden10)
        Me.Controls.Add(Me.imgHidden6)
        Me.Controls.Add(Me.imgHidden8)
        Me.Controls.Add(Me.imgHidden7)
        Me.Controls.Add(Me.imgHidden9)
        Me.Controls.Add(Me.imgHidden14)
        Me.Controls.Add(Me.imgHidden12)
        Me.Controls.Add(Me.imgHidden13)
        Me.Controls.Add(Me.imgHidden11)
        Me.Controls.Add(Me.imgHidden15)
        Me.Controls.Add(Me.imgChoice10)
        Me.Controls.Add(Me.imgHidden19)
        Me.Controls.Add(Me.imgHidden17)
        Me.Controls.Add(Me.imgHidden18)
        Me.Controls.Add(Me.imgHidden16)
        Me.Controls.Add(Me.imgHidden20)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Memory Game"
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Declarations"
    Dim Choice As Integer, Picked(2) As Integer
    Dim Behind(20) As Integer
    Dim Guesses As Integer, Remaining As Integer, score As Integer
    Dim imgChoice(9) As PictureBox, imgHidden(19) As PictureBox
#End Region
#Region "Game Events"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblTitle.BringToFront()
        'Initialize imgChoice
        imgChoice(0) = imgChoice1
        imgChoice(1) = imgChoice2
        imgChoice(2) = imgChoice3
        imgChoice(3) = imgChoice4
        imgChoice(4) = imgChoice5
        imgChoice(5) = imgChoice6
        imgChoice(6) = imgChoice7
        imgChoice(7) = imgChoice8
        imgChoice(8) = imgChoice9
        imgChoice(9) = imgChoice10

        'initialize imgHidden
        imgHidden(0) = imgHidden1
        imgHidden(1) = imgHidden2
        imgHidden(2) = imgHidden3
        imgHidden(3) = imgHidden4
        imgHidden(4) = imgHidden5
        imgHidden(5) = imgHidden6
        imgHidden(6) = imgHidden7
        imgHidden(7) = imgHidden8
        imgHidden(8) = imgHidden9
        imgHidden(9) = imgHidden10
        imgHidden(10) = imgHidden11
        imgHidden(11) = imgHidden12
        imgHidden(12) = imgHidden13
        imgHidden(13) = imgHidden14
        imgHidden(14) = imgHidden15
        imgHidden(15) = imgHidden16
        imgHidden(16) = imgHidden17
        imgHidden(17) = imgHidden18
        imgHidden(18) = imgHidden19
        imgHidden(19) = imgHidden20

    End Sub
    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        StartGame()
        cmdNew.Text = "&New Game"
        lblTitle.Visible = False
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        If cmdExit.Text = "E&xit" Then
            End
        Else
            cmdExit.Text = "E&xit"
            cmdNew.Enabled = True
            cmdNew.Text = "&New Game"
            cmdPause.Visible = False
            cmdPlay.Visible = False
        End If
        tmrTime.Enabled = False
        lblSeconds.Text = "00"
        lblMinutes.Text = "00"
    End Sub

    Private Sub imgHidden1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden1.Click
        PlayGame(0)
    End Sub

    Private Sub imgHidden2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden2.Click
        PlayGame(1)
    End Sub

    Private Sub imgHidden3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden3.Click
        PlayGame(2)
    End Sub

    Private Sub imgHidden4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden4.Click
        PlayGame(3)
    End Sub

    Private Sub imgHidden5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden5.Click
        PlayGame(4)
    End Sub

    Private Sub imgHidden6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden6.Click
        PlayGame(5)
    End Sub

    Private Sub imgHidden7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden7.Click
        PlayGame(6)
    End Sub

    Private Sub imgHidden8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden8.Click
        PlayGame(7)
    End Sub

    Private Sub imgHidden9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden9.Click
        PlayGame(8)
    End Sub

    Private Sub imgHidden10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden10.Click
        PlayGame(9)
    End Sub

    Private Sub imgHidden11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden11.Click
        PlayGame(10)
    End Sub

    Private Sub imgHidden12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden12.Click
        PlayGame(11)
    End Sub

    Private Sub imgHidden13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden13.Click
        PlayGame(12)
    End Sub

    Private Sub imgHidden14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden14.Click
        PlayGame(13)
    End Sub

    Private Sub imgHidden15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden15.Click
        PlayGame(14)
    End Sub

    Private Sub imgHidden16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden16.Click
        PlayGame(15)
    End Sub

    Private Sub imgHidden17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden17.Click
        PlayGame(16)
    End Sub

    Private Sub imgHidden18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden18.Click
        PlayGame(17)
    End Sub

    Private Sub imgHidden19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden19.Click
        PlayGame(18)
    End Sub

    Private Sub imgHidden20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHidden20.Click
        PlayGame(19)
    End Sub
    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        For x As Integer = 0 To imgHidden.Length - 1
            imgHidden(x).Enabled = False
            tmrTime.Enabled = False
        Next
        cmdPlay.Enabled = True
        cmdPause.Enabled = False
    End Sub

    Private Sub cmdPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
        For x As Integer = 0 To imgHidden.Length - 1
            imgHidden(x).Enabled = True
            tmrTime.Enabled = True
        Next
        cmdPlay.Enabled = False
        cmdPause.Enabled = True
    End Sub
    Private Sub lblSkipGame_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblSkipGame.LinkClicked
        cmdPause.Visible = False
        cmdPlay.Visible = False
        lblSkipGame.Enabled = False
        lblStat.Text = "Game Skipped"
        For index As Integer = 0 To imgHidden.Length - 1
            imgHidden(index).Image = imgChoice(Behind(index)).Image
        Next
        tmrTime.Enabled = False
        For x As Integer = 0 To imgHidden.Length - 1
            imgHidden(x).Enabled = False
        Next
        Dim ans
        ans = MsgBox("You have skipped the game, Do you want to try it again?", vbQuestion + vbYesNo, "Game Skipped")
        If ans = vbYes Then StartGame()

    End Sub


#End Region
#Region "My Events (BORLA) "
    Private Sub Shuffle(ByVal NumberOfItems As Integer, ByVal NumberList() As Integer)
        'Shuffles integers from 1 to NumberOfItems
        Dim TempValue As Integer
        Dim LoopCounter As Integer
        Dim ItemPicked As Integer
        Dim Remaining As Integer
        'Initialize array
        For LoopCounter = 1 To NumberOfItems
            NumberList(LoopCounter) = LoopCounter
        Next LoopCounter
        For Remaining = NumberOfItems To 2 Step -1
            'Pick item at random
            ItemPicked = Int(Rnd() * Remaining) + 1
            'Swap picked item with bottom item
            TempValue = NumberList(Remaining)
            NumberList(Remaining) = NumberList(ItemPicked)
            NumberList(ItemPicked) = TempValue
        Next Remaining
    End Sub

    Private Sub StartGame()
        For e As Integer = 0 To imgHidden.Length - 1
            imgHidden(e).Enabled = True
        Next
        lblStat.Text = ""
        lblSkipGame.Visible = True
        lblSkipGame.Enabled = True
        cmdPlay.Visible = True
        cmdPause.Visible = True
        cmdPlay.Enabled = False
        Dim i As Integer
        Randomize()
        Guesses = 0 : Remaining = 10
        lblGuesses.Text = "0"
        For i = 0 To 19
            'replace with default image
            imgHidden(i).Image = imgBack.Image
            imgHidden(i).Visible = True
        Next i

        Call Shuffle(20, Behind)
        For i = 1 To 20
            If Behind(i) > 10 Then
                Behind(i) = Behind(i) - 10
            End If
            'Shift value back one since picture 
            Behind(i) = Behind(i) - 1
            'Shift back one since our indices 
            Behind(i - 1) = Behind(i)
        Next i
        'Refreshes all elements 
        Choice = 1
        cmdNew.Enabled = False
        cmdExit.Text = "&Stop"
        lblScore.Text = "0"
        lblGuesses.Text = "0"
        score = 0
        Guesses = 0
        tmrTime.Enabled = True
        lblSeconds.Text = "00"
        lblMinutes.Text = "00"

    End Sub

    Private Sub PlayGame(ByVal index As Integer)
        On Error Resume Next
        'If trying to pick same box, picking already selected box
        'or trying pick when not playing, exit
        If (Choice = 2 And index = Picked(1)) Or Behind(index) = -1 Or cmdNew.Enabled Then
            Exit Sub
        End If
        'Display selected picture
        imgHidden(index).Image = imgChoice(Behind(index)).Image
        imgHidden(index).Refresh()
        If Choice = 1 Then
            Picked(1) = index
            Choice = 2
            Exit Sub
        End If
        Picked(2) = index
        'If match
        If Behind(Picked(1)) = Behind(Picked(2)) Then

            Behind(Picked(1)) = -1
            Behind(Picked(2)) = -1
            Remaining = Remaining - 1
            score = score + 1
            lblScore.Text = score
            If score = 10 Then
                Dim ans
                tmrTime.Enabled = False
                cmdPlay.Visible = False
                cmdPause.Visible = False
                lblStat.Text = "COMPLETED"
                lblSkipGame.Enabled = False
                ans = MsgBox("Congratulations!!! You have finished the game in only  " + lblMinutes.Text & " minute(s) and " & lblSeconds.Text & " second(s)" + vbCrLf + "Do you want to try another game?", vbQuestion + vbYesNo, "Done")
                If ans = vbYes Then StartGame()

            End If
        Else
            'If no match, blank picture, restore backs
            Guesses = Guesses + 1
            lblGuesses.Text = Format(Guesses, "0")
            Timer1.Enabled = True

            If Guesses >= 50 Then
                Dim anst
                tmrTime.Enabled = False
                cmdPlay.Visible = False
                cmdPause.Visible = False
                lblStat.Text = "GAME OVER"
                lblSkipGame.Enabled = False
                anst = MsgBox("GAME OVER!!, Do you want try another game", vbInformation + vbOKOnly, "Game over")
                If anst = vbOK Then StartGame()

            End If
        End If
        Choice = 1
        If Remaining = 0 Then
            If cmdExit.Text = "E&xit" Then
                End
            Else
                cmdExit.Text = "E&xit"
                cmdNew.Enabled = True
            End If
            cmdNew.Focus()
        End If
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'This timer used to delay if a number of pair are not the same
        'using progressbar
        ProgressBar1.Value += 20
        For x As Integer = 0 To imgHidden.Length - 1
            imgHidden(x).Enabled = False
        Next
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            ProgressBar1.Value = ProgressBar1.Minimum
            Timer1.Enabled = False
            imgHidden(Picked(1)).Image = imgBack.Image
            imgHidden(Picked(2)).Image = imgBack.Image
            For x As Integer = 0 To imgHidden.Length - 1
                imgHidden(x).Enabled = True
            Next
        End If
    End Sub

#End Region
#Region "Enabled"

    Private Sub tmrTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrTime.Tick
        lblSeconds.Text = "0" & Val(lblSeconds.Text) + 1
        If Val(lblSeconds.Text) > 9 Then
            lblSeconds.Text = Mid(lblSeconds.Text, 2)
        Else
            lblSeconds.Text = Mid(lblSeconds.Text, 1)
        End If
        If Val(lblSeconds.Text) = 60 Then
            lblSeconds.Text = ("00")
            lblMinutes.Text = "0" & Val(lblMinutes.Text) + 1
            If Val(lblMinutes.Text) > 9 Then
                lblMinutes.Text = Mid(lblMinutes.Text, 2)
            Else
                lblMinutes.Text = Mid(lblMinutes.Text, 1)
            End If
            If Val(lblMinutes.Text) = 60 Then
                lblMinutes.Text = "00"
            End If
        End If

        If Val(lblSeconds.Text) Mod 2 = 0 Then
            dot.ForeColor = Color.Green
        Else
            dot.ForeColor = Color.White

        End If
    End Sub

    
    Private Sub cmdNew_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.MouseEnter
        cmdNew.BackColor = Color.YellowGreen
        cmdNew.ForeColor = Color.Black
    End Sub

    Private Sub cmdNew_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.MouseLeave
        cmdNew.BackColor = Color.DarkGreen
        cmdNew.ForeColor = Color.White
    End Sub

    Private Sub cmdNew_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdNew.MouseDown
        If e.Button = MouseButtons.Left Then cmdNew.ForeColor = Color.White
    End Sub

    Private Sub cmdExit_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExit.MouseEnter
        cmdExit.BackColor = Color.YellowGreen
        cmdExit.ForeColor = Color.Black
    End Sub

    Private Sub cmdExit_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExit.MouseLeave
        cmdExit.BackColor = Color.DarkGreen
        cmdExit.ForeColor = Color.White
    End Sub

    Private Sub cmdExit_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdExit.MouseDown
        If e.Button = MouseButtons.Left Then cmdExit.ForeColor = Color.White
    End Sub
    Private Sub cmdPlay_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPlay.MouseEnter
        cmdPlay.BackColor = Color.YellowGreen
        cmdPlay.ForeColor = Color.Black
    End Sub

    Private Sub cmdPlay_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPlay.MouseLeave
        cmdPlay.BackColor = Color.DarkGreen
        cmdPlay.ForeColor = Color.White
    End Sub

    Private Sub cmdPlay_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdPlay.MouseDown
        If e.Button = MouseButtons.Left Then cmdPlay.ForeColor = Color.White
    End Sub
    Private Sub cmdPause_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPause.MouseEnter
        cmdPause.BackColor = Color.YellowGreen
        cmdPause.ForeColor = Color.Black
    End Sub

    Private Sub cmdPause_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPause.MouseLeave
        cmdPause.BackColor = Color.DarkGreen
        cmdPause.ForeColor = Color.White
    End Sub

    Private Sub cmdPause_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdPause.MouseDown
        If e.Button = MouseButtons.Left Then cmdPause.ForeColor = Color.White
    End Sub
#End Region
End Class
