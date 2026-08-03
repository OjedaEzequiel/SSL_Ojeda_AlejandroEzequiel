' Este programa realiza un benchmark del algoritmo Quick Sort.
' Genera arreglos de distintos tamaños, los ordena varias veces,
' verifica que el resultado sea correcto y muestra el tiempo promedio.

Imports System
Imports System.Diagnostics

Module QuickSortBenchmark
    'Crea un arreglo con la cantidad de elementos indicada y lo llena con números pseudoaleatorios.
    Function GenerarDatos(tamanio As Integer) As Integer()
        Dim datos(tamanio - 1) As Integer
        Dim x As Long = 1234567L

        ' Genero siempre los mismos valores para que el benchmark sea comparable.
        'Recorre todas las posiciones y las llena con números pseudoaleatorios entre 0 y 999999.
        For i As Integer = 0 To tamanio - 1
            x = (x * 48271L) Mod 2147483647L
            datos(i) = CInt(x Mod 1000000L)
        Next

        Return datos
    End Function

    'Ordena una parte del arreglo usando el algoritmo Quick Sort
    ' Selecciona el elemento central como pivote, divide los valores en
    ' menores y mayores al pivote, y luego ordena cada parte por separado.
    Sub QuickSort(arr As Integer(), izquierda As Integer, derecha As Integer)
        ' i recorre desde la izquierda y j desde la derecha,
        ' buscando elementos que deban intercambiarse.
        Dim i As Integer = izquierda
        Dim j As Integer = derecha
        Dim pivote As Integer = arr((izquierda + derecha) \ 2)

        ' Se acomodan los elementos según sean menores o mayores al pivote.
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

        ' Se repite el proceso sobre cada parte del arreglo.
        If izquierda < j Then
            QuickSort(arr, izquierda, j)
        End If

        If i < derecha Then
            QuickSort(arr, i, derecha)
        End If
    End Sub

    ' Verifica que el arreglo esté ordenado en forma ascendente.
    ' Devuelve False si encuentra un elemento mayor que el siguiente.
    Function EstaOrdenado(arr As Integer()) As Boolean
        For i As Integer = 0 To arr.Length - 2
            If arr(i) > arr(i + 1) Then
                Return False
            End If
        Next

        Return True
    End Function
    
    ' Ejecuta Quick Sort varias veces sobre arreglos del tamaño indicado.
    ' Mide el tiempo de cada ejecución, verifica el resultado
    ' y devuelve el tiempo promedio en milisegundos.
Function MedirQuickSort(tamanio As Integer, repeticiones As Integer) As Double
    Dim tiempoTotal As Double = 0

    ' Repite la prueba varias veces para calcular un tiempo promedio.
    For r As Integer = 1 To repeticiones

        ' Genera un arreglo nuevo con la cantidad de elementos indicada.
        Dim datos As Integer() = CType(GenerarDatos(tamanio).Clone(), Integer())

        ' Inicia el cronómetro justo antes de comenzar el ordenamiento.
        Dim reloj As Stopwatch = Stopwatch.StartNew()

        ' Ordena todo el arreglo, desde la primera hasta la última posición.
        QuickSort(datos, 0, datos.Length - 1)

        ' Detiene la medición cuando termina Quick Sort.
        reloj.Stop()

        ' Comprueba que el arreglo haya quedado correctamente ordenado.
        If Not EstaOrdenado(datos) Then
            Throw New Exception("El arreglo no quedó ordenado.")
        End If

        ' Suma el tiempo de esta ejecución al tiempo total.
        tiempoTotal += reloj.Elapsed.TotalMilliseconds
    Next

    ' Devuelve el promedio de todas las mediciones.
    Return tiempoTotal / repeticiones
End Function

    Sub Main()
        Console.WriteLine("Benchmark Quick Sort - VB.NET")
        Console.WriteLine("---------------------------------------------")
        Console.WriteLine("Tamaño" & vbTab & vbTab & "Tiempo promedio (ms)")
        Console.WriteLine("---------------------------------------------")

        Dim repeticiones As Integer = 5
        Dim tamanios As Integer() = {10000, 50000, 100000}

        ' Ejecución previa para reducir el efecto inicial de .NET.
        Dim prueba As Integer() = GenerarDatos(1000)
        QuickSort(prueba, 0, prueba.Length - 1)

        For Each tamanio As Integer In tamanios
            Dim tiempo As Double = MedirQuickSort(tamanio, repeticiones)
            Console.WriteLine(tamanio & vbTab & vbTab & tiempo.ToString("F3"))
        Next
    End Sub

End Module