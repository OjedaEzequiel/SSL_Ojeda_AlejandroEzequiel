#include <stdio.h>
#include <ctype.h>

int columnaOperacion(int c) //Devuelve el numero de columna que corresponde
{
	switch(c)
	{
		case '0':
			return 0;
		case '+':
			return 2;
        case '-':
           return 3;
        case '*':
           return 4;
		default:
			return 1; // Si no es 0 ni un operador, luego de verificar el alfabeto, sabemos que pertenece a [1-9]
	}
}
int palabraEsOperacion(const char *cadena)
{
    //Obs: declaramos la matriz como static para que se cargue una sola vez
  static int tt[4][5] = {
	                    {2,1,3,3,3},
				        {1,1,0,0,0},
				        {3,3,0,0,0},
						{3,3,3,3,3}
					   };
   	int e = 0; //e=Estado, me dice el estado actual
	unsigned int i = 0; 
	int c = cadena[0]; //c=caracter consumido
	
	while (c != '\0') 
	{
        //Este cruce me va a decir el nuevo estado
		e = tt[e][columnaOperacion(c)]; //fila es el estado actual, y la columna la determino con la funcion columna que lo determina c(el caracter consumido)
		c = cadena[++i]; //avanzo al siguiente caracter
	}

	if (e == 1 || e==2)  // Verifico que haya llegado a uno de los estados de aceptación
		return 1;

	return 0;
}


//verificacion del alfabeto
int verificaOperacion(const char *s)
{
	unsigned i;
	for(i=0;s[i]!='\0';i++)
	{
	    if(!(s[i]=='+'||s[i]=='-'|| s[i]=='*'||
			isdigit(s[i]))) //isdigit->Indica si es un numero 
		{
			return 0;
		}
	}
	return 1;
}
