<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class custmedrep
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(custmedrep))
        Me.label3 = New System.Windows.Forms.Label
        Me.comboBox1 = New System.Windows.Forms.ComboBox
        Me.button2 = New System.Windows.Forms.Button
        Me.crystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.CrystalReport61 = New PDMS.CrystalReport6
        Me.Button4 = New System.Windows.Forms.Button
        Me.CrystalReport11 = New PDMS.CrystalReport1
        Me.CrystalReport21 = New PDMS.CrystalReport2
        Me.CrystalReport31 = New PDMS.CrystalReport3
        Me.CrystalReport51 = New PDMS.CrystalReport5
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Georgia", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(52, 156)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(70, 16)
        Me.label3.TabIndex = 107
        Me.label3.Text = "For Date"
        '
        'comboBox1
        '
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Location = New System.Drawing.Point(24, 201)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(121, 21)
        Me.comboBox1.TabIndex = 1
        '
        'button2
        '
        Me.button2.Font = New System.Drawing.Font("Georgia", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button2.Image = Global.PDMS.My.Resources.Resources.Register
        Me.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.button2.Location = New System.Drawing.Point(26, 275)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(109, 47)
        Me.button2.TabIndex = 2
        Me.button2.Text = "        Search For Date"
        Me.button2.UseVisualStyleBackColor = True
        '
        'crystalReportViewer1
        '
        Me.crystalReportViewer1.ActiveViewIndex = 0
        Me.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crystalReportViewer1.DisplayGroupTree = False
        Me.crystalReportViewer1.Location = New System.Drawing.Point(177, 12)
        Me.crystalReportViewer1.Name = "crystalReportViewer1"
        Me.crystalReportViewer1.ReportSource = Me.CrystalReport61
        Me.crystalReportViewer1.Size = New System.Drawing.Size(835, 620)
        Me.crystalReportViewer1.TabIndex = 112
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Georgia", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.Location = New System.Drawing.Point(26, 361)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(109, 42)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "&Exit"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'custmedretrep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 641)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.crystalReportViewer1)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.comboBox1)
        Me.Controls.Add(Me.label3)
        Me.Name = "custmedretrep"
        Me.Text = "Customer Medicine Return Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents crystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents CrystalReport61 As PDMS.CrystalReport6
    Friend WithEvents CrystalReport11 As PDMS.CrystalReport1
    Friend WithEvents CrystalReport21 As PDMS.CrystalReport2
    Friend WithEvents CrystalReport31 As PDMS.CrystalReport3
    Friend WithEvents CrystalReport51 As PDMS.CrystalReport5
End Class
