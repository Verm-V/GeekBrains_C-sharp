using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_05
{
    class Program
    {
        const string filename = "text.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Пожалуйста, введите любой набор символов:");
            string text = Console.ReadLine();

            File.WriteAllText(filename, text);

            Console.WriteLine("\n--------\npress any key\n");
            Console.ReadKey();

        }

    }
}
