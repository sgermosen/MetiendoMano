Public Class Dates

    Public Function AddMonths(ByRef myDate As Date, ByRef rel As Short) As Date
        ' Dada una fecha devuelve la fecha con el mes modificado
        ' la modificacion que recibe el mes es una desplazamiento
        ' basado en el valor del entero "rel", esto es, adelanto
        ' o retraso
        Dim myMonth As Short
        Dim myYear As Short
        Dim factor As Short
        '  Dim toret As Date
        Dim myStr As String
        AddMonths = myDate
        myMonth = Month(myDate)
        If rel < 0 Then
            factor = (-(rel + myMonth) \ 12) + 1
            myYear = IIf(myMonth + rel <= 0, Year(myDate) - factor, Year(myDate))
            ' La siguiente linea calcula el techo positivo de rel respecto a 12
            factor = IIf(-rel Mod 12 = 0, -rel \ 12, (-rel \ 12) + 1)
            myMonth = myMonth + (12 * factor)
        Else
            factor = IIf(((rel - (12 - myMonth)) Mod 12) = 0, ((rel - (12 - myMonth)) \ 12), ((rel - (12 - myMonth)) \ 12) + 1)
            myYear = IIf(myMonth + rel > 12, Year(myDate) + factor, Year(myDate))
        End If
        myMonth = ((myMonth + rel) Mod 12)
        myMonth = IIf(myMonth = 0, 12, myMonth)
        myStr = Day(myDate) & "/" & myMonth & "/" & myYear
        If IsDate(myStr) Then
            AddMonths = CDate(myStr)
        Else
            myStr = Day(DateTime.FromOADate(myDate.ToOADate - 1)) & "/" & Str(myMonth) & "/" & myYear
            If IsDate(myStr) Then
                AddMonths = CDate(myStr)
            Else
                myStr = Day(DateTime.FromOADate(myDate.ToOADate - 2)) & "/" & Str(myMonth) & "/" & myYear
                If IsDate(myStr) Then
                    AddMonths = CDate(myStr)
                End If
            End If
        End If
    End Function
    Public Function NextLaborDay(pdFecha As Date) As Date
        Dim ldFechaAux As Date
        Dim lbDiaHabil As Boolean
        ldFechaAux = pdFecha
        lbDiaHabil = False
        Do While lbDiaHabil = False
            ' lbDiaHabil = True
            Select Case Weekday(ldFechaAux)
                Case Is = vbSunday
                    ldFechaAux = ldFechaAux.AddDays(1)
                    lbDiaHabil = False
                Case Is = vbSaturday
                    ldFechaAux = ldFechaAux.AddDays(2)
                    lbDiaHabil = False
                Case Else
                    lbDiaHabil = True
            End Select
        Loop
        Return ldFechaAux
    End Function
    Friend Function TotalYears(fechaNacimiento As DateTime) As Int32

        Dim year1, year2, month1, month2, day1, day2 As Int32
        Dim cumplidoAnio As Boolean
        Dim fechaActual As DateTime = DateTime.Now

        With fechaNacimiento
            year1 = .Year
            month1 = .Month
            day1 = .Day
        End With

        With fechaActual
            year2 = .Year
            month2 = .Month
            day2 = .Day
        End With

        ' Calculo la diferencia de años.
        '
        If month2 > month1 Then
            ' El segundo més es superior al primero, por lo que
            ' se entiende que se ha cumplido el año.
            cumplidoAnio = True

        ElseIf month2 = month1 Then
            ' Los dos meses son iguales, por lo que si el segundo
            ' día es igual o superior al primero, se ha cumplido
            ' el año.
            If day1 <= day2 Then cumplidoAnio = True
        End If

        If cumplidoAnio = True Then
            Return year2 - year1
        Else
            Return year2 - year1 - 1
        End If

    End Function

    Public Function MesesDias(f1 As Date, retur As Integer) As Integer

        'Cantidad de Días
        Dim f3 As TimeSpan, nf1 As Date, nf2 As Date

        f3 = Date.Now.Subtract(f1)
        Dim meses As Integer
        'Cantidad de Meses
        '  meses = DateDiff(DateInterval.Month, f1, Date.Now)

        nf1 = CDate(f1)

        nf2 = DateAdd(DateInterval.Day, -1, Date.Now)
        Dim dias As Integer
        meses = DateDiff(DateInterval.Month, nf1, nf2)
        dias = F3.Days - DateDiff(DateInterval.Day, nf1, nf2) + -1

        If dias < 0 Then
            dias = dias + DateAdd(DateInterval.Month, 1, nf2).Day + 1
        End If

        If retur = 0 Then
            Return meses
        Else
            Return dias
        End If

    End Function

End Class
