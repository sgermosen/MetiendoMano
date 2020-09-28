Imports System.Data.SqlClient


Public Class stkreturn

    Private Sub txticode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txticode.KeyDown
        Try
            If e.KeyCode = 13 Then
                Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
                Dim cm As New SqlCommand
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
                        txtsupid.Text = Trim(ds1.Tables(0).Rows(0)(1))
                        txtiname.Text = Trim(ds1.Tables(0).Rows(0)(4))
                        txtprice.Text = Trim(ds1.Tables(0).Rows(0)(5))
                        txtbat.Text = Trim(ds1.Tables(0).Rows(0)(7))
                        txttaxamt.Text = Trim(ds1.Tables(0).Rows(0)(9))
                        txtpur.Text = Trim(ds1.Tables(0).Rows(0)(0))
                        txtexp.Text = Trim(ds1.Tables(0).Rows(0)(8))
                        txtqty.Focus()
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txticode_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txticode.MouseDown


    End Sub

    Private Sub txticode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txticode.TextChanged

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
        Dim cm As New SqlCommand
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim qty As Integer
            Dim qty1 As Integer
            Dim amt As Double
            Dim amt1 As Double
            cm.Connection = con
            con.Open()
            da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
            da1.Fill(ds1)
            qty = Trim(ds1.Tables(0).Rows(0)(6))

            If txticode.Text = "" Then
                MsgBox("Enter the Item Code")
                txticode.Focus()
            ElseIf txttaxamt.Text = "" Then
                MsgBox("Enter the Tax Amount")
                txttaxamt.Focus()
            ElseIf txtqty.Text = "" Then
                MsgBox("Enter the Quantity")
                txtqty.Focus()
            ElseIf txtamt.Text = "" Then
                MsgBox("For amount press enter after inserting the Quantity")
                txtqty.Focus()
            ElseIf txtpur.Text = "" Then
                MsgBox("Enter the Purchase Date")
                txtpur.Focus()
            ElseIf (txtqty.Text > Val(qty)) Then
                MsgBox("No required Stock is available")
                txtqty.Focus()
            Else
                da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                da1.Fill(ds1)
                qty = Trim(ds1.Tables(0).Rows(0)(6))
                amt = Trim(ds1.Tables(0).Rows(0)(10))
                qty1 = qty - Val(txtqty.Text)
                amt1 = amt - Val(txtamt.Text)

                cm.CommandText = "Update stockentry set quantity=" & Val(qty1) & " ,amount='" & Val(amt1) & "'where [Item code]='" & txticode.Text & "'"
                cm.ExecuteNonQuery()
                cm.CommandText = "insert into stockreturn values('" & txtbill.Text & "','" & txtsupid.Text & "','" & Label13.Text & "','" & txtbat.Text & "','" & txticode.Text & "','" & txtiname.Text & "','" & txtqty.Text & "','" & txtprice.Text & "','" & txttaxamt.Text & "','" & txtamt.Text & "','" & txtpur.Text & "','" & txtexp.Text & "')"
                cm.ExecuteNonQuery()
                MessageBox.Show("Returned successfully", "PDMS")

                txtsupid.Text = ""
                txtpur.Text = ""
                txticode.Text = ""
                txtiname.Text = ""
                txtbat.Text = ""
                txtprice.Text = ""
                txttaxamt.Text = ""
                txtqty.Text = ""
                txtamt.Text = ""
                txtexp.Text = ""
                txticode.Focus()

                Dim da4 As New SqlDataAdapter("select * from stockentry", con)
                Dim ds4 As New DataSet

                da4.Fill(ds4)
                DataGrid1.Refresh()
                DataGrid1.DataSource = ds4.Tables(0)

                Dim da2 As New SqlDataAdapter
                Dim ds2 As New DataSet
                da2 = New SqlDataAdapter("select * from stockreturn", con)
                da2.Fill(ds2)
                Dim i As Integer
                For i = 0 To ds2.Tables(0).Rows.Count - 1
                    txtbill.Text = ds2.Tables(0).Rows(i)(0)
                Next
                txtbill.Text = txtbill.Text + 1
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim ds5 As New DataSet
        Dim da5 As New SqlDataAdapter("select * from stockreturn", con)
        da5.Fill(ds5)
        DataGrid2.DataSource = ds5.Tables(0)

        Dim ds6 As New DataSet
        Dim da6 As New SqlDataAdapter("select * from stockentry", con)
        da6.Fill(ds6)

    End Sub

    Private Sub returndet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Label10.Text = Now.ToString("hh:mm:ss tt")
            Label13.Text = Format(Now, "dd/MM/yyyy")

            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim da2 As New SqlDataAdapter("select * from stockreturn", con)
            Dim ds2 As New DataSet
            DataGrid2.Refresh()
            da2.Fill(ds2)
            DataGrid2.Refresh()
            DataGrid2.DataSource = ds2.Tables(0)

            Dim da4 As New SqlDataAdapter("select * from stockentry", con)
            Dim ds4 As New DataSet

            da4.Fill(ds4)
            DataGrid1.Refresh()
            DataGrid1.DataSource = ds4.Tables(0)

            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from stockreturn", con)
            da1.Fill(ds1)
            txtbill.Text = 0
            Dim i As Integer
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                txtbill.Text = ds1.Tables(0).Rows(i)(0)
            Next
            txtbill.Text = txtbill.Text + 1

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtqty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtqty.KeyDown
        Try
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            Dim qty1 As Integer
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            da2 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
            da2.Fill(ds2)
            qty1 = Trim(ds2.Tables(0).Rows(0)(6))
            If e.KeyCode = 13 Then
                If txtqty.Text = "" Then
                    MsgBox("Enter the Quantity")
                    txtqty.Focus()
                ElseIf (txtqty.Text > Val(qty1)) Then
                    MsgBox("No required Stock is available")
                    txtqty.Focus()
                    txtqty.Text = ""
                ElseIf txttaxamt.Text = "" Then
                    MsgBox("Enter the Tax Amount")
                    txttaxamt.Focus()
                ElseIf (txttaxamt.Text = 0) Then
                    txtamt.Text = txtprice.Text * txtqty.Text
                Else
                    Dim a As Double
                    Dim b As Double
                    b = txttaxamt.Text / 100
                    a = txtprice.Text * txtqty.Text
                    txtamt.Text = a + (txtprice.Text * txtqty.Text * b)

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        stockdet.Show()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub txtpur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpur.TextChanged

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub

    Private Sub txtqty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtqty.TextChanged

    End Sub

    Private Sub txtiname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtiname.TextChanged

    End Sub

    Private Sub txtbat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbat.TextChanged

    End Sub

    Private Sub txtprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.TextChanged

    End Sub

    Private Sub txttaxamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttaxamt.TextChanged

    End Sub

    Private Sub txtamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamt.TextChanged

    End Sub
End Class