// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApplication2
{ 
    class Massiv
    {
        int [] a;

        public Massiv (int n)
        {
            bool f = true;
            while(f)
            {
                if (n<=0)
                {
                    Console.WriteLine("Отрицательное или нулевое количество элементов. Повторите ввод. ");
                    n= Convert.ToInt32(Console.ReadLine());
                }    
                else
                {
                    a = new int[n];
                    f = false;
                }
            }
        }
        public int length { get { return a.Length; } }
        public int this[int i]
        {
            get { return a[i]; }
            set { a[i] = value; }
        }
        public void vvod (char Mname)
        {
            for(int i=0; i < a.Length; i++)
            {
                Console.Write(Mname+" [ "+(i+1)+" ] = ");
                a[i]=Convert.ToInt32(Console.ReadLine());
            }
        }
        public void vyvod(string Mname)
        {
            Console.WriteLine(Mname);
            foreach (int xt in a) Console.Write("{0,9}", xt);
            Console.WriteLine();
        }
        public int Max()
        {
            int max = a[0];
            for(int i=0; i<a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }
        public int Max(int i1, int i2)
        {
            int max = a[i1];
            for(int i=i1;i<i2;i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max;
        }
        public static int Skalyar(Massiv A, Massiv B)
        {
            int size = A.length;
            int skl = 0;
            int [] arr = new int[size];
            for(int i=0; i<size; i++)
            {
                arr[i] = A[i] * B[i];
                skl +=arr[i];
            }
            return skl;
        }
        public static bool operator >(Massiv A, Massiv B)
        {
            if (A.length > B.length)
                return true;
            else
            {
                bool f = true;
                if (A.length == B.length)
                {
                    int count1 = 0;
                    int count2 = 0;
                    for(int i=0; i<A.length; i++)
                    {
                        if (A[i] > 0) count1++;
                        if (B[i] > 0) count2++;
                    }
                    if (count1 > count2)
                        f = true;
                    else f = false;
                }
                if(f) return true;
                return false;
            }

        }
        public static bool operator <(Massiv A, Massiv B)
        {
            if (A.length < B.length)
                return true;
            else
            {
                bool f = true;
                if (A.length == B.length)
                {
                    int count1 = 0;
                    int count2 = 0;
                    for (int i = 0; i < A.length; i++)
                    {
                        if (A[i] > 0) count1++;
                        if (B[i] > 0) count2++;
                    }
                    if (count1 < count2)
                        f = true;
                    else f = false;
                }
                if (f) return true;
                return false;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива А: ");
            Massiv A = new Massiv(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine();
            A.vvod('A');
            Console.WriteLine();
            Console.WriteLine("Введите количество элементов массива B: ");
            Massiv B = new Massiv(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine();
            B.vvod('B');
            Console.WriteLine();
            Console.Clear();
            A.vyvod("Массив A");
            Console.WriteLine();
            B.vyvod("Массив B");
            Console.WriteLine();
            Console.WriteLine("Максимальный элемент массива А равен "+A.Max());
            bool f = true;
            while(f)
            {
                Console.Write("Поиск максимального элемента массива B с 3 по  элемент с заданным номером, введите номер: ");
                int number = Convert.ToInt32(Console.ReadLine());
                if(number <=3) { Console.WriteLine("Некорректный ввод"); }
                else
                {
                    Console.WriteLine("Максимальный элемент массива B равен " + B.Max(3, number));
                    f = false;
                }
            }
            Console.WriteLine("Скалярное умножение массива А и В");
            if (A.length != B.length) Console.WriteLine("Для выполнения скалярного умножения массивы должны иметь одинаковую размерность");
            else Console.WriteLine("Результат равен "+Convert.ToString(Massiv.Skalyar(A,B)));
            if (A > B)
            {
                Console.WriteLine("Массив А больше массива В");
                Console.WriteLine("Замена всех элементов массива А, расположенных после максимального," +
                    "на значение минимального среди всех элементов массива В");
                int maxIndexA = 0;
                int maxA = A[0];
                int minB = B[0];
                for (int i = 0; i < A.length; i++)
                {
                    if (A[i] > maxA)
                    {
                        maxA = A[i];
                        maxIndexA = i;
                    }
                }
                for (int i = 0; i < B.length; i++)
                {
                    if (B[i] < minB) minB = B[i];
                }
                if (maxIndexA + 1 > A.length) Console.WriteLine("Невозможно выполнить замену, максимальный элемент является концом массива");
                else if (minB >= 0)
                {
                    minB = 0;
                    Console.WriteLine("Отрицательных элементов не найдено, замена производится на 0");
                    
                }
                for (int i = maxIndexA + 1; i < A.length; i++)
                {
                    A[i] = minB;
                }
                A.vyvod("Массив A");
            }
            else if (A < B) Console.WriteLine("Массив А меньше массива В");
            else Console.WriteLine("Сравнение не сработало корректно");

            

           

            Console.ReadKey();

        }
    }
}
