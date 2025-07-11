#include <iostream>
#include <time.h>
#include <stdlib.h>
#include <iomanip>
#include <cmath>
#include <Windows.h>
using namespace std;

int main()
{
    setlocale(LC_ALL, "rus");
    srand(time(NULL));
    cout << "Первая часть\n";
    int m, n, max, min, sum;
    sum = 0;
    cout << "Будет сформирован динамический массив случайных чисел\n";
    cout << "Введите количество строк: ";
    cin >> m;
    cout << endl;
    cout << "Введите количество столбцов: ";
    cin >> n;
    cout << endl;
    cout << "Введите нижний предел генерации: ";
    cin >> min;
    cout << endl;
    cout << "Введите верхний предел генерации: ";
    cin >> max;
    cout << endl;
    int** arr = new int*[m];
    for (int i = 0; i < m; i++)
        arr[i] = new int[n];    
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            arr[i][j] = rand() % (max - min + 1) + min;
        }
    }
    cout << "Сформирован массив:\n";
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            cout << "\t" << arr[i][j];
        }
        cout << endl;
    }
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (((j + 1) % 2 == 0) && (arr[i][j] > 0))
                sum += arr[i][j];
        }
    }
    cout << "Суииа положитнльных элементов на столбцах с четными номерами составляет: " << sum << endl;
    for (int i = 0; i < m; i++)
        delete[]arr[i];
    delete[]arr;
    
    system("pause");
    system("cls");
    cout << "Вторая часть\n";
    cout << "Будет сформирован динамический массив случайных чисел\n";
    cout << "Введите количество строк: ";
    cin >> m;
    cout << endl;
    cout << "Введите количество столбцов: ";
    cin >> n;
    cout << endl;
    cout << "Введите нижний предел генерации: ";
    cin >> min;
    cout << endl;
    cout << "Введите верхний предел генерации: ";
    cin >> max;
    cout << endl;
    int** arr1 = new int*[m];
    for (int i = 0; i < m; i++)
        arr1[i] = new int[n];
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            arr1[i][j] = rand() % (max - min + 1) + min;
        }
    }
    cout << "Сформирован массив:\n";
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            cout << "\t" << arr1[i][j];
        }
        cout << endl;
    }
    int temp;
    int multi = 1;
    int flag;
    for (int i = 0; i < m; i++)
    {
        flag = 0;
        if ((i + 1) % 2 != 0)
        {
            flag = 1;
            temp = arr1[i][0];
        }
        
        for (int j = 0; j < n; j++)
        {
            
            if ((i + 1) % 2 != 0 && arr1[i][j] < temp)
            {
                temp = arr1[i][j];
                
            }
           
        }
        if (flag == 1)
        {
            multi *= temp;
        }
    }
    
    cout << "Произведение минимальных элементов на нечетных строчках составит: " << multi << endl;
    for (int i = 0; i < m; i++)
        delete[]arr1[i];
    delete[]arr1;
    system("pause");
    system("cls");
    
    return 0;
}


