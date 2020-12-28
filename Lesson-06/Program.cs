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
        #region ---- Статические поля класса ----

        #region -- Строковые константы --

        /// <summary> Ключи для словаря аргументов командной строки </summary>
        enum Arguments
        {
            help,
            lines,
            lines_help
        }

        /// <summary> Словарь аргументов командной строки </summary>
        private static readonly Dictionary<Arguments, string> arguments = new Dictionary<Arguments, string>
        {
        { Arguments.help, "-h"},
        { Arguments.lines, "-l"},
        { Arguments.lines_help, "-l <int> - number of processes to show on screen. 0 - to show all"},
        };

        /// <summary> Ключи для словаря с сообщениями об ошибках </summary>
        enum Errors
        {
            process_not_found,
            process_kill_fail,
            process_kill_succes,
            repeat_input_error,
            repeat_input_string_too_long
        }

        /// <summary> Словарь с сообщениями об ошибках </summary>
        private static readonly Dictionary<Errors, string> errors = new Dictionary<Errors, string>
        {
        { Errors.process_not_found, "Процесс не найден."},
        { Errors.process_kill_succes, "Процесс успешно завершен."},
        { Errors.process_kill_fail, "Не удалось завершить процесс."},
        { Errors.repeat_input_error, "Ошибка. Повторите ввод."},
        { Errors.repeat_input_string_too_long, "Превышена максимальная длина строки, повторите ввод."}
        };

        /// <summary> Ключи для словаря с ссобщениями для пользователя </summary>
        enum Messages
        {
            choose_option,
            enter_process_ID_to_kill,
            enter_process_name_to_kill,
            press_any_key,
            minimum,
            maximum,
            symbols,
            from,
            to,
            process_list,
            amount

        }

        /// <summary> Словарь с сообщениями для пользователя </summary>
        private static readonly Dictionary<Messages, string> messages = new Dictionary<Messages, string>
        {
        { Messages.choose_option, "Выберите опцию:"},
        { Messages.enter_process_ID_to_kill, "Введите ID процесса котороый требуется удалить."},
        { Messages.enter_process_name_to_kill, "Введите имя процесса котороый требуется удалить."},
        { Messages.press_any_key, "Нажмите любую клавишу."},
        { Messages.minimum, "минимум"},
        { Messages.maximum, "максимум"},
        { Messages.symbols, "символов"},
        { Messages.from, "от"},
        { Messages.to, "до"},
        { Messages.process_list, "Список процессов"},
        { Messages.amount, "всего"}
        };

                /// <summary> Пункты главного меню, последний пункт выход из программы </summary>
        private static readonly string[] mainMenu = new string[]
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

        #endregion

        #region -- Числовые константы --

        /// <summary> Количество строк из списка процессов, одновременно отображающихся на экране </summary>
        private const int LINES_ON_SCREEN = 10;

        /// <summary> Длина поля с номером процесса в таблице </summary>
        private const int INDEX_FIELD_LENGTH = 3;
        /// <summary> Длина поля с ID процесса в таблице </summary>
        private const int ID_FIELD_LENGTH = 5;
        /// <summary> Длина поля с именем процесса в таблице </summary>
        private const int NAME_FIELD_LENGTH = 40;

        /// <summary> Максимально возможный ID процесса </summary>
        private const int ID_MAX = int.MaxValue;


        #endregion

        #endregion


        #region ---- Основной цикл ----

        static int Main(string[] args)
        {
            int lines = LINES_ON_SCREEN;//Количество процессво отображаемых на экране, далее может быть скорректировано.

            //Обработка аругментов командной строки
            if (args.Length != 0)
            {
                if(args[0] == arguments[Arguments.help])//Вывод справки по аргументам
                {
                    Console.WriteLine(arguments[Arguments.lines_help]);
                    return 0;
                }
                else if (args[0] == arguments[Arguments.lines])//Изменение количества процессов выводимых на экран
                {
                    try
                    {
                        int.TryParse(args[1], out lines);
                        if (lines < 0) lines = 0;
                    }
                    catch //Если аргумент не введен или введен неправильно, то устанавливаем количество строк максимальным
                    {
                        lines = 0;
                    }
                }
            }

            //Формируем сообщение главного меню справочника
            string mainMenuMessage = messages[Messages.choose_option]+"\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";


            Process[] processes = Process.GetProcesses();


            int number = 0;
            ScreenOutput(processes, number, lines);

            bool isExit = false;
            //Основной цикл
            while (!isExit)
            {
                int input = NumberInput(mainMenuMessage, 1, mainMenu.Length);
                switch (input)
                {
                    case 1://line up
                        number--;
                        number = CheckSetLimits(number, processes.Length, lines);
                        break;
                    case 2://line down
                        number++;
                        number = CheckSetLimits(number, processes.Length, lines);
                        break;
                    case 3://page up
                        number -= lines;
                        number = CheckSetLimits(number, processes.Length, lines);
                        break;
                    case 4://page down
                        number += lines;
                        number = CheckSetLimits(number, processes.Length, lines);
                        break;
                    case 5://refrsh process list
                        processes = Process.GetProcesses();
                        number = 0;
                        break;
                    case 6://kill process by ID
                        ScreenOutput(processes, number, lines);
                        int id = NumberInput(messages[Messages.enter_process_ID_to_kill], 0, ID_MAX, false);
                        Console.WriteLine(KillProcess(processes, id));
                        MessageWaitKey(string.Empty);
                        processes = Process.GetProcesses();
                        break;
                    case 7://kill process by name
                        ScreenOutput(processes, number, lines);
                        string name = StringInput(messages[Messages.enter_process_name_to_kill], NAME_FIELD_LENGTH);
                        Console.WriteLine(KillProcess(processes, name));
                        MessageWaitKey(string.Empty);
                        processes = Process.GetProcesses();
                        break;
                    case 8://exit
                        isExit = true;
                        break;
                }
                ScreenOutput(processes, number, lines);
            }


            MessageWaitKey(string.Empty);

            return 0;
        }

        #endregion


        #region ---- Методы класса ----

        #region -- Вывод на экран --


        /// <summary>
        /// Выводит на экран список процессов с очисткой консоли
        /// </summary>
        /// <param name="processes">Массив содержащий список процессов</param>
        /// <param name="number">Идекс масива с которого надо выводить список на экран</param>
        /// <param name="lines">Количество строк отображаемых на экране, 0 - если все</param>
        private static void ScreenOutput(Process[] processes, int number, int lines)
        {
            if (lines == 0 || lines > processes.Length) lines = processes.Length;
         
            //number--;
            if (number < 0) number = 0;
            if (number + lines > processes.Length) number = processes.Length - lines;

            //Рассчет позиции для отметки на скроллбаре
            int scrollPosition = (number * lines / (processes.Length - lines + 1));
                        
            Console.Clear();
            Console.WriteLine($"{messages[Messages.process_list]} ({messages[Messages.amount]} {processes.Length})");
            Console.WriteLine("   №      ID    Process name");
            Console.WriteLine("+-----+-------+------------------------------------------+");
            for (int i = number; i < number + lines; i++)
            {

                Console.Write(string.Format($"" +
                    $"| {i + 1,INDEX_FIELD_LENGTH:d} " +
                    $"| {processes[i].Id,ID_FIELD_LENGTH} " +
                    $"| {processes[i].ProcessName,-NAME_FIELD_LENGTH} " +
                    (scrollPosition == i - number ? '#' : '|') +  "\n"));
            }
            Console.WriteLine("+-----+-------+------------------------------------------+");
        }

        /// <summary> Выводит на экран сообщение и ждет нажатия любой клавиши </summary>
        /// <param name="message">Сообщение для пользователя</param>
        private static void MessageWaitKey(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(messages[Messages.press_any_key]);
            Console.ReadKey();
        }

        #endregion

        #region -- Ввод данных --

        /// <summary>
        /// Метод запрашивает у пользователя текстовую строку данных
        /// </summary>
        /// <param name="message">Сообщение для пользователя</param>
        /// <param name="max_length">Ограничение на количество вводимых символов</param>
        /// <returns>Введенную пользователем строку</returns>
        private static string StringInput(string message, int max_length)
        {
            bool check = false; //флаг проверки
            string input = string.Empty;
            while (!check) //Цикл будет повторятся, пока вводимая строка не пройдет все проверки
            {
                Console.WriteLine($"{message} ({messages[Messages.maximum]} {max_length} {messages[Messages.symbols]})");
                //Ввод и проверка на то, длину
                input = Console.ReadLine();
                if (input.Length > max_length)
                {
                    Console.WriteLine(errors[Errors.repeat_input_string_too_long]);
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
        /// <param name="message">Сообщение для пользователя</param>
        /// <param name="min">Минимальное значение ввода</param>
        /// <param name="max">Максимальное значение ввода</param>
        /// <param name="isOneDigit">Запрашивать одну цифру или несколько</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int NumberInput(string message, int min, int max, bool isOneDigit = true)
        {
            bool isInputCorrect = false; //флаг проверки
            int input = 0;
            Console.WriteLine($"{message}({messages[Messages.from]} {min} {messages[Messages.to]} {max})");
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
                        Console.WriteLine(errors[Errors.repeat_input_error]);
            }
            return input;
        }

        #endregion

        #region -- Методы для завершения процесса --

        /// <summary> Завершает заданный процесс </summary>
        /// <param name="process">Процесс который нужно завершить</param>
        /// <returns>Сообщение о статусе операции</returns>
        private static string KillProcess(Process process)
        {
            string message = string.Empty;
            try
            {
                process.Kill();
                message = errors[Errors.process_kill_succes];
            }
            catch
            {
                message = errors[Errors.process_kill_fail];
            }
            return message;

        }

        /// <summary> Завершает процесс по его ID </summary>
        /// <param name="processes">Массив процессов</param>
        /// <param name="id">ID процесса</param>
        /// <returns>Сообщение о статусе операции</returns>
        private static object KillProcess(Process[] processes, int id)
        {
            int index = -1;//Индекс нужного процесса в массиве

            //Поиск нужного процесса по его ID
            for (int i = 0; i < processes.Length; i++)
                if (processes[i].Id == id) index = i;

            string message = string.Empty;
            if (index >= 0)
                message = KillProcess(processes[index]);
            else
                message = errors[Errors.process_not_found];

            return message;
        }

        /// <summary> Завершает процесс по его имени </summary>
        /// <param name="processes">Массив процессов</param>
        /// <param name="name">Ммя процесса</param>
        /// <returns>Сообщение о статусе операции</returns>
        private static object KillProcess(Process[] processes, string name)
        {
            int index = -1;//Индекс нужного процесса в массиве

            //Поиск нужного процесса по его имени
            for (int i = 0; i < processes.Length; i++)
                if (processes[i].ProcessName == name) index = i;

            string message = string.Empty;
            if (index >= 0)
                message = KillProcess(processes[index]);
            else
                message = errors[Errors.process_not_found];

            return message;

        }

        #endregion

        /// <summary>
        /// Проверка индекса на вхождение в число количества процессов
        /// с учетом количества процессов отображаемых на экране
        /// </summary>
        /// <param name="number">Индекс массива процессов</param>
        /// <param name="length">Длина массива процессов</param>
        /// <param name="lines">Количество строк списка отображаемых на экране</param>
        /// <returns>Скорректированный индекс входящий в нужный интервал</returns>
        private static int CheckSetLimits(int number, int length, int lines)
        {
            if (number < 0) number = 0;
            if (number + lines > length) number = length - lines;
            return number;
        }

        #endregion
    }
}
