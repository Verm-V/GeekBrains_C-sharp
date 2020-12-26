using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_06
{
    class Program
    {
        /// <summary> количество строк из списка процессов, одновременно отображающихся на экране </summary>
        private const int LINES_ON_SCREEN = 16;

        /// <summary> длина поля с номером процесса в таблице </summary>
        private const int INDEX_FIELD_LENGTH = 3;
        /// <summary> длина поля с ID процесса в таблице </summary>
        private const int ID_FIELD_LENGTH = 5;
        /// <summary> длина поля с именем процесса в таблице </summary>
        private const int NAME_FIELD_LENGTH = 40;

        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[]
        { "Строка вверх", "Строка вниз", "Страница вверх", "Страница вниз", "Выход" };


        static void Main(string[] args)
        {
            //формируем сообщение главного меню справочника
            string mainMenuMessage = "Выберите опцию:\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";

            Process[] processes = Process.GetProcesses();


            int number = 0;
            ScreenOutput(processes, number, LINES_ON_SCREEN);

            bool isExit = false;
            //основной цикл
            while (!isExit)
            {
                int input = NumberInput(mainMenuMessage, 1, mainMenu.Length);
                switch (input)
                {
                    case 1://line up
                        number--;
                        number = CheckSetLimits(number, processes.Length);
                        break;
                    case 2://line down
                        number++;
                        number = CheckSetLimits(number, processes.Length);
                        break;
                    case 3://page up
                        number -= LINES_ON_SCREEN;
                        number = CheckSetLimits(number, processes.Length);
                        break;
                    case 4://page down
                        number += LINES_ON_SCREEN;
                        number = CheckSetLimits(number, processes.Length);
                        break;
                    case 5://exit
                        isExit = true;
                        break;
                }
                ScreenOutput(processes, number, LINES_ON_SCREEN);
            }


            MessageWaitKey("");
        }

        /// <summary>
        /// выводит на экран список процессов
        /// </summary>
        /// <param name="processes">массив содержащий список процессов</param>
        /// <param name="number">идекс масива с которого надо выводить список на экран</param>
        /// <param name="lines">количество строк отображаемых на экране</param>
        private static void ScreenOutput(Process[] processes, int number, int lines)
        {
            //number--;
            if (number < 0) number = 0;
            if (number + lines > processes.Length) number = processes.Length - lines;

            //рассчет позиции для отметки на скроллбаре
            int scrollPosition = (number * lines / (processes.Length - lines + 1));
                        
            Console.Clear();
            Console.WriteLine("Список заданий");
            Console.WriteLine("   №      ID    Process name");
            Console.WriteLine("+-----+-------+------------------------------------------+");
            for (int i = number; i < number + lines; i++)
            {

                //Console.WriteLine($"{ processes[i].Id}");
                Console.Write(string.Format($"" +
                    $"| {i + 1,INDEX_FIELD_LENGTH:d} " +
                    $"| {processes[i].Id,ID_FIELD_LENGTH} " +
                    $"| {processes[i].ProcessName,-NAME_FIELD_LENGTH} " +
                    (scrollPosition == i - number ? '#' : '|') +  "\n"));
            }
            Console.WriteLine("+-----+-------+------------------------------------------+");
        }

        /// <summary>
        /// Метод запрашивает у пользователя целое число.
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="min">минимальное значение ввода</param>
        /// <param name="max">максимальное значение ввода</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int NumberInput(string message, int min, int max)
        {
            bool check = false; //флаг проверки
            int input = 0;
            Console.WriteLine($"{message}(от {min} до {max})");
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                //ввод и проверка на то, что это целове число и оно входит в заданные границы
                check = int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                if (check && (input < min || input > max))
                    check = false;
                Console.CursorLeft--;
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


        /// <summary>
        /// проверка индекса на вхождение в число количества процессов
        /// с учетом количества процессов отображаемых на экране
        /// </summary>
        /// <param name="number">индекс массива процессов</param>
        /// <param name="length">длина массива процессов</param>
        /// <returns>скорректированный индекс входящий в нужный интервал</returns>
        private static int CheckSetLimits(int number, int length)
        {
            if (number < 0) number = 0;
            if (number + LINES_ON_SCREEN > length) number = length - LINES_ON_SCREEN;
            return number;
        }

    }
}
