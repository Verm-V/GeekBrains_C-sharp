using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lesson_05_03
{

    class Program
    {
        /// <summary> разделитель между числами в вводимой строке </summary>
        const char separator = ' ';
        /// <summary> имя файла для записи </summary>
        const string filename = "numbers.txt";


        static void Main(string[] args)
        {
            byte[] numbers = Array.Empty<byte>();

            StringInput(ref numbers);

            //поскольку стоит задача просто записать байты в файл, 
            //то обошелся без сериализации - обычным BinaryWriter 
            BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create));
            writer.Write(numbers);

            Console.WriteLine($"Введенные данные сохранены в файл: {filename}");

            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();

        }


        /// <summary>
        /// Запрашивает у пользователя ввод строки byte чисел разделенных пробелами
        /// и проверяет правильность ввода
        /// </summary>
        /// <param name="numbers">массив в который будут записаны числа</param>
        /// <returns>строка введенная пользователем</returns>
        private static void StringInput(ref byte[] numbers)
        {
            Console.WriteLine("Введите строку целых чисел от 0 до 255 разделенных пробелами");
            bool isTextGood = false;
            string text = string.Empty;
            string[] textNumbers;

            while(!isTextGood)
            {
                text = Console.ReadLine();
                isTextGood = true;
                //проверка на то, что строка содержит только цифры и пробелы
                for (int i = 0; i < text.Length; i++)
                    if (!char.IsDigit(text[i]) && !char.IsWhiteSpace(text[i]))
                        isTextGood = false;
 
                if (isTextGood)
                {
                    //разделение введенной строки на части и инициализация массива для чисел
                    textNumbers = text.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                    numbers = new byte[textNumbers.Length];

                    //конвертирование текстовых значений в числовые
                    int i = 0;//array index
                    foreach (string textNumber in textNumbers)
                    {
                        if (!byte.TryParse(textNumber, out numbers[i]))
                            isTextGood = false;
                        i++;
                    }
                }

                if (!isTextGood)
                    Console.WriteLine("Ошибка. Повторите ввод.");
            }
        }

    }
}
