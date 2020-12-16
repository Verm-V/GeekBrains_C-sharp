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
        private static string[,] phonebook = new string[5, 2];
        /// <summary> пункты главного меню, последний пункт выход из программы </summary>
        private static string[] mainMenu = new string[3] { "Добавить запись", "Удалить запсь", "Выход" };

        static void Main(string[] args)
        {
            //формируем сообщение главного меню справочника
            string mainMenuMessage = "Выберите опцию:\n";
            for (int i = 0; i < mainMenu.Length; i++)
            {
                mainMenuMessage += $"{i + 1} - {mainMenu[i]}\n";
            }

            int option = 0; //переменная содержащая выбранный пользователем пункт главного меню

            while(option != mainMenu.Length)
            {
                Console.Clear();
                PhonebookOutput();
                option = IntNumberInput(mainMenuMessage, 1, mainMenu.Length);
            }


            Console.WriteLine($"--------\n");
            Console.WriteLine("\nНажмите любую клавишу");
            Console.ReadKey();



        }

        private static void PhonebookOutput()
        {
            Console.WriteLine("Содержимое справочника");
            Console.WriteLine("+---+----------------------+----------------------+");
            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                Console.Write(string.Format("| {0,1:d} | {1,20:s} | {2,20:s} |\n", i+1, phonebook[i,0], phonebook[i, 1]));
            }
            Console.WriteLine("+---+----------------------+----------------------+\n");
        }

        /// </summary>
        /// Метод запрашивает у пользователя целое число.
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="min">минимальное значение ввода</param>
        /// <param name="max">максимальное значение ввода</param>
        /// <returns>Введенное пользователем целое число больше нуля.</returns>
        private static int IntNumberInput(string message, int min, int max)
        {
            bool check = false; //флаг проверки
            int number = 0;
            while (!check) //цикл будет повторятся, пока вводимое число не пройдет все проверки
            {
                Console.WriteLine($"{message} (от {min} до {max})");
                //ввод и проверка на то, что это целове число и оно входит в заданные границы
                check = int.TryParse(Console.ReadLine(), out number);
                if (check && (number < min || number > max))
                    check = false;
            }
            return number;
        }


    }


}
