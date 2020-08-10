Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.Common
'Namespace Ps.Datacore
'    Public Class SqlDataCore
'        ''' <summary>
'        ''' Propiedad privada que sirve para realizar la conexión a la base de datos de Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Private ReadOnly connection As SqlConnection
'        ''' <summary>
'        ''' Propiedad privada que sirve para la creación y manejo de comandos de Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Private command As SqlCommand
'        ''' <summary>
'        ''' Propiedad privada que sirve como contenedor de los datos extraídos de la base de datos Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Private ds As DataSet
'        ''' <summary>
'        ''' Propiedad privada que sirve de enlace entre el contenedor de los datos y la base de datos Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Private da As SqlDataAdapter
'        ''' <summary>
'        ''' Constructor de la clase que sirve para la inicialización de ciertos valores requeridos al momento de realizar el acceso a la base de datos Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Public Sub New()
'            Dim cs = ConfigurationManager.ConnectionStrings("PsConnection")
'            Dim str As String
'            If cs Is Nothing Then
'                '´´str = Decifrar(ConfigurationManager.ConnectionStrings("cadenaConexionProd").ConnectionString)
'                str = ConfigurationManager.ConnectionStrings("PsConnectionProd").ConnectionString
'            Else
'                str = cs.ConnectionString
'            End If

'            Me.connection = New SqlConnection(str)
'        End Sub
'        Public Property Transaction() As SqlTransaction
'            Get
'                Return m_Transaction
'            End Get
'            Set(value As SqlTransaction)
'                m_Transaction = value
'            End Set
'        End Property
'        Private m_Transaction As SqlTransaction
'        ''' <summary>
'        ''' Método privado que permite la apertura de la conexión a la base de datos Oracle.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Public Sub OpenConnection()
'            If connection.State = ConnectionState.Closed Then
'                Me.connection.Open()
'            End If

'        End Sub
'        ''' <summary>
'        ''' Método que extrae datos de la base de datos Oracle basado en un procedimiento almacenado (Stored Procedure) y los parámetros que este recibe.
'        ''' </summary>
'        ''' <param name="spName">Nombre del procedimiento almacenado (Stored Procedure)</param>
'        ''' <param name="spParams">Arreglo de parámetros requeridos por el procedimiento almacenado (Stored Procedure)</param>
'        ''' <returns>Retorna un DataSet con los datos obtenidos de la base de datos Oracle extraídos mediante la ejecución del procedimiento almacenado (Stored Procedure)</returns>
'        ''' <remarks></remarks>
'        Public Function GetDataBySP(ByVal spName As String, ByVal ParamArray spParams() As SqlParameter) As DataSet
'            ' Inicialización de la propiedad propiedad privada que servirá de contendor de datos
'            Me.ds = New DataSet()

'            IniCommandAddParam(spName, spParams)
'            ' Se crea el adaptador que servirá de enlace entre la base de datos y el contenedor de datos
'            Me._da = New SqlDataAdapter(Me.command)
'            Try
'                Me.OpenConnection()
'                ' Se cargan los datos de la base de datos
'                Me._da.Fill(Me.ds)
'                ' Se cierra la conexión a la base de datos

'            Catch ex As Exception
'                Throw New Exception(ex.Message)
'            Finally
'                Me.CloseConnection()
'            End Try

'            Return Me.ds
'        End Function
'        Private Sub IniCommandAddParam(ByVal spName As String, ByVal ParamArray spParams() As SqlParameter)
'            Me.command = New SqlCommand(spName, Me.connection)
'            Me.command.CommandType = commandType.StoredProcedure
'            For Each spParam As SqlParameter In spParams
'                Me.command.Parameters.Add(spParam)
'            Next
'        End Sub
'        Friend Sub ExecuteCommandScalar(ByVal spName As String, ByVal ParamArray spParams() As SqlParameter)
'            IniCommandAddParam(spName, spParams)
'            Try
'                Me.OpenConnection()
'                Me.command.ExecuteScalar()
'            Catch ex As Exception
'                Throw New Exception(ex.Message)
'            Finally
'                Me.CloseConnection()
'            End Try
'        End Sub
'        Public Sub ExecuteCommandScalarTrans(ByVal spName As String, ByVal ParamArray spParams() As SqlParameter)
'            IniCommandAddParam(spName, spParams)
'            Try
'                Me.command.ExecuteScalar()
'            Catch ex As Exception
'                Throw New Exception(ex.Message)
'            Finally
'                Me.CloseConnection()
'            End Try
'        End Sub
'        Public Sub RollbackTrans()
'            command.Transaction.Rollback()
'        End Sub
'        Public Sub BeginTrans()
'            OpenConnection()
'            command.Transaction = connection.BeginTransaction
'        End Sub
'        Public Sub CommitTrans()
'            command.Transaction.Commit()
'        End Sub
'        ''' <summary>
'        ''' Método que permite la liberación de memoria de los objetos utilizados en el acceso a datos.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Friend Sub Dispose()
'            Me.ds.Dispose()
'            Me.da.Dispose()
'            Me.command.Dispose()
'            If Not Me.connection.State = ConnectionState.Closed Then
'                Me.CloseConnection()
'            End If
'        End Sub
'        Private _da As SqlDataAdapter
'        Public Property Command() As SqlCommand
'            Get
'                Return m_Command
'            End Get
'            Set(value As SqlCommand)
'                m_Command = value
'            End Set
'        End Property
'        Private m_Command As SqlCommand
'        Public Property commandType() As CommandType
'            Get
'                Return m_commandType
'            End Get
'            Set(value As CommandType)
'                m_commandType = value
'            End Set
'        End Property
'        Private m_commandType As CommandType
'        'Public Property Ds() As DataSet
'        '    Get
'        '        Return m_Ds
'        '    End Get
'        '    Set(value As DataSet)
'        '        m_Ds = value
'        '    End Set
'        'End Property
'        Private m_Ds As DataSet
'        ''' <summary>
'        ''' Método privado que sirve para cerrar la conexión a la base de datos de MSSQL.
'        ''' </summary>
'        ''' <remarks></remarks>
'        Public Sub CloseConnection()
'            If connection.State = ConnectionState.Open Then
'                Me.connection.Close()
'            End If
'        End Sub
'        Public Function GetDataIdSet(ByVal str As String) As SqlDataAdapter
'            GetDataIdSet = New SqlDataAdapter
'            Me.OpenConnection()
'            GetDataIdSet = New SqlDataAdapter(str, connection)
'            Me.OpenConnection()
'        End Function
'        Public Function FillCommand(sqlCommand As String, values As [Object](), Optional paramDirs As [Object]() = Nothing) As DbCommand
'            Me.LoadCommandObj(sqlCommand, values, paramDirs)
'            Return command
'        End Function
'        Public Function FillCommand(procedureName As String, values As [Object](), parameterNames As [String], Optional paramDirs As [Object]() = Nothing) As DbCommand
'            Me.LoadCommandObj(procedureName, values, paramDirs, True, parameterNames)
'            Return command
'        End Function
'        'Dim isTransaction As Boolean = False
'        Private Sub LoadCommandObj(sqlCommand As [String], values As [Object](), Optional paramDirs As [Object]() = Nothing, Optional isProcedure As Boolean = False, Optional parameters As String = Nothing)
'            command = New SqlCommand()
'            command.Connection = connection
'            If isTransaction Then
'                command.Transaction = Transaction
'            End If
'            command.CommandText = sqlCommand
'            Dim names As New ArrayList()
'            If isProcedure Then
'                Dim prms As String() = parameters.Split(","c).[Select](Function(r) r.Trim()).ToArray()
'                names.AddRange(prms)
'                command.CommandType = commandType.StoredProcedure
'            Else
'                names = GetParameterNames(sqlCommand)
'                command.CommandType = commandType
'            End If
'            Dim index As Integer = 0
'            If values IsNot Nothing Then
'                For Each val As [Object] In values
'                    If paramDirs Is Nothing Then
'                        If names.Count = 0 Then
'                            AddWithValue("", val)
'                        Else
'                            AddWithValue(names(index).ToString(), val)
'                        End If
'                    Else
'                        If paramDirs.Contains(names(index)) Then
'                            AddWithValue(names(index).ToString(), val, ParameterDirection.Output)
'                        ElseIf Not paramDirs.Contains(names(index)) AndAlso paramDirs.Length < values.Length Then
'                            AddWithValue(names(index).ToString(), val)
'                        Else
'                            AddWithValue(names(index).ToString(), val, DirectCast([Enum].Parse(GetType(ParameterDirection), paramDirs(index).ToString()), ParameterDirection))
'                        End If
'                    End If
'                    index += 1
'                Next
'            End If
'        End Sub
'        Public Function GetParameterNames(query As String) As ArrayList
'            Dim paramNames As New ArrayList()
'            Dim pattern As New Regex(":([A-Za-z0-9]+)(\s*)")
'            For Each match As Match In pattern.Matches(query)
'                paramNames.Add(match.Value.Replace(":"c, " "c).Trim())
'            Next
'            Return paramNames
'        End Function
'        Public Sub AddWithValue(paramName As String, value As Object, Optional direction As ParameterDirection = ParameterDirection.Input)
'            Dim param = command.CreateParameter()
'            param.ParameterName = paramName
'            param.Value = value
'            param.Direction = direction
'            command.Parameters.Add(param)
'        End Sub
'    End Class
'End Namespace

