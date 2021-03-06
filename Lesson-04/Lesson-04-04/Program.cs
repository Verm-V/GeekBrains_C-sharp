﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_04
{
    class Program
    {

        /// <summary> максимальное количество членов последовательности 
        /// больше 93 ставить не стоит, т.к. используется тип ulong и 
        /// на 94-м члене последовательности произойдет переполнение</summary>
        private const int SEQUENCE_MAX = 93;

        static void Main(string[] args)
        {
            //запросим какой элемент последовательности нужен пользователю
            int sequenceMax = NumberInput("Введите количество членов последовательности Фибоначчи: ", 1, SEQUENCE_MAX);

            //массив для хранения рассчитанных чисел Фибоначчи
            ulong[] memory = new ulong[sequenceMax + 1];
            //делаем рассчет нужного элемента последовательности
            Console.WriteLine($"\nЭлемент №{sequenceMax} последовательности Фибоначчи равен: {Fibonachi(sequenceMax, memory)}\n");

            //поскольку в массиве все еще хранятся элементы, то можно их все вывести на экран
            Console.WriteLine($"Вся последовательность чисел Фибоначчи до элемента №{sequenceMax}\n");
            for (int i = 1; i <= sequenceMax; i++)
                Console.WriteLine("{0,2:d} - {1}", i, memory[i]);

            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();

        }

        /// <summary>
        /// Рекурсивно рассчитывает члены последовательноси Фибоначчи до заданного номера
        /// Вычисление происходит по формуле: F(n) = F(n-1) + F(n-2)
        /// Используется обычная рекурсия без заморочек. Метод применим приблизительно до номера 40
        /// Далее время рассчета очень сильно увеличивается, и уже даже на 43-м нужно ждать минуты 3,
        /// а на 50 более получаса.
        /// </summary>
        /// <param name="n">номер члена последовательности до которого считать</param>
        /// <returns>значение члена последовательности</returns>
        private static ulong Fibonachi(int n)
        {
            if (n > 1)
                return (ulong)(Fibonachi(n - 1) + Fibonachi(n - 2));
            else
                return (ulong)n;

            //конструкцию выше можно заменить на одну строку с использованием тернарного оператора:
            //return n > 1 ? (ulong)(FibStandart(n - 1) + FibStandart(n - 2)) : (ulong)n;
        }


        /// <summary>
        /// Рекурсивно рассчитывает члены последовательноси Фибоначчи до заданного номера
        /// используя массив для хранения ранее рассчитанных значений
        /// массив передается параметром и должен содержать количество элементов равное 
        /// значению рассчитываемого члена + 1
        /// метод работает значительно быстрее, теперь можно рассчитать последовательность
        /// до 93-го члена за секунду.
        /// </summary>
        /// <param name="n">номер члена последовательности до которого считать</param>
        /// <param name="memory">массив элементов хранящий ранее рассчитанные члены последовательности</param>
        /// <returns>значение члена последовательности</returns>
        private static ulong Fibonachi(int n, ulong [] memory)
        {
            if (n <= 1) return (ulong)n;

            //если в массиве памяти данный элемент уже вычислен, то вернем его, иначе пойдем по рекурсии
            if (memory[n] != 0) 
                return memory[n];
            else
            {
                memory[n] = Fibonachi(n - 1, memory) + Fibonachi(n - 2, memory);
                return memory[n];
            }
        }


        /// <summary>
        /// запрашивает у пользовател число с проверкой ввода на правильность и вхождение в заданный интервал
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="min">минимальное значение</param>
        /// <param name="max">максимальное значение</param>
        /// <returns>Число введенное пользователем</returns>
        private static int NumberInput(string message, int min, int max)
        {
            bool isInputCorrect = false; //флаг проверки
            int number = 0;
            while (!isInputCorrect) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от {min} до {max})");
                //ввод и проверка на то, что это целове число
                isInputCorrect = int.TryParse(Console.ReadLine(), out number);
                //проверка на вхождение в заданный интервал
                if (isInputCorrect && (number < min || number > max))
                    isInputCorrect = false;

                if (!isInputCorrect) Console.Write("Ошибка. ");
            }
            return number;
        }
    }
}
