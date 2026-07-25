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

fun bubbleSort(arr: IntArray) {
    val n = arr.size
    var huboCambio: Boolean

    for (i in 0 until n - 1) {
        huboCambio = false

        for (j in 0 until n - i - 1) {
            if (arr[j] > arr[j + 1]) {
                val aux = arr[j]
                arr[j] = arr[j + 1]
                arr[j + 1] = aux
                huboCambio = true
            }
        }

        if (!huboCambio) {
            break
        }
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

fun medirBubbleSort(tamanio: Int, repeticiones: Int): Double {
    var tiempoTotal = 0.0

    repeat(repeticiones) {
        val datosOriginales = generarDatos(tamanio)
        val datos = datosOriginales.copyOf()

        val tiempo = measureNanoTime {
            bubbleSort(datos)
        }

        if (!estaOrdenado(datos)) {
            throw RuntimeException("Error: el arreglo no quedó ordenado.")
        }

        tiempoTotal += tiempo / 1_000_000.0
    }

    return tiempoTotal / repeticiones
}

fun main() {
    println("Benchmark Bubble Sort - Kotlin")
    println("---------------------------------------------")
    println("Tamaño\t\tTiempo promedio (ms)")
    println("---------------------------------------------")

    val repeticiones = 5
    val tamanios = intArrayOf(1000, 3000, 5000)

    for (tamanio in tamanios) {
        val tiempo = medirBubbleSort(tamanio, repeticiones)
        println("$tamanio\t\t${"%.3f".format(tiempo)}")
    }
}