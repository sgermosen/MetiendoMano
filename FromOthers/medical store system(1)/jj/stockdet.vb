
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class stockdet

    Private Sub stockdet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim da1 As New SqlDataAdapter("select * from stockentry", con)
        Dim ds1 As New DataSet

        da1.Fill(ds1)
        DataGrid2.Refresh()
        DataGrid2.DataSource = ds1.Tables(0)
        TxtSearch.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()

            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet

            da2 = New SqlDataAdapter("select * from stockentry ", con)
            da2.Fill(ds2)
            If ds2.Tables(0).Rows.Count < 1 Then
                MsgBox("No more stocks are in the list")
            ElseIf MsgBox("Are You Sure to Delete the all Stock Details ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "delete from stockentry"
                cm.ExecuteNonQuery()
            End If

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from stockentry", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim ds2 As New DataSet
            Dim da2 As New SqlDataAdapter("select * from stockentry where [Item code] like '%" & TxtSearch.Text & "%'", con)

            da2.Fill(ds2)
            DataGrid2.DataSource = ds2.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        billing.Show()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub
End Class