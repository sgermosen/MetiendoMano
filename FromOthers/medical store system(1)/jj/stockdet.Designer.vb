<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class stockdet
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(stockdet))
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.panelBOTTOM = New System.Windows.Forms.Panel()
        Me.picBOTTOM_LEFT = New System.Windows.Forms.PictureBox()
        Me.panelTOP = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.picTOP_LEFT_02 = New System.Windows.Forms.PictureBox()
        Me.picTOP_LEFT_01 = New System.Windows.Forms.PictureBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGrid2 = New System.Windows.Forms.DataGrid()
        Me.panelBOTTOM.SuspendLayout()
        CType(Me.picBOTTOM_LEFT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTOP.SuspendLayout()
        CType(Me.picTOP_LEFT_02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTOP_LEFT_01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(83, 160)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(116, 22)
        Me.TxtSearch.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Georgia", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label4.Location = New System.Drawing.Point(80, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 16)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "Enter Item Code"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button4
        '
        Me.Button4.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button4.Location = New System.Drawing.Point(83, 282)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(116, 43)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = " &Delete All"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'panelBOTTOM
        '
        Me.panelBOTTOM.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_09
        Me.panelBOTTOM.Controls.Add(Me.picBOTTOM_LEFT)
        Me.panelBOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBOTTOM.Location = New System.Drawing.Point(0, 642)
        Me.panelBOTTOM.Name = "panelBOTTOM"
        Me.panelBOTTOM.Size = New System.Drawing.Size(1020, 56)
        Me.panelBOTTOM.TabIndex = 106
        '
        'picBOTTOM_LEFT
        '
        Me.picBOTTOM_LEFT.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_09
        Me.picBOTTOM_LEFT.Dock = System.Windows.Forms.DockStyle.Left
        Me.picBOTTOM_LEFT.Location = New System.Drawing.Point(0, 0)
        Me.picBOTTOM_LEFT.Name = "picBOTTOM_LEFT"
        Me.picBOTTOM_LEFT.Size = New System.Drawing.Size(430, 56)
        Me.picBOTTOM_LEFT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picBOTTOM_LEFT.TabIndex = 1
        Me.picBOTTOM_LEFT.TabStop = False
        '
        'panelTOP
        '
        Me.panelTOP.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_03
        Me.panelTOP.Controls.Add(Me.Label1)
        Me.panelTOP.Controls.Add(Me.Label16)
        Me.panelTOP.Controls.Add(Me.picTOP_LEFT_02)
        Me.panelTOP.Controls.Add(Me.picTOP_LEFT_01)
        Me.panelTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTOP.Location = New System.Drawing.Point(0, 0)
        Me.panelTOP.Name = "panelTOP"
        Me.panelTOP.Size = New System.Drawing.Size(1020, 78)
        Me.panelTOP.TabIndex = 104
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(549, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(766, 26)
        Me.Label1.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(156, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(766, 26)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Stock Details"
        '
        'picTOP_LEFT_02
        '
        Me.picTOP_LEFT_02.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_03
        Me.picTOP_LEFT_02.Dock = System.Windows.Forms.DockStyle.Left
        Me.picTOP_LEFT_02.Location = New System.Drawing.Point(156, 0)
        Me.picTOP_LEFT_02.Name = "picTOP_LEFT_02"
        Me.picTOP_LEFT_02.Size = New System.Drawing.Size(320, 78)
        Me.picTOP_LEFT_02.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTOP_LEFT_02.TabIndex = 1
        Me.picTOP_LEFT_02.TabStop = False
        '
        'picTOP_LEFT_01
        '
        Me.picTOP_LEFT_01.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_01
        Me.picTOP_LEFT_01.Dock = System.Windows.Forms.DockStyle.Left
        Me.picTOP_LEFT_01.Location = New System.Drawing.Point(0, 0)
        Me.picTOP_LEFT_01.Name = "picTOP_LEFT_01"
        Me.picTOP_LEFT_01.Size = New System.Drawing.Size(156, 78)
        Me.picTOP_LEFT_01.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTOP_LEFT_01.TabIndex = 0
        Me.picTOP_LEFT_01.TabStop = False
        '
        'ButtonClose
        '
        Me.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ButtonClose.Image = CType(resources.GetObject("ButtonClose.Image"), System.Drawing.Image)
        Me.ButtonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonClose.Location = New System.Drawing.Point(81, 353)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(118, 42)
        Me.ButtonClose.TabIndex = 4
        Me.ButtonClose.Text = " &Exit"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button1.Image = Global.PDMS.My.Resources.Resources.Refresh
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(83, 216)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 43)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "&Billing"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGrid2
        '
        Me.DataGrid2.AllowNavigation = False
        Me.DataGrid2.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.DataGrid2.BackColor = System.Drawing.Color.GhostWhite
        Me.DataGrid2.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGrid2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGrid2.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.DataGrid2.CaptionForeColor = System.Drawing.Color.White
        Me.DataGrid2.CaptionVisible = False
        Me.DataGrid2.DataMember = ""
        Me.DataGrid2.FlatMode = True
        Me.DataGrid2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DataGrid2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.DataGrid2.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DataGrid2.HeaderForeColor = System.Drawing.Color.Lavender
        Me.DataGrid2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DataGrid2.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid2.Location = New System.Drawing.Point(273, 84)
        Me.DataGrid2.Name = "DataGrid2"
        Me.DataGrid2.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.DataGrid2.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.PreferredColumnWidth = 125
        Me.DataGrid2.ReadOnly = True
        Me.DataGrid2.RowHeadersVisible = False
        Me.DataGrid2.SelectionBackColor = System.Drawing.Color.Teal
        Me.DataGrid2.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGrid2.Size = New System.Drawing.Size(788, 506)
        Me.DataGrid2.TabIndex = 0
        Me.DataGrid2.TabStop = False
        '
        'stockdet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 698)
        Me.Controls.Add(Me.DataGrid2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.panelBOTTOM)
        Me.Controls.Add(Me.panelTOP)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.Button1)
        Me.Name = "stockdet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panelBOTTOM.ResumeLayout(False)
        Me.panelBOTTOM.PerformLayout()
        CType(Me.picBOTTOM_LEFT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTOP.ResumeLayout(False)
        Me.panelTOP.PerformLayout()
        CType(Me.picTOP_LEFT_02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTOP_LEFT_01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Private WithEvents panelTOP As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents picTOP_LEFT_02 As System.Windows.Forms.PictureBox
    Private WithEvents picTOP_LEFT_01 As System.Windows.Forms.PictureBox
    Private WithEvents panelBOTTOM As System.Windows.Forms.Panel
    Private WithEvents picBOTTOM_LEFT As System.Windows.Forms.PictureBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGrid2 As System.Windows.Forms.DataGrid
End Class
