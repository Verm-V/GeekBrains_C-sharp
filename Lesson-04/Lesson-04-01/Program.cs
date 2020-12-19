using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_04_01
{
    class Program
    {
        /// <summary> количество запросов ФИО </summary>
        const int MAX = 5;

        static void Main(string[] args)
        {
            string lastName;
            string firstName;
            string patronimic;
            string fullName;

            for (int i = 0; i < MAX; i++)
            {
                lastName = StringInput("Введите фамилию: ");
                firstName = StringInput("Введите имя: ");
                patronimic = StringInput("Введите отчество: ");
                fullName = GetFullName(lastName, firstName, patronimic);
                Console.WriteLine($"ФИО: {fullName}\n");
            }

            Console.WriteLine("--------\npress any key\n");
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

        /// <summary>
        /// Объединяет вместе три строки: имя, фамилию и отчество
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="patronymic">отчество</param>
        /// <returns>полное Фамилию Имя Отчество</returns>
        private static string GetFullName(string lastName, string firstName, string patronymic)
        {
            string fullName = lastName + " " + firstName + " " + patronymic; ;
            return fullName;
        }


    }
}
