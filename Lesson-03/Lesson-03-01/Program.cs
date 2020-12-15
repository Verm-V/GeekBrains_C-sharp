using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_04
{
    class Program
    {
        /// <summary> максимально возможный размер массива.</summary>
        private const int MAX_DIMENSION = 10;
        static void Main(string[] args)
        {
            int[,] array;

            int rows = NumberInput("Введите количество строк в массиве");
            int columns = NumberInput("Введите количество столбцов в массиве");
            array = new int[rows, columns];
            Console.WriteLine($"--------\n");

            //заполняем массив рандомной чепухой
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i / columns, i % columns] = rnd.Next(100);

            //вывод всего массива на экран
            //ради интереса сделал без использования вложенных циклов
            //использовал форматирование через параметры, чтобы вывод выглядел красиво
            Console.WriteLine("Содержимое всего массива");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write
                    (
                    "{0,2:d} {1}", 
                    array[i / columns, i % columns], 
                    ((i + 1) % columns == 0 ? "\n" : string.Empty)
                    );
            }
            Console.WriteLine();

            //вывод главной диагонали
            //здесь вывод делается с помощью вложенных циклов
            Console.WriteLine("Содержимое главной диагонали");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write
                        (
                        "{0,2:d} {1}", 
                        ((i == j) ? array[i, j].ToString() : ".."), 
                        ((j == columns - 1) ? "\n" : string.Empty)
                        );
                    //ToString() используется из-за тернарного оператора
                }
            }
            Console.WriteLine();

            //вывод содержимого главной диагонали в одну строку
            Console.WriteLine("Содержимое главной диагонали одной строкой");
            int min = (rows < columns) ? rows : columns; //определяем наименьшее измерение массива
            for (int i = 0; i < min; i++)
            {
                Console.Write("{0,2:d} ", array[i,i]);
            }
            Console.WriteLine();

            Console.WriteLine($"--------\n");
            Console.WriteLine("\nНажмите любую клавишу");
            Console.ReadKey();

        }

        /// <summary>
        /// Метод запрашивает у пользователя целое число.
        /// </summary>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int NumberInput(string message)
        {
            bool check = false; //флаг проверки
            int number = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от 1 до {MAX_DIMENSION})");
                //ввод и проверка на то, что это целове число
                check = int.TryParse(Console.ReadLine(), out number);
                if (check && (number < 1 || number > MAX_DIMENSION))
                    check = false;
            }
            return number;
        }

    }
}
