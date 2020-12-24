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
        ///<summary> имя файла для записи поиска с рекурсией</summary>
        const string filenameRecursion = "FileListRecursion.txt";
        ///<summary> имя файла для записи поиска без рекурсии</summary>
        const string filenameNoRecursion = "FileListNoRecuersion.txt";

        static void Main(string[] args)
        {

            string path = PathInput("Пожалуйста, введите путь к каталогу,\nсписок файлов из которого нужно получить:");
            string seek = string.Empty;//сюда запишется результат просмотра заданного каталога

            //поиск рекурсией
            SeekDirectoryRecursion(path, ref seek);
            Console.WriteLine("Список полученный с помощью рекурсии");
            Console.WriteLine(seek);
            File.WriteAllText(filenameRecursion, seek);

            //поиск без рекурсии
            seek = string.Empty;
            seek = SeekDirectoryNoRecursion(path);
            Console.WriteLine("Список полученный без рекурсии");
            Console.WriteLine(seek);
            File.WriteAllText(filenameNoRecursion, seek);


            MessageWaitKey("---------");

        }

        /// <summary>
        /// рекурсивно просматривает заданный каталог на наличие каталогов и файлов
        /// </summary>
        /// <param name="path">путь к просматриваемому каталогу</param>
        /// <param name="seek">сюда записывается все найденное</param>
        private static void SeekDirectoryRecursion(string path, ref string seek)
        {
            string[] allDirs = Directory.GetDirectories(path);
            foreach (string dirName in allDirs)
            {
                seek += $"{dirName}\n";
                //Console.WriteLine(dirName);
                SeekDirectoryRecursion(dirName, ref seek);
            }

            string[] allFiles = Directory.GetFiles(path);
            foreach (string fileName in allFiles)
            {
                seek += $"{fileName}\n";
                //Console.WriteLine(fileName);
            }
        }

        /// <summary>
        /// выдает полный список каталогов и файлов в заданном каталоге без использования рекурсии
        /// </summary>
        /// <param name="path">путь к просматриваемому каталогу</param>
        /// <returns>строку содержащую все найденные файлы и каталоги</returns>
        private static string SeekDirectoryNoRecursion(string path)
        {
            string seek = string.Empty;
            string[] allDirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            
            foreach (string dirName in allDirs)
            {
                seek += $"{dirName}\n";
                //Console.WriteLine(dirName);
            }

            string[] allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (string fileName in allFiles)
            {
                seek += $"{fileName}\n";
                //Console.WriteLine(fileName);
            }
            return seek;
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
