<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class adminmain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(adminmain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockPurchaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockReturnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BillToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomerMedicineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SupplierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DetailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BillTransactionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockReturnDetailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomerMedicineDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SupplierDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PurchaseReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalesReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockReturnReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomerMedicineReturnReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.MenuStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolToolStripMenuItem, Me.DetailToolStripMenuItem, Me.ReportToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1026, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdminToolStripMenuItem, Me.ExitToolStripMenuItem1})
        Me.FileToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.users2
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'AdminToolStripMenuItem
        '
        Me.AdminToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangePasswordToolStripMenuItem})
        Me.AdminToolStripMenuItem.Image = CType(resources.GetObject("AdminToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AdminToolStripMenuItem.Name = "AdminToolStripMenuItem"
        Me.AdminToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.AdminToolStripMenuItem.Text = "&Admin"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Image = CType(resources.GetObject("ChangePasswordToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ChangePasswordToolStripMenuItem.Text = "&Change Password"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Image = Global.PDMS.My.Resources.Resources.MovePrevious
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(123, 22)
        Me.ExitToolStripMenuItem1.Text = "&Exit"
        '
        'ToolToolStripMenuItem
        '
        Me.ToolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StockPurchaseToolStripMenuItem, Me.StockReturnToolStripMenuItem, Me.BillToolStripMenuItem, Me.CustomerMedicineToolStripMenuItem, Me.SupplierToolStripMenuItem})
        Me.ToolToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Reports2
        Me.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem"
        Me.ToolToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.ToolToolStripMenuItem.Text = "&Tools"
        '
        'StockPurchaseToolStripMenuItem
        '
        Me.StockPurchaseToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Edit_3
        Me.StockPurchaseToolStripMenuItem.Name = "StockPurchaseToolStripMenuItem"
        Me.StockPurchaseToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.StockPurchaseToolStripMenuItem.Text = "Stock &Purchase"
        '
        'StockReturnToolStripMenuItem
        '
        Me.StockReturnToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Refresh
        Me.StockReturnToolStripMenuItem.Name = "StockReturnToolStripMenuItem"
        Me.StockReturnToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.StockReturnToolStripMenuItem.Text = "Stock &Return"
        '
        'BillToolStripMenuItem
        '
        Me.BillToolStripMenuItem.Image = CType(resources.GetObject("BillToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BillToolStripMenuItem.Name = "BillToolStripMenuItem"
        Me.BillToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.BillToolStripMenuItem.Text = "&Bill"
        '
        'CustomerMedicineToolStripMenuItem
        '
        Me.CustomerMedicineToolStripMenuItem.Image = CType(resources.GetObject("CustomerMedicineToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CustomerMedicineToolStripMenuItem.Name = "CustomerMedicineToolStripMenuItem"
        Me.CustomerMedicineToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.CustomerMedicineToolStripMenuItem.Text = "&Customer Medicine"
        '
        'SupplierToolStripMenuItem
        '
        Me.SupplierToolStripMenuItem.Image = CType(resources.GetObject("SupplierToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SupplierToolStripMenuItem.Name = "SupplierToolStripMenuItem"
        Me.SupplierToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.SupplierToolStripMenuItem.Text = "Supplier"
        '
        'DetailToolStripMenuItem
        '
        Me.DetailToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StockDetailsToolStripMenuItem, Me.BillTransactionToolStripMenuItem, Me.StockReturnDetailToolStripMenuItem, Me.CustomerMedicineDetailsToolStripMenuItem, Me.SupplierDetailsToolStripMenuItem})
        Me.DetailToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Register
        Me.DetailToolStripMenuItem.Name = "DetailToolStripMenuItem"
        Me.DetailToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.DetailToolStripMenuItem.Text = "&Details"
        '
        'StockDetailsToolStripMenuItem
        '
        Me.StockDetailsToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Move_Next1
        Me.StockDetailsToolStripMenuItem.Name = "StockDetailsToolStripMenuItem"
        Me.StockDetailsToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.StockDetailsToolStripMenuItem.Text = "&Stock Details"
        '
        'BillTransactionToolStripMenuItem
        '
        Me.BillTransactionToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Move_Previous
        Me.BillTransactionToolStripMenuItem.Name = "BillTransactionToolStripMenuItem"
        Me.BillTransactionToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.BillTransactionToolStripMenuItem.Text = "&Bill Transaction"
        '
        'StockReturnDetailToolStripMenuItem
        '
        Me.StockReturnDetailToolStripMenuItem.Image = CType(resources.GetObject("StockReturnDetailToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StockReturnDetailToolStripMenuItem.Name = "StockReturnDetailToolStripMenuItem"
        Me.StockReturnDetailToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.StockReturnDetailToolStripMenuItem.Text = "Stock &Return Details"
        '
        'CustomerMedicineDetailsToolStripMenuItem
        '
        Me.CustomerMedicineDetailsToolStripMenuItem.Image = CType(resources.GetObject("CustomerMedicineDetailsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CustomerMedicineDetailsToolStripMenuItem.Name = "CustomerMedicineDetailsToolStripMenuItem"
        Me.CustomerMedicineDetailsToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.CustomerMedicineDetailsToolStripMenuItem.Text = "&Customer Medicine Details"
        '
        'SupplierDetailsToolStripMenuItem
        '
        Me.SupplierDetailsToolStripMenuItem.Image = CType(resources.GetObject("SupplierDetailsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SupplierDetailsToolStripMenuItem.Name = "SupplierDetailsToolStripMenuItem"
        Me.SupplierDetailsToolStripMenuItem.Size = New System.Drawing.Size(242, 22)
        Me.SupplierDetailsToolStripMenuItem.Text = "Supplier Details"
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PurchaseReportToolStripMenuItem, Me.SalesReportToolStripMenuItem, Me.StockReturnReportToolStripMenuItem, Me.CustomerMedicineReturnReportToolStripMenuItem})
        Me.ReportToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Print
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.ReportToolStripMenuItem.Text = "&Report"
        '
        'PurchaseReportToolStripMenuItem
        '
        Me.PurchaseReportToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Register
        Me.PurchaseReportToolStripMenuItem.Name = "PurchaseReportToolStripMenuItem"
        Me.PurchaseReportToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.PurchaseReportToolStripMenuItem.Text = "&Purchase Report"
        '
        'SalesReportToolStripMenuItem
        '
        Me.SalesReportToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Reports
        Me.SalesReportToolStripMenuItem.Name = "SalesReportToolStripMenuItem"
        Me.SalesReportToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.SalesReportToolStripMenuItem.Text = "&Sales Report"
        '
        'StockReturnReportToolStripMenuItem
        '
        Me.StockReturnReportToolStripMenuItem.Image = Global.PDMS.My.Resources.Resources.Reports2
        Me.StockReturnReportToolStripMenuItem.Name = "StockReturnReportToolStripMenuItem"
        Me.StockReturnReportToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.StockReturnReportToolStripMenuItem.Text = "Stock &Return Report"
        '
        'CustomerMedicineReturnReportToolStripMenuItem
        '
        Me.CustomerMedicineReturnReportToolStripMenuItem.Image = CType(resources.GetObject("CustomerMedicineReturnReportToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CustomerMedicineReturnReportToolStripMenuItem.Name = "CustomerMedicineReturnReportToolStripMenuItem"
        Me.CustomerMedicineReturnReportToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.CustomerMedicineReturnReportToolStripMenuItem.Text = "&Customer Medicine Report"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = CType(resources.GetObject("AboutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(70, 20)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 75
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 1000
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(46, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 21)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "WELCOME"
        '
        'Label5
        '
        Me.Label5.Image = Global.PDMS.My.Resources.Resources.dance
        Me.Label5.Location = New System.Drawing.Point(139, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 36)
        Me.Label5.TabIndex = 52
        '
        'Label4
        '
        Me.Label4.Image = Global.PDMS.My.Resources.Resources.dance
        Me.Label4.Location = New System.Drawing.Point(6, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 36)
        Me.Label4.TabIndex = 51
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Cursor = System.Windows.Forms.Cursors.No
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker1.Location = New System.Drawing.Point(88, 94)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(90, 20)
        Me.DateTimePicker1.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 20)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Time Log In"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Font = New System.Drawing.Font("Georgia", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(178, 84)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "Developed By Atish" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Email:- atishamte@yahoo.com" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(40, 81)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(109, 31)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Notepad"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(40, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 33)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "   Calculator"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(0, -3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(840, 73)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "PRANAV MEDICAL STORE"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(206, 27)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(822, 70)
        Me.Panel4.TabIndex = 50
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Button3
        '
        Me.Button3.Image = Global.PDMS.My.Resources.Resources.Print
        Me.Button3.Location = New System.Drawing.Point(146, 27)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(54, 50)
        Me.Button3.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Button3, "Billing")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(20, 27)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(60, 50)
        Me.Button5.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.Button5, "Stock Purchase")
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(86, 27)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(54, 50)
        Me.Button4.TabIndex = 51
        Me.ToolTip1.SetToolTip(Me.Button4, "Stock Details")
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(206, 103)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(822, 596)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 252)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(188, 153)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Utilities"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 103)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(188, 143)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Welcome"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 411)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(188, 122)
        Me.GroupBox3.TabIndex = 54
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "About"
        '
        'adminmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1026, 696)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "adminmain"
        Me.Text = "Pranav Medical Store System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockPurchaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockReturnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PurchaseReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdminToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents BillTransactionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockReturnReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BillToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomerMedicineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockReturnDetailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomerMedicineReturnReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomerMedicineDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SupplierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SupplierDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
