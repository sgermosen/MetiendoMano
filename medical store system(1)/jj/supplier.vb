Imports System.Data.SqlClient
Public Class supplier
    Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")

    Private Sub supplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim da1 As New SqlDataAdapter
        Dim ds1 As New DataSet
        da1 = New SqlDataAdapter("select * from supplier", con)
        da1.Fill(ds1)
        Label11.Text = Format(Now, "dd/MM/yyyy")
        dataGrid1.DataSource = ds1.Tables(0)
    End Sub

    Private Sub Buttonsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonsave.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            cm.Connection = con
            cm.Connection.Open()
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet
            da1 = New SqlDataAdapter("select * from supplier ", con)
            da1.Fill(ds1)

            If (txtsupid.Text = 0) Then
                MsgBox("Enter the Supplier ID Number other than Zero")
                txtsupid.Focus()
            ElseIf txtsupid.Text = "" Then
                MsgBox("Enter the Supplier ID")
                txtsupid.Focus()
            ElseIf txtsupname.Text = "" Then
                MsgBox("Enter the Supplier Name")
                txtsupname.Focus()
            ElseIf txtcompname.Text = "" Then
                MsgBox("Enter the Company Name")
                txtcompname.Focus()
            ElseIf txtphone.Text = "" Then
                MsgBox("Enter the Valid Phone Number")
                txtphone.Focus()
            ElseIf txtfax.Text = "" Then
                MsgBox("Enter the Valid Fax Number")
                txtfax.Focus()
            End If

            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                If (txtsupid.Text = Val(ds1.Tables(0).Rows(i)("Supplier ID"))) Then
                    MsgBox("Enter Supplier ID other than used")
                    txtsupid.Focus()
                    Exit Sub
                End If
            Next

            cm.CommandText = "Insert into supplier values('" & txtsupid.Text & "','" & txtsupname.Text & "', '" & txtcompname.Text & "','" & txtphone.Text & "','" & txtfax.Text & "')"
            cm.ExecuteNonQuery()

            MsgBox("Added Successfully")

            txtsupid.Text = ""
            txtsupname.Text = ""
            txtcompname.Text = ""
            txtphone.Text = ""
            txtfax.Text = ""

            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            da2 = New SqlDataAdapter("select * from supplier", con)
            da2.Fill(ds2)
            dataGrid1.DataSource = ds2.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtsupid.Focus()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            Dim con As New SqlConnection("Initial Catalog=stock;Data source=.;Integrated security =true")
            Dim cm As New SqlCommand
            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            cm.Connection = con
            cm.Connection.Open()

            If txtsupid.Text = "" Then
                MsgBox("Enter the Supplier ID")
                txtsupid.Focus()
            ElseIf txtsupname.Text = "" Then
                MsgBox("Enter the Supplier Name")
                txtsupname.Focus()
            ElseIf txtcompname.Text = "" Then
                MsgBox("Enter the Company Name")
                txtcompname.Focus()
            ElseIf txtphone.Text = "" Then
                MsgBox("Enter the Valid Phone Number")
                txtphone.Focus()
            ElseIf txtfax.Text = "" Then
                MsgBox("Enter the Valid Fax Number")
                txtfax.Focus()
            Else

                da2 = New SqlDataAdapter("select * from supplier where [Supplier ID]='" & txtsupid.Text & "'", con)
                da2.Fill(ds2)

                If ds2.Tables(0).Rows.Count < 1 Then
                    MsgBox("Invalid Supplier ID number")
                Else

                    Dim da3 As New SqlDataAdapter
                    Dim ds3 As New DataSet
                    da3 = New SqlDataAdapter("select * from supplier where [Supplier ID]='" & txtsupid.Text & "'", con)
                    da3.Fill(ds3)

                    cm.CommandText = "Update supplier set [Supplier ID]='" & txtsupid.Text & "',[Supplier Name]='" & txtsupname.Text & "',[Company Name]='" & txtcompname.Text & "',[Phone Number]='" & txtphone.Text & "',[Fax Number]='" & txtfax.Text & "' where [Supplier ID]='" & txtsupid.Text & "'"
                    cm.ExecuteNonQuery()
                    MsgBox("Updated Successfully.")

                    txtsupid.Text = ""
                    txtsupname.Text = ""
                    txtcompname.Text = ""
                    txtphone.Text = ""
                    txtfax.Text = ""

                    Dim da4 As New SqlDataAdapter
                    Dim ds4 As New DataSet
                    da4 = New SqlDataAdapter("select * from supplier", con)
                    da4.Fill(ds4)
                    dataGrid1.DataSource = ds4.Tables(0)
                End If
            End If
            txtsupid.Enabled = True
            txtsupname.Enabled = True
            txtcompname.Enabled = True
            txtsupid.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim da1 As New SqlDataAdapter
            Dim ds1 As New DataSet

            da1 = New SqlDataAdapter("select * from supplier where [Supplier ID]='" & txtsupid.Text & "'", con)
            da1.Fill(ds1)

            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("Invalid Supplier ID Number")
            Else
                txtsupid.Text = Trim(ds1.Tables(0).Rows(0)(0))
                txtsupname.Text = Trim(ds1.Tables(0).Rows(0)(1))
                txtcompname.Text = Trim(ds1.Tables(0).Rows(0)(2))
                txtphone.Text = Trim(ds1.Tables(0).Rows(0)(3))
                txtfax.Text = Trim(ds1.Tables(0).Rows(0)(4))
                txtsupid.Enabled = False
                txtsupname.Enabled = False
                txtcompname.Enabled = False

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        txtsupname.Focus()
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
            da1 = New SqlDataAdapter("select * from supplier ", con)
            da1.Fill(ds1)
            If ds1.Tables(0).Rows.Count < 1 Then
                MsgBox("No more Suppliers are in the list")
                txtsupid.Focus()
            ElseIf MsgBox("Are You Sure Delete Supplier Details ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                cm.CommandText = "Delete from  supplier  where [Supplier ID]= '" & txtsupid.Text & "'"
                cm.ExecuteNonQuery()

                MsgBox("Successfully Deleted")

                txtsupid.Text = ""
                txtsupname.Text = ""
                txtcompname.Text = ""
                txtphone.Text = ""
                txtfax.Text = ""
                txtsupid.Focus()
            End If

            Dim da3 As New SqlDataAdapter
            Dim ds3 As New DataSet
            da3 = New SqlDataAdapter("select * from supplier", con)
            da3.Fill(ds3)
            dataGrid1.DataSource = ds3.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Buttonclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonclear.Click
        txtsupid.Enabled = True
        txtsupname.Enabled = True
        txtcompname.Enabled = True
        txtsupid.Text = ""
        txtsupname.Text = ""
        txtcompname.Text = ""
        txtphone.Text = ""
        txtfax.Text = ""
        txtsupid.Focus()
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClose.Click
        adminmain.Show()
        Me.Close()
    End Sub
End Class