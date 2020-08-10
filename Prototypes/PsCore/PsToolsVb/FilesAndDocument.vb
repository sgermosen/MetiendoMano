Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml

Public Class FilesAndDocument

    Public Function ReadMsgFromXml(pRuta As String, pIdMessage As String)
        Dim xml As XmlDocument
        Dim nodeList As XmlNodeList
        Dim node As XmlNode
        Dim mensage As String = ""
        Try
            xml = New XmlDocument()
            xml.Load(pRuta)
            nodeList = xml.SelectNodes("/mensages/" & pIdMessage)
            For Each node In nodeList
                With node.Attributes
                    mensage = .GetNamedItem("msg").Value
                End With
            Next
            Return mensage
        Catch ex As Exception
            mensage = ex.ToString
            Return mensage
        Finally
        End Try
    End Function
    Public Function ReadUsingXmlDocument(pRuta As String, pIdMessage As String)
        Dim xml As XmlDocument
        Dim nodeList As XmlNodeList
        Dim node As XmlNode
        Dim mensage As String = ""
        Try
            Xml = New XmlDocument()
            Xml.Load(pRuta)
            NodeList = Xml.SelectNodes("/mensages/" & pIdMessage)
            'Console.WriteLine("Nodos por Leer: " & NodeList.Count & vbNewLine)
            For Each Node In NodeList
                With Node.Attributes
                    Mensage = .GetNamedItem("msg").Value
                    'Mensage = .GetNamedItem("message").Value
                    'Console.WriteLine("ID: " & .GetNamedItem("id").Value)
                    'Console.WriteLine("Artist: " & .GetNamedItem("artist").Value)
                    ' Console.WriteLine("Title: " & .GetNamedItem("title").Value)
                    ' Console.Write(vbNewLine)
                End With

            Next
            Return Mensage
        Catch ex As Exception
            ' Console.WriteLine(ex.GetType.ToString & vbNewLine & ex.Message.ToString)
            Mensage = ex.ToString
            Return Mensage
        End Try
        'Try
        '    Dim m_xmld As XmlDocument
        '    Dim m_nodelist As XmlNodeList
        '    Dim m_node As XmlNode
        '    'Creamos el "XML Document"
        '    m_xmld = New XmlDocument()
        '    'Cargamos el archivo
        '    m_xmld.Load(P_Ruta)
        '    'Obtenemos la lista de los nodos "name"
        '    m_nodelist = m_xmld.SelectNodes("/usuarios/name")
        '    'Iniciamos el ciclo de lectura
        '    For Each m_node In m_nodelist
        '        'Obtenemos el atributo del codigo
        '        Dim mCodigo = m_node.Attributes.GetNamedItem("codigo").Value
        '        'Obtenemos el Elemento nombre
        '        Dim mNombre = m_node.ChildNodes.Item(0).InnerText
        '        'Obtenemos el Elemento apellido
        '        Dim mApellido = m_node.ChildNodes.Item(1).InnerText
        '        'Escribimos el resultado en la consola,
        '        'pero tambien podriamos utilizarlos en
        '        'donde deseemos
        '        Dim variable As String
        '        variable= ("Codigo usuario: " & mCodigo _
        '        & " Nombre: " & mNombre _
        '        & " Apellido: " & mApellido
        '        Return variable
        '    Next
        'Catch ex As Exception
        '    Dim variable As String
        '    variable = ex.ToString
        '    Return variable
        '    'Error trapping
        '    '  Console.Write(ex.ToString())
        'End Try
    End Function
    Public Function ReadTextFromImage(inputDir As String) As String
        Dim md As New MODI.Document
        md.Create(inputDir)
        md.OCR(MODI.MiLANGUAGES.miLANG_SPANISH, True, True)

        Dim image As MODI.Image = DirectCast(md.Images(0), MODI.Image)
        Dim layout As MODI.Layout = image.Layout
        Dim out As String = ""
        For j As Integer = 0 To layout.Words.Count - 1
            Dim word As MODI.Word = DirectCast(layout.Words(j), MODI.Word)
            out += " " & word.Text
        Next
        Return out
    End Function
    ''  Private Const sSecretKey As String = "824455"
    '''' <summary>
    '''' Comprobando que un archivo exista en una ruta especifica, con el nombre dado
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks> 
    'Private Function SourceFileExists(ByVal inputDir As String) As Boolean
    '    If Not (File.Exists(inputDir)) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
    Sub EncryptFile(sInputFilename As String,
                     sOutputFilename As String,
                     sKey As String)
        Dim fsInput As New FileStream(sInputFilename,
                                      FileMode.Open, FileAccess.Read)
        Dim fsEncrypted As New FileStream(sOutputFilename,
                                          FileMode.Create, FileAccess.Write)
        Dim des As New DESCryptoServiceProvider()
        'Establecer la clave secreta para el algoritmo DES.
        'Se necesita una clave de 64 bits y IV para este proveedor
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
        'Establecer el vector de inicialización.
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
        'crear cifrado DES a partir de esta instancia
        Dim desencrypt As ICryptoTransform = des.CreateEncryptor()
        'Crear una secuencia de cifrado que transforma la secuencia
        'de archivos mediante cifrado DES
        Dim cryptostream As New CryptoStream(fsEncrypted,
                                             desencrypt,
                                             CryptoStreamMode.Write)
        'Leer el texto del archivo en la matriz de bytes
        Dim bytearrayinput(fsInput.Length - 1) As Byte
        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)
        'Escribir el archivo cifrado con DES
        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length)
        cryptostream.Close()
    End Sub
    Sub DecryptFile(sInputFilename As String,
                     sOutputFilename As String,
                     sKey As String)
        Dim des As New DESCryptoServiceProvider()
        'Se requiere una clave de 64 bits y IV para este proveedor.
        'Establecer la clave secreta para el algoritmo DES.
        des.Key() = ASCIIEncoding.ASCII.GetBytes(sKey)
        'Establecer el vector de inicialización.
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
        'crear la secuencia de archivos para volver a leer el archivo cifrado
        Dim fsread As New FileStream(sInputFilename, FileMode.Open, FileAccess.Read)
        'crear descriptor DES a partir de nuestra instancia de DES
        Dim desdecrypt As ICryptoTransform = des.CreateDecryptor()
        'crear conjunto de secuencias de cifrado para leer y realizar 
        'una transformación de descifrado DES en los bytes entrantes
        Dim cryptostreamDecr As New CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read)
        'imprimir el contenido de archivo descifrado
        Dim fsDecrypted As New StreamWriter(sOutputFilename)
        fsDecrypted.Write(New StreamReader(cryptostreamDecr).ReadToEnd)
        fsDecrypted.Flush()
        fsDecrypted.Close()
    End Sub
    Public Sub CreateTextFile(pRuta As String, oDtable As DataTable)
        Dim oSw As New StreamWriter(pRuta)
        Dim oLinea As String
        Dim oFilas() As DataRow
        oFilas = oDtable.Select

        Dim oIndex As Integer = 0
        If oFilas.Length > 0 Then
            For Each dr As DataRow In oFilas
                If dr(oIndex).ToString.Length > 0 Then
                    oLinea = dr(oIndex)
                    oSw.WriteLine(oLinea)
                    oSw.Flush()
                    oIndex += 1
                Else
                    Exit Sub
                End If
            Next
        End If
    End Sub

End Class
