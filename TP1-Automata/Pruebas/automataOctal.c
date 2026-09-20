#include <stdio.h>
#include <ctype.h>

int columna(int c) //Devuelve el numero de columna que corresponde
{
	switch(c)
	{
		case '0':
			return 0;
		default:
			return 1; //Devuelve o porque como "verificamos" que es una cadena valida ya es seguro que el caracter que resive es uno valido que pertenece a la primera fila. 
	}
}
int esPalabra(const char *cadena)
{
    //Obs: declaramos la matriz como static para que se cargue una sola vez
  static int tt[3][2] = {{1,2},
				        {1,1},
				        {2,2},
					   };
   	int e = 0; //e=Estado, me dice el estado actual
	unsigned int i = 0; 
	int c = cadena[0]; //c=caracter consumido
	
	while (c != '\0') 
	{
        //Este cruce me va a decir el nuevo estado
		e = tt[e][columna(c)]; //fila es el estado actual, y la columna la determino con la funcion columna que lo determina c(el caracter consumido)
		c = cadena[++i]; //avanzo al siguiente caracter
	}

	if (e == 1)  //Verifico que haya llegado al estado de aceptacion
		return 1;

	return 0;
}


//verificacion del alfabeto
int verifica(char *s)
{
	unsigned i;
	for(i=0;s[i]!='\0';i++)
	{
	    if(!(s[i] >= '0' && s[i] <= '7')) //isdigit->Indica si es un numero 
		{
			return 0;
		}
	}
	return 1;
}
int main()
{

	char palabra[]="077";
	
	//si verificamos que los caracteres sean correctos
	if(!verifica(palabra))
	{
		printf("HAY CARACTERES QUE NO PERTENECEN AL ALFABETO");
		return 0;
	}
;
	if(esPalabra(palabra))
	{
		printf("PERTENECE AL LENGUAJE");
		return 0;
	}else
	{
		printf("NO PERTENECE AL LENGUAJE");
		return 0;
	}
}