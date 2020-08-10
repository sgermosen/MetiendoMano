Imports System.Data.SqlClient

Public Class DbProces
    Dim Conect As New Ps.Datacore.ModConexion 'Ps.Data.ModConexion ' ModConexion.ModConexion
    Dim cmd As SqlCommand
    Public x As Integer
    'Dim Sqld_adapter As New SqlDataAdapter("SELECT * FROM paciente", conect.sql_cnn)
    Dim d_set As New DataSet
    Dim ds As New DataSet
    Dim Lee As SqlDataReader
    Dim Condicion As String
    Dim Control As Integer = 0

    Dim dv As New DataView
    Public Function GeneraCódigo(Local As Boolean) As String
        Dim strCodigo As String

        If Conect.OpenConection(Local) = False Then
            If Local = True Then
                Return "ERROR"
            End If
        End If

        strCodigo = Conect.ExecuteScalar("select max(record) from paciente")

        Try
            strCodigo = strCodigo + 1
            Return Format(strCodigo, "000000")
        Catch ex As Exception
            Return "000001"
        End Try
    End Function

End Class
