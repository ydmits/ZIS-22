using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
 // fffffffffffffffffffffffffffffusk
namespace lab5
{
    abstract class Polygon
    {
        public string Type;
        public string Color;
        public double[] Cords;
        public Polygon() { Type = Color = null; }
        public Polygon(string Type, string Color, double[] Cords) 
        {
            this.Type = Type;
            this.Color = Color;
            this.Cords = Cords;
        }
        abstract public double Square();
      
        abstract public void PrintInfo();
    }
    class Rectangle: Polygon
    {
        public Rectangle(string Color, double[] Cords): base ("прямоугольник", Color, Cords) { }
        override public double Square()
        {
            return Cords[0] * Cords[1];
        }
        
        public override void PrintInfo()
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Color);
            Console.WriteLine("║{0,13}║{1,10:.##}║{2,10}║", Type, Square(), Color);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Triangle : Polygon
    {
        public Triangle(string Color, double[] Cords): base("треугольник", Color, Cords) { }
        public double Perimetr()
        {
            return Cords[0] + Cords[1] + Cords[2];
        }
        override public double Square()
        {
            double p = Perimetr() / 2;

            return Math.Sqrt(p * (p - Cords[0]) * (p - Cords[1]) * (p - Cords[2]));
        }
        
        public bool Proverka()
        {

            if (Math.Pow(Cords[0], 2) == Math.Pow(Cords[1], 2) + Math.Pow(Cords[2], 2)) return true;
             else if (Math.Pow(Cords[1], 2) == Math.Pow(Cords[0], 2) + Math.Pow(Cords[2], 2)) return true;
             else if (Math.Pow(Cords[2], 2) == Math.Pow(Cords[1], 2) + Math.Pow(Cords[0], 2)) return true;
             else return false;
            
        }
        public override void PrintInfo()
        {
            
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Color);
            if (Console.ForegroundColor == ConsoleColor.Red &&  Proverka() == true )
                 Console.WriteLine("║{0,13}║{1,10:.##}║{2,10}║Периметр{3,10:.##}║", Type, Square(), Color, Perimetr());
            else Console.WriteLine("║{0,13}║{1,10:.##}║{2,10}║", Type, Square(), Color);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Sort : IComparer
    {
        public int Compare(object Obj, object obj)
        {
            Polygon p = Obj as Polygon;
            Polygon p2 = obj as Polygon;
            if (p2.Square() > p.Square()) return 1;
            else
            {
                if (p2.Square() == p.Square()) return 0;
                else return -1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя файла");
            try
            {
                string s = Console.ReadLine();
                StreamReader f = new StreamReader(s, Encoding.Default);
                StreamReader file = new StreamReader(s, Encoding.Default);
                int count = 0;
                while (file.ReadLine() != null) count++;
                Polygon[] obj = new Polygon[count];
                string line;
                int i, j = 0;
                while ((line = f.ReadLine()) != null)
                {
                    i = 0;
                    string temp = "";
                    while (!(Char.IsDigit(line[i])))
                    {
                        if (line[i] == '‐') break;
                        temp = temp + line[i];
                        i++;
                    }
                    temp = temp.Trim().ToLower();
                    double[] tmparr = new double[10];
                    int k = 0;
                    string tmp = "";
                    while (!Char.IsLetter(line[i]))
                    {
                        tmp = "";
                        while ((line[i] == '-')||(Char.IsDigit(line[i])) || line[i]==',')
                    {
                            tmp = tmp + line[i];
                            i++;
                        }
                        if (tmp != "")
                        {
                            try
                            {
                                if ((tmparr[k] += double.Parse(tmp))
                                != 0) k++;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                break;
                            }
                        }
                        i++;
                    }
                    string color = "";
                    while (i < line.Length)
                    {
                        color = color + line[i];
                        i++;
                    }
                    if (String.Compare(temp, "треугольник") == 0)
                    {
                        obj[j] = new Triangle(color, tmparr);
                        j++;
                    }
                    else
                    if (String.Compare(temp, "прямоугольник")
                    == 0)
                    {
                        obj[j] = new Rectangle(color, tmparr);
                        j++;
                    }
                }
                f.Close();
                Console.WriteLine("╔═════════════╦══════════╦══════════╗");
                Console.WriteLine("║ Тип Фигуры  ║ Площадь  ║ Цвет     ║");
                Console.WriteLine("╠═════════════╬══════════╬══════════╣");
            for (i = 0; i < j; i++)
                    obj[i].PrintInfo();
                Console.WriteLine("╚═════════════╩══════════╩══════════╝");
                Array.Sort(obj, new Sort());
                Console.WriteLine("╔═════════════╦══════════╦══════════╗");
                Console.WriteLine("║ Тип Фигуры  ║ Площадь  ║ Цвет     ║");
                Console.WriteLine("╠═════════════╬══════════╬══════════╣");
            for (i = 0; i < j; i++)
                    obj[i].PrintInfo();
                Console.WriteLine("╚═════════════╩══════════╩══════════╝");
            }
            catch (FileNotFoundException e)
            { Console.WriteLine(e.Message); }
            Console.ReadKey();
        }
    }
    
}
    


