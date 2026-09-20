#include <stdio.h>
#include "automatas.h"
#include <string.h> //La implementamos para usar strlen() que devuelve la cantidad de caracteres sin contar '\0'
#include "punto1.h"

int procesar(const char *palabra, int *cantDecimal, int *cantOctal, int *cantHexadecimal)
{
    if (verificaDecimal(palabra) && palabraEsDecimal(palabra))
        (*cantDecimal)++;

    else if (verificaOctal(palabra) && palabraEsOctal(palabra))
        (*cantOctal)++; // octal

    else if (verificaHexadecimal(palabra) && palabraEsHexadecimal(palabra))
        (*cantHexadecimal)++; // hexadecimal
    else
        return 0; // Si hay un error lexico me devuelve 0

    return 1;
}

void ejecutarPunto1(void)
{
    char cadena[100];

    printf("Ingrese los numeros separados por @: ");
    scanf("%99s", cadena);

    char palabra[100];
    int longitud = strlen(cadena);
    int j = 0;

    int cantDecimal = 0;
    int cantOctal = 0;
    int cantHexadecimal = 0;

    for (int i = 0; i <= longitud; i++)
    {
        if (cadena[i] != '@' && cadena[i] != '\0')
        {
            palabra[j] = cadena[i];
            j++;
        }
        else
        {
            palabra[j] = '\0';
            if (!procesar(palabra, &cantDecimal, &cantOctal, &cantHexadecimal))
            {
                printf("ERROR LEXICO\n");
                return;
            }

            j = 0;
        }
    }

    printf("Decimales: %d\n", cantDecimal);
    printf("Octales: %d\n", cantOctal);
    printf("Hexadecimales: %d\n", cantHexadecimal);
}