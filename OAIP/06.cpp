#include <iostream>
#include <windows.h>
#include <string.h>
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

void print_str(char* str)
{
    cout << str << endl;
}



int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    cout << "Первая часть\n";
    char* str = input_str();
    cout << "Вы ввели:\n";
    print_str(str);
    int size = len_str(str);
    int counter = 0;
    int counter_A = 0;
    int counter_B = 0;
    for (int i = 0; i < size; i++)
    {
        if (str[i] == ',')
            counter++;
        if (str[i] == 'А')
            counter_A++;
        if (str[i] == 'В')
            counter_B++;
    }
    if (counter_A == 0)
        cout << "В строке нет символов 'А'" << endl;
    else
        cout<< "Подсчёт символов А: " << counter_A << endl;
    if (counter_B == 0)
        cout << "В строке нет символов 'В'" << endl;
    else
        cout << "Подсчёт символов В: " << counter_B << endl;
    if (counter == 0)
        cout << "В строке нет символов ','" << endl;
    else if (size + counter > MAX)
        cout << "Ошибка, превышение максимально допустимого размера строки" << endl;
    else
    {
        size += counter;
        str[size] = '\0';
        char temp;
        for (int i = 0; i < size; i++)
        {
            if (str[i] == ',')
            {     
                temp = str[i + 1];
                str[i + 1] = ' ';
                for (int j = size - 1; str[j] != ' '; j--)
                {
                    str[j] = str[j - 1];
                    if (str[j - 1] == ' ')
                        str[j] = temp;
                }
               
            }
        }
    }
    cout << "Результат преобразования\n";
    print_str(str);
    //free(str);
    system("pause");
    cout << "Вторая часть\n";
    char* str1 = input_str();
    cout << "Вы ввели:\n";
    print_str(str1);
    int size1 = len_str(str1);
    int counter1 = 0;
    int counter2 = 0;
    for (int i = 0; i < size1; i++)
    {
        if (str1[i] == ' ')
            counter1++;
        if (str1[i] == 'о')
            if (str1[i + 1] == 'й')
                counter2++;
    }
    if (size1 + counter +1 > MAX)
        cout << "Ошибка, превышение максимально допустимого размера строки" << endl;
    else
    {
        size1++;
        for (int i = size1; i != 0; i--)
        {
            str1[i] = str1[i - 1];
        }
        str1[0] = '$';
        char temp;
        size1 += counter1;
        for (int i = 0; i < size1; i++)
        {
            if (str1[i] == ' ')
            {
                temp = str1[i + 1];
                str1[i + 1] = '$';
                for (int j = size1 - 1; str1[j] != '$'; j--)
                {
                    str1[j] = str1[j - 1];
                    if (str1[j - 1] == '$')
                        str1[j] = temp;
                  
                }
               
            }
        }
    }
    cout << "Количество слов, заканчивающихся на 'ой': " << counter2 << endl;
    cout << "Результат преобразования\n";
    print_str(str1);
    //free(str1);
    return 0;
}


