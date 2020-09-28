<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class billing
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
        Me.components = New System.ComponentModel.Container
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(billing))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txticode = New System.Windows.Forms.TextBox
        Me.txtiname = New System.Windows.Forms.TextBox
        Me.txtprice = New System.Windows.Forms.TextBox
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.txtamt = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button2 = New System.Windows.Forms.Button
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.DataGrid2 = New System.Windows.Forms.DataGrid
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.panelBOTTOM = New System.Windows.Forms.Panel
        Me.picBOTTOM_LEFT = New System.Windows.Forms.PictureBox
        Me.panelTOP = New System.Windows.Forms.Panel
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.picTOP_LEFT_02 = New System.Windows.Forms.PictureBox
        Me.picTOP_LEFT_01 = New System.Windows.Forms.PictureBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Bill1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BillingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.ButtonClose = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txttaxamt = New System.Windows.Forms.TextBox
        Me.txttotal = New System.Windows.Forms.TextBox
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.CrystalReport23 = New PDMS.CrystalReport2
        Me.CrystalReport22 = New PDMS.CrystalReport2
        Me.CrystalReport21 = New PDMS.CrystalReport2
        Me.Buttonexit = New System.Windows.Forms.Button
        Me.CrystalReport24 = New PDMS.CrystalReport2
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelBOTTOM.SuspendLayout()
        CType(Me.picBOTTOM_LEFT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTOP.SuspendLayout()
        CType(Me.picTOP_LEFT_02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTOP_LEFT_01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bill1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BillingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(49, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(141, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Medicine name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(266, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Price"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(450, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Quantity"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(528, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Amount"
        '
        'txticode
        '
        Me.txticode.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txticode.Location = New System.Drawing.Point(52, 112)
        Me.txticode.Name = "txticode"
        Me.txticode.Size = New System.Drawing.Size(62, 21)
        Me.txticode.TabIndex = 1
        '
        'txtiname
        '
        Me.txtiname.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtiname.Location = New System.Drawing.Point(120, 112)
        Me.txtiname.Name = "txtiname"
        Me.txtiname.Size = New System.Drawing.Size(134, 21)
        Me.txtiname.TabIndex = 2
        '
        'txtprice
        '
        Me.txtprice.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprice.Location = New System.Drawing.Point(269, 112)
        Me.txtprice.Name = "txtprice"
        Me.txtprice.Size = New System.Drawing.Size(84, 21)
        Me.txtprice.TabIndex = 3
        '
        'txtqty
        '
        Me.txtqty.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtqty.Location = New System.Drawing.Point(453, 112)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.Size = New System.Drawing.Size(69, 21)
        Me.txtqty.TabIndex = 5
        '
        'txtamt
        '
        Me.txtamt.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtamt.Location = New System.Drawing.Point(531, 112)
        Me.txtamt.Name = "txtamt"
        Me.txtamt.Size = New System.Drawing.Size(99, 21)
        Me.txtamt.TabIndex = 6
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.HideSelection = False
        Me.TextBox1.Location = New System.Drawing.Point(7, 112)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(37, 21)
        Me.TextBox1.TabIndex = 15
        Me.TextBox1.Text = "1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 14)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Si No."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(533, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 14)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(832, 47)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 14)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Bill No."
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(737, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 25)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "Label10"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(890, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 22)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "0"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(692, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 14)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Time"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(584, 46)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(102, 23)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "Label13"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(151, 570)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(0, 13)
        Me.Label14.TabIndex = 37
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(22, 516)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(95, 14)
        Me.Label15.TabIndex = 38
        Me.Label15.Text = "Total Amount"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = Global.PDMS.My.Resources.Resources.Delete_2
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(172, 594)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 45)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "&Remove Items"
        Me.ToolTip1.SetToolTip(Me.Button2, "Select one item you want to remove")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        ListViewGroup2.Header = "ListViewGroup"
        ListViewGroup2.Name = "ListViewGroup1"
        Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup2})
        Me.ListView1.Location = New System.Drawing.Point(103, 269)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowItemToolTips = True
        Me.ListView1.Size = New System.Drawing.Size(435, 136)
        Me.ListView1.TabIndex = 43
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Icode"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "si no"
        Me.ColumnHeader2.Width = 61
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ITEM NAME"
        Me.ColumnHeader3.Width = 112
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "PRICE PER UNIT"
        Me.ColumnHeader4.Width = 104
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "QUANTITY"
        Me.ColumnHeader5.Width = 75
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "AMOUNT"
        Me.ColumnHeader6.Width = 77
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
        Me.DataGrid2.Enabled = False
        Me.DataGrid2.FlatMode = True
        Me.DataGrid2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DataGrid2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.DataGrid2.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DataGrid2.HeaderForeColor = System.Drawing.Color.Lavender
        Me.DataGrid2.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid2.Location = New System.Drawing.Point(2, 139)
        Me.DataGrid2.Name = "DataGrid2"
        Me.DataGrid2.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.DataGrid2.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid2.PreferredColumnWidth = 125
        Me.DataGrid2.ReadOnly = True
        Me.DataGrid2.RowHeadersVisible = False
        Me.DataGrid2.SelectionBackColor = System.Drawing.Color.Teal
        Me.DataGrid2.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGrid2.Size = New System.Drawing.Size(628, 304)
        Me.DataGrid2.TabIndex = 0
        Me.DataGrid2.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("MS Reference Sans Serif", 48.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(147, 482)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(352, 73)
        Me.Label8.TabIndex = 109
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Image = Global.PDMS.My.Resources.Resources.Edit_3
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button7.Location = New System.Drawing.Point(668, 595)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(102, 42)
        Me.Button7.TabIndex = 11
        Me.Button7.Text = "&Notepad"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button6.Location = New System.Drawing.Point(545, 593)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(103, 43)
        Me.Button6.TabIndex = 10
        Me.Button6.Text = "&Calculator"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Image = Global.PDMS.My.Resources.Resources.Refresh
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(422, 594)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(100, 44)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "         New Bill"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'panelBOTTOM
        '
        Me.panelBOTTOM.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_09
        Me.panelBOTTOM.Controls.Add(Me.picBOTTOM_LEFT)
        Me.panelBOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBOTTOM.Location = New System.Drawing.Point(0, 649)
        Me.panelBOTTOM.Name = "panelBOTTOM"
        Me.panelBOTTOM.Size = New System.Drawing.Size(1026, 11)
        Me.panelBOTTOM.TabIndex = 40
        '
        'picBOTTOM_LEFT
        '
        Me.picBOTTOM_LEFT.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_09
        Me.picBOTTOM_LEFT.Dock = System.Windows.Forms.DockStyle.Left
        Me.picBOTTOM_LEFT.Location = New System.Drawing.Point(0, 0)
        Me.picBOTTOM_LEFT.Name = "picBOTTOM_LEFT"
        Me.picBOTTOM_LEFT.Size = New System.Drawing.Size(430, 11)
        Me.picBOTTOM_LEFT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picBOTTOM_LEFT.TabIndex = 1
        Me.picBOTTOM_LEFT.TabStop = False
        '
        'panelTOP
        '
        Me.panelTOP.BackgroundImage = Global.PDMS.My.Resources.Resources.payroll_03
        Me.panelTOP.Controls.Add(Me.Label22)
        Me.panelTOP.Controls.Add(Me.Label16)
        Me.panelTOP.Controls.Add(Me.picTOP_LEFT_02)
        Me.panelTOP.Controls.Add(Me.picTOP_LEFT_01)
        Me.panelTOP.Controls.Add(Me.Label7)
        Me.panelTOP.Controls.Add(Me.Label13)
        Me.panelTOP.Controls.Add(Me.Label12)
        Me.panelTOP.Controls.Add(Me.Label10)
        Me.panelTOP.Controls.Add(Me.Label9)
        Me.panelTOP.Controls.Add(Me.Label11)
        Me.panelTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTOP.Location = New System.Drawing.Point(0, 0)
        Me.panelTOP.Name = "panelTOP"
        Me.panelTOP.Size = New System.Drawing.Size(1026, 78)
        Me.panelTOP.TabIndex = 39
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label22.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(482, 21)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(870, 26)
        Me.Label22.TabIndex = 36
        Me.Label22.Text = "  "
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(156, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(870, 26)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "  Billing System"
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
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = Global.PDMS.My.Resources.Resources.Print
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button3.Location = New System.Drawing.Point(302, 594)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 45)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "        &Print Bill"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.PDMS.My.Resources.Resources.INFO
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(791, 594)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 44)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "&Stock Details"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Bill1BindingSource
        '
        Me.Bill1BindingSource.DataMember = "bill1"
        '
        'DataGrid1
        '
        Me.DataGrid1.AllowNavigation = False
        Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.DataGrid1.BackColor = System.Drawing.Color.White
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.LightGray
        Me.DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGrid1.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid1.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.CaptionVisible = False
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.FlatMode = True
        Me.DataGrid1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DataGrid1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.DataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DataGrid1.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid1.Location = New System.Drawing.Point(636, 85)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ParentRowsBackColor = System.Drawing.Color.White
        Me.DataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.PreferredColumnWidth = 125
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.RowHeadersVisible = False
        Me.DataGrid1.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.DataGrid1.SelectionForeColor = System.Drawing.Color.White
        Me.DataGrid1.Size = New System.Drawing.Size(390, 472)
        Me.DataGrid1.TabIndex = 0
        Me.DataGrid1.TabStop = False
        '
        'ButtonClose
        '
        Me.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ButtonClose.Image = Global.PDMS.My.Resources.Resources.Move_Previous
        Me.ButtonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonClose.Location = New System.Drawing.Point(908, 594)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(97, 45)
        Me.ButtonClose.TabIndex = 13
        Me.ButtonClose.Text = "      &Back"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(20, 560)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(186, 23)
        Me.Label17.TabIndex = 113
        Me.Label17.Text = "Up Arrow     - Print"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(19, 583)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(147, 23)
        Me.Label18.TabIndex = 114
        Me.Label18.Text = "Down Arrow  - New Bill"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(20, 606)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(146, 23)
        Me.Label19.TabIndex = 115
        Me.Label19.Text = "Right Arrow  - Remove"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(22, 629)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(135, 16)
        Me.Label20.TabIndex = 116
        Me.Label20.Text = "Left Arrow - Search"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(359, 83)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(85, 14)
        Me.Label21.TabIndex = 118
        Me.Label21.Text = "Tax Amount"
        '
        'txttaxamt
        '
        Me.txttaxamt.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttaxamt.Location = New System.Drawing.Point(363, 112)
        Me.txttaxamt.Name = "txttaxamt"
        Me.txttaxamt.Size = New System.Drawing.Size(84, 21)
        Me.txttaxamt.TabIndex = 4
        '
        'txttotal
        '
        Me.txttotal.Font = New System.Drawing.Font("Verdana", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotal.ForeColor = System.Drawing.Color.Blue
        Me.txttotal.Location = New System.Drawing.Point(487, 529)
        Me.txttotal.Name = "txttotal"
        Me.txttotal.Size = New System.Drawing.Size(99, 27)
        Me.txttotal.TabIndex = 14
        Me.txttotal.Visible = False
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = 0
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.DisplayStatusBar = False
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(636, 84)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ReportSource = Me.CrystalReport24
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(390, 473)
        Me.CrystalReportViewer1.TabIndex = 110
        '
        'Buttonexit
        '
        Me.Buttonexit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Buttonexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Buttonexit.Image = CType(resources.GetObject("Buttonexit.Image"), System.Drawing.Image)
        Me.Buttonexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Buttonexit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Buttonexit.Location = New System.Drawing.Point(908, 594)
        Me.Buttonexit.Name = "Buttonexit"
        Me.Buttonexit.Size = New System.Drawing.Size(97, 44)
        Me.Buttonexit.TabIndex = 14
        Me.Buttonexit.Text = "   &Exit"
        Me.Buttonexit.UseVisualStyleBackColor = True
        '
        'billing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(1026, 660)
        Me.Controls.Add(Me.Buttonexit)
        Me.Controls.Add(Me.txttaxamt)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.DataGrid2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.panelBOTTOM)
        Me.Controls.Add(Me.panelTOP)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.txtamt)
        Me.Controls.Add(Me.txtqty)
        Me.Controls.Add(Me.txtprice)
        Me.Controls.Add(Me.txtiname)
        Me.Controls.Add(Me.txticode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "billing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "m "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelBOTTOM.ResumeLayout(False)
        Me.panelBOTTOM.PerformLayout()
        CType(Me.picBOTTOM_LEFT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTOP.ResumeLayout(False)
        Me.panelTOP.PerformLayout()
        CType(Me.picTOP_LEFT_02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTOP_LEFT_01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bill1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BillingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txticode As System.Windows.Forms.TextBox
    Friend WithEvents txtiname As System.Windows.Forms.TextBox
    Friend WithEvents txtprice As System.Windows.Forms.TextBox
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents txtamt As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents panelTOP As System.Windows.Forms.Panel
    Private WithEvents picTOP_LEFT_02 As System.Windows.Forms.PictureBox
    Private WithEvents picTOP_LEFT_01 As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents panelBOTTOM As System.Windows.Forms.Panel
    Private WithEvents picBOTTOM_LEFT As System.Windows.Forms.PictureBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    '  Friend WithEvents StockDataSet As WindowsApplication1.stockDataSet
    Friend WithEvents BillingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents BillingTableAdapter As WindowsApplication1.stockDataSetTableAdapters.billingTableAdapter
    Friend WithEvents SinoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QtyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillnoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    ' Friend WithEvents StockDataSet As WindowsApplication1.stockDataSet
    Friend WithEvents Bill1BindingSource As System.Windows.Forms.BindingSource
    ' Friend WithEvents Bill1TableAdapter As WindowsApplication1.stockDataSetTableAdapters.bill1TableAdapter
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents DataGrid2 As System.Windows.Forms.DataGrid
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents CrystalReport21 As PDMS.CrystalReport2
    Friend WithEvents CrystalReport22 As PDMS.CrystalReport2
    Friend WithEvents CrystalReport23 As PDMS.CrystalReport2
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txttaxamt As System.Windows.Forms.TextBox
    Friend WithEvents txttotal As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Buttonexit As System.Windows.Forms.Button
    Friend WithEvents CrystalReport24 As PDMS.CrystalReport2

End Class
