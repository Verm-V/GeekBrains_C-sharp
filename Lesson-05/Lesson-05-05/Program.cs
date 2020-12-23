using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_05
{
    class Program
    {
        /// <summary> имя файла для записи </summary>
        const string filename = "ToDoList.json";
        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[4] 
        { "Добавить задание", "Удалить задание", "Изменить статус задания", "Выход" };

        static void Main(string[] args)
        {
            //формируем сообщение главного меню справочника
            string mainMenuMessage = "Выберите опцию:\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";

            //отладочный массив
            ToDo[] taskList = new ToDo[3];
            taskList[0] = new ToDo("Выполнить домашнее задание");
            taskList[1] = new ToDo("Закоммитить изменения");
            taskList[2] = new ToDo("Сдать домашнее задание");
            taskList[0].IsDone = true;
            //--

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
                        break;
                    case 2: //удаление задания
                        break;
                    case 3: //изменение статуса задания
                        number = IntNumberInput("Введите номер задания", 1, taskList.Length + 1) - 1;
                        taskList[number].IsDone = !taskList[number].IsDone;
                        break;
                    case 4: //выход
                        isExit = true;
                        break;
                }

            }



            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();

        }


        /// <summary>
        /// выводит на экран содержимое списка заданий
        /// </summary>
        private static void ListOutput(ToDo [] taskList)
        {
            Console.Clear();
            Console.WriteLine("Список заданий");
            Console.WriteLine("+---+-----+------------------------------------------+");
            for (int i = 0; i < taskList.Length; i++)
            {
                Console.Write(string.Format(
                    "| {0,1:d} | [{1,-1}] | {2,-40} |\n", 
                    i + 1,
                    (taskList[i].IsDone) ? 'X' : '.',
                    taskList[i].Title));
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
    }
}
