using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_02
{
    class Program
    {
        static void Main(string[] args)
        {
            int monthNumber;
            string monthName = string.Empty;
            //запросили номер месяца
            switch(monthNumber = MonthInput())
            {
                case 1:
                    monthName = "январь";
                    break;
                case 2:
                    monthName = "февраль";
                    break;
                case 3:
                    monthName = "март";
                    break;
                case 4:
                    monthName = "апрель";
                    break;
                case 5:
                    monthName = "май";
                    break;
                case 6:
                    monthName = "июнь";
                    break;
                case 7:
                    monthName = "июль";
                    break;
                case 8:
                    monthName = "август";
                    break;
                case 9:
                    monthName = "сеньябрь";
                    break;
                case 10:
                    monthName = "октябрь";
                    break;
                case 11:
                    monthName = "ноябрь";
                    break;
                case 12:
                    monthName = "декабрь";
                    break;
            }

            Console.WriteLine($"--------\n");

            Console.WriteLine($"Месяц №{monthNumber} - это {monthName}");

            Console.WriteLine($"\nНажмите любую клавишу. ");
            Console.ReadKey();

        }

        /// <summary>
        /// Метод запрашивает у пользователя номер месяца.
        /// </summary>
        /// <returns>Возвращает число от 1 до 12</returns>
        private static int MonthInput()
        {
            bool check = false; //флаг проверки
            int month = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine("Введите номер текущего месяца: ");
                //ввод и проверка на то, что это целове число
                check = int.TryParse(Console.ReadLine(), out month);
                if (!check)
                {
                    Console.WriteLine("Вы ввели не целое число, попробуйте еще раз.\n");
                }
                //далее проверка на минимальное и максимальное значение
                else if (month < 1 || month > 12)
                {
                    Console.WriteLine("В нашем календаре всего 12 месяцев. Повторите ввод.\n");
                    check = false;
                }

            }
            return month;
        }


    }
}
