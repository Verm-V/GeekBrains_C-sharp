using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_02
{
    class Program
    {
        /// <summary> разделитель между числами в вводимой строке </summary>
        const char separator = ' '; 
        static void Main(string[] args)
        {
            bool isTextGood = false;//флаг проверки введеной строки на соответсвие условиям задачи
            float sum = 0;//здесь будет сумма строк из чисел

            while (!isTextGood)//цикл будет крутится пока строка не пройдет все проверки
            {
                //запрашиваем у пользователя строку чисел разделенных пробелами
                string text = StringInput("Введите список чисел отделенных пробелами друг от друга:\n");

                //разбиваем ее на массив строк, с удалением пустых строк (если будет несколько пробелов подряд)
                string[] textNumbers = text.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                float number = 0;

                sum = 0;
                isTextGood = true;

                //пытаемся конвертировать каждую строку в массиве в число и прибавить его к сумме
                //если какое-то из элементов не конвертируется в массив, то сбрасываем флаг проверки
                //и цикл перезапустится с новым запросом строки у пользователя
                foreach (string textNumber in textNumbers)
                {
                    if (!float.TryParse(textNumber, out number))
                    {
                        isTextGood = false;
                        if (!isTextGood) Console.WriteLine("введенная строка не соответствует критериям, повторите ввод.\n");
                        number = 0;
                    }
                    sum += number;
                }
                
            }


            Console.WriteLine($"\nСумма всех чисел в введенной строке равна {sum}");

            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает у пользователя ввод строки с заданным сообщением
        /// </summary>
        /// <param name="message">собщение для пользователя</param>
        /// <returns>строка введенная пользователем</returns>
        private static string StringInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

    }
}
