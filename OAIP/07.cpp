

#include <iostream>
#include <Windows.h>
#include <iomanip>
using namespace std;
const int MAX = 10;

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    struct Auto
    {
        unsigned int number;
        char mark[MAX];
        unsigned int mileage;
        unsigned int cost;
        unsigned int year;
    };
    int size;
    cout << "Введите количество авто: ";
    cin >> size;
    cout << endl;
    Auto* cars = (Auto*)malloc(size * sizeof(Auto));
    for (int i = 0; i < size; i++)
    {
        cout << "Введите регистрационный номер " << i + 1 << "-го автомобиля: ";
        cin >> cars[i].number;
        cout << endl;
        cout << "Введите марку " << i + 1 << "-го автомобиля: ";
        cin >> cars[i].mark;
        cout << endl;
        cout << "Введите пробег " << i + 1 << "-го автомобиля: ";
        cin >> cars[i].mileage;
        cout << endl;
        cout << "Введите стоимость " << i + 1 << "-го автомобиля: ";
        cin >> cars[i].cost;
        cout << endl;
        cout << "Введите год выпуска " << i + 1 << "-го автомобиля: ";
        cin >> cars[i].year;
        cout << endl;
        system("pause");
        system("cls");
    }
    cout << "-----------------------------------------------------------" << endl;
    cout << "|  numder  ||    mark    ||  mileage  ||  cost  ||  year  |" << endl;
    cout << "----------------------------------------------------------" << endl;
    for (int i = 0; i < size; i++)
    {
        cout << "|  " << setw(8) << cars[i].number << "  |";
        cout << "|  " << setw(10) << cars[i].mark << "  |";
        cout << "|  " << setw(8) << cars[i].mileage << "  |";
        cout << "|  " << setw(8) << cars[i].cost << "  |";
        cout << "|  " << setw(6) << cars[i].year << "  |";
        cout << endl;
    }
    system("pause");
    system("cls");
    char mark[MAX];
    cout << "Введите марку: ";
    cin >> mark;
    cout << endl;
    int position = 100;
    int cost = 0;
    for (int i = 0; i < size; i++)
    {
        if (cars[i].mark == mark)
        {
            position = i;
            cost = cars[i].cost;
        }
        if ((cars[i].mark == mark) && cars[i].cost > cost)
        {
            position = i;
            cost = cars[i].cost;
        }
    }
    if (position != 100)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "|  numder  ||    mark    ||  mileage  ||  cost  ||  year  |" << endl;
        cout << "----------------------------------------------------------" << endl;
        cout << "|  " << setw(8) << cars[position].number << "  |";
        cout << "|  " << setw(10) << cars[position].mark << "  |";
        cout << "|  " << setw(8) << cars[position].mileage << "  |";
        cout << "|  " << setw(8) << cars[position].cost << "  |";
        cout << "|  " << setw(6) << cars[position].year << "  |";
        cout << endl;
    }
    else
        cout << "Нет совпадений\n";
    system("pause");
    system("cls");
    cout << "Введите максимальную стоимость автомобиля: ";
    cin >> cost;
    cout << endl;
    int counter = 0;
    int j = 0;
    for (int i = 0; i < size; i++)
    {
        if (cars[i].cost <= cost)
            counter++;
    }
    Auto* temp = (Auto*)malloc(counter * sizeof(Auto));
    if (counter == 0)
        cout << "Нет совпадений\n";
    else
    {
        for (int i = 0; i < size; i++)
        {
            if (cars[i].cost <= cost)
            {
                temp[j] = cars[i];
                j++;
            }
        }
        Auto tmp;
        for (int k = counter; j > 1; j--)
        {
            for (int i = 1; i < j; i++)
            {
                if (temp[i - 1].mileage > temp[i].mileage)
                {
                    tmp = temp[i - 1];
                    temp[i - 1] = temp[i];
                    temp[i] = tmp;
                }
            }
        }
        cout << "Авто не дороже заданной суммы в порядке возрастания пробега:\n";
        cout << "-----------------------------------------------------------" << endl;
        cout << "|  numder  ||    mark    ||  mileage  ||  cost  ||  year  |" << endl;
        cout << "----------------------------------------------------------" << endl;
        for (int i = 0; i < counter; i++)
        {
            cout << "|  " << setw(8) << temp[i].number << "  |";
            cout << "|  " << setw(10) << temp[i].mark << "  |";
            cout << "|  " << setw(8) << temp[i].mileage << "  |";
            cout << "|  " << setw(8) << temp[i].cost << "  |";
            cout << "|  " << setw(6) << temp[i].year << "  |";
            cout << endl;
        }
    }
    free(cars);
    //free(temp);
    

    return 0;
}
