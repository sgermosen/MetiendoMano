Imports System.Data.SqlClient

'Public Class Usages

'    Dim datacore As New Ps.Data.MsSqlDataCore
'    Dim sql As String
'    Dim ds As New DataSet
'    Dim A9999999999 As String
'    Dim odao As New Ps.Data.MsSqlDataCore

'    Public Function ExampleUsage()

'        datacore.GetDataIdSet(sql).Fill(ds)


'        'poner un procedure que llame a los procedures que estan regados 
'        Try
'            datacore.BeginTrans()

'            datacore.ExecuteCommandScalarTrans("99999.99999999999",
'New SqlParameter("9999999999", SqlDbType.VarChar, A9999999999, ParameterDirection.Input),
'New SqlParameter("99999999999", SqlDbType.VarChar, A9999999999, ParameterDirection.Input),
'New SqlParameter("tip", SqlDbType.SmallInt, A9999999999, ParameterDirection.Input)
'                        )
'            datacore.CommitTrans()
'        Catch ex As Exception
'            datacore.RollbackTrans()
'        End Try



'        ''traer datos 
'        'Ds = New DataSet
'        'oDao.OpenConnection()
'        'Dim oArray() As Object = {1111111, 22222222, 3333333, 444444, 555555, Nothing}
'        'Dim pArray() As Object = {"RecordsReturn"}
'        'oDao.FillCommand("9999999.99999999", oArray, "1111111, 2222222, 3333333, 4444444, 5555555, RecordsReturn", pArray)
'        'oDao.Command.Parameters("RecordsReturn").SqlDbType = SqlDbType.Structured
'        'Ds = oDao.ExecuteCursor()
'        'oDao.CloseConnection()
'        'Return ds


'        'traer datos que ya sabremos es cursor

'        'Ds = New DataSet
'        'Dim oArray2() As Object = {1111111111, 222222222, 3333333, 444444444, 5555555, Nothing}
'        'Dim pArray2() As Object = {"RecordsReturn"}
'        'ds = oDao.ReturnDsFromCursor("000000.000000000", oArray2, "1111111, 2222222, 3333333, 4444444, 5555555, RecordsReturn", pArray2, "RecordsReturn")
'        'Return Ds


'        'this insertando

'        'Try
'        '    oDao.OpenConnection()
'        '    oDao.BeginTransaction()

'        '    Dim oArray3() As Object = {1111111, 22222222, 3333333, 444444, 555555}
'        '    oDao.FillCommand("0000000.000000000", oArray3, "1111111, 2222222, 3333333, 4444444, 5555555")

'        '    oDao.Command.ExecuteNonQuery()

'        '    oDao.CommitTransaction()
'        '    oDao.CloseConnection()
'        '    Return True

'        'Catch ex As Exception
'        '    oDao.RollBackTransaction()
'        '    oDao.CloseConnection()
'        '    Return False
'        'End Try



'        'insertando o ejecutando procedure

'        'oDao.OpenConnection()
'        'oDao.BeginTransaction()
'        'Dim oArray4() As Object = {1111111, 22222222, 3333333, 444444, 555555}
'        'oDao.FillCommand("0000000.000000000", oArray4, "1111111, 2222222, 3333333, 4444444, 5555555")

'        'If oDao.Command.ExecuteNonQuery() = 0 Then
'        '    oDao.RollBackTransaction()
'        '    Exit Function
'        'End If
'        'oDao.CloseConnection()
'        'oDao.CommitTransaction()





'        'returnnando valor
'        Dim variable As Decimal 'as tipo de datos 
'        ds = New DataSet
'        oDao.OpenConnection()
'        'el ultimo parametro debe ser del tipo de la variable retorno si mando nothing debo especificar el parameter type
'        Dim oArray5() As Object = {999999, 99999999, 99999999, 99999999, 99999999, 0} 'el ultimo parametro debe ser del tipo de la variable retorno si mando nothing debo especificar el parameter type
'        Dim pArray5() As Object = {"@ReturnId"}
'        oDao.FillCommand("9999999999", oArray5, "@999999,@999999,@999999,@999999,@999999,@999999,@ReturnId", pArray5)
'        'solo necesario si mandare nothing en el paramdirection
'        'oDao.Command.Parameters("@ReturnId").SqlDbType = SqlDbType.int ' formato variable 
'        oDao.Command.ExecuteNonQuery()
'        variable = oDao.Command.Parameters("@ReturnId").Value



'    End Function


'End Class
