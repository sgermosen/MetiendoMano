
'Public Class DbProcess

'    Dim DataCore As New Ps.Data.MsSqlDataCore
'    Dim DbUtils As New Ps.Data.DbUtils
'    Dim Ds As New DataSet
'    Public Function SavePacientes(Dec As Byte, ID As Integer, record As String, record2 As String) As String
'        'Dim variable As Decimal 'as tipo de datos 
'        ' Ds = New DataSet
'        DataCore.OpenConnection()
'        Dim oArray5() As Object = {Dec, ID, record, record2, 0}
'        Dim pArray5() As Object = {"@Sucess"}
'        DataCore.FillCommand("SavePacientes", oArray5, "@Dec,@per_id,@record,@record2,@Sucess", pArray5)
'        'oDao.Command.Parameters("@ReturnId").SqlDbType = SqlDbType.in ' formato variable 
'        DataCore.Command.ExecuteNonQuery()
'        ID = DataCore.Command.Parameters("@Sucess").Value
'        DataCore.CloseConnection()
'        Return ID

'    End Function

'End Class
