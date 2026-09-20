#include <stdio.h>
#include <ctype.h>

int columnaOctal(int c) //Devuelve el numero de columna que corresponde
{
	switch(c)
	{
		case '0':
			return 0;
		default:
			return 1; //Devuelve o porque como "verificamos" que es una cadena valida ya es seguro que el caracter que resive es uno valido que pertenece a la primera fila. 
	}
}
int palabraEsOctal(const char *cadena)
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
		e = tt[e][columnaOctal(c)]; //fila es el estado actual, y la columna la determino con la funcion columna que lo determina c(el caracter consumido)
		c = cadena[++i]; //avanzo al siguiente caracter
	}

	if (e == 1)  //Verifico que haya llegado al estado de aceptacion
		return 1;

	return 0;
}


//verificacion del alfabeto
int verificaOctal(const char *s)
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
