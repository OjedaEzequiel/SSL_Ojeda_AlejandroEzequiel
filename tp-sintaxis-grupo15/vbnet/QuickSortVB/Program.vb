Imports System
Imports System.Diagnostics

Module QuickSortBenchmark

    Function GenerarDatos(tamanio As Integer) As Integer()
        Dim datos(tamanio - 1) As Integer
        Dim x As Long = 1234567L

        For i As Integer = 0 To tamanio - 1
            x = (x * 48271L) Mod 2147483647L
            datos(i) = CInt(x Mod 1000000L)
        Next

        Return datos
    End Function

    Sub QuickSort(arr As Integer(), izquierda As Integer, derecha As Integer)
        Dim i As Integer = izquierda
        Dim j As Integer = derecha
        Dim pivote As Integer = arr((izquierda + derecha) \ 2)

        While i <= j
            While arr(i) < pivote
                i += 1
            End While

            While arr(j) > pivote
                j -= 1
            End While

            If i <= j Then
                Dim aux As Integer = arr(i)
                arr(i) = arr(j)
                arr(j) = aux
                i += 1
                j -= 1
            End If
        End While

        If izquierda < j Then
            QuickSort(arr, izquierda, j)
        End If

        If i < derecha Then
            QuickSort(arr, i, derecha)
        End If
    End Sub

    Function EstaOrdenado(arr As Integer()) As Boolean
        For i As Integer = 0 To arr.Length - 2
            If arr(i) > arr(i + 1) Then
                Return False
            End If
        Next

        Return True
    End Function

    Function MedirQuickSort(tamanio As Integer, repeticiones As Integer) As Double
        Dim tiempoTotal As Double = 0

        For r As Integer = 1 To repeticiones
            Dim datosOriginales As Integer() = GenerarDatos(tamanio)
            Dim datos As Integer() = CType(datosOriginales.Clone(), Integer())

            Dim reloj As Stopwatch = Stopwatch.StartNew()
            QuickSort(datos, 0, datos.Length - 1)
            reloj.Stop()

            If Not EstaOrdenado(datos) Then
                Throw New Exception("Error: el arreglo no quedó ordenado.")
            End If

            tiempoTotal += reloj.Elapsed.TotalMilliseconds
        Next

        Return tiempoTotal / repeticiones
    End Function

    Sub Main()
        Console.WriteLine("Benchmark Quick Sort - VB.NET")
        Console.WriteLine("---------------------------------------------")
        Console.WriteLine("Tamaño" & vbTab & vbTab & "Tiempo promedio (ms)")
        Console.WriteLine("---------------------------------------------")

        Dim repeticiones As Integer = 5
        Dim tamanios As Integer() = {10000, 50000, 100000}

        ' Warm up para reducir el impacto inicial de .NET
        Dim prueba As Integer() = GenerarDatos(1000)
        QuickSort(prueba, 0, prueba.Length - 1)

        For Each tamanio As Integer In tamanios
            Dim tiempo As Double = MedirQuickSort(tamanio, repeticiones)
            Console.WriteLine(tamanio & vbTab & vbTab & tiempo.ToString("F3"))
        Next
    End Sub

End Module