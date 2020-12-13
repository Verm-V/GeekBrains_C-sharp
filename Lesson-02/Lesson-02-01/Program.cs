using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_01
{
    class Program
    {
        static void Main(string[] args)
        {

            int min, max;
            //запросили минимум
            min = TemperatureInput("Введите МИНИМАЛЬНУЮ температуру за сутки");

            Console.WriteLine($"--------\n");

            //запрос максимума, будет повторяться до тех пор пока не окажется больше чем минимум
            while ((max = TemperatureInput("Введите МАКСИМАЛЬНУЮ температуру за сутки")) <= min)
                {Console.WriteLine("Максимальная температура должна быть больше минимальной. Повторите ввод.\n");}

            Console.WriteLine($"--------\n");

            //окончательный вывод
            Console.WriteLine($"Минимальная температура: {min}");
            Console.WriteLine($"Максимальная температура: {max}");
            Console.WriteLine($"Средняя температура за сутки: {(float)(min+max)/2}");

            Console.WriteLine($"\nНажмите любую клавишу. ");
            Console.ReadKey();
        }

        /// <summary>
        /// Метод запрашивает у пользователя значение температуры.
        /// </summary>
        /// <param name="message">Сообщение для пользователя.</param> 
        /// <returns>Введенное пользователем значение температуры.</returns>
        private static int TemperatureInput(string message)
        {
            bool check = false; //флаг проверки
            int temperature = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от -80 до 120 градусов цельсия): ");
                //ввод и проверка на то, что это целове число
                check = int.TryParse(Console.ReadLine(), out temperature);
                if (!check)
                {
                    Console.WriteLine("Вы ввели не целое число, попробуйте еще раз.\n");
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
    }
}
