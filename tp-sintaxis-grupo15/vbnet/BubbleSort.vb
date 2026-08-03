Imports System  'Permite utilizar elementos basicos de .NET
Imports System.Diagnostics
' Permite utilizar "Stopwatch" la cual es la clase que se usa para medir cuánto tarda en ejecutarse una parte del programa.

Module BubbleSortBenchmark

    
    'Crea un arreglo de la cantidad indicada y lo llena con números pseudoaleatorios.
    Function GenerarDatos(tamanio As Integer) As Integer() 'Esta funcion recibe un número entero y devuelve un arreglo de enteros.
        Dim datos(tamanio - 1) As Integer 'crea un arreglo llamado datos.
        Dim x As Long = 1234567L 'Se declara una variable x de tipo Long
        'La L indica que el número debe interpretarse como un valor de tipo Long.

        ' Uso siempre la misma fórmula para poder comparar mejor los tiempos.
        For i As Integer = 0 To tamanio - 1 'Este ciclo recorre todas las posiciones del arreglo.
            x = (x * 48271L) Mod 2147483647L
            datos(i) = CInt(x Mod 1000000L)
        Next

        Return datos
    End Function
    
    'Ordena el arreglo recibido de menor a mayor usando el algoritmo Bubble Sort.'Compara elementos vecinos y los intercambia cuando están en el orden incorrecto
    Sub BubbleSort(arr As Integer()) 'recibe un arreglo de enteros llamado arr.
        Dim n As Integer = arr.Length 'devuelve la cantidad de elementos del arreglo y lo guarda en la variable n.
        Dim huboCambio As Boolean

        ' Bubble Sort compara elementos vecinos y los intercambia si hace falta.
        For i As Integer = 0 To n - 2
            huboCambio = False 'Esta variable sirve para saber si durante una pasada se realizó algún intercambio. Si no se hizo ningún intercambio, significa que el arreglo ya estaba ordenado.

            For j As Integer = 0 To n - i - 2 'Este ciclo controla la cantidad de pasadas del algoritmo.
                If arr(j) > arr(j + 1) Then
                    Dim aux As Integer = arr(j)
                    arr(j) = arr(j + 1)
                    arr(j + 1) = aux
                    huboCambio = True
                End If
            Next

            ' Si no hubo cambios, ya está ordenado.
            If Not huboCambio Then
                Exit For
            End If
        Next
    End Sub

    'Resive un arreglo de enteros y devuelve un booleano. True si esta ordenado, False si no lo esta.
    Function EstaOrdenado(arr As Integer()) As Boolean
        For i As Integer = 0 To arr.Length - 2
            If arr(i) > arr(i + 1) Then
                Return False
            End If
        Next

        Return True
    End Function

    'Esta funcion recibe:
    'tamanio: cantidad de elementos del arreglo
    'repeticiones: cantidad de veces que se ejecutará Bubble Sort
    'Devuelve un Double, que representa el tiempo promedio en milisegundos.
    'Osea Mide cuánto tarda Bubble Sort.
Function MedirBubbleSort(tamanio As Integer, repeticiones As Integer) As Double
    Dim tiempoTotal As Double = 0

    ' Repite la prueba varias veces para obtener un promedio.
    For r As Integer = 1 To repeticiones

        ' Genera un arreglo nuevo con la cantidad de elementos indicada.
        Dim datos As Integer() = CType(GenerarDatos(tamanio).Clone(), Integer())

        ' Inicia el cronómetro justo antes de ordenar.
        Dim reloj As Stopwatch = Stopwatch.StartNew()

        BubbleSort(datos)

        ' Detiene la medición cuando termina el ordenamiento.
        reloj.Stop()

        ' Comprueba que el algoritmo haya ordenado correctamente el arreglo.
        If Not EstaOrdenado(datos) Then
            Throw New Exception("El arreglo no quedó ordenado.")
        End If

        ' Suma el tiempo de esta ejecución al tiempo total.
        tiempoTotal += reloj.Elapsed.TotalMilliseconds
    Next

    ' Devuelve el tiempo promedio de todas las ejecuciones.
    Return tiempoTotal / repeticiones
End Function

    Sub Main()
        Console.WriteLine("Benchmark Bubble Sort - VB.NET")
        Console.WriteLine("---------------------------------------------")
        Console.WriteLine("Tamaño" & vbTab & vbTab & "Tiempo promedio (ms)")
        Console.WriteLine("---------------------------------------------")

        Dim repeticiones As Integer = 5 'Cada tamaño de arreglo será probado 5 veces.
        Dim tamanios As Integer() = {1000, 3000, 5000} 'Se crea un arreglo con tres tamaños:

        For Each tamanio As Integer In tamanios 'recorre directamente cada elemento del arreglo.
            Dim tiempo As Double = MedirBubbleSort(tamanio, repeticiones) 'Llama a la funcion MedirBubbleSort y guarda el resultado en la variable tiempo.
            Console.WriteLine(tamanio & vbTab & vbTab & tiempo.ToString("F3")) 'Muestra resultado
        Next
    End Sub

End Module