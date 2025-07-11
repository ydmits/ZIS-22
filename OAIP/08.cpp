
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <stdio.h>
#include<conio.h>
#include <Windows.h>
#include <iomanip>
#include <string.h>
using namespace std;
const int MAX = 1000;

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    int str_count = 0;
    cout << "Введите строку\n";
    char str[MAX];
    char str1[MAX];
    char X;
    int size = 0;
    while (str_count != 5)
    {
        X = getchar();
        str[size] = X;
        size++;
        if(X=='\n')
            str_count++;
    }
    str[size] = '\0';
    FILE* fptr, *fptr1;
    fptr = fopen("file1.txt", "w");
    fputs(str, fptr);
    fclose(fptr);
    fopen_s(&fptr, "file1.txt", "r");
    for (int i = 0; i < MAX; i++)
    {
        str[i] = '\0';
        str1[i] = '\0';
    }
    int i = 0;
    while (fread(&str[i], sizeof(char), 1, fptr))
        i++;
    str[size] = '\0';
    fclose(fptr);
    char sym[] = { "БбВбГгЛлЖжЗзЙйКкЛлМмНнПпРрСсТтФфХхЦцШшЩщ" };
    char la[] = {"ла"};
    int pos = 0;
    int size1 = 0;
    int len;
    len = strlen(sym);
        for (int k = 0; k < size; k++)
        {
            for (int j = 0; j < len; j++)
            {
                if (sym[j] == str[k])
                {
                    pos = k + 1;
                    size1 = size - pos;
                    for (int t = 0; t < size1; t++, pos++)
                    {
                        str1[t] = str[pos];
                    }
                    
                    size += 2;
                    str[k + 1] = 'л';
                    str[k + 2] = 'а';
                    k += 2;
                    pos = 0;
                    for (int i = k + 1; i < size; pos++, i++)
                    {
                        str[i] = str1[pos];
                    }


                }
            }
        }
    size++;
    str[size] = '\0';



    /*int start = 0;
    int end = 0;
     pos = 0;
    int flag = 0;
    for (int i = 0; i < size; i++)
    {
        if (str[i] != 'л')
            flag = 1;
        if (str[i] == '\n')
        {
            pos = start;
            end = i - 1;
            while (flag)
            {
                str[pos] = '_';
                pos++;
                if (pos == end)
                    flag = 0;
            }
            start = end + 2;
        }
    }*/
    fopen_s(&fptr1,"file2.txt", "w");
    

    fputs(str, fptr1);
    fclose(fptr1);
    fopen_s(&fptr1, "file2.txt", "r");
    i = 0;
    while (fread(&str[i], sizeof(char), 1, fptr1))
        i++;
    str[size] = '\n';
    fclose(fptr1);
    cout << "Прочитан файл:\n";
    
    cout << str;

    return 0;
}


/*
int len;
    int new_size = size;
    len = strlen(sym);
    for (int j = 0; j < len; j++)
    {
        for (int k = 0; k < size; k++)
        {
            if (sym[j] == str[k])
            {
                str[k + 1] = 'л';
                str[k + 2] = 'а';

                char w1, w2;
                w1 = str[k + 1];
                w2 = str[k + 2];
                str[k + 1] = 'л';
                str[k + 2] = 'а';
                k += 2;
                new_size+=2;
                for (int z = new_size - 1; z < k; z-=2)
                {
                    str[z] = str[z - 2];
                    str[z - 1] = str[z - 3];
                    if (z - k <= 2)
                    {
                        str[z-1] = w1;
                        str[z-2] = w2;
                    }

                }

                size = new_size;


            }
        }
    }
    str[size] = '\0';
    fptr1 = fopen("file2.txt", "w");
    fputs(str, fptr1);
    fclose(fptr1);
    fopen_s(&fptr1, "file2.txt", "r");
    i = 0;
    while (fread(&str[i], sizeof(char), 1, fptr1))
        i++;
    str[size] = '\0';
    fclose(fptr1);
    cout << "Прочитан файл:\n";

    cout << str;

    return 0;
}
*/
