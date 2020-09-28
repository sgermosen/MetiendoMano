Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class billtnsdet

    Private Sub billtnsdet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim da1 As New SqlDataAdapter("select * from bill", con)
        Dim ds1 As New DataSet

        DataGrid2.Refresh()
        da1.Fill(ds1)
        DataGrid2.DataSource = ds1.Tables(0)

        Dim da2 As New SqlDataAdapter
        Dim ds2 As New DataSet
        da2 = New SqlDataAdapter("select * from bill", con)
        da2.Fill(ds2)
        Dim count As Decimal = 0

        For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
            count = count + Val(ds2.Tables(0).Rows(i)(1))
        Next

        Label1.Text = Format(count, "#,###.##")

        Dim da5 As New SqlDataAdapter
        Dim ds5 As New DataSet
        da5 = New SqlDataAdapter("select distinct[date] from bill", con)
        da5.Fill(ds5)
        If ds5.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To ds5.Tables(0).Rows.Count - 1
                comboBox1.Items.Add(ds5.Tables(0).Rows(i)("date"))
            Next
        End If
       
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ButtonDeleteName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDeleteName.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet

            da2 = New SqlDataAdapter("select * from bill where [billno]='" & txtbillno.Text & "'", con)
            da2.Fill(ds2)
            cm.Connection = con
            cm.Connection.Open()
            da1 = New SqlDataAdapter("select * from bill ", con)
            da1.Fill(ds1)
            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("No more Bills are in the list")
            ElseIf (txtbillno.Text = "") Then
                MsgBox("Enter The Bill Number")
                txtbillno.Focus()

            ElseIf ds2.Tables(0).Rows.Count < 1 Then
                MsgBox("Invalid Bill Number")

            ElseIf MsgBox("Are You Sure Delete Selected Bill ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "Delete from bill where billNo=" & Val(txtbillno.Text) & ""
                cm.ExecuteNonQuery()

                'refresh
                Dim ds As New DataSet
                Dim da As New SqlDataAdapter("select * from bill", con)
                da.Fill(ds)
                DataGrid2.DataSource = ds.Tables(0)

                MsgBox("Deleted Successfully")
                txtbillno.Text = ""
                txtbillno.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub picTOP_LEFT_02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picTOP_LEFT_02.Click

    End Sub

    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet

            da2 = New SqlDataAdapter("select * from bill ", con)
            da2.Fill(ds2)
            If ds2.Tables(0).Rows.Count < 1 Then
                MsgBox("No more bills are in the list")
            ElseIf MsgBox("Are You Sure to Delete the all Bill Details? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                cm.CommandText = "delete from bill"
                cm.ExecuteNonQuery()
                Label1.Text = ""
            End If
            'refresh
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from bill", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

            txtbillno.Text = ""
            txtbillno.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim da2 As New SqlDataAdapter("select *from bill where [date]='" + comboBox1.Text + "' order by[date]", con)
            Dim ds2 As New DataSet
            da2.Fill(ds2)

            DataGrid2.DataSource = ds2.Tables(0)
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet

            da1 = New SqlDataAdapter("select * from bill ", con)
            da1.Fill(ds1)

            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("No more bills are in the list")
            Else
                Dim da3 As New SqlDataAdapter
                Dim ds3 As New DataSet
                da3 = New SqlDataAdapter("select * from bill where [date]='" + comboBox1.Text + "' order by[date]", con)
                da3.Fill(ds3)

                Dim count As Decimal = 0
                For i As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                    count = count + Val(ds3.Tables(0).Rows(i)(1))
                Next
                Label1.Text = Format(count, "#,###.##")
            End If
            comboBox1.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub txtbillno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbillno.TextChanged
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim ds2 As New DataSet
            Dim da2 As New SqlDataAdapter("select * from bill where BillNo like '%" & txtbillno.Text & "%'", con)

            da2.Fill(ds2)
            DataGrid2.DataSource = ds2.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub comboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox1.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub
End Class