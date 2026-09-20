#ifndef AUTOMATAS_H
#define AUTOMATAS_H
//Este archivo sirve para decirle a main.c qué funciones existen.
int verificaDecimal(const char *cadena);
int palabraEsDecimal(const char *cadena);

int verificaOctal(const char *cadena);
int palabraEsOctal(const char *cadena);

int verificaHexadecimal(const char *cadena);
int palabraEsHexadecimal(const char *cadena);

#endif