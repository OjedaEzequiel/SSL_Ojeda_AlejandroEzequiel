#include <stdio.h>
#include "Punto1/punto1.h"
#include "Punto2/punto2.h"
#include "Punto3/punto3.h"
#include <stdlib.h> //  Implemento esta libreria para poder hacer una limpieza de pantalla

int main()
{
    int opcion = -1;
    while (opcion != 0)
    {
        system("cls"); // Hace una limpieza de pantalla

        printf("Seleccione el Punto: \n");
        printf("Punto 1: \n");
        printf("Punto 2: \n");
        printf("Punto 3: \n");
        printf("0 - Salir\n");
        printf("Opcion: ");
        scanf("%d", &opcion);

        switch (opcion)
        {
        case 1:
            printf("Punto 1: \n");
            ejecutarPunto1();
            break;
        case 2:
            printf("Punto 2: \n");
            ejecutarPunto2();
            break;
        case 3:

            printf("Punto 3: \n");
            ejecutarPunto3();
            break;
        default:
            printf("Ingrese una opcion valida: \n");
            break;
        }

        //Este if sirve para que haga pausa, asi podemos ver el resultado antes de que haga una limpieza de pantalla
        if (opcion != 0)
        {
            printf("\n");
            system("pause");
        }
    }

    return 0;
}