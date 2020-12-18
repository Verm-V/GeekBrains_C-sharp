using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите какое-нибудь предложение");
            string line = Console.ReadLine();

            Console.WriteLine("\nэта же строка в обратном порядке выглядит так:\n");

            //вариант первый переворот строки в цикле
            Console.WriteLine("\nразворот строки через цикл");
            for (int i = 0; i < line.Length; i++)
                Console.Write(line[line.Length - (i + 1)]);

            //вариант второй переворот строки через массив char
            Console.WriteLine("\nразворот строки через массив char");
            char[] letters = line.ToCharArray();
            Array.Reverse(letters);
            Console.WriteLine(new string(letters));

            Console.WriteLine("\n\nНажмите любую клавишу");
            Console.ReadKey();

        }


    }
}
