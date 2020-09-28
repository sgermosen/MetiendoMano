Imports System.Data.SqlClient
Public Class supplierdetail
    Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")

    Private Sub supplierdetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim da1 As New SqlDataAdapter
        Dim ds1 As New DataSet
        da1 = New SqlDataAdapter("select * from supplier", con)
        da1.Fill(ds1)

        DataGrid2.DataSource = ds1.Tables(0)

    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim ds2 As New DataSet
            Dim da2 As New SqlDataAdapter("select * from supplier where [Supplier ID] like '%" & TxtSearch.Text & "%'", con)

            da2.Fill(ds2)
            DataGrid2.DataSource = ds2.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()

            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet

            da2 = New SqlDataAdapter("select * from supplier ", con)
            da2.Fill(ds2)
            If ds2.Tables(0).Rows.Count < 1 Then
                MsgBox("No more Suppliers are in the list")
            ElseIf MsgBox("Are You Sure to Delete the all Supplier Details ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "delete from supplier"
                cm.ExecuteNonQuery()
            End If

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from supplier", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        adminmain.Show()
        Me.Close()
    End Sub
End Class