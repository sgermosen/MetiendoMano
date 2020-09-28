Public Class adminmain

    Private Sub OrderingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockDetailsToolStripMenuItem.Click
        stockdet.Show()
    End Sub

    Private Sub OrderReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReportToolStripMenuItem.Click
        purchaserep.Show()
    End Sub

    Private Sub SalesReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesReportToolStripMenuItem.Click
        salesrep.Show()
    End Sub

    Private Sub StockDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockPurchaseToolStripMenuItem.Click
        stkpurchase.Show()
    End Sub

    Private Sub BillTransactionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReturnToolStripMenuItem.Click
        stkreturn.Show()
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        chngpass.Show()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        Dim ans As Integer
        ans = MsgBox("Are you sure you want to exit this application?", vbYesNo + vbQuestion, "Exit ")
        If ans = MsgBoxResult.Yes Then
            End
        Else
            Exit Sub
        End If

    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        frmabout.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick 
        Label1.ForeColor = System.Drawing.ColorTranslator.FromOle(RGB(Rnd() * 255, Rnd() * 255, Rnd() * 255))
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

    End Sub

    Private Sub adminmain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MenuStrip1.Focus()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Shell("calc.exe", AppWinStyle.NormalFocus)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Shell("Notepad.exe", AppWinStyle.NormalFocus)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        billing.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        stockdet.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        stkpurchase.Show()
    End Sub

    Private Sub ReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillTransactionToolStripMenuItem.Click
        billtnsdet.Show()
    End Sub

    Private Sub ReturnReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReturnReportToolStripMenuItem.Click
        stkreturnrep.Show()
    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub ReturnDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillToolStripMenuItem.Click
        billing.Show()
    End Sub

    Private Sub PurchaseDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerMedicineToolStripMenuItem.Click
        custmed.Show()
    End Sub

    Private Sub CustomerReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReturnDetailToolStripMenuItem.Click
        stkretdet.Show()
    End Sub

    Private Sub CustomerReturnReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerMedicineReturnReportToolStripMenuItem.Click
        custmedrep.Show()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub CustomerReturnDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerMedicineDetailsToolStripMenuItem.Click
        custmeddet.show()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierToolStripMenuItem.Click
        supplier.Show()
    End Sub

    Private Sub SupplierDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierDetailsToolStripMenuItem.Click
        supplierdetail.Show()
    End Sub
End Class