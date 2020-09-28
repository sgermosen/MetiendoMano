Imports System.Data.SqlClient
Public Class purchaserep

    Private Sub purchaserep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim da1 As New SqlDataAdapter
        Dim ds1 As New DataSet
        da1 = New SqlDataAdapter("select distinct[Purchase Date] from stockentry", con)
        da1.Fill(ds1)
        If ds1.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                comboBox1.Items.Add(ds1.Tables(0).Rows(i)("Purchase Date"))
            Next
        End If

    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim da2 As New SqlDataAdapter("select *from stockentry where [Purchase Date]='" + comboBox1.Text + "' order by[Purchase Date]", con)
            Dim ds2 As New DataSet
            da2.Fill(ds2)

            CrystalReportViewer1.RefreshReport()
            CrystalReportViewer1.SelectionFormula = "{stockentry.Purchase Date}='" + comboBox1.Text + "'"
            CrystalReportViewer1.RefreshReport()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub comboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox1.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class