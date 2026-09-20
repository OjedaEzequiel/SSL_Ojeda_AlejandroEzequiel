#include <stdio.h>
#include "punto2.h"

int caracterAEntero(char caracter)
{
    if (caracter >= '0' && caracter <= '9')
    {
        return caracter - '0';
    }

    return -1; // indica que el carácter no era numérico
}

void ejecutarPunto2()
{
    char caracter;
    printf("Ingrese un caracter numerico: ");
    scanf(" %c", &caracter);

    int numero= caracterAEntero(caracter);
    if(numero ==-1){
        printf("ERROR: CARACTER NO ES NUMERICO\n");
    }
    else
    {
        printf("Caracter: %c\n",caracter);
        printf("Entero: %d\n",numero);
    }
}