
#include "automatapto3.h"
#include "descensoRecursivo.h"
#include <stdio.h>
/*
void ejecutarPunto3()
{
    char cadena[100];
    printf("Ingrese una operacion con enteros decimales usando +, - y *: ");
    scanf("%99s", cadena);
    
    if(verificaOperacion(cadena) && palabraEsOperacion(cadena)){
        int resultado = evaluarExpresion(cadena);
        printf("Cadena: %s\n", cadena);
         printf("Resultado: %d\n", resultado);

    }
    else
    {
         printf("No verifica\n");
    }

}
*/

void ejecutarPunto3()
{
    char cadena[100];
    printf("Ingrese una operacion con enteros decimales usando +, - y *: ");
    scanf("%99s", cadena);
    
    if(!verificaOperacion(cadena)){
        printf("Hay un caracter que no pertenece al alfabeto\n");
    }
    else if(!palabraEsOperacion(cadena))
    {
        printf("Todos los caracteres pertenecen al alfabeto, pero la cadena no pertenece al lenguaje\n");
    }
    else
    {
        int resultado = evaluarExpresion(cadena);
        printf("Cadena: %s\n", cadena);
         printf("Resultado: %d\n", resultado);
    }

}