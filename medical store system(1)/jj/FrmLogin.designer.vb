<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLogin))
        Me.GroupBoxMain = New System.Windows.Forms.GroupBox
        Me.ButtonClose = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.txtpassword = New System.Windows.Forms.TextBox
        Me.LabelPassword = New System.Windows.Forms.Label
        Me.txtusername = New System.Windows.Forms.TextBox
        Me.LabelUserName = New System.Windows.Forms.Label
        Me.PictureBoxLogin = New System.Windows.Forms.PictureBox
        Me.LabelHeader = New System.Windows.Forms.Label
        Me.GroupBoxMain.SuspendLayout()
        CType(Me.PictureBoxLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBoxMain
        '
        Me.GroupBoxMain.BackColor = System.Drawing.Color.Transparent
        Me.GroupBoxMain.Controls.Add(Me.ButtonClose)
        Me.GroupBoxMain.Controls.Add(Me.ButtonOk)
        Me.GroupBoxMain.Controls.Add(Me.txtpassword)
        Me.GroupBoxMain.Controls.Add(Me.LabelPassword)
        Me.GroupBoxMain.Controls.Add(Me.txtusername)
        Me.GroupBoxMain.Controls.Add(Me.LabelUserName)
        Me.GroupBoxMain.Controls.Add(Me.PictureBoxLogin)
        resources.ApplyResources(Me.GroupBoxMain, "GroupBoxMain")
        Me.GroupBoxMain.Name = "GroupBoxMain"
        Me.GroupBoxMain.TabStop = False
        '
        'ButtonClose
        '
        Me.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.ButtonClose, "ButtonClose")
        Me.ButtonClose.Image = Global.PDMS.My.Resources.Resources.Delete_5
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonOk
        '
        Me.ButtonOk.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.ButtonOk, "ButtonOk")
        Me.ButtonOk.Image = Global.PDMS.My.Resources.Resources.oAccess
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'txtpassword
        '
        resources.ApplyResources(Me.txtpassword, "txtpassword")
        Me.txtpassword.ForeColor = System.Drawing.Color.Red
        Me.txtpassword.Name = "txtpassword"
        '
        'LabelPassword
        '
        resources.ApplyResources(Me.LabelPassword, "LabelPassword")
        Me.LabelPassword.Name = "LabelPassword"
        '
        'txtusername
        '
        resources.ApplyResources(Me.txtusername, "txtusername")
        Me.txtusername.ForeColor = System.Drawing.Color.Black
        Me.txtusername.Name = "txtusername"
        '
        'LabelUserName
        '
        resources.ApplyResources(Me.LabelUserName, "LabelUserName")
        Me.LabelUserName.Name = "LabelUserName"
        '
        'PictureBoxLogin
        '
        resources.ApplyResources(Me.PictureBoxLogin, "PictureBoxLogin")
        Me.PictureBoxLogin.Name = "PictureBoxLogin"
        Me.PictureBoxLogin.TabStop = False
        '
        'LabelHeader
        '
        Me.LabelHeader.AutoEllipsis = True
        Me.LabelHeader.BackColor = System.Drawing.Color.Transparent
        Me.LabelHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.LabelHeader, "LabelHeader")
        Me.LabelHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LabelHeader.Name = "LabelHeader"
        Me.LabelHeader.UseMnemonic = False
        '
        'FrmLogin
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBoxMain)
        Me.Controls.Add(Me.LabelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLogin"
        Me.ShowInTaskbar = False
        Me.GroupBoxMain.ResumeLayout(False)
        Me.GroupBoxMain.PerformLayout()
        CType(Me.PictureBoxLogin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBoxMain As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBoxLogin As System.Windows.Forms.PictureBox
    Friend WithEvents LabelUserName As System.Windows.Forms.Label
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents LabelPassword As System.Windows.Forms.Label
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
End Class
