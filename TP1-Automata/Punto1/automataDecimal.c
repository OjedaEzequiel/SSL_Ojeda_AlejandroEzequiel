#include <stdio.h>
#include <ctype.h>

int columnaDecimal(int c) //Devuelve el numero de columna que corresponde
{
	switch(c)
	{
		case '+':
			return 2;
		case '-':
			return 3;
        case '0':
           return 1;
		default:
			return 0; //Devuelve o porque como "verificamos" que es una cadena valida ya es seguro que el caracter que resive es uno valido que pertenece a la primera fila. 
	}
}
int palabraEsDecimal(const char *cadena)
{
    //Obs: declaramos la matriz como static para que se cargue una sola vez
  static int tt[4][4] = {{2,3,1,1},
				        {2,3,3,3},
				        {2,2,3,3},
				        {3,3,3,3},
					   };
   	int e = 0; //e=Estado, me dice el estado actual
	unsigned int i = 0; 
	int c = cadena[0]; //c=caracter consumido
	
	while (c != '\0') 
	{
        //Este cruce me va a decir el nuevo estado
		e = tt[e][columnaDecimal(c)]; //fila es el estado actual, y la columna la determino con la funcion columna que lo determina c(el caracter consumido)
		c = cadena[++i]; //avanzo al siguiente caracter
	}

	if (e == 2)  //Verifico que haya llegado al estado de aceptacion
		return 1;

	return 0;
}


//verificacion del alfabeto
int verificaDecimal(const char *s)
{
	unsigned i;
	for(i=0;s[i]!='\0';i++)
	{
	    if(!(s[i]=='+'||s[i]=='-'||
			isdigit(s[i]))) //isdigit->Indica si es un numero 
		{
			return 0;
		}
	}
	return 1;
}
