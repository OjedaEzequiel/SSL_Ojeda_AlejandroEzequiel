%{
#include <stdio.h>
#include "y.tab.h"
#include <stdlib.h>

int hay_error_lexico = 0;
%}

%option noyywrap
%option yylineno


%%

"inicio"                    { return INICIO; }
"fin"                       { return FIN; }
"leer"                      { return LEER; }
"escribir"                  { return ESCRIBIR; }

[a-zA-Z][a-zA-Z0-9]*        { return ID; } 
[0-9]+                      { return CONSTANTE; }

":="                        { return ASIGNACION; }
"+"                         { return SUMA; }
"-"                         { return RESTA; }
";"                         { return PUNTOYCOMA; }
"("                         { return PARENIZQUIERDO; }
")"                         { return PARENDERECHO; }

[ \t\r\n]+                  ;

. {
    printf("Error lexico en linea %d: caracter no reconocido '%s'\n",
           yylineno, yytext);
    hay_error_lexico = 1;
    exit(EXIT_FAILURE);
}

%%