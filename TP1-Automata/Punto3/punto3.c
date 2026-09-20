/*No te voy a escribir la función completa, pero sí te marco qué debería hacer:

tener una cadena de prueba, por ejemplo "12*34-5";
llamar primero a verificaOperacion(cadena);
si falla, mostrar que hay caracteres fuera del alfabeto;
si pasa, llamar a palabraEsOperacion(cadena);
si también pasa, informar que pertenece al lenguaje;
si no, informar que no pertenece.

Y además en punto3.h deberías declarar:*/
#include "automatapto3.h"
#include "descensoRecursivo.h"
#include <stdio.h>

void ejecutarPunto3()
{
    char cadena[] = "50-10-5";
    
    if(verificaOperacion(cadena) && palabraEsOperacion(cadena)){
        printf("Verifica\n");
        int resultado = evaluarExpresion(cadena);
        printf("Cadena: %s\n", cadena);
         printf("Resultado: %d\n", resultado);

    }
    else
    {
         printf("No verifica\n");
    }

}