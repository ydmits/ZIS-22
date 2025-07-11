
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <stdio.h>
#include <math.h>
#include <locale.h>
using namespace std;

int main()
{
    setlocale(LC_ALL, "rus");
    cout << "Первая часть задания\n";
    cout << "Согласно условия задачи, программа работает корректно при вводе чисел\n";
    cout << "Введите переменную x: ";
    float x;
    cin >> x;
    cout << "Вы ввели переменную x= " << x << endl;
    if (x >= -5 && x != 0)
    {
        cout << "Номер формулы, по которому происходит вычисление функции y - 1\n";
        cout << "Функция y = " << 1 / x << endl;
    }
    else if (x <= -10)
    {
        cout << "Номер формулы, по которому происходит вычисление функции y - 2\n";
        cout << "Функция y = " << x * x << endl;
    }
    else if ((x<-5 && x>-10) || x == 0)
    {
        cout << "Номер формулы, по которому происходит вычисление функции y - 3\n";
        cout << "Функция y = " << sqrt(abs(x + 1)) << endl;
    }
    else cout << "Некорректный ввод";

    cout << "Вторая часть задания\n";
    cout << "Введите номер месяца (1-12)\n";
    int n = 0;
    cin >> n;
    switch (n)
    {
    case 1: cout << "Январь\n"; break;
    case 2: cout << "Февраль\n"; break;
    case 3: cout << "Март\n"; break;
    case 4: cout << "Апрель\n"; break;
    case 5: cout << "Май\n"; break;
    case 6: cout << "Июнь\n"; break;
    case 7: cout << "Июль\n"; break;
    case 8: cout << "Август\n"; break;
    case 9: cout << "Сентябрь\n"; break;
    case 10: cout << "Октябрь\n"; break;
    case 11: cout << "Декабрь\n"; break;
    case 12: cout << "Ноябрь\n"; break;

    default:
        cout << "Некорректный ввод\n"; break;
    }
    cout << "Третья часть задания\n";
    float X, Y;
    cout << "Введите координату X: ";
    cin >> X;
    cout << "\nВведите координату Y: ";
    cin >> Y;
    cout << endl;
    if (X == 0 && Y == 0)
        cout << "Точка находится в центре оси координат\n";
    else if (X == 0 && Y > 0)
        cout << "Точка находится на оси OY в верхней части\n";
    else if (X == 0 && Y < 0)
        cout << "Точка находится на оси OY в нижней части\n";
    else if (X > 0 && Y == 0)
        cout << "Точка находится на оси OX в правой части\n";
    else if (X < 0 && Y == 0)
        cout << "Точка находится на оси OX в левой части\n";
    else if (X > 0 && Y > 0)
        cout << "Точка находится в верхней правой части оси координат\n";
    else if (X < 0 && Y > 0)
        cout << "Точка находится в верхней левой части оси координат\n";
    else if (X < 0 && Y < 0)
        cout << "Точка находится в нижней левой части оси координат\n";
    else if (X < 0 && Y < 0)
        cout << "Точка находится в нижней левой части оси координат\n";
    else cout << "Некорректный ввод\n";

    return 0;
}

