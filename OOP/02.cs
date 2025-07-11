// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;



namespace ConsoleApplication2
{
    class Table
    {
        public static void Tablehead(string head, string NameArg, int n1, string NameFun, int n2)
        {
            Console.Write("╔");
            for (int i = 0; i < n1; i++) Console.Write("═");
            Console.Write("╦");
            for (int i = 0; i < n2; i++) Console.Write("═");
            Console.WriteLine("╗");
            string s = "║{0,-" + n1.ToString() + "}║{1,-" + n2.ToString() + "}║";
            Console.WriteLine(s, NameArg, NameFun);
            Console.Write("╠");
            for (int i = 0; i < n1; i++) Console.Write("═");
            Console.Write("╬");
            for (int i = 0; i < n2; i++) Console.Write("═");
            Console.WriteLine("╣");
        }
        public static void TableDown(int n1, int n2)
        {
            Console.Write("╚");
            for (int i = 0; i < n1; i++) Console.Write("═");
            Console.Write("╩");
            for (int i = 0; i < n2; i++) Console.Write("═");
            Console.WriteLine("╝");
        }
        public static void TableLine(double x, int n1, double y,int n2)
        {
            string s = "║{0," + n1.ToString() + "}║{1," + n2.ToString() + ":f3}║";
            Console.WriteLine(s,x,y);
        }

    }
    class Function
    {
        double y, z;
        public void SetYZ(double y, double z)
            { this.y = y; this.z = z; }
        public double Mean(double x)
        { return (1+ Math.Pow(Math.Cos(x + y),2)) / Math.Abs(Math.Pow(x,3) - 2 * Math.Pow(z,2)); }
        public void FunctionTable(double xn, double xk, double dx)
        {
            Table.Tablehead(ToString(), "x", 7, "f", 10);
            double x = xn;
            while ((xn < xk) ? (x <= xk + dx / 2) : (x >= xk - dx / 2))
            {
                Table.TableLine(x, 7, Mean(x), 10);
                x = (xn < xk) ? (x + dx) : (x - dx);
            }
            Table.TableDown(7, 10);
        }
        public string ToStging()
        {
            string s;
            s = "f(x)=( 1 +cos^2 ( x + " + y.ToString() + ") / | x^3 - 2 *" + z.ToString() + "^2|";
            return s;
        }
        public static void InputXnXkDx(out double xn, out double xk, out double dx)
        {
            Console.WriteLine("Введите начальное значение аргумента: ");
            xn = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите конечное значение аргумента: ");
            xk = Convert.ToDouble(Console.ReadLine());
            dx = 0;
            bool f = true;
            while(f)
            {
                Console.WriteLine("Введите шаг изменения аргумента: ");
                dx=Convert.ToDouble(Console.ReadLine());
                if (dx > 0) f = false;
                else Console.WriteLine("Шаг отрицательный или нулевой, повторите ввод");
            }
        }
    }
    class Rectangle
    {
        double x1, x2, y1, y2;
        public Rectangle()
        {
            x1 = 0;
            y1 = 0;
            x2 = 1;
            y2 = -1;
        }
        public Rectangle(double xx1, double xx2,double yy1, double yy2 )
        {
            x1= xx1;
            y1= yy1;
            x2= yy2;
            y2= xx2;
            
        }
        void setX1(double X1) { x1 = X1; }
        void setX2(double X2) { x2 = X2; }
        void setY1(double Y1) { y1 = Y1; }
        void setY2(double Y2) { y2 = Y2; }
        public double getX1() { return x1; }
        public double getX2() { return x2; }
        public double getY1() { return y1; }
        public double getY2() { return y2; }
        public void proverka()
        {
            double buf;
            if (x1 > x2)
            {
                Console.WriteLine("Координата x1 или х2 введена некорректно, данные скорректированы");
                buf = x1;
                x1 = x2;
                x2 = buf;
            }
            else Console.WriteLine("Проверка показала - координаты x1 и x2 введены корректно");
            if (y1 > y2)
            {
                Console.WriteLine("Координата y1 или y2 введена некорректно, данные скорректированы");
                buf = x1;
                x1 = x2;
                x2 = buf;
            }
            else Console.WriteLine("Проверка показала - координаты y1 и y2 введены корректно");
        }
        public double Square(double xx1, double xx2, double yy1, double yy2)
        {
            double square;
            double X1 = Math.Abs(xx1);
            double X2 = Math.Abs(xx2);
            double Y1 = Math.Abs(yy1);
            double Y2 = Math.Abs(yy2);
            square= Math.Abs(X2 - X1) * Math.Abs(Y2 -Y1);
            return square;
        }
        public bool Kvadrat(Rectangle R1)
        {
            bool key = false;
            if ((R1.x2 - R1.x1) == (R1.y2 - R1.y1)) key = true;
            return key;
        }
        public void Moving()
        {
            string buf;
            double vertical = 0;
            double horizontal = 0;
            Console.WriteLine("Введите величину смещения по вертикали");
            buf = Console.ReadLine();
            vertical = Convert.ToDouble(buf);
            Console.WriteLine("Введите величину смещения по горизонтали");
            buf = Console.ReadLine();
            horizontal = Convert.ToDouble(buf);
            y1+=vertical;
            y2 += vertical;
            x1+=horizontal;
            x2 += horizontal;
            Console.WriteLine("Выполнено смещение на "+vertical+" по вертикали и на "+horizontal+" по горизонтали");
        }
        public void Povorot()
        {
            double X1,X2,Y1,Y2;
            X1= x1;
            X2= x2;
            Y1= y1;
            Y2= y2;
            x1 = X1 - (Y2 - Y1);
            y1 = Y1;
            x2 = X1;
            y2 = Y1 + (X2 - X1);
            Console.WriteLine("Выполнен поворот прямоугольника");
        }
        public static bool Vnutri(Rectangle R1,Rectangle R2)
        {
            if (R1.x1 < R2.x1 && R1.y1 < R2.y1 && R1.x2 < R2.x2 && R1.y2 < R2.y2) return true;
            else return false;
        }
        

    }
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
       
       
        static void Main(string[] args)
        {
            double xxn, xxk, dxx;
            Function f1 = new Function();
            f1.SetYZ(2, 5);
            Function f2 = new Function();
            Console.WriteLine("Введите параметры функции a и b"); // в коде y соотв a, z соотв b
            f2.SetYZ(double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine()));
            Console.WriteLine("f1(5)="+f1.Mean(5));
            Console.WriteLine("f1(3.5)=" + f1.Mean(3.5));
            Table.Tablehead("Функция f2(x)", "x", 5, "f2", 10);
            Table.TableLine(3.5, 5, f2.Mean(3.5), 10);
            Table.TableLine(-2, 5, f2.Mean(-2), 10);
            Table.TableDown(5, 10);
            Function.InputXnXkDx(out xxn, out xxk, out dxx);
            Console.WriteLine(f1.ToStging());
            f1.FunctionTable(xxn, xxk, dxx);
            Console.WriteLine(f2.ToStging());
            f2.FunctionTable(xxn, xxk, dxx);

            Program.Pause();

            string buf;
            double X1, X2, Y1, Y2,S;
            Console.WriteLine("Введите координату X1");
            buf = Console.ReadLine();
            X1 = Convert.ToDouble(buf);
            Console.WriteLine("Введите координату Y1");
            buf = Console.ReadLine();
            Y1 = Convert.ToDouble(buf);
            Console.WriteLine("Введите координату X2");
            buf = Console.ReadLine();
            X2 = Convert.ToDouble(buf);
            Console.WriteLine("Введите координату Y2");
            buf = Console.ReadLine();
            Y2 = Convert.ToDouble(buf);
            Rectangle R1 = new Rectangle();
            Rectangle R2 = new Rectangle(1,5,2,10);
            Rectangle R3 = new Rectangle(X1,X2,Y1,Y2);
            //Console.WriteLine("Проверка R1"); R1.proverka();
            Console.WriteLine("Проверка R2"); R2.proverka();
            Console.WriteLine("Проверка R3"); R3.proverka();
            Console.WriteLine("Точка (X1,Y1)        Точка (X2,Y2)        Площадь        Квадрат ");
            X1=R1.getX1(); X2 = R1.getX2(); Y1 = R1.getY1(); Y2 = R1.getY2(); S = R1.Square(X1, X2, Y1, Y2);
            if (R1.Kvadrat(R1) == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("({0};{1})         ({2};{3})          {4}          {5}", X1, Y1, X2, Y2, S, buf);
            X1 = R2.getX1(); X2 = R2.getX2(); Y1 = R2.getY1(); Y2 = R2.getY2(); S = R2.Square(X1, X2, Y1, Y2);
            if (R2.Kvadrat(R2) == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("({0};{1})         ({2};{3})          {4}          {5}", X1, Y1, X2, Y2, S, buf);
            X1 = R3.getX1(); X2 = R3.getX2(); Y1 = R3.getY1(); Y2 = R3.getY2(); S = R3.Square(X1, X2, Y1, Y2);
            if (R3.Kvadrat(R3) == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("({0};{1})         ({2};{3})          {4}          {5}", X1, Y1, X2, Y2, S, buf);
            bool key = Rectangle.Vnutri(R2, R3);
            if (key == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("Проверка, распологается ли прямоугольник R2 внутри прямоугольника R3: {0}",buf);

            R2.Moving();

            Console.WriteLine("Точка (X1,Y1)        Точка (X2,Y2)        Площадь        Квадрат ");
            X1 = R2.getX1(); X2 = R2.getX2(); Y1 = R2.getY1(); Y2 = R2.getY2(); S = R2.Square(X1, X2, Y1, Y2);
            if (R2.Kvadrat(R2) == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("({0};{1})         ({2};{3})          {4}          {5}", X1, Y1, X2, Y2, S, buf);

            R2.Povorot();

            Console.WriteLine("Точка (X1,Y1)        Точка (X2,Y2)        Площадь        Квадрат ");
            X1 = R2.getX1(); X2 = R2.getX2(); Y1 = R2.getY1(); Y2 = R2.getY2(); S = R2.Square(X1, X2, Y1, Y2);
            if (R2.Kvadrat(R2) == true) buf = "Да";
            else buf = "Нет";
            Console.WriteLine("({0};{1})         ({2};{3})          {4}          {5}", X1, Y1, X2, Y2, S, buf);
            Console.ReadKey();

        }
    }
}
