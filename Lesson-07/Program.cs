using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_07
{
    class Program
    {
        /// <summary> Символ которым рисуем блока </summary>
        private static readonly char BLOCK = '#';
        /// <summary> Минмальная длина стороны квадрата </summary>
        private static readonly int MIN_LENGTH = 2;
        /// <summary> Максимальная длина стороны квадрата </summary>
        private static readonly int MAX_LENGTH = 10;
        /// <summary> Задержка для визуализации рисования </summary>
        private static readonly int DELAY = 100;

        static void Main(string[] args)
        {
            //Запрашиваем размер квадрата
            int length = NumberInput("Введите длину стороны квадрата", MIN_LENGTH, MAX_LENGTH);

            //Рисуем квадрат
            DrawSquare(length);

            Console.SetCursorPosition(0, length + 1);
            MessageWaitKey(string.Empty);
        }

        /// <summary> Рисует квадрат в консоли </summary>
        /// <param name="length"> Длина стороны квадрата </param>
        private static void DrawSquare(int length)
        {
            Console.Clear();
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(BLOCK);
                Console.SetCursorPosition(length - 1, i);
                Console.Write(BLOCK);
                Console.SetCursorPosition(i, 0);
                Console.Write(BLOCK);
                Console.SetCursorPosition(i, length - 1);
                Console.Write(BLOCK);
                System.Threading.Thread.Sleep(DELAY);//Небольшая задержка

            }
        }


        /// <summary> Выводит на экран сообщение и ждет нажатия любой клавиши </summary>
        /// <param name="message">Сообщение для пользователя</param>
        private static void MessageWaitKey(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Нажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает у пользовател число с проверкой ввода на правильность и вхождение в заданный интервал
        /// </summary>
        /// <param name="message">Сообщение для пользователя</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Число введенное пользователем</returns>
        private static int NumberInput(string message, int min, int max)
        {
            bool isInputCorrect = false; //Флаг проверки
            int number = 0;
            while (!isInputCorrect) //Цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от {min} до {max})");
                //Ввод и проверка на то, что это целове число
                isInputCorrect = int.TryParse(Console.ReadLine(), out number);
                //Проверка на вхождение в заданный интервал
                if (isInputCorrect && (number < min || number > max))
                    isInputCorrect = false;

                if (!isInputCorrect) Console.Write("Ошибка. ");
            }
            return number;
        }

    }



}
