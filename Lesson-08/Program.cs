﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_08
{
    class Program
    {
        private static readonly int MAX_STR_LENGTH = 40;
        private static readonly int MIN_AGE = 1;
        private static readonly int MAX_AGE = 200;

        static void Main(string[] args)
        {
            string greeting = Properties.Settings.Default.Greeting;

            bool isNameExist = !string.IsNullOrEmpty(Properties.Settings.Default.UserName);
            bool isAgeExist = !string.IsNullOrEmpty(Properties.Settings.Default.UserAge);
            bool isJobExist = !string.IsNullOrEmpty(Properties.Settings.Default.UserJob);

            if(isNameExist && isAgeExist && isJobExist)
            {
                string userName = Properties.Settings.Default.UserName;
                string userAge = Properties.Settings.Default.UserAge;
                string userJob = Properties.Settings.Default.UserJob;
                Console.WriteLine($"{greeting}, {userName}.");
                Console.WriteLine($"Ваш возраст: {userAge}.");
                Console.WriteLine($"Ваше место работы: {userJob}.");
            }
            else
            {
                Console.WriteLine($"{greeting}.");
                if (!isNameExist)
                    Properties.Settings.Default.UserName = StringInput("Введите имя пользователя", MAX_STR_LENGTH);
                if (!isAgeExist)
                    Properties.Settings.Default.UserAge = NumberInput("Введите возраст пользователя", MIN_AGE, MAX_AGE, false).ToString();
                if (!isJobExist)
                    Properties.Settings.Default.UserJob = StringInput("Введите место работы пользователя", MAX_STR_LENGTH);
                Properties.Settings.Default.Save();
                Console.WriteLine($"Пожалуйста, перезапустите приложение.");
            }


            MessageWaitKey(string.Empty);        
        }


        /// <summary>
        /// Метод запрашивает у пользователя целое int число.
        /// </summary>
        /// <param name="message">Сообщение для пользователя</param>
        /// <param name="min">Минимальное значение ввода</param>
        /// <param name="max">Максимальное значение ввода</param>
        /// <param name="isOneDigit">Запрашивать одну цифру или несколько</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int NumberInput(string message, int min, int max, bool isOneDigit = true)
        {
            bool isInputCorrect = false; //флаг проверки
            int input = 0;
            Console.WriteLine($"{message} (от {min} до {max})");
            while (!isInputCorrect) //Цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                if (isOneDigit)
                    isInputCorrect = int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                else
                    isInputCorrect = int.TryParse(Console.ReadLine(), out input);

                if (isInputCorrect && (input < min || input > max))
                    isInputCorrect = false;

                if (!isInputCorrect)
                    if (isOneDigit)
                        try
                        {
                            Console.CursorLeft--;//Если ввели что-то не то, то просто возвращаем курсор на прежнее место
                        }
                        catch
                        {
                            //В случае ошибки, ввода каких-либо управляющих символов или попытках выхода курсора
                            //за пределы консоли, просто ничего не делаем и остаемся на месте
                        }
                    else
                        Console.WriteLine("Ошибка. Повторите ввод.");
            }
            return input;
        }

        /// <summary>
        /// Метод запрашивает у пользователя текстовую строку данных
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="max_length">ограничение на количество вводимых символов</param>
        /// <returns>введенную пользователем строку</returns>
        private static string StringInput(string message, int max_length)
        {
            bool check = false; //флаг проверки
            string input = string.Empty;
            while (!check) //цикл будет повторятся, пока вводимая строка не пройдет все проверки
            {
                Console.WriteLine($"{message} (максимум {max_length} символов)");
                //ввод и проверка на то, длину
                input = Console.ReadLine();
                if (input.Length > max_length)
                {
                    Console.WriteLine("Превышена максимальная длина строки, повторите ввод.");
                    check = false;
                }
                else
                    check = true;
            }
            return input;
        }

        /// <summary> выводит на экран сообщение и ждет нажатия любой клавиши </summary>
        /// <param name="message">сообщение для пользователя</param>
        private static void MessageWaitKey(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
