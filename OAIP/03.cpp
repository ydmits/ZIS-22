

#include <iostream>
#include <cmath>
#include <iomanip>
using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");
	float y, a, b, x;
	int x_min, x_max, x_step, counter;
	counter = 0;
	cout << "Первая часть.\n";
	//ввод параметров
	cout << "Будет выполнен расчёт функции с вводом её параметров с помощью цикла while\n";
	cout << "Введите переменную a: ";
	counter += scanf_s("%f",&a);
	cout << "\nВведите переменную b: ";
	counter += scanf_s("%f", &b);
	cout << "\nВведите начало длиапазона x_min: ";
	counter += scanf_s("%d", &x_min);
	cout << "\nВведите конец диапазона x_max: ";
	counter += scanf_s("%d", &x_max);
	cout << "\nВведите шаг изменения x_step\n";
	counter += scanf_s("%d", &x_step);
	cout << "\n";
	//проверка на корректность ввода и расчёт
	if (counter == 5 && x_min < x_max && x_step < x_max)
	{
		x = x_min;
		while (x<x_max)
		{
			y = ((a + b * x) * (a + b * x)) / (1 + pow((cos(a * x)), 3));
			cout << "y = " << y << "\n";
			x += x_step;
		}
	}
	else
		cout << "Некорректный ввод\n";
	cout << "Вторая часть.\n";
	//ввод параметров
	float X, Y;
	int X_min, X_max, X_step;
	
	cout << "Введите начало диапазона X_min: ";
	cin >> X_min;
	cout << "\nВведите конец диапазона X_max: ";
	cin >> X_max;
	cout << "\nВведите шаг изменения X_step: ";
	cin >> X_step;
	cout << "\n";
	//проверка на корректность ввода и расчёт
	if (X_min < X_max && X_step > 0)
	{
		cout << "----------------------------------\n";
		cout << "|   x     |       y       |    n  |\n";
		cout << "----------------------------------\n";
		
		for (X=X_min; X <= X_max; X += X_step)
		{
			int n = 0;
			if (X >= 3)
			{
				Y = sin(X);
				n = 1;
			}
			else if (X <= -10)
			{
				Y = X * X;
				n = 2;
			}
			else
			{
				Y = sqrt(abs(X + 1));
				n = 3;
			}
			cout << "|" << setw(5) << X << setw(5) << "|" << setw(5) << Y << setw(8) << "|" << setw(4) << n << setw(4) << "|\n";
			cout << "-------------------------------\n";

		}
	}
	else
		cout << "Некорректный ввод\n";
	cout << "Третья часть\n";
	cout << "Программа принимает вводимые числа и считает произведение, пока не будет введён ноль\n";
	int flag = 1;
	float A, B = 1;
	while (flag)
	{
		cout << "Введите число: ";
		cin >> A;
		cout << endl;
		if (A == 0) break;
		B *= A;
	}
	cout << "Произведение чисел составило: " << B << endl;
	return 0;


}


