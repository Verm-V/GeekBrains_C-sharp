using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите имя пользователя: ");
            string userName = Console.ReadLine();
            //способ форматирования даты/времени подсмотрел на MSDN
            //вынес в отдельную переменную, чтобы не удлинять WritelLine
            string todayDate = DateTime.Now.ToString("D", new System.Globalization.CultureInfo("ru-Ru"));
            Console.WriteLine($"Приветствую, {userName}, сегодня {todayDate}\n");
        }
    }
}
