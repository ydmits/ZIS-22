#include <iostream>
#include <time.h>
#include <stdlib.h>
#include <iomanip>
#include <cmath>
#include <Windows.h>
using namespace std;
const int MAX = 100;

char* input_str()
{
    cout << "Введите строку\n";
    char* str = (char*)calloc(MAX, sizeof(char));
    int i = 0;
    while ((*(str + i++) = (char)getchar()) != '\n');
    *(str + --i) = '\0';
    str = (char*)realloc(str, i + 1);
    cout << endl;
    return str;
}

int len_str(char* str)
{
    int i = 0;
    while (*(str + i))
        i++;
    return i;
}

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    srand(time(NULL));
   /* cout << "Первое задание\n";
    int m1, n1, max1, min1;
    int arr1[MAX];
    int arr2[MAX];
    int arr3[MAX];
    
    cout << "Введите размерность первого массива: ";
    cin >> n1;
    cout << endl;
    cout << "Введите размерность второго массива: ";
    cin >> m1;
    cout << endl;
    cout << "Введите нижний предел генерации: ";
    cin >> min1;
    cout << endl;
    cout << "\nВведите верхний предел генерации: ";
    cin >> max1;
    cout << endl;
    for (int i = 0; i < n1; i++)
        arr1[i] = rand() % (max1 - min1 + 1) + min1;
    for (int i = 0; i < m1; i++)
        arr2[i] = rand() % (max1 - min1 + 1) + min1;
    int counter1 = 0;
    for (int i = 0; i < n1; i++)
    {
        if (arr1[i] < 0)
        {
            arr3[counter1] = arr1[i];
            counter1++;
        }
    }
    int position1 = 0;
    for (int i = 0; i <m1; i++)
    {
        if (arr2[i] >= arr2[position1])
        {
            position1 = i;
            
        }
    }
    int new_size1 = counter1 + position1;
    int j = 0;
    for (int i = counter1; i < new_size1; i++)
    {
        arr3[i] = arr2[j];
        j++;
    }
    cout << "Сформирован первый массив: \n";
    for (int i = 0; i < n1; i++)
        cout << arr1[i] << "\t";
    cout << endl;
    cout << "Сформирован второй массив: \n";
    for (int i = 0; i < m1; i++)
        cout << arr2[i] << "\t";
    cout << endl;
    cout << "Сформирован третий массив: \n";
    for (int i = 0; i < new_size1; i++)
        cout << arr3[i] << "\t";
    cout << endl;
    system("pause");
    system("cls");
    int n2, m2, max2, min2;
    cout << "Второе задание\n";
    cout << "Введите количество строк: ";
    cin >> m2;
    cout << endl;
    cout << "Введите количество столбцов: ";
    cin >> n2;
    cout << endl;
    int arr[MAX][MAX];
    cout << "Введите нижний предел генерации: ";
    cin >> min2;
    cout << endl;
    cout << "\nВведите верхний предел генерации: ";
    cin >> max2;
    cout << endl;
    for (int i = 0; i < m2; i++)
    {
        for (int j = 0; j < n2; j++)
        {
            arr[i][j] = rand() % (max2 - min2 + 1) + min2;
        }
    }
    int Y = 0;
    int sum = 0;
    cout << "Введите число Y: ";
    cin >> Y;
    cout << endl;
    for (int i = 0; i < m2; i++)
    {
        for (int j = 0; j < n2; j++)
        {
            if ((m2 + 3) % 2 == 0 && arr[i][j] < Y)
                sum += (arr[i][j] * arr[i][j]);
        }
    }
    for (int i = 0; i < m2; i++)
    {
        for (int j = 0; j < n2; j++)
        {
            cout << "\t" << arr[i][j];
        }
        cout << endl;
    }
    cout << endl;
    cout << "Подсчёт суммы квадратов элементов, меньших " << Y << " : " << sum << endl;
    system("pause");
    system("cls");*/
    cout << "Третье задание\n";
    char* str = input_str();
    int size = len_str(str);
    cout << endl;
    cout << "Вы ввели:\n";
    cout << str << endl;
    int count12 = 0;
    int count34 = 0;
    int counter3 = 0;
  
    for (int i = 0; i < size; i++)
    {
        if (str[i] == '1' && str[i + 1] == '2')
            count12++;
        if (str[i] == '3' && str[i + 1] == '4')
            count34++;
        if (str[i] == '%')
            counter3++;
    }
    if (count12 == 0)
        cout << "В строке нет совпадений '12'" << endl;
    else
        cout << "Подсчёт совпадений 12: " << count12 << endl;
    if (count34 == 0)
        cout << "В строке нет совпадений '34'" << endl;
    else
        cout << "Подсчёт совпадений 34: " << count34 << endl;
    if (counter3 == 0)
        cout << "В строке нет символов '%'" << endl;
    else if (size + counter3 > MAX)
        cout << "Ошибка, превышение максимально допустимого размера строки" << endl;
    else
    {
        size += counter3;
        str[size] = '\0';
        char temp;
        for (int i = 0; i < size; i++)
        {
            if (str[i] == '%')
            {
                temp = str[i + 1];
                str[i] = '&';
                str[i + 1] = '$';
                for (int j = size - 1; str[j] != '$'; j--)
                {
                    str[j] = str[j - 1];
                    if (str[j - 1] == '$')
                        str[j] = temp;
                }

            }
        }
    }
    cout << "Результат преобразования\n";
    cout << str << endl;
    //free(str);
    system("pause");
    system("cls");
    return 0;
}


