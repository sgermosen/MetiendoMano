
Imports System.Data.SqlClient

Public Class chngpass

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim uname As String
            Dim pwd As String
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim cm As New SqlCommand

            cm.Connection = con
            cm.Connection.Open()
            da1 = New SqlDataAdapter("select * from login", con)
            da1.Fill(ds1)

            uname = Trim(ds1.Tables(0).Rows(0)(0))
            pwd = Trim(ds1.Tables(0).Rows(0)(1))
            If TextBox1.Text = "" Then
                MsgBox("Enter Username")
                TextBox1.Focus()
            ElseIf TextBox2.Text = "" Then
                MsgBox("Enter Your Old Password")
                TextBox2.Focus()
            ElseIf TextBox3.Text = "" Then
                MsgBox("Enter Your New Password")
                TextBox3.Focus()
            ElseIf String.Compare(uname, TextBox1.Text) <> 0 Then
                MsgBox("Invalid Username")
                TextBox1.Focus()
            ElseIf String.Compare(pwd, TextBox2.Text) <> 0 Then
                MsgBox("Invalid Old Password")
                TextBox2.Focus()
            Else
                cm.CommandText = "Update login set name='" & TextBox1.Text & "',password='" & TextBox3.Text & "'where name='" & TextBox1.Text & "'"
                cm.ExecuteNonQuery()
                MsgBox("Password Updated Successfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox1.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
