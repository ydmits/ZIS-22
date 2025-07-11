#include <iostream>
#include <time.h>
#include <stdlib.h>
#include <iomanip>
#include <cmath>
#include <Windows.h>
using namespace std;
const int MAX_SIZE = 50;

int main()
{
    setlocale(LC_ALL, "rus");
    srand(time(NULL));
    
    {
    cout << "Первая часть\n";
    cout << "Программа работает корректно при вводе целых чисел\n";
    int size, min, max;
    cout << "Введите размерность массива (не более " << MAX_SIZE << "): ";
    cin >> size;
    cout << "\nВведите нижний предел генерации: ";
    cin >> min;
    cout << "\nВведите верхний предел генерации: ";
    cin >> max;
    cout << endl;
    int arr[MAX_SIZE];
    for (int i = 0; i < size; i++)
        arr[i] = rand() % (max - min + 1) + min;
    cout << "Сформирован массив: \n";
    for (int i = 0; i < size; i++)
        cout << arr[i] << "\t";
    cout << endl;
    int a, b;
    cout << "Подсчёт количества чисел из заданного промежутка\n";
    cout << "Введите начало промежутка: ";
    cin >> a;
    cout << "\nВведите конец промежутка: ";
    cin >> b;
    int counter = 0;
    for (int i = 0; i < size; i++)
    {
        if (arr[i] >= a && arr[i] <= b)
            counter++;
    }
    cout << "Найдено количество чисел, принадлежащих промежутку от " << a << " до " << b << " : " << counter << endl;
    int sum = 0;
    for (int i = 0; i < size; i++)
    {
        if (i % 3 == 0)
            sum += arr[i];
    }
    cout << "Сумма чисел, стоящих на местах, кратным 3: "<<sum<<endl;
    system("pause");
    system("cls");
    cout << "Вторая часть\n";
    cout << "Введите размерность массивов (не более " << MAX_SIZE/2 << "): ";
    cin >> size;
    cout << "\nВведите нижний предел генерации: ";
    cin >> min;
    cout << "\nВведите верхний предел генерации: ";
    cin >> max;
    cout << endl;
    int arr1[MAX_SIZE];
    int arr2[MAX_SIZE];
    int arr3[MAX_SIZE];
    for (int i = 0; i < size; i++)
    {
        arr1[i] = rand() % (max - min + 1) + min;
        arr2[i] = rand() % (max - min + 1) + min;
    }
    counter = 0;
    int position = 0;
    for (int i = 0; i < size; i++) //
    {
        if (arr1[i] > 0)
        {
            arr3[counter] = arr1[i];
            counter++;
        }
    }

    for (int i = 0; i < size; i++)
    {
        if (arr2[i] < 0)
        {
            position = i;
            break;
        }
    }
    int new_size = (counter) + (size - position);
    int j = 0;
    for (int i = counter; i < new_size; i++)
    {
        arr3[i] = arr2[position + j];
        j++;
    }
    cout << "Сформирован первый массив: \n";
    for (int i = 0; i < size; i++)
        cout << arr1[i] << "\t";
    cout << endl;
    cout << "Сформирован второй массив: \n";
    for (int i = 0; i < size; i++)
        cout << arr2[i] << "\t";
    cout << endl;
    cout << "Сформирован третий массив: \n";
    for (int i = 0; i < new_size; i++)
        cout << arr3[i] << "\t";
    cout << endl;
    system("pause");
    system("cls");
    cout << "Третья часть\n";
    cout << "Введите размерность массивов (не более " << MAX_SIZE << "): ";
    cin >> size;
    cout << "\nВведите нижний предел генерации: ";
    cin >> min;
    cout << "\nВведите верхний предел генерации: ";
    cin >> max;
    for (int i = 0; i < size; i++)
        arr[i] = rand() % (max - min + 1) + min;
    cout << "Сформирован массив: \n";
    for (int i = 0; i < size; i++)
        cout << arr[i] << "\t";
    cout << endl;
    int position1 = 0;
    int position2 = 0;
    int temp = arr[0];
    for (int i = position1 +1; i < size; i++)
    {

        if (arr[i] < temp)
        {
            position1 = i;
            temp = arr[i];
        }
    }
    cout << "Минимальный элемент массива находится на позиции: " << position1 << endl;
    temp = arr[0];
    for (int i = position2 + 1; i < size; i++)
    {

        if (arr[i] > temp)
        {
            position2 = i;
            temp = arr[i];
        }
    }
    cout << "Максимальный элемент массива находится на позиции: " << position2 << endl;
    temp = position1;
    if (position1 > position2)
    {
        position1 = position2;
        position2 = temp;
    }
    temp = 1;
    for (int i = position1; i <= position2; i++)
        temp *= arr[i];
    cout << "Произведение элементов массива между позициями " << position1 << " и " << position2 << " составляет: "<<temp << endl;
    system("pause");
    system("cls");
    cout << "Четвертая часть\n";
    int n;
    float speed[MAX_SIZE];
    cout << "Введите количество автомобилей (не более "<<MAX_SIZE<< "): ";
    cin >> n;
    cout << endl;
    for (int i = 0; i < n; i++)
    {
        arr[i] = i;//порядковый номер авто
        arr1[i] = rand() % (1000 - 10 + 1) + 10;//пройденный путь, км
        arr2[i] = rand() % (50 -  + 10) + 10;//затраченное время
        speed[i] = (float)arr1[i] / (float)arr2[i];//скорость
        speed[i] = round(speed[i] * 100) / 100;
    }
    cout << setw(25) << "Позиция автомобитя: ";
    for (int i = 0; i < n; i++)
        cout << setw(6) << arr[i];
    cout << endl;
    cout << setw(25) << "Пройденный путь, км: ";
    for (int i = 0; i < n; i++)
        cout << setw(6) << arr1[i];
    cout << endl;
    cout << setw(25) << "Затраченное время, час: ";
    for (int i = 0; i < n; i++)
        cout << setw(6) << arr2[i];
    cout << endl;
    cout << setw(25) << "Средняя скорость, км/час: ";
    for (int i = 0; i < n; i++)
        cout << setw(6) << speed[i];
    cout << endl;
    position = 0;
    float temp1 = speed[0];
    for (int i = 0; i < n; i++)
    {
        if (speed[i] > temp1)
        {
            temp1 = speed[i];
            position = i;
        }
    }
    cout << "Максимальную скорость имел автомобиль с позицией: "<< position << endl;
    system("pause");
    system("cls");
    }
    
    int flag = 1;
    while (flag)
    {
        cout << "Пятая часть\n";
        cout << endl;
        cout << "Выберите метод сортировки:\n";
        cout << "1 - По возрастанию обменом  рядом стоящих элементов с фиксированным числом просмотров, направленных слева направо\n";
        cout << "2 - Шейкерная по убыванию\n";
        cout << "3 - По возрастанию извлечением максимального элемента, извлечение максимального элемента проводить справа налево\n";
        cout << "4 - Расчёской по возрастанию\n";
        cout << "5 - Получение упорядоченного по убыванию массива методом слияния двух массивов,один  из которых упорядочен по убыванию,";
        cout << "а другой - по возрастанию\n";
        cout << "0 - Выход\n";
        int menu = 0;
        cout << "Выбор: ";
        cin >> menu;
        cout << endl;
        switch (menu)
        {
        case 0: flag = 0; break;
        case 1:
        {
            cout << "Будет сформирован динамический массив случайных чисел от -45 до 45\n";
            cout << "Введите размерность массива: ";
            int n;
            cin >> n;
            cout << endl;
            int* arr_ptr;
            arr_ptr = (int*)malloc(n * sizeof(int));
            for (int i = 0; i < n; i++)
                arr_ptr[i] = rand() % (45 - -45 + 1) + -45;
            cout << "Сформирован массив:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            int temp;
            for (int j = n; j > 1; j--)
            {
                for (int i = 1; i < j; i++)
                {
                    if (arr_ptr[i - 1] > arr_ptr[i])
                    {
                        temp = arr_ptr[i - 1];
                        arr_ptr[i - 1] = arr_ptr[i];
                        arr_ptr[i] = temp;
                    }
                }
            }
            cout << "Результат сортировки:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            free(arr_ptr);
            system("pause");
            system("cls");
           break;
        }
        case 2:
        {
            cout << "Будет сформирован динамический массив случайных чисел от -45 до 45\n";
            cout << "Введите размерность массива: ";
            int n;
            cin >> n;
            cout << endl;
            int* arr_ptr;
            arr_ptr = (int*)malloc(n * sizeof(int));
            for (int i = 0; i < n; i++)
                arr_ptr[i] = rand() % (45 - -45 + 1) + -45;
            cout << "Сформирован массив:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            int temp;
            int left = 0;
            int right = n - 1;
            int arr_flag = 1;
            while ((left < right) && flag > 0)
            {
                arr_flag = 0;
                for (int i = left; i < right; i++)
                {
                    if (arr_ptr[i] < arr_ptr[i + 1])
                    {
                        temp = arr_ptr[i];
                        arr_ptr[i] = arr_ptr[i + 1];
                        arr_ptr[i + 1] = temp;
                        arr_flag = 1;
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (arr_ptr[i - 1] < arr_ptr[i])
                    {
                        temp = arr_ptr[i];
                        arr_ptr[i] = arr_ptr[i - 1];
                        arr_ptr[i - 1] = temp;
                        flag = 1;
                    }
                }
                left++;
            }
            cout << "Результат сортировки:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            free(arr_ptr);
            system("pause");
            system("cls");
            break;
        }
        case 3:
        {
            cout << "Будет сформирован динамический массив случайных чисел от -45 до 45\n";
            cout << "Введите размерность массива: ";
            int n;
            cin >> n;
            cout << endl;
            int* arr_ptr;
            arr_ptr = (int*)malloc(n * sizeof(int));
            for (int i = 0; i < n; i++)
                arr_ptr[i] = rand() % (45 - -45 + 1) + -45;
            cout << "Сформирован массив:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            //По возрастанию извлечением максимального элемента, извлечение максимального элемента проводить справа налево
            // стр 106 лекция
            for (int i = 0; i < n; i++)
            {
                int temp = arr_ptr[0];
                for (int j = i + 1; j < n; j++)
                {
                    if (arr_ptr[i] > arr_ptr[j])
                    {                     
                        temp = arr_ptr[i];
                        arr_ptr[i] = arr_ptr[j];
                        arr_ptr[j] = temp;
                    }                   
                }                
            }
            cout << "Результат сортировки:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            free(arr_ptr);
            system("pause");
            system("cls");
            break;
        }
        case 4:
        {
            cout << "Будет сформирован динамический массив случайных чисел от -45 до 45\n";
            cout << "Введите размерность массива: ";
            int n;
            cin >> n;
            cout << endl;
            int* arr_ptr;
            arr_ptr = (int*)malloc(n * sizeof(int));
            for (int i = 0; i < n; i++)
                arr_ptr[i] = rand() % (45 - -45 + 1) + -45;
            cout << "Сформирован массив:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            int temp;
            double k = 1.2473309;
            int step = n - 1;
            while (step > 1)
            {
                for (int i = 0; i + step < n; i++)
                {
                    if (arr_ptr[i] > arr_ptr[i + step])
                    {
                        temp = arr_ptr[i];
                        arr_ptr[i] = arr_ptr[i + step];
                        arr_ptr[i + step] = temp;
                    };

                }
                step /= k;
                
            }
            int flag_arr = 1;
            while (flag_arr)
            {
                flag_arr = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    if (arr_ptr[i] > arr_ptr[i + 1])
                    {
                        temp = arr_ptr[i];
                        arr_ptr[i] = arr_ptr[i + 1];
                        arr_ptr[i + 1] = temp;
                        flag_arr = 1;
                    }
                }
            }
            cout << "Результат сортировки:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            free(arr_ptr);
            system("pause");
            system("cls");
            break;
        }
        case 5:
        {
            cout << "Будут сформированы два динамическх массива случайных чисел от -45 до 45\n";
            cout << "Введите размерность массивов: ";
            int n;
            cin >> n;
            int m = n;
            cout << endl;
            int* arr_ptr1;
            int* arr_ptr2;
            arr_ptr1 = (int*)malloc(n * sizeof(int));
            arr_ptr2 = (int*)malloc(n * sizeof(int));
            for (int i = 0; i < n; i++)
            {
                arr_ptr1[i] = rand() % (45 - -45 + 1) + -45;
                arr_ptr2[i] = rand() % (45 - -45 + 1) + -45;
            }     
            int temp1;
            for (int j = n; j > 1; j--)
            {
                for (int i = 1; i < j; i++)
                {
                    if (arr_ptr1[i - 1] > arr_ptr1[i])
                    {
                        temp1 = arr_ptr1[i - 1];
                        arr_ptr1[i - 1] = arr_ptr1[i];
                        arr_ptr1[i] = temp1;
                    }
                }
            }
            int temp;
            int left = 0;
            int right = m - 1;
            int arr_flag = 1;
            while ((left < right) && flag > 0)
            {
                arr_flag = 0;
                for (int i = left; i < right; i++)
                {
                    if (arr_ptr2[i] < arr_ptr2[i + 1])
                    {
                        temp = arr_ptr2[i];
                        arr_ptr2[i] = arr_ptr2[i + 1];
                        arr_ptr2[i + 1] = temp;
                        arr_flag = 1;
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (arr_ptr2[i - 1] < arr_ptr2[i])
                    {
                        temp = arr_ptr2[i];
                        arr_ptr2[i] = arr_ptr2[i - 1];
                        arr_ptr2[i - 1] = temp;
                        flag = 1;
                    }
                }
                left++;
            }
            cout << "Сформированы массивы:\n";
            for (int i = 0; i < n; i++)
                cout << setw(5) << arr_ptr1[i];
            cout << endl;
            for (int i = 0; i < m; i++)
                cout << setw(5) << arr_ptr2[i];
            cout << endl;
            int* arr_ptr = (int*)malloc((n + m) * sizeof(int));
            int i = 0;
            int j = m - 1;
            for (int l = 0; l < n + m; l++)
            {
                if (i < n)
                {
                    if (j >= 0)
                    {
                        if (arr_ptr1[i] <= arr_ptr2[j])
                        {
                            arr_ptr[l] = arr_ptr1[i];
                            i++;
                        }
                        else
                        {
                            arr_ptr[l] = arr_ptr2[j];
                            j--;
                        }
                    }
                    else
                    {
                        arr_ptr[l] = arr_ptr1[i];
                        i++;
                    }
                }
                else
                {
                    arr_ptr[l] = arr_ptr2[j];
                    j--;
                }
            }
            cout << "Результат сортировки:\n";
            for (int i = 0; i < n + m; i++)
                cout << setw(5) << arr_ptr[i];
            cout << endl;
            free(arr_ptr);
            free(arr_ptr1);
            free(arr_ptr2);
            system("pause");
            system("cls");
            break;
        }
        default:
            break;
        }
    }
    return 0;
}