using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Lesson_05_05
{
    class Program
    {
        /// <summary> имя файла для записи </summary>
        const string filename = "ToDoList.json";

        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[] 
        { "Добавить задание", "Удалить задание", "Изменить статус задания", "Загрузить список с диска", "Сохранить список на диск", "Выход" };

        /// <summary> опции сериализации </summary>
        private static JsonSerializerOptions options = new JsonSerializerOptions { AllowTrailingCommas = true, WriteIndented = true };

        /// <summary> максимальная длина для описания задачи </summary>
        const int TASK_MAX_LENGTH = 40;

        static void Main(string[] args)
        {
            //формируем сообщение главного меню справочника
            string mainMenuMessage = "Выберите опцию:\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";

            TaskList taskList = new TaskList();

            bool isExit = false; //если true, то происходит выход из основного цикла
            //основной рабочий цикл
            while (!isExit)
            {
                Console.Clear();
                ListOutput(taskList);
                int number; //для ввода номера записи в таблице
                switch (IntNumberInput(mainMenuMessage, 1, mainMenu.Length))
                {
                    case 1: //добавление задания
                        ListOutput(taskList);
                        taskList.AddItem(StringInput("Введите текст задачи", TASK_MAX_LENGTH));
                        ListOutput(taskList);
                        MessageWaitKey($"Новое задание добавлено в список.");
                        break;
                    case 2: //удаление задания
                        ListOutput(taskList);
                        number = IntNumberInput("Введите номер задания", 1, taskList.Count) - 1;
                        taskList.DeleteItem(number);
                        ListOutput(taskList);
                        MessageWaitKey($"Задание №{number+1} удалено из списка задач");
                        break;
                    case 3: //изменение статуса задания
                        ListOutput(taskList);
                        number = IntNumberInput("Введите номер задания", 1, taskList.Count) - 1;
                        taskList.ChangeStatus(number);
                        break;
                    case 4: //считать список из файла на диске
                        if (File.Exists(filename))
                        { 
                            taskList.DeserializeJSON(File.ReadAllText(filename));
                            ListOutput(taskList);
                            MessageWaitKey($"Список задач успешно загружен из файла {filename}");
                        }
                        else
                        {
                            MessageWaitKey("Файл со списком задач отсутствует.");
                        }
                        break;
                    case 5: //сохранить список в файл на диске
                        ListOutput(taskList);
                        File.WriteAllText(filename, taskList.SerializeJSON());
                        MessageWaitKey($"Список задач успешно сохранен в файл {filename}");
                        break;
                    case 6: //выход
                        isExit = true;
                        MessageWaitKey("До свидания.");
                        break;
                }

            }

        }


        /// <summary>
        /// выводит на экран содержимое списка заданий
        /// </summary>
        private static void ListOutput(TaskList taskList)
        {
            Console.Clear();
            Console.WriteLine("Список заданий");
            Console.WriteLine("+---+-----+------------------------------------------+");
            for (int i = 0; i < taskList.Count; i++)
            {
                Console.Write(string.Format($"" +
                    $"| {i + 1,1:d} " +
                    $"| [{(taskList.GetStatus(i) ? 'X' : '.')}] " +
                    $"| {taskList.GetTask(i),-TASK_MAX_LENGTH} |\n"));
            }
            Console.WriteLine("+---+-----+------------------------------------------+\n");
        }

        /// <summary>
        /// Метод запрашивает у пользователя целое число.
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="min">минимальное значение ввода</param>
        /// <param name="max">максимальное значение ввода</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int IntNumberInput(string message, int min, int max)
        {
            bool check = false; //флаг проверки
            int input = 0;
            Console.WriteLine($"{message} (от {min} до {max})");
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
