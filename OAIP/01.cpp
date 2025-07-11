

#include <stdio.h>
#include <math.h>
#include <locale.h>

int main()
{
    setlocale(LC_ALL, "rus");
    float x, z, y;
    x = y = z = 0;
    printf ("Первая часть задания\n");
    printf("Согласно условия задачи, программа работает корректно при вводе чисел\n");
    printf("Введите переменную Х: ");
    scanf_s("%f", &x);
    printf("\nВведите переменную Y: ");
    scanf_s("%f", &y);
    printf("\nВведите переменную Z: ");
    scanf_s("%f", &z);
    printf("\nПеременная b = %f", x + (cbrt(z * y) / (y + cos(x))));
    int a, a1, a2, a3;
    a = 0;
    printf("\nВторая часть задания\n");
    printf("Согласно условия задачи, программа работает корректно при вводе целого трехзначного числа\n");
    printf("Введите число ");
    scanf_s("%d", &a);
    a1 = a / 100;
    a %= 100;
    a2 = a / 10;
    a %= 10;
    a3 = a;
    a= a1 + a2 + a3;
    printf("\nПеременная a = %d", a1 + a2 + a3);
    printf("\n");
    return 0;
}

