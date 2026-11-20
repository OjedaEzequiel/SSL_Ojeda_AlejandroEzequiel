%{
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "y.tab.h"
int hay_error_lexico = 0;
%}

%option noyywrap
%option yylineno


%%

"inicio"                    { return INICIO; }
"fin"                       { return FIN; }
"leer"                      { return LEER; }
"escribir"                  { return ESCRIBIR; }


[a-zA-Z][a-zA-Z0-9]*    { yylval.cadena = malloc(strlen(yytext) + 1); strcpy(yylval.cadena, yytext); return ID; }

[0-9]+ {
    yylval.num = atoi(yytext);
    return CONSTANTE;
}

":="                        { return ASIGNACION; }
"+"                         { return SUMA; }
"-"                         { return RESTA; }
";"                         { return PUNTOYCOMA; }
","                         { return COMA; }
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