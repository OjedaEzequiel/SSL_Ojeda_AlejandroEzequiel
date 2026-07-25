import kotlin.system.measureNanoTime

fun generarDatos(tamanio: Int): IntArray {
    val datos = IntArray(tamanio)
    var x = 1234567L

    for (i in 0 until tamanio) {
        x = (x * 48271L) % 2147483647L
        datos[i] = (x % 1000000L).toInt()
    }

    return datos
}

fun quickSort(arr: IntArray, izquierda: Int, derecha: Int) {
    var i = izquierda
    var j = derecha
    val pivote = arr[(izquierda + derecha) / 2]

    while (i <= j) {
        while (arr[i] < pivote) {
            i++
        }

        while (arr[j] > pivote) {
            j--
        }

        if (i <= j) {
            val aux = arr[i]
            arr[i] = arr[j]
            arr[j] = aux
            i++
            j--
        }
    }

    if (izquierda < j) {
        quickSort(arr, izquierda, j)
    }

    if (i < derecha) {
        quickSort(arr, i, derecha)
    }
}

fun estaOrdenado(arr: IntArray): Boolean {
    for (i in 0 until arr.size - 1) {
        if (arr[i] > arr[i + 1]) {
            return false
        }
    }

    return true
}

fun medirQuickSort(tamanio: Int, repeticiones: Int): Double {
    var tiempoTotal = 0.0

    repeat(repeticiones) {
        val datosOriginales = generarDatos(tamanio)
        val datos = datosOriginales.copyOf()

        val tiempo = measureNanoTime {
            quickSort(datos, 0, datos.size - 1)
        }

        if (!estaOrdenado(datos)) {
            throw RuntimeException("Error: el arreglo no quedó ordenado.")
        }

        tiempoTotal += tiempo / 1_000_000.0
    }

    return tiempoTotal / repeticiones
}

fun main() {
    println("Benchmark Quick Sort - Kotlin")
    println("---------------------------------------------")
    println("Tamanio\t\tTiempo promedio (ms)")
    println("---------------------------------------------")

    val repeticiones = 5
    val tamanios = intArrayOf(10000, 50000, 100000)

    // Warm up para reducir el impacto inicial de la JVM
    val prueba = generarDatos(1000)
    quickSort(prueba, 0, prueba.size - 1)

    for (tamanio in tamanios) {
        val tiempo = medirQuickSort(tamanio, repeticiones)
        println("$tamanio\t\t${"%.3f".format(tiempo)}")
    }
}