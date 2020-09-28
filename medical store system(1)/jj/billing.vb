Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class billing
    Dim con As New SqlConnection(str)

    Private Sub txtqty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtqty.KeyDown
        Try

            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            Dim qty1 As Integer
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
                Else
                    If txttaxamt.Text = "" Then
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

                    Dim da1 As New SqlDataAdapter
                    Dim ds1 As New DataSet
                    Dim qty As Integer
                    Dim con As New SqlConnection(str)
                    Dim cm As New SqlCommand

                    da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                    da1.Fill(ds1)
                    qty = Trim(ds1.Tables(0).Rows(0)(6))

                    cm.Connection = con
                    cm.Connection.Open()
                    txtamt.Focus()

                End If
            End If

            Dim ds5 As New DataSet
            Dim da5 As New SqlDataAdapter("select * from stockentry", con)
            da5.Fill(ds5)
            DataGrid1.DataSource = ds5.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txticode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txticode.KeyDown
        Try
            If e.KeyCode = 13 Then
                Dim con As New SqlConnection(str)
                Dim cm As New SqlCommand
                Dim da1 As New SqlDataAdapter
                Dim ds1 As New DataSet

                cm.Connection = con
                cm.Connection.Open()

                If txticode.Text = "" Then
                    MsgBox("Enter the Item Code")
                    txticode.Focus()
                Else
                    da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                    da1.Fill(ds1)

                    If ds1.Tables(0).Rows.Count < 1 Then
                        MsgBox("Invalid Item code")
                        txticode.Focus()
                    Else
                        txtiname.Text = Trim(ds1.Tables(0).Rows(0)(4))
                        txtprice.Text = Trim(ds1.Tables(0).Rows(0)(5))
                        txttaxamt.Text = Trim(ds1.Tables(0).Rows(0)(9))
                        txtqty.Focus()
                    End If
                End If
            End If

            If e.KeyCode = Keys.Up Then
                print()
                txticode.Show()
            End If

            If e.KeyCode = Keys.Down Then
                bill()
                txticode.Show()

            End If

            If e.KeyCode = Keys.Right Then
                remove()
                txticode.Show()

            End If
            If e.KeyCode = Keys.Left Then
                stockdet.Show()
                txticode.Show()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txtamt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtamt.KeyDown
        Try
            If e.KeyCode = 13 Then

                Dim con As New SqlConnection(str)
                Dim cm As New SqlCommand
                Dim ds As New DataSet

                cm.Connection = con
                cm.Connection.Open()
                cm.CommandText = "Insert into bill1 values('" & TextBox1.Text & "','" & txtiname.Text & "'," & Val(txtprice.Text) & "," & Val(txtqty.Text) & "," & Val(txtamt.Text) & ")"
                cm.ExecuteNonQuery()

                'refresh
                Dim da As New SqlDataAdapter("select * from bill1", con)
                da.Fill(ds)
                DataGrid2.DataSource = ds.Tables(0)

                txticode.Focus()
                txticode.Text = ""
                txtiname.Text = ""
                txtprice.Text = ""
                txtqty.Text = ""
                txttaxamt.Text = ""
                txtamt.Text = ""

                Dim da1 As New SqlDataAdapter
                Dim ds1 As New DataSet
                da1 = New SqlDataAdapter("select * from bill1", con)
                da1.Fill(ds1)

                If ds1.Tables(0).Rows.Count > 0 Then
                    TextBox1.Text = ds1.Tables(0).Rows.Count + 1

                Else
                    TextBox1.Text = 1
                End If

                Dim da2 As New SqlDataAdapter
                Dim ds2 As New DataSet
                da2 = New SqlDataAdapter("select * from bill1", con)
                da2.Fill(ds2)

                Dim count1 As Integer = 0
                For j As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                    count1 = count1 + Val(ds2.Tables(0).Rows(j)(3))

                Next
                Label14.Text = count1
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        total()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        DataGrid1.Show()
        Button1.Hide()
        Buttonexit.Hide()
        ButtonClose.Show()
        txticode.Focus()
        Dim ds5 As New DataSet
        Dim da5 As New SqlDataAdapter("select * from stockentry", con)
        da5.Fill(ds5)
        DataGrid1.DataSource = ds5.Tables(0)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim qty As Integer
            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            Dim y As Integer

            y = InputBox("Enter the Serial No")

            Dim da8 As New SqlDataAdapter("select * from bill1 where [Si No]='" & y & "'", con)
            Dim ds8 As New DataSet
            da8.Fill(ds8)
            Dim qty1 As Integer
            qty1 = ds8.Tables(0).Rows(0)(3)

            Dim x As Integer
            x = InputBox("Enter the Item Code")
            da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & x & "'", con)
            da1.Fill(ds1)

            qty = Trim(ds1.Tables(0).Rows(0)(6)) + Val(qty1)

            Dim da6 As New SqlDataAdapter("delete from bill1 where [si no]='" & y & "'", con)
            Dim ds6 As New DataSet
            da6.Fill(ds6)
            cm.Connection = con
            cm.Connection.Open()

            cm.CommandText = "Update stockentry set quantity=" & Val(qty) & " where [Item code]='" & x & "'"
            cm.ExecuteNonQuery()

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from bill1", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

            MsgBox("Removed Successfully")
            txticode.Text = ""
            txtiname.Text = ""
            txtprice.Text = ""
            txtqty.Text = ""
            txttaxamt.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label10.Text = Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub stmntsys_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DataGrid1.Hide()
        Label10.Text = Now.ToString("hh:mm:ss tt")
        Label13.Text = Format(Now, "dd/MM/yyyy")

        Dim con As New SqlConnection(str)
        Dim da1 As New SqlDataAdapter
        Dim ds1 As New DataSet
        da1 = New SqlDataAdapter("select * from bill", con)
        da1.Fill(ds1)

        Dim i As Integer
        For i = 0 To ds1.Tables(0).Rows.Count - 1
            Label11.Text = ds1.Tables(0).Rows(i)(0)
        Next
        Label11.Text = Label11.Text + 1

        Dim da2 As New SqlDataAdapter("select * from bill1", con)
        Dim ds2 As New DataSet
        DataGrid2.Refresh()
        da2.Fill(ds2)
        DataGrid2.Refresh()
        DataGrid2.DataSource = ds2.Tables(0)
        ButtonClose.Hide()
        txticode.Focus()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()

            cm.CommandText = "Insert into bill values(" & Val(Label11.Text) & ",'" & txttotal.Text & "','" & Label13.Text & "','" & Label10.Text & "')"
            cm.ExecuteNonQuery()

            cm.CommandText = "Insert into netamt values(" & Val(txttotal.Text) & "," & Val(Label11.Text) & ")"
            cm.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CrystalReportViewer1.RefreshReport()
        CrystalReportViewer1.PrintReport()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            'remove listbox
            txticode.Text = ""
            txtiname.Text = ""
            txtprice.Text = ""
            txtqty.Text = ""
            Label8.Text = ""
            Label10.Text = Now.ToString("hh:mm:ss tt")
            TextBox1.Text = "1"
            txttotal.Text = ""
            Label14.Text = ""
            txttaxamt.Text = ""
            txticode.Focus()

            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            cm.CommandText = "delete from bill1"
            cm.ExecuteNonQuery()

            ' Refresh()
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from bill1", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

            cm.Connection = con
            cm.CommandText = "delete from netamt"
            cm.ExecuteNonQuery()

            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from bill", con)
            da1.Fill(ds1)

            If ds1.Tables(0).Rows.Count > 0 Then
                Label11.Text = ds1.Tables(0).Rows.Count + 1
            Else
                Label11.Text = 1
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CrystalReportViewer1.RefreshReport()

    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        stkpurchase.Show()
    End Sub

    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub Button6_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Shell("calc.exe")
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Shell("notepad.exe", AppWinStyle.NormalFocus)
    End Sub
    Private Sub total()
        Try
            Dim con As New SqlConnection(str)
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from bill1", con)
            da1.Fill(ds1)
            Dim count As Decimal = 0
            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                count = count + (ds1.Tables(0).Rows(i)(4))
            Next
            txttotal.Text = count
            Label8.Text = Format(count, "#,###.##")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub newbill()
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from bill", con)
            da1.Fill(ds1)
            Dim i As Integer
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                Label11.Text = ds1.Tables(0).Rows(i)(0)
            Next
            Label11.Text = Label11.Text + 1

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Label8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        Button1.Show()
        ButtonClose.Hide()
        DataGrid1.Hide()
        Buttonexit.Show()
        txticode.Focus()
    End Sub
    Private Sub remove()
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim qty As Integer
            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            Dim y As Integer
            y = InputBox("Enter the Serial Number")

            Dim da8 As New SqlDataAdapter("select * from bill1 where [Si No]='" & y & "'", con)
            Dim ds8 As New DataSet
            da8.Fill(ds8)
            Dim qty1 As Integer
            qty1 = ds8.Tables(0).Rows(0)(3)
            MsgBox(qty)

            Dim x As Integer
            x = InputBox("Enter the Item Code")
            da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & x & "'", con)
            da1.Fill(ds1)

            qty = Trim(ds1.Tables(0).Rows(0)(6)) + Val(qty1)

            Dim da6 As New SqlDataAdapter("delete from bill1 where [si no]='" & y & "'", con)
            Dim ds6 As New DataSet
            da6.Fill(ds6)
            cm.Connection = con
            cm.Connection.Open()

            cm.CommandText = "Update stockentry set quantity=" & Val(qty) & " where [Item code]='" & x & "'"
            cm.ExecuteNonQuery()

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from bill1", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

            MsgBox("Removed Successfully")
            txticode.Text = ""
            txtiname.Text = ""
            txtprice.Text = ""
            txtqty.Text = ""
            txttaxamt.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub print()
        Try
            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()

            cm.CommandText = "Insert into bill values(" & Val(Label11.Text) & ",'" & txttotal.Text & "','" & Label13.Text & "','" & Label10.Text & "')"
            cm.ExecuteNonQuery()

            cm.CommandText = "Insert into netamt values(" & Val(txttotal.Text) & "," & Val(Label11.Text) & ")"
            cm.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CrystalReportViewer1.RefreshReport()
        CrystalReportViewer1.PrintReport()
    End Sub
    Private Sub bill()
        Try

            Label8.Text = ""
            Label10.Text = Now.ToString("hh:mm:ss tt")
            TextBox1.Text = "1"
            txttaxamt.Text = ""
            txttotal.Text = ""
            Label14.Text = ""
            txticode.Focus()

            Dim con As New SqlConnection(str)
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            cm.CommandText = "delete from bill1"
            cm.ExecuteNonQuery()

            ' Refresh()
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from bill1", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)

            cm.Connection = con
            cm.CommandText = "delete from netamt"
            cm.ExecuteNonQuery()

            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from bill", con)
            da1.Fill(ds1)

            If ds1.Tables(0).Rows.Count > 0 Then
                Label11.Text = ds1.Tables(0).Rows.Count + 1
            Else
                Label11.Text = 1
            End If

            CrystalReportViewer1.RefreshReport()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub txticode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txticode.TextChanged

    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub

    Private Sub DataGrid1_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid1.Navigate

    End Sub

    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load

    End Sub

    Private Sub txtqty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtqty.TextChanged

    End Sub

    Private Sub txttotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotal.TextChanged

    End Sub

    Private Sub txttaxamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttaxamt.TextChanged

    End Sub

    Private Sub Buttonexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonexit.Click
        adminmain.Show()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

    End Sub
End Class
