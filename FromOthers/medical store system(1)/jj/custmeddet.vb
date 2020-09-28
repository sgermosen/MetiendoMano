Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class custmeddet

    Private Sub custmedretdet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim da1 As New SqlDataAdapter("select * from re", con)
        Dim ds1 As New DataSet

        da1.Fill(ds1)
        dataGrid1.Refresh()
        dataGrid1.DataSource = ds1.Tables(0)
    End Sub
    Private Sub textBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox1.TextChanged
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim ds2 As New DataSet
            Dim da2 As New SqlDataAdapter("select * from re where [billno] like '%" & textBox1.Text & "%'", con)

            da2.Fill(ds2)
            dataGrid1.DataSource = ds2.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet

            da2 = New SqlDataAdapter("select * from re ", con)
            da2.Fill(ds2)

            If ds2.Tables(0).Rows.Count < 1 Then
                MsgBox("No more customer returns are in the list")
            ElseIf MsgBox("Are You Sure Delete the all customer return Details?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "delete from re"
                cm.ExecuteNonQuery()
            End If

            'refresh
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from re", con)
            da.Fill(ds)
            dataGrid1.DataSource = ds.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class