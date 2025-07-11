// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;



namespace ConsoleApplication2
{
   class Student
    {
        string Fam;
        string Group;
        int [] Ocenki;

        public Student(string fam, string group, int n)
        {
            Fam = fam;
            Group = group;
            Ocenki = new int[n]; 
        }
        ~Student() { }
        public double Sr_b()
        {
            get:
            {
                double s = 0;
                foreach (int x in Ocenki)
                    s += x;
                return s/Ocenki.Length;
            }
        }   
        public string Family
        {
            get { return Fam; }
            set { Fam = value; }
        }
        public string Gruppa
        { 
            get { return Group; } 
            set { Group = value; }
        }

        public void vvod_ocenok()
        {
            Console.WriteLine("Введите оценки в количестве "+Ocenki.Length);
            for(int i = 0; i < Ocenki.Length; i++) Ocenki[i] = Convert.ToInt32(Console.ReadLine());
        }
        public int this[int i]
        {
            get
            {
                if (i < 0 || i >= Ocenki.Length) throw new Exception("Недопустимый индекс");
                else return Ocenki[i];
            }
        }
        public int Kol_Ocenok
            { get { return Ocenki.Length; } }



    }
    class Program
    {
        static void Main(string[] args)
        {
            Student[] Stud;
            Console.WriteLine("Сколько студентов?");
            int m = int.Parse(Console.ReadLine());
            Stud = new Student[m];  
            for(int i = 0; i < m; i++)
            {
                Console.WriteLine("Задайте Фамилию, группу и количество оценок за сессию");
                Stud[i] = new Student(Console.ReadLine(), Console.ReadLine(), int.Parse(Console.ReadLine()));
                Stud[i].vvod_ocenok();
            }
            foreach(Student x in Stud) { Console.WriteLine(x.Family + " Группа " + x.Gruppa +" Средний бал " + Convert.ToString(x.Sr_b()) ); }
            
            Console.WriteLine("В какой группе хотите узнать количество двоечников?");
            string k1 = Console.ReadLine();
            int k_dv = 0;
            bool f = false;
            foreach (Student s in Stud) if (s.Gruppa==k1 && s.Sr_b() <= 2){ k_dv++; f = true; }
            if (f)
                Console.WriteLine("В группе " + k1 + " количество двоечников " + k_dv);
            else Console.WriteLine($"Такой группы {k1} не существует");
            
            Console.ReadKey();
            
        }
    }
}
