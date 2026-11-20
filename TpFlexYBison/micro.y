%{
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

int yylex(void);
void yyerror(char *s);

extern int yylineno;
extern char *yytext;
extern int hay_error_lexico;

#define MAX_IDS 100
#define MAX_LONG 50

char tablaSimbolos[MAX_IDS][MAX_LONG]; //Va a guardar los nombres.
int cantidadIds = 0; //Nos dice cuantos elementos tenemos guardados en la tabla.

int buscar_id(char *nombre);
void procesar_id(char *nombre);
char *GenInfijo(char *izq, char operador, char *der);

%}

%union {
    char *cadena;
    int num;
    char operador;
}



%token INICIO FIN
//%token ID CONSTANTE
//Con cadena le decimos a Bison que el token ID tiene asociado un texto.
%token <cadena> ID
%token <num> CONSTANTE

%token ASIGNACION
%token PUNTOYCOMA
%token SUMA RESTA
%token PARENIZQUIERDO PARENDERECHO
%token LEER ESCRIBIR
%token COMA

%type <cadena> identificador
%type <cadena> primaria
%type <cadena> expresion
%type <operador> operadorAditivo

%%

programa:
    INICIO listaSentencias FIN
    /*{
        printf("Programa sintacticamente correcto\n");
    }*/
;
identificador:
    ID
    {
        procesar_id($1); //Es la accion semantica. La gramatica reconoce un ID y  llama a la rutina.
        $$ = $1; //Significa El resultado de identificador será el texto que vino en ID
        //El $1 representa el valor asociado al primer elemento de la producción.
    }
;

listaSentencias:
      sentencia
    | listaSentencias sentencia
;

sentencia:
    identificador ASIGNACION expresion PUNTOYCOMA
    | LEER PARENIZQUIERDO listaIdentificadores PARENDERECHO PUNTOYCOMA
    | ESCRIBIR PARENIZQUIERDO listaExpresiones PARENDERECHO PUNTOYCOMA
;
expresion:
      primaria
      {
          $$ = $1;
      }
    | expresion operadorAditivo primaria
      {
          $$ = GenInfijo($1, $2, $3);
      }
;

primaria:
      identificador
      {
          $$ = $1;
      }
    | CONSTANTE
      {
          char *texto = malloc(20);
          sprintf(texto, "%d", $1);
          $$ = texto;
      }
    | PARENIZQUIERDO expresion PARENDERECHO
      {
          $$ = $2;
      }
;

operadorAditivo:
      SUMA
      {
          $$ = '+';
      }
    | RESTA
      {
          $$ = '-';
      }
;

listaIdentificadores:
      identificador
    | listaIdentificadores COMA identificador
;

listaExpresiones:
      expresion
    | listaExpresiones COMA expresion
;

%%

int main(void)
{
    int resultado = yyparse();

    if(resultado == 0 && !hay_error_lexico)
        printf("Programa correcto: analisis lexico,sintactico y semantico exitoso\n");

    return resultado;
}
    

void yyerror(char *s)
{
    printf("Error sintactico en linea %d cerca de '%s'\n",
           yylineno, yytext);
}

int buscar_id(char *nombre) //Busca si esta el identificador a la tabla de símbolos
{
    int i;

    for(i = 0; i < cantidadIds; i++)
    {
        if(strcmp(tablaSimbolos[i], nombre) == 0)
            return 1;
    }

    return 0;
}

void procesar_id(char *nombre)
{
    if(!buscar_id(nombre)) //Verifica si el identificador NO esta en la tabla
    {
        strcpy(tablaSimbolos[cantidadIds], nombre); //Agrega el identificador a la tabla de simbolos
        cantidadIds++; //Incrementa el contador de la cantidad de elementos que tenemos en la tabla

        printf("Rutina semantica: se agrego '%s' a la tabla de simbolos\n",
               nombre);
    }
}

char *GenInfijo(char *izq, char operador, char *der)
{
    static int numTemp = 1;

    char *temp = malloc(20);

    sprintf(temp, "Temp&%d", numTemp);
    numTemp++;

    procesar_id(temp);

    if(operador == '+')
    {
        printf("GenInfijo: ADD %s, %s, %s\n",
               izq, der, temp);
    }
    else
    {
        printf("GenInfijo: SUBS %s, %s, %s\n",
               izq, der, temp);
    }

    return temp;
}