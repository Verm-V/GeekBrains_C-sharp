using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_04
{
    class Program
    {
        ///<summary> имя файла для записи </summary>
        const string filename = "FileList.txt";

        static void Main(string[] args)
        {

            string path = PathInput("Пожалуйста, введите путь к каталогу,\nсписок файлов из которого нужно получить:");
            string seek = string.Empty;//сюда запишется результат просмотра заданного каталога

            SeekDirectory(path, ref seek);
            Console.WriteLine(seek);
            File.WriteAllText(filename, seek);

            MessageWaitKey("---------");

        }

        /// <summary>
        /// рекурсивно просматривает заданный каталог на наличие каталогов и файлов
        /// </summary>
        /// <param name="path">путь к просматриваемому каталогу</param>
        /// <param name="seek">сюда записывается все найденное</param>
        private static void SeekDirectory(string path, ref string seek)
        {
            string[] allDirs = Directory.GetDirectories(path);
            foreach (string dirName in allDirs)
            {
                seek = $"{seek}\n{dirName}";
                //Console.WriteLine(dirName);
                SeekDirectory(dirName, ref seek);
            }

            string[] allFiles = Directory.GetFiles(path);
            foreach (string fileName in allFiles)
            {
                seek = $"{seek}\n{fileName}";
                //Console.WriteLine(fileName);
            }
        }

        /// <summary>
        /// Метод запрашивает путь к каталогу
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <returns>введенную пользователем строку</returns>
        private static string PathInput(string message)
        {
            bool check = false; //флаг проверки
            string input = string.Empty;
            while (!check) //цикл будет повторятся, пока вводимая строка не пройдет все проверки
            {
                Console.WriteLine(message);
                //ввод и проверка на то, длину
                input = Console.ReadLine();
                if (Directory.Exists(input))
                {
                    check = true;
                }
                else
                {
                    Console.WriteLine("Такого пути не существует. Повторите ввод.");
                    check = false;
                }
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
