#include "../Punto2/punto2.h"
#include "descensoRecursivo.h"

int numero(const char *cadena, int *pos) //Recorre la cadena y me devuelve el numero un numero(corte cuando se encuentra con +,-,* o \0 )
{
    int valor = 0;

    while (cadena[*pos] >= '0' && cadena[*pos] <= '9')
    {
        int digito = caracterAEntero(cadena[*pos]); //Usamos caracterAEntero()Funcion del punto2
        valor = valor * 10 + digito;
        (*pos)++;
    }

    return valor;
}

int termino(const char *cadena, int *pos)
{
    int resultado = numero(cadena, pos);
    
    //verifica si el caracter en la direccion *pos es *, si lo es toma el siguientre numero y realiza la multiplicacion
    while (cadena[*pos] == '*')
    {
        (*pos)++;

        int siguienteNumero = numero(cadena, pos);

        resultado = resultado * siguienteNumero;
    }

    return resultado;
}

int expresion(const char *cadena, int *pos)
{
    int resultado = termino(cadena, pos);

    while (cadena[*pos] == '+' || cadena[*pos] == '-')
    {
        char operador = cadena[*pos];
        (*pos)++;

        int siguienteTermino = termino(cadena, pos);

        if (operador == '+')
            resultado = resultado + siguienteTermino;
        else
            resultado = resultado - siguienteTermino;
    }

    return resultado;
}

int evaluarExpresion(const char *cadena)
{
    int pos = 0;

    return expresion(cadena, &pos);
}