Imports System.Text
Namespace Ps.Datacore
    '    Public Class DbTools
    '        Dim SqlStr As New StringBuilder
    '        Dim DataCore As New Ps.Datacore.SqlDataCore
    '        Dim Ds As New DataSet
    '        Public Function ListEstatus(Optional id As Byte = 0, Optional Codigo As String = "",
    '                                    Optional tabla As String = "", Optional descripcion As String = "") As DataSet
    '            SqlStr.Clear()
    '            SqlStr.Append("select * from EstatusM ")
    '            If Codigo <> "" Or tabla <> "" Or descripcion <> "" Or id <> 0 Then
    '                SqlStr.Append(" where sts_id=sts_id ")
    '                If Codigo <> "" Then
    '                    SqlStr.Append(" and sts_codigo='" & Codigo & "'")
    '                End If
    '                If tabla <> "" Then
    '                    SqlStr.Append(" and sts_tablas='" & tabla & "'")
    '                End If
    '                If descripcion <> "" Then
    '                    SqlStr.Append(" and sts_descripcion='" & descripcion & "'")
    '                End If
    '                If id <> 0 Then
    '                    SqlStr.Append(" and sts_id=" & id & "")
    '                End If
    '            End If
    '            DataCore.GetDataIdSet(SqlStr.ToString).Fill(Ds)
    '            Return Ds
    '        End Function
    '        Public Function ListPuestos(Optional id As Byte = 0, Optional Nombre As String = "") As DataSet
    '            SqlStr.Clear()
    '            SqlStr.Append("select * from EmpPuestos ")
    '            If Nombre <> "" Or id <> 0 Then
    '                SqlStr.Append(" where pues_id=pues_id ")
    '                If Nombre <> "" Then
    '                    SqlStr.Append(" and pues_nombre='" & Nombre & "'")
    '                End If

    '                If id <> 0 Then
    '                    SqlStr.Append(" and pues_id=" & id & "")
    '                End If
    '            End If
    '            DataCore.GetDataIdSet(SqlStr.ToString).Fill(Ds)
    '            Return Ds
    '        End Function
    '        Public Function NextLaborDay(ByVal pdFecha As Date) As Date
    '            Dim ldFechaAux As Date
    '            Dim lbDiaHabil As Boolean
    '            ldFechaAux = pdFecha
    '            lbDiaHabil = False
    '            Do While lbDiaHabil = False
    '                lbDiaHabil = True
    '                Select Case Weekday(ldFechaAux)
    '                    Case Is = vbSunday
    '                        ldFechaAux = ldFechaAux.AddDays(1)
    '                        lbDiaHabil = False
    '                    Case Is = vbSaturday
    '                        ldFechaAux = ldFechaAux.AddDays(2)
    '                        lbDiaHabil = False
    '                    Case Else
    '                        lbDiaHabil = True
    '                End Select
    '                If IsDiaFeriado(ldFechaAux) Then
    '                    ldFechaAux = ldFechaAux.AddDays(2)
    '                    lbDiaHabil = False
    '                Else
    '                    lbDiaHabil = True
    '                End If

    '            Loop
    '            Return dFechaAux
    '        End Function
    '        Public Function FechaAntesDelVencimiento(ByVal pdFecha As Date) As Date
    '            Dim ldFechaAux As Date
    '            Dim lbDiaHabil As Boolean
    '            ldFechaAux = pdFecha
    '            lbDiaHabil = False
    '            Do While lbDiaHabil = False
    '                lbDiaHabil = True
    '                Select Case Weekday(ldFechaAux)
    '                    Case Is = vbSunday
    '                        ldFechaAux = ldFechaAux.AddDays(1)
    '                        lbDiaHabil = False
    '                    Case Is = vbSaturday
    '                        ldFechaAux = ldFechaAux.AddDays(2)
    '                        lbDiaHabil = False
    '                    Case Else
    '                        '     lbDiaHabil = True
    '                End Select
    '                If IsDiaFeriado(ldFechaAux) Then
    '                    ldFechaAux = ldFechaAux.AddDays(2)
    '                    lbDiaHabil = False
    '                Else
    '                    lbDiaHabil = True
    '                End If

    '            Loop
    '            FechaAntesDelVencimiento = ldFechaAux

    '        End Function

    '        Public Function validar_feriado(ByRef Fecha As Date) As Date
    '            Dim selec As New DataSet
    '            Dim es_feriado As Boolean
    '            Dim nueva As Date
    '            Dim Numdia As Short
    '            es_feriado = True
    '            nueva = Fecha
    '            validar_feriado = nueva

    '            While es_feriado

    '                Ds = New DataSet
    '                Dim oArray() As Object = {Format(nueva, "yyyyMMdd"), Nothing}
    '                Dim pArray() As Object = {"RecordsReturn"}
    '                Ds = oDao.ReturnDsFromCursor("PKG_COMMON.ValidarFeriado", oArray, "Fec, RecordsReturn", pArray, "RecordsReturn")

    '                If Ds.Tables(0).Rows.Count = 0 Then ' No es feriado
    '                    Numdia = Weekday(nueva)
    '                    If Numdia = 1 Then 'Si es domingo
    '                        nueva = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, nueva)
    '                    ElseIf Numdia = 7 Then  'Si es sabado
    '                        nueva = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 2, nueva)
    '                    Else
    '                        es_feriado = False
    '                    End If
    '                Else
    '                    nueva = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, nueva)
    '                End If

    '            End While

    '        End Function
    '        Function VerificarDiaFeriado(ByRef Fecha As Date, Optional ByRef Municipio As Object = Nothing) As Date
    '            Dim rs As New DataSet
    '            Dim Busca As Boolean

    '            Busca = True
    '            VerificarDiaFeriado = Fecha
    '            ' Dim query As String
    '            Do While Busca
    '                '''''Para Verificar en Feriado
    '                Dim oArray() As Object = {Format(Fecha, "yyyyMMdd"), 0, Nothing}
    '                Dim pArray() As Object = {"RecordsReturn"}
    '                rs = oDao.ReturnDsFromCursor("PKG_COMMON.GetFeriado", oArray, "fecha, municipio, RecordsReturn", pArray, "RecordsReturn")
    '                If rs.Tables(0).Rows.Count > 0 Then
    '                    Fecha = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Fecha)
    '                    GoTo Salir
    '                End If
    '                '''''Para Verificar en Feriado Municipales
    '                If Trim(Municipio) <> "" Then
    '                    Ds = New DataSet
    '                    Dim oArray2() As Object = {Format(Fecha, "yyyyMMdd"), Municipio, Nothing}
    '                    Dim pArray2() As Object = {"RecordsReturn"}
    '                    rs = DataCore.ReturnDsFromCursor("PKG_COMMON.GetFeriado", oArray2, "fecha, municipio, RecordsReturn", pArray2, "RecordsReturn")
    '                    If rs.Tables(0).Rows.Count > 0 Then
    '                        Fecha = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Fecha)
    '                        GoTo Salir
    '                    End If
    '                End If
    '                '''''Para Verificar si es dia Sabado o Domingo
    '                VerificarDiaFeriado = System.DateTime.FromOADate(Weekday(Fecha))
    '                If VerificarDiaFeriado = System.DateTime.FromOADate(1) Then 'Si es domingo
    '                    Fecha = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Fecha)
    '                    GoTo Salir
    '                ElseIf VerificarDiaFeriado = System.DateTime.FromOADate(7) Then  'Si es sabado
    '                    Fecha = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 2, Fecha)
    '                    GoTo Salir
    '                End If
    '                Busca = False
    'Salir:
    '            Loop
    '            VerificarDiaFeriado = Fecha
    '        End Function
    '        Public Function SavePersonasM(Nombre As String, ApellidoP As String, ApellidoM As String, Status As String, cedula As String, Dec As Byte, Optional Id As String = "0") As String
    '            'Dim variable As Decimal 'as tipo de datos 
    '            Ds = New DataSet
    '            DataCore.OpenConnection()
    '            Dim oArray5() As Object = {Dec, Nombre, ApellidoP, ApellidoM, Status, cedula, Id, 0}
    '            Dim pArray5() As Object = {"@ReturnId"}
    '            DataCore.FillCommand("SavePersonas", oArray5, "@Dec,@per_nombre,@per_apellidop,@per_apellidom,@sts_id,@per_cedula,@Per_id,@ReturnId", pArray5)
    '            'oDao.Command.Parameters("@ReturnId").SqlDbType = SqlDbType.in ' formato variable 
    '            DataCore.Command.ExecuteNonQuery()
    '            Id = DataCore.Command.Parameters("@ReturnId").Value
    '            DataCore.CloseConnection()
    '            Return Id
    '        End Function
    '        Public Function SavePersonasD(Dec As Byte, ID As Integer, sexo As String, fecnac As Date, sectorId As Integer, direccion As String, tel1 As String, tel2 As String, email As String, seguro1 As String, seguro2 As String, GSanguineo As Byte) As String
    '            'Dim variable As Decimal 'as tipo de datos 
    '            Ds = New DataSet
    '            DataCore.OpenConnection()
    '            Dim oArray5() As Object = {Dec, ID, sexo, fecnac, sectorId, direccion, tel1, tel2, email, seguro1, seguro2, GSanguineo, 0}
    '            Dim pArray5() As Object = {"@Sucess"}
    '            DataCore.FillCommand("SavePersonasD", oArray5, "@Dec,@per_id,@per_sexo,@per_fecnac ,@sec_id,@per_direccion,@per_tel1,@per_tel2,@per_email,@per_seguro1,@Per_seguro2,@gSanguineo,@Sucess", pArray5)
    '            'oDao.Command.Parameters("@ReturnId").SqlDbType = SqlDbType.in ' formato variable 
    '            DataCore.Command.ExecuteNonQuery()
    '            ID = DataCore.Command.Parameters("@Sucess").Value
    '            DataCore.CloseConnection()
    '            Return ID
    '        End Function
    '        Public Function SaveEmpleados(Dec As Byte, ID As Integer, codigo As String, puesto As Integer, fecIngreso As Date, salario As Decimal) As String
    '            'Dim variable As Decimal 'as tipo de datos 
    '            Ds = New DataSet
    '            DataCore.OpenConnection()
    '            Dim oArray5() As Object = {Dec, ID, codigo, puesto, fecIngreso, salario, 0}
    '            Dim pArray5() As Object = {"@Sucess"}
    '            DataCore.FillCommand("SaveEmpleados", oArray5, "@Dec,@per_id,@emp_codigo,@pues_id,@emp_fecing,@emp_salario,@Sucess", pArray5)
    '            'oDao.Command.Parameters("@ReturnId").SqlDbType = SqlDbType.in ' formato variable 
    '            DataCore.Command.ExecuteNonQuery()
    '            ID = DataCore.Command.Parameters("@Sucess").Value
    '            DataCore.CloseConnection()
    '            Return ID
    '        End Function
    '    End Class
End Namespace
