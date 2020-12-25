using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_02
{
    class Program
    {

        const string filename = "time.txt";

        static void Main(string[] args)
        {
            string startingTime = DateTime.Now.ToString("T");
            //если файл уже существует, то записываем в него перенос строки
            if (File.Exists(filename)) File.AppendAllText(filename, Environment.NewLine);
            File.AppendAllText(filename, $"Program starting time: {startingTime}");
        }


    }
}
