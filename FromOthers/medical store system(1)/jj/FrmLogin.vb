
Imports System.Data.SqlClient

'Developed By jaya kumbhar........
'Mail :- jayakumbhar@ymail.com


Public Class FrmLogin

#Region "Variables ....."
    Private _Empty_Font As New Font("Verdana", 9D, FontStyle.Italic)
    Private _Fill_Font_User_ID As New Font("Consolas", 9D, FontStyle.Regular)
    Private _Fill_Font_Password As New Font("Wingdings 2", 10D, FontStyle.Bold)
#End Region

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;integrated security=true")
            Dim ds1 As New DataSet
            Dim da1 As New SqlDataAdapter("select * from login where Name='" & Trim(txtusername.Text) & "'and password='" & Trim(txtpassword.Text) & "'", con)

            If da1.Fill(ds1) Then
                adminmain.Show()
                Me.Close()
            Else
                MsgBox("Invalid Password or Username")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        End
    End Sub
End Class