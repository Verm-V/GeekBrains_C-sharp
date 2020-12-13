using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_04
{
    class Program
    {
        static void Main(string[] args)
        {
            // в этом задании выбор месяца буду делать, не через switch, а извлекать из массива по индексу.
            // создается массив со списком всех месяцев с момощью System.Globalization
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;

            int monthNumber;///номер месяца от 1 до 12
            string monthName; //сюда поместим имя месяца
            float temperature; //сюда пользоваль введет среднюю температуру за месяц

            //запрос средней температуры за месяц
            temperature = TemperatureInput("Введите среднее значение температуры за текущий месяц: ");
            Console.WriteLine($"--------\n");

            //запросили номер месяца
            monthNumber = MonthInput();
            monthName = monthNames[monthNumber-1];
            Console.WriteLine($"Месяц №{monthNumber} - это {monthName}");
            Console.WriteLine($"--------\n");

            //
            if (IsWinterMonth(monthNumber))
            {
                if (temperature > 0)
                {
                    Console.WriteLine("Дождливая зима.");
                }
                else
                {
                    Console.WriteLine("Обычная зима.");
                }
            }
            else
            {
                Console.WriteLine("Сейчас точно не зима.");
            }

            Console.WriteLine($"--------\n");

            Console.WriteLine($"\nНажмите любую клавишу. ");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод проверяет является ли месяц зимним по его номеру
        /// </summary>
        /// <param name="monthNumber">Номер месяца от 1 до 12</param>
        /// <returns>true если месяц зимний</returns>
        private static bool IsWinterMonth(int monthNumber)
        {
            int maskWinter = 0b_1000_0000_0011; //маска для проверки является ли месяц зимним
            
            //проверку сделал через преобразование в разрядное представление номера месяца
            //и сравнение по маске
            return (((0b1 << (monthNumber - 1)) & maskWinter) != 0);
        }

        /// <summary>
        /// Метод запрашивает у пользователя значение температуры.
        /// </summary>
        /// <param name="message">Сообщение для пользователя.</param> 
        /// <returns>Введенное пользователем значение температуры.</returns>
        private static float TemperatureInput(string message)
        {
            bool check = false; //флаг проверки
            float temperature = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от -80 до 120 градусов цельсия): ");
                //ввод и проверка на то, что это целове число
                check = float.TryParse(Console.ReadLine(), out temperature);
                if (!check)
                {
                    Console.WriteLine("Вы ввели не число, попробуйте еще раз.\n");
                }
                //далее проверки на минимальное и максимальное значение
                else if (temperature < -80)
                {
                    Console.WriteLine("На этой планете, нет таких холодных мест, повторите ввод.\n");
                    check = false;
                }
                else if (temperature > 120)
                {
                    Console.WriteLine("Вы бы вряд ли выжили в такой жаре, повторите ввод.\n");
                    check = false;
                }

            }
            return temperature;
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
