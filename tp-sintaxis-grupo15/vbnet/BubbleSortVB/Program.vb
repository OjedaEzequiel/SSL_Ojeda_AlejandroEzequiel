Imports System
Imports System.Diagnostics

Module BubbleSortBenchmark

    Function GenerarDatos(tamanio As Integer) As Integer()
        Dim datos(tamanio - 1) As Integer
        Dim x As Long = 1234567L

        For i As Integer = 0 To tamanio - 1
            x = (x * 48271L) Mod 2147483647L
            datos(i) = CInt(x Mod 1000000L)
        Next

        Return datos
    End Function

    Sub BubbleSort(arr As Integer())
        Dim n As Integer = arr.Length
        Dim huboCambio As Boolean

        For i As Integer = 0 To n - 2
            huboCambio = False

            For j As Integer = 0 To n - i - 2
                If arr(j) > arr(j + 1) Then
                    Dim aux As Integer = arr(j)
                    arr(j) = arr(j + 1)
                    arr(j + 1) = aux
                    huboCambio = True
                End If
            Next

            If Not huboCambio Then
                Exit For
            End If
        Next
    End Sub

    Function EstaOrdenado(arr As Integer()) As Boolean
        For i As Integer = 0 To arr.Length - 2
            If arr(i) > arr(i + 1) Then
                Return False
            End If
        Next

        Return True
    End Function

    Function MedirBubbleSort(tamanio As Integer, repeticiones As Integer) As Double
        Dim tiempoTotal As Double = 0

        For r As Integer = 1 To repeticiones
            Dim datosOriginales As Integer() = GenerarDatos(tamanio)
            Dim datos As Integer() = CType(datosOriginales.Clone(), Integer())

            Dim reloj As Stopwatch = Stopwatch.StartNew()
            BubbleSort(datos)
            reloj.Stop()

            If Not EstaOrdenado(datos) Then
                Throw New Exception("Error: el arreglo no quedó ordenado.")
            End If

            tiempoTotal += reloj.Elapsed.TotalMilliseconds
        Next

        Return tiempoTotal / repeticiones
    End Function

    Sub Main()
        Console.WriteLine("Benchmark Bubble Sort - VB.NET")
        Console.WriteLine("---------------------------------------------")
        Console.WriteLine("Tamaño" & vbTab & vbTab & "Tiempo promedio (ms)")
        Console.WriteLine("---------------------------------------------")

        Dim repeticiones As Integer = 5
        Dim tamanios As Integer() = {1000, 3000, 5000}

        For Each tamanio As Integer In tamanios
            Dim tiempo As Double = MedirBubbleSort(tamanio, repeticiones)
            Console.WriteLine(tamanio & vbTab & vbTab & tiempo.ToString("F3"))
        Next
    End Sub

End Module