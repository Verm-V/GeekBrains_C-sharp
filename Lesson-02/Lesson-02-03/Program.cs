using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_03
{
    class Program
    {
        static void Main(string[] args)
        {            
            int number = NumberInput();

            Console.WriteLine($"--------\n");

            Console.WriteLine("Введенное число является " + ((number % 2 == 0) ? string.Empty : "НЕ") + "четным.");

            Console.WriteLine($"\nНажмите любую клавишу. ");
            Console.ReadKey();

        }


        /// <summary>
        /// Метод запрашивает у пользователя целое число.
        /// </summary>
        /// <returns>Введенное пользователем целое число.</returns>
        private static int NumberInput()
        {
            bool check = false; //флаг проверки
            int number = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine("Введите целое число: ");
                //ввод и проверка на то, что это целове число
                check = int.TryParse(Console.ReadLine(), out number);
            }
            return number;
        }


    }
}
