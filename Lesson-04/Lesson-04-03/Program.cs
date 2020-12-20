using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_03
{
    class Program
    {
        /// <summary> общее количество месяцев в году </summary>
        const int MONTHS_AMOUNT = 12;
        /// <summary> количество месяцев в одном сезоне </summary>
        const int MONTHS_IN_SEASON = 3;
        /// <summary> названия времен года </summary>
        private static readonly string[] SeasonNames = new string[4] { "зима", "весна", "лето", "осень" };

        /// <summary> времена года </summary>
        private enum Season
        {
            Winter = 0,
            Spring,
            Summer,
            Autumn
        }
        static void Main(string[] args)
        {
            int monthNumber = NumberInput("Введите номер месяца: ", 1, MONTHS_AMOUNT);
            Season season = GetSeason(monthNumber);
            Console.WriteLine($"Месяцу №{monthNumber} соответствует время года - {GetSeasonName(season)}");

            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();

        }

        /// <summary>
        /// преобразует Season в текстовое представление
        /// </summary>
        /// <param name="season"></param>
        /// <returns>Название времени года</returns>
        private static string GetSeasonName (Season season)
        {
            return SeasonNames[(int)season];
        }

        /// <summary>
        /// возвращает значение времени года по номеру месяца
        /// </summary>
        /// <param name="monthNumber">Номер месяца от 1 до 12</param>
        /// <returns>Значение из перечисления Season, соответствующее номеру месяца</returns>
        private static Season GetSeason(int monthNumber)
        {
            return (Season)((monthNumber < MONTHS_AMOUNT ? monthNumber : 0)/MONTHS_IN_SEASON);
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
