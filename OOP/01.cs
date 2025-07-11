// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;



namespace ConsoleApplication1
{
    class Program
    {
        static void Pause()
        {
            string buf;
            char key;
            do
            {
                Console.WriteLine("Перейти к следующей части? (y / n)");
                buf = Console.ReadLine();
                key = Convert.ToChar(buf);

            } while (key != 'y');
            Console.Clear();
        }
        static double vvod()
        {
            string buf;
            Console.Write("Введите число: ");
            buf = Console.ReadLine();
            return Convert.ToDouble(buf);
        }
        static double func(double x, double y, double z)
        {
            return ( (Math.Sin(x) + Math.Sqrt(Math.Cos(x + z)) ) / (Math.Abs(Math.Cos(Math.Pow(x,3))) - 2 * Math.Sqrt(y)) );
        }
        static void Main(string[] args)
        {
            
            string buf;

            Console.WriteLine("Часть 1");
            Console.WriteLine("Введите Х");
            buf= Console.ReadLine();
            double x = Convert.ToDouble(buf);
            Console.WriteLine("Введите А");
            buf = Console.ReadLine();
            double a=double.Parse(buf);
            double f= (x + Math.Sqrt(a)) / (1.4 * a) - x;
            Console.WriteLine("Для х = {0} и а = {1} функция f = {2}", x, a, f);
            Program.Pause();
            
            Console.WriteLine("Часть 2");
            Console.WriteLine("Введите Х");
            buf = Console.ReadLine();
            x = Convert.ToDouble(buf);
            Console.WriteLine("Введите Z");
            buf = Console.ReadLine();
            a = double.Parse(buf);                      //переменная а используется как Z
            if (a > -5 && a <= 0) f = Math.Sqrt(a) + a;
            else if (a <= -5) f = 2.5 * a;
            else f = (Math.Pow(x, 3) + 1.3) / a;
            Console.WriteLine("f = {0}",f);
            Program.Pause();

            Console.WriteLine("Часть 3");
            double xn, xk, dx;
            Console.WriteLine("Введите начальное значение Х");
            buf = Console.ReadLine();
            xn = Convert.ToDouble(buf);
            Console.WriteLine("Введите конечное значение Х");
            buf = Console.ReadLine();
            xk = Convert.ToDouble(buf);
            Console.WriteLine("Введите шаг Х");
            buf = Console.ReadLine();
            dx = Convert.ToDouble(buf);
            if(xn>xk && (dx > (xk-xn)) && (((xk-xn)%dx )!= 0) && dx == 0)
                Console.WriteLine("Исходные данные введены некорректно, расчёт можеть содержать ошибки");
            x = xn;
            Console.WriteLine("|          X      |             B        |");
            while(x<=xk)
            {
                f = (Math.Sin(x) + 3.7) / (Math.Abs(Math.Cos(Math.Pow(x, 3)) - 2)); //переменная f используется как B
                Console.WriteLine("|    {0,6}    |    {1,6}    |", x, f);
                x += dx;
            }
            Program.Pause();

            Console.WriteLine("Часть 4");
            Console.WriteLine("Введите количество итераций расчета функции");
            buf = Console.ReadLine();
            int n = Convert.ToInt32(buf);
            if (n <= 0) Console.WriteLine("Некорректный ввод, расчёт может содержать ошибки");
            double y,z,b;
            for(int i = 0; i < n; i++)
            {
                x = Program.vvod();
                y = Program.vvod();
                z = Program.vvod();
                b = Program.func(x,y,z);
                Console.WriteLine("Для x = {0}, y = {1}, z = {2}, значение функции b = {3}",x,y,z,b);
            }
            Program.Pause();

            Console.ReadKey();
            
        }
    }
}
