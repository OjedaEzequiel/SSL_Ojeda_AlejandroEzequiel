%{
#include <stdio.h>

int yylex(void);
void yyerror(char *s);

extern int yylineno;
extern char *yytext;
extern int hay_error_lexico;
%}

%token INICIO FIN
%token ID CONSTANTE
%token ASIGNACION
%token PUNTOYCOMA
%token SUMA RESTA
%token PARENIZQUIERDO PARENDERECHO
%token LEER ESCRIBIR
%token COMA

%%

programa:
    INICIO listaSentencias FIN
    /*{
        printf("Programa sintacticamente correcto\n");
    }*/
;

listaSentencias:
      sentencia
    | listaSentencias sentencia
;

sentencia:
    ID ASIGNACION expresion PUNTOYCOMA
    | LEER PARENIZQUIERDO listaIdentificadores PARENDERECHO PUNTOYCOMA
    | ESCRIBIR PARENIZQUIERDO listaExpresiones PARENDERECHO PUNTOYCOMA
;
expresion:
      primaria
    | expresion operadorAditivo primaria
;

primaria:
      ID
    | CONSTANTE
    | PARENIZQUIERDO expresion PARENDERECHO
;

operadorAditivo:
      SUMA
    | RESTA
;
listaIdentificadores:
      ID
    | listaIdentificadores COMA ID
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
        printf("Programa correcto: analisis lexico y sintactico exitoso\n");

    return resultado;
}
    

void yyerror(char *s)
{
    printf("Error sintactico en linea %d cerca de '%s'\n",
           yylineno, yytext);
}