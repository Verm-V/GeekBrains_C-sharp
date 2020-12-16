using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_02
{
    class Program
    {

        /// <summary> первый элемент хранит имя контакта, второй — номер телефона/e-mail </summary>
        private static string[,] phonebook = new string[5, 2]
        {
            {"Иван Иваныч", "12-34-56" },
            {"Петр Петрович", "+7-999-888-6644" },
            {" ", " " },
            {"Василий Васильевич", "ящик@почта.ру" },
            {" ", " " }
        };
        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[3] { "Добавить/изменить запись", "Удалить запись", "Выход" };

        static void Main(string[] args)
        {
            //формируем сообщение главного меню справочника
            string mainMenuMessage = "Выберите опцию:\n";
            for (int i = 0; i < mainMenu.Length; i++)
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";

            bool isExit = false; //если true, то происходит выход из основного цикла

            //основной рабочий цикл
            while(!isExit)
            {
                Console.Clear();
                PhonebookOutput();
                int number; //для ввода номера записи в таблице
                switch (IntNumberInput(mainMenuMessage, 1, mainMenu.Length))
                {
                    case 1: //добавить или изменить запись
                        number = IntNumberInput("\nВведите номер записи", 1, 5) - 1;
                        phonebook[number, 0] = StringInput("Ведите имя контакта", 30);
                        phonebook[number, 1] = StringInput("Ведите телефон или email контакта", 30);
                        break;
                    case 2: //удаление записи
                        number = IntNumberInput("\nВведите номер записи", 1, 5) - 1;
                        phonebook[number, 0] = string.Empty;
                        phonebook[number, 1] = string.Empty;
                        break;
                    case 3: //выход
                        Console.WriteLine("До новых встреч.");
                        isExit = true;
                        break;
                }

            }


            Console.WriteLine($"--------\n");
            Console.WriteLine("\nНажмите любую клавишу");
            Console.ReadKey();



        }

        /// <summary>
        /// выводит на экран содержимое справочника
        /// </summary>
        private static void PhonebookOutput()
        {
            Console.WriteLine("Содержимое справочника");
            Console.WriteLine("+---+--------------------------------+--------------------------------+");
            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                Console.Write(string.Format("| {0,1:d} | {1,-30} | {2,-30} |\n", i+1, phonebook[i,0], phonebook[i, 1]));
            }
            Console.WriteLine("+---+--------------------------------+--------------------------------+\n");
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
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от {min} до {max})");
                //ввод и проверка на то, что это целове число и оно входит в заданные границы
                check = int.TryParse(Console.ReadLine(), out input);
                if (check && (input < min || input > max))
                    check = false;
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
