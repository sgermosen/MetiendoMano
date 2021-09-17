'Imports System.Data.OracleClient
'Imports EID.MySqlClient
Imports System.Data.SqlClient

Imports System.Xml
Imports System.Threading
Imports System.Data

Namespace Ps.Datacore
    Public Class ModConexion
        Private conexion2 As String

        Private nombredatabase2 As String

        Public strconecion2 As String
        'conexion a sql
        Private sql_cnn2 As New SqlConnection
        Private conexion As String
        Private nombredatabase As String
        Dim m_xmlr As XmlTextReader
        Private strconecion As String
        Public strconecion3 As String
        'conexion a sql
        Public sql_cnn As New SqlConnection '("Data Source=192.168.2.17; initial catalog = pln_retiro ; integrated security = true")
        Public sql_cnn3 As New SqlConnection
        'conxion a Access
        'Public acc_cnn As New OleDbConnection '("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DbPlanRet.mdb; User Id=admin;Password=;")
        'Public Sub ConecionXmlServerAcess()
        '    Dim ejecutarrutacompletaserver = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length - 10, My.Application.Info.DirectoryPath.Length)
        '    'genero solo el path dela aplicacion poniendolo mas corto a modo que que has lo pedio anterior mente
        '    Dim RutaDeaplicacionserver As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompletaserver)
        '    'ejecutamos solo la ruta especificada
        '    Dim EjecutarArchivoserver As String = RutaDeaplicacionserver & "\Base de dato Acess\" & "DbPlanRet.mdb"
        '    acc_cnn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & EjecutarArchivoserver & "; user Admin;Password=;")
        'End Sub
        Public Function OpenConection(Local As Boolean) As Boolean
            If sql_cnn.State = ConnectionState.Open Then
                Try
                    sql_cnn.Close()
                Catch ex As Exception
                End Try
            End If
            ConecionXmlServerSql(Local)
            sql_cnn = New SqlConnection(strconecion)
            Try
                sql_cnn.Open()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function OpenConection2(Local As Boolean) As Boolean
            If sql_cnn2.State = ConnectionState.Open Then
                Try
                    sql_cnn2.Close()
                Catch ex As Exception
                End Try
            End If
            ConecionXmlServerSql(False)
            sql_cnn2 = New SqlConnection(strconecion)
            Try
                sql_cnn2.Open()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function OpenConection20(Local As Boolean) As Boolean
            If sql_cnn2.State = ConnectionState.Open Then
                Try
                    sql_cnn2.Close()
                Catch ex As Exception
                End Try
            End If
            ConecionXmlServerSql2()
            sql_cnn2 = New SqlConnection(strconecion2)
            Try
                sql_cnn2.Open()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Public Sub CloseConection()
            sql_cnn.Close()
        End Sub
        Public Sub CloseConection2()
            sql_cnn2.Close()
        End Sub

        Public Function ExecuteScalar(StrComand As String) As String
            Dim cmd As New SqlCommand(StrComand, sql_cnn)
            Return cmd.ExecuteScalar()
        End Function
        Public Function ExecuteScalar2(StrComand As String) As String
            Dim cmd As New SqlCommand(StrComand, sql_cnn2)
            Return cmd.ExecuteScalar()
        End Function
        Public Function ExecuteScalar3(StrComand As String) As String
            Dim cmd As New SqlCommand(StrComand, sql_cnn3)
            Return cmd.ExecuteScalar()
        End Function


        Public Function ExecuteReader(StrComand As String) As SqlDataReader
            Dim cmd As New SqlCommand(StrComand, sql_cnn)
            Return cmd.ExecuteReader()
        End Function
        Public Function dAdapter(Sel As String) As SqlDataAdapter
            dAdapter = New SqlDataAdapter(Sel, sql_cnn)
        End Function
        Public Function ExecuteNonQuery(Sentencia As String) As Boolean

            Dim cmd As New SqlCommand(Sentencia, sql_cnn)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Public Function ExecuteNonQuery2(Sentencia As String) As Boolean

            Dim cmd As New SqlCommand(Sentencia, sql_cnn2)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Public Function ExecuteNonQuerySP(Cmd As SqlCommand) As Boolean
            Cmd.Connection = sql_cnn
            Try
                Cmd.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Sub ConecionXmlServerSql(ByVal Local As Boolean)

            Dim ejecutarrutacompletaserver = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length(), My.Application.Info.DirectoryPath.Length())
            'genero solo el path dela aplicacion poniendolo mas corto a modo que que has lo pedio anterior mente
            Dim RutaDeaplicacionserver As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompletaserver)
            'ejecutamos solo la ruta especificada
            Dim EjecutarArchivoserver As String
            If Local = True Then
                EjecutarArchivoserver = RutaDeaplicacionserver & "\servidor\" & "LOCAL.xml"
            Else
                EjecutarArchivoserver = RutaDeaplicacionserver & "\servidor\" & "ServerSql.xml"

            End If
            Try
                m_xmlr = New XmlTextReader(EjecutarArchivoserver)
                m_xmlr.WhitespaceHandling = WhitespaceHandling.None
                m_xmlr.Read()
                m_xmlr.Read()
                While Not m_xmlr.EOF
                    m_xmlr.Read()
                    If Not m_xmlr.IsStartElement() Then
                        Exit While
                    End If
                    conexion = m_xmlr.ReadElementString("server")
                    'nombredatabase = m_xmlr.ReadElementString("database")
                    '   Console.WriteLine(conexion)
                    'Console.WriteLine(nombredatabase)
                    '  Console.Write(vbCrLf)
                    strconecion = conexion '"Data Source=" & _
                    ' Nombreserver & ";" & "initial catalog =" & nombredatabase & ";" & "User Id=sa; Password=kl36mm"
                    'sql_cnn = New SqlConnection(strconecion)
                    ' sql_cnn.Open()
                    ' sql_cnn.Close()
                End While
                m_xmlr.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Con la conexion al servidor" & " " & My.Application.Info.ProductName)
                ' fallo = True
                Exit Sub
            End Try
        End Sub
        Public Sub ConecionXmlServerSql2()
            Dim ejecutarrutacompletaserver = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length(), My.Application.Info.DirectoryPath.Length())
            'genero solo el path dela aplicacion poniendolo mas corto a modo que que has lo pedio anterior mente
            Dim RutaDeaplicacionserver As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompletaserver)
            'ejecutamos solo la ruta especificada
            Dim EjecutarArchivoserver As String = RutaDeaplicacionserver & "\servidor\" & "LOCAL.xml"
            Try
                m_xmlr = New XmlTextReader(EjecutarArchivoserver)
                m_xmlr.WhitespaceHandling = WhitespaceHandling.None
                m_xmlr.Read()
                m_xmlr.Read()
                While Not m_xmlr.EOF
                    m_xmlr.Read()
                    If Not m_xmlr.IsStartElement() Then
                        Exit While
                    End If
                    conexion2 = m_xmlr.ReadElementString("server")
                    'nombredatabase = m_xmlr.ReadElementString("database")
                    Console.WriteLine(conexion2)
                    'Console.WriteLine(nombredatabase)
                    Console.Write(vbCrLf)
                    strconecion2 = conexion2 '"Data Source=" & _
                    ' Nombreserver & ";" & "initial catalog =" & nombredatabase & ";" & "User Id=sa; Password=kl36mm"
                    sql_cnn2 = New SqlConnection(strconecion2)
                    sql_cnn2.Open()
                    sql_cnn2.Close()
                End While
                m_xmlr.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Con la conexion al servidor" & " " & My.Application.Info.ProductName)
                ' fallo = True
                Exit Sub
            End Try
        End Sub
        Public Sub ConecionXmlServerSql3()
            Dim ejecutarrutacompletaserver = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length(), My.Application.Info.DirectoryPath.Length())
            'genero solo el path dela aplicacion poniendolo mas corto a modo que que has lo pedio anterior mente
            Dim RutaDeaplicacionserver As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompletaserver)
            'ejecutamos solo la ruta especificada
            Dim EjecutarArchivoserver As String = RutaDeaplicacionserver & "\servidor\" & "ServerSql.xml"
            Try
                m_xmlr = New XmlTextReader(EjecutarArchivoserver)
                m_xmlr.WhitespaceHandling = WhitespaceHandling.None
                m_xmlr.Read()
                m_xmlr.Read()
                While Not m_xmlr.EOF
                    m_xmlr.Read()
                    If Not m_xmlr.IsStartElement() Then
                        Exit While
                    End If
                    conexion = m_xmlr.ReadElementString("server")
                    'nombredatabase = m_xmlr.ReadElementString("database")
                    Console.WriteLine(conexion)
                    'Console.WriteLine(nombredatabase)
                    Console.Write(vbCrLf)
                    strconecion3 = conexion '"Data Source=" & _
                    ' Nombreserver & ";" & "initial catalog =" & nombredatabase & ";" & "User Id=sa; Password=kl36mm"
                    sql_cnn3 = New SqlConnection(strconecion3)
                    sql_cnn3.Open()
                    sql_cnn3.Close()
                End While
                m_xmlr.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Con la conexion al servidor" & " " & My.Application.Info.ProductName)
                ' fallo = True
                Exit Sub
            End Try
        End Sub
        Sub conectarSenasa()
            Dim ejecutarrutacompletaserver = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length, My.Application.Info.DirectoryPath.Length())
            'genero solo el path dela aplicacion poniendolo mas corto a modo que que has lo pedio anterior mente
            Dim RutaDeaplicacionserver As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompletaserver)
            'ejecutamos solo la ruta especificada
            Dim EjecutarArchivoserver As String = RutaDeaplicacionserver & "\Otros\" & "XMLDirecSenasa.xml"
            Try
                ' Application.DoEvents()
                m_xmlr = New XmlTextReader(EjecutarArchivoserver)
                m_xmlr.WhitespaceHandling = WhitespaceHandling.None
                m_xmlr.Read()
                m_xmlr.Read()
                While Not m_xmlr.EOF
                    m_xmlr.Read()
                    If Not m_xmlr.IsStartElement() Then
                        Exit While
                    End If
                    ' Application.DoEvents()
                    conexion = m_xmlr.ReadElementString("Directora")
                    Console.WriteLine(conexion)
                    Console.Write(vbCrLf)
                    ' directora = conexion
                    'Application.DoEvents()
                End While
                m_xmlr.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Con la conexion del Archivo" & " " & My.Application.Info.ProductName)
                'fallo = True
                Exit Sub
            End Try
        End Sub


        ''' <summary>
        ''' todo lo relacionado con la clase de data
        ''' 
        ''' 
        ''' 
        ''' 
        ''' viene aca
        ''' </summary>
        ''' <remarks></remarks>
#Region "Poner Parametros"
        Shared mColComandos As New System.Collections.Hashtable()

        Protected Function Comando(ByVal ProcedimientoAlmacenado As String) As System.Data.IDbCommand
            Dim mComando As System.Data.SqlClient.SqlCommand
            If mColComandos.Contains(ProcedimientoAlmacenado) Then
                mComando = CType(mColComandos.Item(ProcedimientoAlmacenado),
                                 System.Data.SqlClient.SqlCommand)
            Else
                If sql_cnn.State = ConnectionState.Closed Then
                    Try
                        sql_cnn.Open()
                    Catch ex As Exception
                    End Try
                End If

                mComando = New System.Data.SqlClient.SqlCommand(ProcedimientoAlmacenado, sql_cnn)
                Dim mContructor As New System.Data.SqlClient.SqlCommandBuilder()
                mComando.Connection = sql_cnn
                mComando.CommandType = CommandType.StoredProcedure
                'mContructor.DeriveParameters(mComando)
                SqlCommandBuilder.DeriveParameters(mComando)
                sql_cnn.Close()
                mColComandos.Add(ProcedimientoAlmacenado, mComando)
            End If
            Return mComando
        End Function

        Protected Sub CargarParametros(ByVal Comando As System.Data.IDbCommand, ByVal Args() As Object)
            Dim I As Integer
            With Comando
                For I = 0 To Args.GetUpperBound(0)
                    Try
                        CType(.Parameters(I + 1), System.Data.SqlClient.SqlParameter).Value = Args(I)
                    Catch Qex As Exception
                        Throw (Qex)
                    End Try
                Next
            End With
        End Sub
#End Region

#Region "Devolver Parametros"
        Protected Function CrearDataAdapter(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Args() As Object) As System.Data.IDataAdapter
            Dim mCom As System.Data.SqlClient.SqlCommand = Comando(ProcedimientoAlmacenado)
            If Not Args Is Nothing Then
                CargarParametros(mCom, Args)
            End If
            Return New System.Data.SqlClient.SqlDataAdapter(mCom)
        End Function

        'En este caso trabajaremos con funciones sobrecargadas con la finalidad de poder llamar a la misma function pero con diferentes parametros. 
        Public Overloads Function TraerDataset(ByVal ProcedimientoAlmacenado As String) As System.Data.DataSet
            Dim mDataset As New System.Data.DataSet()
            CrearDataAdapter(ProcedimientoAlmacenado).Fill(mDataset)
            Return mDataset
        End Function

        'Funcion Sobrecargada 
        Public Overloads Function TraerDataset(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As System.Data.DataSet
            Dim mDataset As New System.Data.DataSet()
            CrearDataAdapter(ProcedimientoAlmacenado, Argumentos).Fill(mDataset)
            Return mDataset
        End Function
#End Region

#Region "Acciones"
        Public Function Ejecutar(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As Integer
            Dim mCom As System.Data.SqlClient.SqlCommand = Comando(ProcedimientoAlmacenado)
            Dim Resp As Integer
            sql_cnn.Open()
            mCom.Connection = sql_cnn
            mCom.CommandType = CommandType.StoredProcedure
            CargarParametros(mCom, Argumentos)
            Resp = mCom.ExecuteNonQuery
            sql_cnn.Close()
            Return Resp
        End Function
#End Region

        ''' <summary>
        ''' aca todo lo relacionado con la clase de 
        ''' produccion
        ''' </summary>
        ''' <param name="ProcedimientoAlmacenado"></param>
        ''' <param name="Argumentos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ListarClientes(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As DataSet
            ListarClientes = TraerDataset(ProcedimientoAlmacenado, Argumentos)
        End Function
    End Class


    Public Class ConectByIp
        '    ''/*  

        'oSQLConn.ConnectionString = "Network Library=DBMSSOCN;" & _
        '                            "Data Source=xxx.xxx.xxx.xxx,1433;" & _
        '                            "Initial Catalog=mySQLServerDBName;" & _
        '                            "User ID=myUsername;" & _
        '                            "Password=myPassword"


        '    '' ''Where: 
        '    '' ''- "Network Library=DBMSSOCN" tells SqlConnection to use TCP/IP Q238949
        '    '' ''- xxx.xxx.xxx.xxx is an IP address.  
        '    '' ''- 1433 is the default port number for SQL Server.  Q269882 and Q287932
        '    '' ''- You can also add "Encrypt=yes" for encryption 
        '    '' ''    ''  * 
        '    '' ''    ''  * 
        '    ' '' ''  * */


    End Class

    Public Class OleDbCnn


        '    ' OLE DB .NET Data Provider (System.Data.OleDb) 
        '    'The OLE DB .NET Data Provider uses native OLE DB through COM interop to enable data access.  

        '    'To use the OLE DB .NET Data Provider, you must also use an OLE DB provider (e.g.  SQLOLEDB, MSDAORA, or Microsoft.JET.OLEDB.4.0).

        '    'For IBM AS/400 OLE DB Provider

        '    ' VB.NET
        '    Dim oOleDbConnection As OleDb.OleDbConnection
        '    Dim sConnString As String = _
        '               "Provider=IBMDA400.DataSource.1;" & _
        '               "Data source=myAS400DbName;" & _
        '               "User Id=myUsername;" & _
        '               "Password=myPassword"
        'oOleDbConnection = New OleDb.OleDbConnection(sConnString)
        'oOleDbConnection.Open()
    End Class

    Public Class JetOleDbCnn


        '    'For JET OLE DB Provider

        '    ' VB.NET
        '    Dim oOleDbConnection As OleDb.OleDbConnection
        '    Dim sConnString As String = _
        '             "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        '             "Data Source=C:\myPath\myJet.mdb;" & _
        '             "User ID=Admin;" & _
        '             "Password="
        'oOleDbConnection = New OleDb.OleDbConnection(sConnString)
        'oOleDbConnection.Open()

    End Class

    Public Class OracleCnn


        '    'For Oracle OLE DB Provider

        '    ' VB.NET
        '    Dim oOleDbConnection As OleDb.OleDbConnection
        '    Dim sConnString As String = _
        '             "Provider=OraOLEDB.Oracle;" & _
        '             "Data Source=MyOracleDB;" & _
        '             "User ID=myUsername;" & _
        '             "Password=myPassword"
        'oOleDbConnection = New OleDb.OleDbConnection(sConnString)
        'oOleDbConnection.Open()

    End Class

    Public Class SqlOleDb



        'For SQL Server OLE DB Provider

        '    ' VB.NET
        '    Dim oOleDbConnection As OleDb.OleDbConnection
        '    Dim sConnString As String = _
        '             "Provider=sqloledb;" & _
        '             "Data Source=myServerName;" & _
        '             "Initial Catalog=myDatabaseName;" & _
        '             "User Id=myUsername;" & _
        '             "Password=myPassword"
        'oOleDbConnection = New OleDb.OleDbConnection(sConnString)
        'oOleDbConnection.Open()


    End Class

    Public Class SybaseCnn

        'For Sybase ASE OLE DB Provider

        '    ' VB.NET
        '    Dim oOleDbConnection As OleDb.OleDbConnection
        '    Dim sConnString As String = _
        '             "Provider=Sybase ASE OLE DB Provider;" & _
        '             "Data Source=MyDataSourceName;" & _
        '             "Server Name=MyServerName;" & _
        '             "Database=MyDatabaseName;" & _
        '             "User ID=myUsername;" & _
        '             "Password=myPassword"
        'oOleDbConnection = New OleDb.OleDbConnection(sConnString)
        'oOleDbConnection.Open()
    End Class

    Public Class OdbcCnn


        '    ' ODBC .NET Data Provider (System.Data.ODBC) 
        '    'The ODBC .NET Data Provider is an add-on component to the .NET 1.0 Framework SDK. It provides access to native ODBC drivers the same way the OLE DB .NET Data Provider provides access to native OLE DB providers.

        'For SQL Server ODBC Driver

        '    ' VB.NET
        '    Dim oODBCConnection As Odbc.OdbcConnection
        '    Dim sConnString As String = _
        '              "Driver={SQL Server};" & _
        '              "Server=MySQLServerName;" & _
        '              "Database=MyDatabaseName;" & _
        '              "Uid=MyUsername;" & _
        '              "Pwd=MyPassword"
        'oODBCConnection = New Odbc.OdbcConnection(sConnString)
        'oODBCConnection.Open()

    End Class

    Public Class OracleOdbcCnn


        'For Oracle ODBC Driver

        '    ' VB.NET
        '    Dim oODBCConnection As Odbc.OdbcConnection
        '    Dim sConnString As String = _
        '             "Driver={Microsoft ODBC for Oracle};" & _
        '             "Server=OracleServer.world;" & _
        '             "Uid=myUsername;" & _
        '             "Pwd=myPassword"
        'oODBCConnection = New Odbc.OdbcConnection(sConnString)
        'oODBCConnection.Open()

    End Class

    Public Class AccessOdbcCnn

        'For Access (JET) ODBC Driver

        '    ' VB.NET
        '    Dim oODBCConnection As Odbc.OdbcConnection
        '    Dim sConnString As String = _
        '             "Driver={Microsoft Access Driver (*.mdb)};" & _
        '             "Dbq=c:\somepath\mydb.mdb;" & _
        '             "Uid=Admin;" & _
        '             "Pwd="
        'oODBCConnection = New Odbc.OdbcConnection(sConnString)
        'oODBCConnection.Open()
    End Class



    Public Class OdbcDriversCnn

        'For all other ODBC Drivers

        '    ' VB.NET
        '    Dim oODBCConnection As Odbc.OdbcConnection
        '    Dim sConnString As String = "Dsn=myDsn;" & _
        '                                "Uid=myUsername;" & _
        '                                "Pwd=myPassword"
        'oODBCConnection = New Odbc.OdbcConnection(sConnString)
        'oODBCConnection.Open()

    End Class


    Public Class OracleClientCnn


        '    '' .NET Framework Data Provider for Oracle (System.Data.OracleClient) 
        '    ''The .NET Framework Data Provider for Oracle is an add-on component to the .NET Framework that provides access to an Oracle database using the Oracle Call Interface (OCI) as provided by Oracle Client software.  

        '    ''Imports System.Data.OracleClient



        '    Dim oOracleConn As OracleConnection = New OracleConnection()
        'oOracleConn.ConnectionString = "Data Source=Oracle8i;" & _
        '                               "Integrated Security=SSPI";
        'oOracleConn.Open()


        '    'Note: You must have the Oracle 8i Release 3 (8.1.7) Client or later installed in order for this provider to work correctly.

        '    'Note: You must have the RTM version of the .NET Framework installed in order for this provider to work correctly.

        '    'Note: There are known Oracle 7.3, Oracle 8.0, and Oracle9i client and server problems in this beta release. The server-side issues should be resolved in the final release of the product.  However, Oracle 7.3 client will not be supported. 

    End Class


    Public Class MySqlCnn



        '    ' MySQL .NET Native Provider 
        '    'The MySQL .NET Native Provider is an add-on component to the .NET Framework that allows you to access the MySQL database through the native protocol, without going through OLE DB.


        '    ''Imports EID.MySqlClient

        '    Dim oMySqlConn As MySqlConnection = New MySqlConnection()
        'oMySqlConn.ConnectionString = "Data Source=localhost;"  & _
        '                              "Database=mySQLDatabase;"  & _
        '                              "User ID=myUsername;"  & _
        '                              "Password=myPassword;"  & _
        '                              "Command Logging=false"
        'oMySqlConn.Open()




    End Class







End Namespace