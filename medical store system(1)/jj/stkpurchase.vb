Imports System.Data.SqlClient
Public Class stkpurchase
    Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
    Private Sub stkpurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim da1 As New SqlDataAdapter
        Dim ds1 As New DataSet
        da1 = New SqlDataAdapter("select * from stockentry", con)
        da1.Fill(ds1)
        Label11.Text = Format(Now, "dd/MM/yyyy")
        DataGrid2.DataSource = ds1.Tables(0)

    End Sub

    Private Sub CrystalReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Buttonsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonsave.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from stockentry ", con)
            da1.Fill(ds1)
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            da2 = New SqlDataAdapter("select * from supplier ", con)
            da2.Fill(ds2)

            If (txticode.Text = 0) Then
                MsgBox("Enter the Item Code Number other than Zero")
                txticode.Focus()
            ElseIf txticode.Text = "" Then
                MsgBox("Enter the Item Code Number")
                txticode.Focus()
            ElseIf txtsupid.Text = "" Then
                MsgBox("Enter the Supplier ID")
                txtsupid.Focus()
            ElseIf txtbatch.Text = "" Then
                MsgBox("Enter the Batch Number")
                txtbatch.Focus()
            ElseIf txtiname.Text = "" Then
                MsgBox("Enter the Medicine Name")
                txtiname.Focus()
            ElseIf txtcompany.Text = "" Then
                MsgBox("Enter the Company Name")
                txtcompany.Focus()
            ElseIf txtexpiry.Text = "" Then
                MsgBox("Enter the Expiry Date")
                txtexpiry.Focus()
            ElseIf unitprice.Text = "" Then
                MsgBox("Enter the Unit Price")
                unitprice.Focus()
            ElseIf txtqty.Text = "" Then
                MsgBox("Enter the Quantity")
                txtqty.Focus()
            ElseIf TextBox5.Text = "" Then
                MsgBox("Enter the Tax Amount")
                TextBox5.Focus()
            ElseIf Label13.Text = "" Then
                MsgBox("For Amount Press Amount Button")
                Label13.Focus()
            ElseIf txttotal.Text = "" Then
                MsgBox("Click Amount Button to Enter the Amount")
            End If

            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                If (txticode.Text = Val(ds1.Tables(0).Rows(i)("item code"))) Then
                    MsgBox("Enter itemcode other than used")
                    txticode.Focus()
                    Exit Sub
                End If
            Next
            For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                If (txtsupid.Text = Val(ds2.Tables(0).Rows(i)("Supplier ID"))) Then

                    TextBox1.Text = Val(TextBox1.Text) + Val(txtqty.Text)

                    cm.CommandText = "Insert into stockentry values('" & Label11.Text & "','" & txtsupid.Text & "','" & txtcompany.Text & "', '" & txticode.Text & "','" & txtiname.Text & "','" & Val(unitprice.Text) & "','" & Val(TextBox1.Text) & "','" & txtbatch.Text & "','" & txtexpiry.Text & "','" & TextBox5.Text & "','" & txttotal.Text & "')"
                    cm.ExecuteNonQuery()

                    MsgBox("Added Successfully")
                    txticode.Text = ""
                    txtsupid.Text = ""
                    txtiname.Text = ""
                    txtqty.Text = ""
                    txttotal.Text = ""
                    unitprice.Text = ""
                    txtprice.Text = ""
                    TextBox1.Text = ""
                    TextBox4.Text = ""
                    txtexpiry.Text = ""
                    Label13.Text = ""
                    Label15.Text = ""
                    txtcompany.Text = ""
                    txtbatch.Text = ""
                    TextBox5.Text = ""

              
            Dim da3 As New SqlDataAdapter
            Dim ds3 As New DataSet
            da3 = New SqlDataAdapter("select * from stockentry", con)
            da3.Fill(ds3)
            DataGrid2.DataSource = ds3.Tables(0)
                    txticode.Focus()
                    Exit Sub
                End If
            Next
            MsgBox("Enter Valid Supplier ID Number")
            txtsupid.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txttotal.Text = unitprice.Text * txtqty.Text
    End Sub

    Private Sub txttotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotal.TextChanged

    End Sub

    Private Sub txtqty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtqty.KeyDown
        If e.KeyCode = 13 Then
            txtcompany.Focus()
        End If
    End Sub

    Private Sub txtqty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtqty.TextChanged
        

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If unitprice.Text = "" Then
                MsgBox("Enter unit price")
                unitprice.Focus()
            ElseIf txtqty.Text = "" Then
                MsgBox("Enter the Quantity")
                txtqty.Focus()
            ElseIf TextBox5.Text = "" Then
                MsgBox("Enter the Tax Amount")
                TextBox5.Focus()
            ElseIf (TextBox5.Text = 0) Then
                txttotal.Text = unitprice.Text * txtqty.Text
                Label13.Text = txttotal.Text
            Else
                Dim a As Double
                Dim b As Double
                b = TextBox5.Text / 100
                a = unitprice.Text * txtqty.Text
                txttotal.Text = a + (unitprice.Text * txtqty.Text * b)
                Label13.Text = txttotal.Text
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet

            da1 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
            da1.Fill(ds1)

            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("Invalid Item Code Number")
           
            Else
                txtiname.Text = Trim(ds1.Tables(0).Rows(0)(4))
                txtsupid.Text = Trim(ds1.Tables(0).Rows(0)(1))
                TextBox1.Text = Trim(ds1.Tables(0).Rows(0)(6))
                unitprice.Text = Trim(ds1.Tables(0).Rows(0)(5))
                TextBox4.Text = Trim(ds1.Tables(0).Rows(0)(10))
                txtcompany.Text = Trim(ds1.Tables(0).Rows(0)(2))
                Label15.Text = Trim(ds1.Tables(0).Rows(0)(7))
                txtbatch.Text = Trim(ds1.Tables(0).Rows(0)(7))
                txtprice.Text = Trim(ds1.Tables(0).Rows(0)(10))
                txtexpiry.Text = Trim(ds1.Tables(0).Rows(0)(8))
                TextBox5.Text = Trim(ds1.Tables(0).Rows(0)(9))
                txticode.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        txtqty.Focus()

    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()

            If MsgBox("Are You Sure Delete all Stock Returns ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "delete from stockreturn"
                cm.ExecuteNonQuery()
                cm.CommandText = "delete from re"
                cm.ExecuteNonQuery()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            Dim qty As Integer
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            cm.Connection = con
            cm.Connection.Open()
            If txtsupid.Text = "" Then
                MsgBox("Enter the Supplier ID")
                txtsupid.Focus()
            ElseIf txtbatch.Text = "" Then
                MsgBox("Enter the Batch Number")
                txtbatch.Focus()
            ElseIf txtiname.Text = "" Then
                MsgBox("Enter the Medicine Name")
                txtiname.Focus()
            ElseIf txtcompany.Text = "" Then
                MsgBox("Enter the Company Name")
                txtcompany.Focus()
            ElseIf txtexpiry.Text = "" Then
                MsgBox("Enter the Expiry Date")
                txtexpiry.Focus()
            ElseIf unitprice.Text = "" Then
                MsgBox("Enter the Unit Price")
                unitprice.Focus()
            ElseIf txtqty.Text = "" Then
                MsgBox("Enter the Quantity")
                txtqty.Focus()
            ElseIf TextBox5.Text = "" Then
                MsgBox("Enter the Tax Amount")
                TextBox5.Focus()
            ElseIf Label13.Text = "" Then
                MsgBox("For Amount Press Amount Button")
                Label13.Focus()
            ElseIf txttotal.Text = "" Then
                MsgBox("Click Amount Button to Enter the Amount")
            Else

                Dim da1 As New SqlDataAdapter
                Dim ds1 As New DataSet
                da1 = New SqlDataAdapter("select * from supplier ", con)
                da1.Fill(ds1)
                

                'If (TextBox5.Text = 0) Then
                '    txttotal.Text = unitprice.Text * txtqty.Text
                '    Label13.Text = txttotal.Text
                'Else
                '    txttotal.Text = unitprice.Text * txtqty.Text * TextBox5.Text
                '    Label13.Text = txttotal.Text
                'End If
                TextBox4.Text = Val(TextBox1.Text) + Val(txtqty.Text)

                da2 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                da2.Fill(ds2)

                If ds2.Tables(0).Rows.Count < 1 Then
                    MsgBox("Invalid code")
                Else
                    For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                        If (txtsupid.Text = Val(ds1.Tables(0).Rows(i)("Supplier ID"))) Then
                            
                    qty = Trim(ds2.Tables(0).Rows(0)(6)) + Val(txtqty.Text)

                    Dim amt As Decimal
                    Dim da3 As New SqlDataAdapter
                    Dim ds3 As New DataSet
                    da3 = New SqlDataAdapter("select * from stockentry where [Item code]='" & txticode.Text & "'", con)
                    da3.Fill(ds3)

                    amt = Trim(ds3.Tables(0).Rows(0)(10)) + Val(Label13.Text)

                    cm.CommandText = "Update stockentry set [Item code]='" & txticode.Text & "',[Batch No]='" & txtbatch.Text & "',[Supplier ID]='" & txtsupid.Text & "',[Medicine name]='" & txtiname.Text & "',[company]='" & txtcompany.Text & "',[Unit Price]='" & unitprice.Text & "',[expiry date]='" & txtexpiry.Text & "',[tax amount]='" & TextBox5.Text & "',[quantity]='" & qty & "',[amount]='" & amt & "' where [Item code]='" & txticode.Text & "'"
                    cm.ExecuteNonQuery()
                    MsgBox("Updated Successfully.")

                    txticode.Text = ""
                    txtsupid.Text = ""
                    txtiname.Text = ""
                    txtqty.Text = ""
                    txttotal.Text = ""
                    unitprice.Text = ""
                    txtprice.Text = ""
                    TextBox1.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                    txtbatch.Text = ""
                    txtexpiry.Text = ""
                    txtcompany.Text = ""
                    Label13.Text = ""
                    Label15.Text = ""
                    txtcompany.Text = ""

                    Dim da4 As New SqlDataAdapter
                    Dim ds4 As New DataSet
                    da4 = New SqlDataAdapter("select * from stockentry", con)
                    da4.Fill(ds4)
                    DataGrid2.DataSource = ds4.Tables(0)
                            txticode.Enabled = True
                            txticode.Focus()
                            Exit Sub
                        End If
                    Next
                    MsgBox("Enter Valid Supplier ID")
                    txtsupid.Focus()
                End If
            End If
            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim da2 As New SqlDataAdapter
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            Dim ds2 As New DataSet
            Dim cm As New SqlCommand

            cm.Connection = con
            cm.Connection.Open()
            da1 = New SqlDataAdapter("select * from stockentry ", con)
            da1.Fill(ds1)
            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("No more stocks are in the list")
                txticode.Focus()
            ElseIf MsgBox("Are You Sure Delete Stock Purchase ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
              
                cm.CommandText = "Delete from  stockentry  where [Item code]= '" & txticode.Text & "'"
                cm.ExecuteNonQuery()

                MsgBox("Successfully Deleted")
                txticode.Text = ""
                txtsupid.Text = ""
                txtiname.Text = ""
                txtprice.Text = ""
                txtqty.Text = ""
                TextBox1.Text = ""
                unitprice.Text = ""
                txttotal.Text = ""
                txtcompany.Text = ""
                TextBox5.Text = ""
                txtbatch.Text = ""
                txtexpiry.Text = ""
                Label15.Text = ""
                Label13.Text = ""
                txticode.Focus()
            End If

            Dim da3 As New SqlDataAdapter
            Dim ds3 As New DataSet
            da3 = New SqlDataAdapter("select * from stockentry", con)
            da3.Fill(ds3)
            DataGrid2.DataSource = ds3.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Buttonnewitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonnewitem.Click
        txticode.Enabled = True
        txticode.Text = ""
        txtsupid.Text = ""
        txtiname.Text = ""
        txtqty.Text = ""
        txttotal.Text = ""
        unitprice.Text = ""
        txtprice.Text = ""
        TextBox1.Text = ""
        TextBox4.Text = ""
        txtexpiry.Text = ""
        Label13.Text = ""
        Label15.Text = ""
        txtcompany.Text = ""
        txtbatch.Text = ""
        TextBox5.Text = ""
        txticode.Focus()
    End Sub

    Private Sub Buttonclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonclear.Click
        txticode.Enabled = True
        txticode.Text = ""
        txtsupid.Text = ""
        txtiname.Text = ""
        txtqty.Text = ""
        txttotal.Text = ""
        unitprice.Text = ""
        txtprice.Text = ""
        TextBox1.Text = ""
        TextBox4.Text = ""
        txtexpiry.Text = ""
        Label13.Text = ""
        Label15.Text = ""
        txtcompany.Text = ""
        txtbatch.Text = ""
        TextBox5.Text = ""
        txticode.Focus()
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            cm.Connection = con
            cm.Connection.Open()
            da1 = New SqlDataAdapter("select * from stockentry ", con)
            da1.Fill(ds1)
            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("No more stocks are in the list")
            ElseIf MsgBox("Are You Sure Delete all Stock Purchase ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                cm.CommandText = "delete from stockentry"
                cm.ExecuteNonQuery()
            End If

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("select * from stockentry", con)
            da.Fill(ds)
            DataGrid2.DataSource = ds.Tables(0)
            txticode.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub unitprice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles unitprice.KeyDown
        If e.KeyCode = 13 Then
            txtqty.Focus()
        End If
    End Sub

    Private Sub txtcompany_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcompany.KeyDown
        If e.KeyCode = 13 Then
            Button1.Focus()
        End If
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        adminmain.Show()
        Me.Close()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub txtprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub

    Private Sub txtbatch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbatch.TextChanged

    End Sub

    Private Sub txtcompany_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcompany.TextChanged

    End Sub

    Private Sub txttaxper_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtexpiry_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtexpiry.TextChanged

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Shell("calc.exe")
    End Sub

    Private Sub unitprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles unitprice.TextChanged

    End Sub

    Private Sub Label8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txticode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txticode.TextChanged

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub
End Class