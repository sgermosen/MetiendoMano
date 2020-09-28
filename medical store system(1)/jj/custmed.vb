Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class custmed

    Private Sub custmedret_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label17.Text = Now.ToString("hh:mm:ss tt")
        Label11.Text = Format(Now, "dd/MM/yyyy")

        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim da1 As New SqlDataAdapter("select * from re", con)
        Dim ds1 As New DataSet

        da1.Fill(ds1)
        dataGrid1.Refresh()
        dataGrid1.DataSource = ds1.Tables(0)

        Dim da2 As New SqlDataAdapter
        Dim ds2 As New DataSet
        da2 = New SqlDataAdapter("select distinct[Purchase Date] from re", con)
        da2.Fill(ds2)
        If ds1.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To ds2.Tables(0).Rows.Count - 1
                comboBox1.Items.Add(ds2.Tables(0).Rows(i)("Purchase Date"))
            Next
        End If

        Dim da4 As New SqlDataAdapter("select * from stockentry", con)
        Dim ds4 As New DataSet

        da4.Fill(ds4)
        DataGrid2.Refresh()
        DataGrid2.DataSource = ds4.Tables(0)

        Dim da3 As New SqlDataAdapter
        Dim ds3 As New DataSet
        da3 = New SqlDataAdapter("select * from re", con)
        da3.Fill(ds3)

        Dim j As Integer
        For j = 0 To ds3.Tables(0).Rows.Count - 1
            txtbill.Text = ds3.Tables(0).Rows(j)(0)
        Next
        txtbill.Text = txtbill.Text + 1
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            Dim qty As Integer
            Dim qty1 As Integer
            Dim amt As Double
            Dim amt1 As Double

            cm.Connection = con
            con.Open()

            If txticode.Text = "" Then
                MsgBox("Enter the Item Code Number")
                txticode.Focus()
            ElseIf txtbat.Text = "" Then
                MsgBox("Enter the Batch Number")
                txtbat.Focus()
            ElseIf txtmed.Text = "" Then
                MsgBox("Enter the Medicine Name")
                txtmed.Focus()
            ElseIf txtmfc.Text = "" Then
                MsgBox("Enter the Company Name")
                txtmfc.Focus()
            ElseIf txtpri.Text = "" Then
                MsgBox("Enter the Unit Price")
                txtpri.Focus()
            ElseIf txtexp.Text = "" Then
                MsgBox("Enter the Expiry Date")
                txtexp.Focus()
            ElseIf txtqty.Text = "" Then
                MsgBox("Enter the Quantity")
                txtqty.Focus()
                Exit Sub
            ElseIf txttaxamt.Text = "" Then
                MsgBox("Enter the Tax Amount")
                txttaxamt.Focus()
            ElseIf txttotamt.Text = "" Then
                MsgBox("For amount click the Amount button")
                Button5.Focus()
                Exit Sub
            ElseIf (Val(txtqty.Text) > Val(txtstock.Text)) Then
                MsgBox("No required Stock is available")
                txtqty.Focus()
                Exit Sub
            End If

            da2 = New SqlDataAdapter("select * from re ", con)
            da2.Fill(ds2)

            For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                If (txtbill.Text = Val(ds2.Tables(0).Rows(i)("billno"))) Then
                    MsgBox("Enter bill number other than used")
                    txtbill.Focus()
                    Exit Sub
                End If
            Next
           
            da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
            da1.Fill(ds1)
            qty = Trim(ds1.Tables(0).Rows(0)(6))
            amt = Trim(ds1.Tables(0).Rows(0)(10))
            qty1 = qty - Val(txtqty.Text)
            amt1 = amt - Val(txttotamt.Text)
            
            cm.CommandText = "Update stockentry set quantity=" & Val(qty1) & " ,amount='" & Val(amt1) & "'where [Item code]='" & txticode.Text & "'"
            cm.ExecuteNonQuery()

            cm.CommandText = "insert into re values('" & txtbill.Text & "','" & txticode.Text & "','" & txtmed.Text & "','" & txtbat.Text & "','" & txtqty.Text & "','" & txtpri.Text & "','" & txttaxamt.Text & "','" & txttotamt.Text & "','" & Label11.Text & "','" & txtexp.Text & "')"
            cm.ExecuteNonQuery()

            MessageBox.Show("Returned successfully", "Customer Return")
            txtbat.Text = ""
            txtmed.Text = ""
            txtpri.Text = ""
            txttotamt.Text = ""
            txtexp.Text = ""
            txtmfc.Text = ""
            txtqty.Text = ""
            txticode.Text = ""
            txttaxamt.Text = ""
            txtstock.Text = ""

            Dim da4 As New SqlDataAdapter("select * from stockentry", con)
            Dim ds4 As New DataSet

            da4.Fill(ds4)
            DataGrid2.Refresh()
            DataGrid2.DataSource = ds4.Tables(0)

            Dim ds5 As New DataSet
            Dim da5 As New SqlDataAdapter("select * from re", con)
            da5.Fill(ds5)
            dataGrid1.DataSource = ds5.Tables(0)

            Dim ds6 As New DataSet
            Dim da6 As New SqlDataAdapter("select * from stockentry", con)
            da6.Fill(ds6)

            Dim da3 As New SqlDataAdapter
            Dim ds3 As New DataSet
            da3 = New SqlDataAdapter("select * from re", con)
            da3.Fill(ds3)

            Dim j As Integer
            For j = 0 To ds3.Tables(0).Rows.Count - 1
                txtbill.Text = ds3.Tables(0).Rows(j)(0)
            Next
            txtbill.Text = txtbill.Text + 1

            comboBox1.Items.Clear()
            Dim da7 As New SqlDataAdapter
            Dim ds7 As New DataSet
            da7 = New SqlDataAdapter("select distinct[Purchase Date] from re", con)
            da7.Fill(ds7)
            If ds7.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To ds7.Tables(0).Rows.Count - 1
                    comboBox1.Items.Add(ds7.Tables(0).Rows(i)("Purchase Date"))
                Next
            End If
            txticode.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub txtbat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")

            Dim cm As New SqlCommand
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
           
            cm.Connection = con
            cm.Connection.Open()
          
            If (txtbill.Text = "") Then
                MsgBox("Enter The Bill No")
                txtbill.Focus()
            Else
                da1 = New SqlDataAdapter("select * from re where [billNo]='" & txtbill.Text & "'", con)
                da1.Fill(ds1)

                If ds1.Tables(0).Rows.Count < 1 Then
                    MsgBox("Invalid Bill Number")
                    comboBox1.Items.Clear()
                ElseIf MsgBox("Are You Sure to Delete the Bill Details? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    cm.CommandText = "Delete from re where billNo=" & Val(txtbill.Text) & ""
                    cm.ExecuteNonQuery()

                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter("select * from re", con)
                    da.Fill(ds)
                    dataGrid1.DataSource = ds.Tables(0)
                    MsgBox("Deleted Successfully")
                End If
            End If

            txtbat.Text = ""
            txtmed.Text = ""
            txtbill.Text = ""
            txtpri.Text = ""
            txttotamt.Text = ""
            txtexp.Text = ""
            txtmfc.Text = ""
            txtqty.Text = ""
            txticode.Text = ""
            txttaxamt.Text = ""
            txtstock.Text = ""
            txtbill.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim da2 As New SqlDataAdapter("select * from re where [Purchase Date]='" + comboBox1.Text + "' order by[Purchase Date]", con)
            Dim ds2 As New DataSet
            da2.Fill(ds2)
            dataGrid1.DataSource = ds2.Tables(0)

            Dim da3 As New SqlDataAdapter
            Dim ds3 As New DataSet
            da3 = New SqlDataAdapter("select * from re where [Purchase Date]='" + comboBox1.Text + "' order by[Purchase Date]", con)
            da3.Fill(ds3)
            Dim count As Decimal = 0

            For i As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                count = count + Val(ds3.Tables(0).Rows(i)(1))
            Next

            comboBox1.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub txticode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txticode.KeyDown
        Try
            If e.KeyCode = 13 Then
                Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
                Dim cm As New SqlCommand
                Dim qty As Integer
                Dim da1 As New SqlDataAdapter
                Dim ds1 As New DataSet

                cm.Connection = con
                cm.Connection.Open()

                If txticode.Text = "" Then
                    MsgBox("Enter the Item Code")
                Else
                    da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                    da1.Fill(ds1)

                    If ds1.Tables(0).Rows.Count < 1 Then
                        MsgBox("Invalid Item code")
                    Else
                        qty = Trim(ds1.Tables(0).Rows(0)(6))
                        txtbat.Text = Trim(ds1.Tables(0).Rows(0)(7))
                        txtmed.Text = Trim(ds1.Tables(0).Rows(0)(4))
                        txtpri.Text = Trim(ds1.Tables(0).Rows(0)(5))
                        txtexp.Text = Trim(ds1.Tables(0).Rows(0)(8))
                        txtmfc.Text = Trim(ds1.Tables(0).Rows(0)(2))
                        txttaxamt.Text = Trim(ds1.Tables(0).Rows(0)(9))
                        txtstock.Text = Val(qty)
                        txtqty.Focus()
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub txtbat_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbat.TextChanged

    End Sub

    Private Sub txtbill_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbill.TextChanged

    End Sub

    Private Sub txtmed_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmed.TextChanged

    End Sub

    Private Sub txtpri_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpri.TextChanged

    End Sub

    Private Sub txttotamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotamt.TextChanged

    End Sub

    Private Sub txtexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtexp.TextChanged

    End Sub

    Private Sub txtmfc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmfc.TextChanged

    End Sub

    Private Sub txtqty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtqty.TextChanged

    End Sub

    Private Sub txtpur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtstock_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtstock.TextChanged

    End Sub

    Private Sub txticode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txticode.TextChanged

    End Sub

    Private Sub comboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If (txttaxamt.Text = 0) Then
                txttotamt.Text = txtpri.Text * txtqty.Text
            Else
                Dim a As Double
                Dim b As Double
                b = txttaxamt.Text / 100
                a = txtpri.Text * txtqty.Text
                txttotamt.Text = a + (txtpri.Text * txtqty.Text * b)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub
End Class