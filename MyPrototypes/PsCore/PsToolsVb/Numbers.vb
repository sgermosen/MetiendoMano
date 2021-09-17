Public Class Numbers
    Public Function VNulo(numero As Object) As Object
        If Trim(numero) = "" Then
            VNulo = "0.00"
        Else
            VNulo = Format(CSng(numero), "###,###,###,##0.00")
        End If
    End Function

End Class
