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
        private const int LINES_ON_SCREEN = 10;

        /// <summary> длина поля с номером процесса в таблице </summary>
        private const int INDEX_FIELD_LENGTH = 3;
        /// <summary> длина поля с ID процесса в таблице </summary>
        private const int ID_FIELD_LENGTH = 5;
        /// <summary> длина поля с именем процесса в таблице </summary>
        private const int NAME_FIELD_LENGTH = 40;

        /// <summary> максимально возможный ID процесса </summary>
        private const int ID_MAX = int.MaxValue;

        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[]
        {
            "Строка вверх",
            "Строка вниз",
            "Страница вверх",
            "Страница вниз",
            "Обновить список процессов",
            "Завершить процесс по ID",
            "Завершить процесс по имени",
            "Выход"
        };


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
                    case 5://refrsh process list
                        processes = Process.GetProcesses();
                        number = 0;
                        break;
                    case 6://kill process by ID
                        int id = NumberInput("Введите ID процесса котороый требуется удалить.", 0, ID_MAX, false);
                        Console.WriteLine(KillProcess(processes, id));
                        MessageWaitKey(string.Empty);
                        processes = Process.GetProcesses();
                        break;
                    case 7://kill process by name
                        string name = StringInput("Введите имя процесса котороый требуется удалить.", NAME_FIELD_LENGTH);
                        Console.WriteLine(KillProcess(processes, name));
                        MessageWaitKey(string.Empty);
                        processes = Process.GetProcesses();
                        break;
                    case 8://exit
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


        /// <summary>
        /// Метод запрашивает у пользователя целое int число.
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="min">минимальное значение ввода</param>
        /// <param name="max">максимальное значение ввода</param>
        /// <param name="isOneDigit">запрашивать одну цифру или несколько</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int NumberInput(string message, int min, int max, bool isOneDigit = true)
        {
            bool isInputCorrect = false; //флаг проверки
            int input = 0;
            Console.WriteLine($"{message}(от {min} до {max})");
            while (!isInputCorrect) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                if (isOneDigit)
                    isInputCorrect = int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                else
                    isInputCorrect = int.TryParse(Console.ReadLine(), out input);

                if (isInputCorrect && (input < min || input > max))
                    isInputCorrect = false;

                if (isOneDigit)
                    Console.CursorLeft--;
                else
                    if (!isInputCorrect) Console.Write("Ошибка. Повторите ввод.");
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

        /// <summary> завершает процесс по его ID </summary>
        /// <param name="processes">массив процессов</param>
        /// <param name="id">ID процесса</param>
        /// <returns>сообщение об успешном или неуспешном выполнении задачи</returns>
        private static object KillProcess(Process[] processes, int id)
        {
            int index = -1;//индекс нужного процесса в массиве

            //поиск нужного процесса по его ID
            for (int i = 0; i < processes.Length; i++)
                if (processes[i].Id == id) index = i;

            string message = string.Empty;
            if (index >= 0)
            {
                try
                {
                    processes[index].Kill();
                    message = "Процесс успешно завершен.";
                }
                catch
                {
                    message = "Не удалось завершить процесс.";
                }
            }
            else
                message = "Процесс не найден";

            return message;

        }

        /// <summary> завершает процесс по его имени </summary>
        /// <param name="processes">массив процессов</param>
        /// <param name="name">имя процесса</param>
        /// <returns>сообщение об успешном или неуспешном выполнении задачи</returns>
        private static object KillProcess(Process[] processes, string name)
        {
            int index = -1;//индекс нужного процесса в массиве

            //поиск нужного процесса по его ID
            for (int i = 0; i < processes.Length; i++)
                if (processes[i].ProcessName == name) index = i;

            string message = string.Empty;
            if (index >= 0)
            {
                try
                {
                    processes[index].Kill();
                    message = "Процесс успешно завершен.";
                }
                catch
                {
                    message = "Не удалось завершить процесс.";
                }
            }
            else
                message = "Процесс не найден";

            return message;

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
