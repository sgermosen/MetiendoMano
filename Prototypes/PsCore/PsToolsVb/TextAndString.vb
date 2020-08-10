
Imports System.Configuration

Public Class TextAndString

    Public Function GetStringConnection() As String
        Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    End Function
    Public Function FormatRnc(ByRef rnc As String) As String
        If Len(rnc) = 11 Then
            'Si entra aqui es porque es una cedula
            'por lo tanto el formato es XXX-XXXXXXX-X
            'si la cedula comienza con 0 la conversion a integer resulta en un overflow,   Formatear_RNC = CType(rnc, integer).ToString("000-0000000-0")
            FormatRnc = FormatRnc(rnc, 1)
        ElseIf Len(rnc) = 9 Then
            'Si entra aqui es porque es un número de RNC
            'por lo tanto el formato es X-XX-XXXXX-X
            FormatRnc = FormatRnc(rnc, 2)
            'Formatear_RNC = CType(rnc, Integer).ToString("0-00-00000-0")
        ElseIf Len(rnc) = 12 Then
            'Si entra aqui es porque es un vehiculo o inmueble
            FormatRnc = rnc
        Else
            FormatRnc = ""
        End If
    End Function
    Shared Function FormatRnc(pRnc As String, pTipoRnc As Integer)
        Select Case pTipoRnc
            Case 1 'Cedula
                Return String.Format("{0}-{1}-{2}", pRnc.Substring(0, 3), pRnc.Substring(3, 7), pRnc.Substring(10, 1))
            Case 2 'RNC
                Return String.Format("{0}-{1}-{2}-{3}", pRnc.Substring(0, 1), pRnc.Substring(1, 2), pRnc.Substring(3, 5), pRnc.Substring(8, 1))
            Case Else
                Return String.Empty
        End Select
    End Function
    Public Function CharactersRemoval(ByRef cString As String, Optional ByRef tipo As Byte = 0) As String
        ' '' ''Retira de uma string todas as letras ou caracteres especiais.
        ' '' ''O parâmetro tipo é opcional.
        ' '' ''Se tipo = 1 então, a função retirará somente os caracteres que não
        ' '' ''forem números e letras
        ' '' ''Senão Se o tipo = 2 então, a função retirará somente os caracteres que
        ' '' ''não forem numéricos, isto é, o Ponto e a Vírgula não serão retirados
        ' '' ''Senão, a função retirará somente os caracteres que não forem números

        '''''''traduccion 
        '''''''' "Toma una cadena de letras o caracteres especiales.
        ' '' '' "El parámetro de tipo es opcional.
        ' '' '' "Si type = 1, entonces la función eliminar no sólo los caracteres
        ' '' '' "¿Están los números y las letras---- 
        ' '' '' "eliminara _todo lo que no sea numero o letra-------
        '' '' ''Else if type = 2, entonces la función se eliminará sólo los caracteres que
        ' '' '' "no son numéricos, es decir, punto y coma no se elimina
        ' '' '' "De lo contrario, la función se eliminará sólo los caracteres que no son números
        Dim st As String
        Dim i As Short
        Dim funcion As Boolean
        st = ""
        For i = 1 To Len(cString)
            If tipo = 1 Then
                funcion = SpecialKeys(Asc(Mid(cString, i, 1)))
            ElseIf tipo = 2 Then
                funcion = IsNumeric(Mid(cString, i, 1)) Or Mid(cString, i, 1) = "." Or Mid(cString, i, 1) = ","
            Else
                funcion = IsNumeric(Mid(cString, i, 1))
            End If
            If funcion Then
                st = st & Mid(cString, i, 1)
            End If
        Next i
        Return st
    End Function
    Function CharacterRemoval(texto As String, separador As String, noCampo As Integer) As String
        Return SeparatorRemoval(texto, separador, noCampo)
    End Function
    Public Function SpecialKeys(ByRef tecla As Short) As Short
        'Verifica se o caracter digitado e diferente de letra ou numero
        SpecialKeys = True
        If tecla = 39 Then ' Símbolos (aspas simples
            SpecialKeys = False
        ElseIf tecla >= 58 And tecla <= 64 Then  ' Símbolos
            SpecialKeys = False
        ElseIf tecla >= 91 And tecla <= 96 Then  ' Símbolos
            SpecialKeys = False
        ElseIf tecla >= 123 And tecla <= 255 Then  ' Símbolos
            SpecialKeys = False
        End If
        Return SpecialKeys
    End Function
    Function SeparatorRemoval(texto As String, separador As String, noCampo As Integer) As String
        '------------------------------------------------------------
        'Funcao para retornar campos em uma string
        'Exemplo: Le_Campo("001*Bruno*Ipanema","*",1) = "001"
        'Exemplo: Le_Campo("001*Bruno*Ipanema","*",2) = "Bruno"
        'Exemplo: Le_Campo("001*Bruno*Ipanema","*",3) = "Ipanema"
        '------------------------------------------------------------
        Dim letra As String
        Dim tam As Integer
        Dim aux As String
        Dim pos As Integer
        Dim noSeparadores As Integer
        separador = IIf(separador = "", Chr(160), separador)
        tam = Len(texto)
        pos = 1
        noSeparadores = 1
        Do While (pos <= tam) And (noSeparadores < noCampo)
            letra = Mid$(texto, pos, 1)
            If letra = separador Then
                noSeparadores = noSeparadores + 1
            End If
            pos = pos + 1
        Loop
        aux = ""
        Do While (pos <= tam)
            letra = Mid$(texto, pos, 1)
            If ((letra <> separador) And (letra <> "")) Then
                aux = aux + letra
                pos = pos + 1
            Else
                Exit Do
            End If
        Loop
        Return aux
    End Function
    Public Function GenDv(ByRef rif As String, ByRef xbase As Short) As Short
        Dim i As Short
        Dim tem As Short
        Dim rifTmp As String

        tem = 0
        rifTmp = Format(CSng(rif), "############")
        For i = 1 To Len(rifTmp)
            tem = tem + Val(Mid(rifTmp, i, 1)) * (((Len(rifTmp) - i) Mod (xbase - 1)) + 2)
        Next
        tem = tem Mod 11
        If tem > 1 Then
            tem = 11 - tem
        Else
            tem = 0
        End If
        GenDv = tem
    End Function
    Public Function GenDV_Para_Republica_Dominicana(ByRef rif As String) As Short
        Dim numeroBase As String
        Dim a As Byte
        Dim suma As Short = 0
        Dim division As Double
        Dim resta As Short
        '  Dim Re As Object


        If Len(rif) = 8 Then
            'If Rs!RRC_TIPO_PERSONA = PersonaJuridica Then
            'Si entra aqui es porque el tipo de persona
            'es Juridica, por lo tanto se calcula el digito
            'para un RNC
            numeroBase = "79865432"
            For a = 1 To 8
                suma = suma + (CDbl(Mid(rif, a, 1)) * CDbl(Mid(numeroBase, a, 1)))
            Next a
            division = suma / 11
            resta = suma - (Int(division) * 11)
            If resta = 0 Then
                GenDV_Para_Republica_Dominicana = 2
            ElseIf resta = 1 Then
                GenDV_Para_Republica_Dominicana = 1
            Else
                GenDV_Para_Republica_Dominicana = 11 - resta
            End If
        ElseIf Len(rif) = 10 Then
            'ElseIf Rs!RRC_TIPO_PERSONA = PersonaFisica Then
            'Si entra aqui es porque el tipo de persona
            'es Juridica, por lo tanto se calcula el digito
            'para un RNC
            rif = Format(CSng(rif), "##########")
            numeroBase = "1212121212"
            For a = 1 To 10
                resta = CDbl(Mid(rif, a, 1)) * CDbl(Mid(numeroBase, a, 1))
                suma = suma + CDbl(Mid(CStr(resta), 1, 1)) + IIf(resta > 9, Mid(CStr(resta), 2, 1), 0)
            Next a
            division = CShort(suma / 10) * 10
            If division < suma Then division = division + 10
            GenDV_Para_Republica_Dominicana = division - suma
        Else
            GenDV_Para_Republica_Dominicana = 0
        End If
    End Function

End Class


