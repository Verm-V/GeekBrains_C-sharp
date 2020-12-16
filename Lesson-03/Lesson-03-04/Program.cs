using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_04
{
    class Program
    {
        /// <summary> игровое поле - true есть часть корабля, false - нет </summary>
        private static bool[,] field = new bool[10, 10];

        static void Main(string[] args)
        {
            Console.Clear();
            Random rnd = new Random();

            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    bool check = false;
                    while (!check)
                    {
                        //размер корабля 5-i
                        int size = 5 - i;
                        bool direction = rnd.Next(2) == 1 ? true : false;
                        int rowMax = direction ? 10 : 10 - size;
                        int columnMax = !direction ? 10 : 10 - size;
                        int row = rnd.Next(rowMax) + 1;
                        int column = rnd.Next(columnMax) + 1;
                        check = PlaceShip(size, direction, column, row);
                    }
                    System.Threading.Thread.Sleep(10);
                    FieldOutput();
                }
            }

            FieldOutput();

            Console.WriteLine($"\n--------");
            Console.WriteLine("\nНажмите любую клавишу");
            Console.ReadKey();

        }

        /// <summary>
        /// выводит на экран содержимое игрового поля
        /// </summary>
        private static void FieldOutput()
        {
            Console.Clear();
            Console.WriteLine("Игровое поле\n");
            Console.WriteLine("    ABCDEFGHIJ");
            for (int i = 0; i < field.GetLength(0); i++)
            {
                Console.Write("{0,2:d}  ", i+1);
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(string.Format($"{(field[i,j] ? 'X' : '.')}"));
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Размещает корабль на поле, проверка на границы не производится
        /// </summary>
        /// <param name="size">размер корабля</param>
        /// <param name="direction">true - горизонтально, false - вертикально</param>
        /// <param name="column">столбец (от 1 до 10)</param>
        /// <param name="row">cnhjrf (от 1 до 10)</param>
        /// <returns>true если корабль удалось разместить.</returns>
        private static bool PlaceShip(int size, bool direction, int startColumn, int startRow)
        {
            bool check = true;

            startRow--;
            startColumn--;

            //проверка на наличие кораблей в месте размещения
            int row = startRow;
            int column = startColumn;

            int rowUp = startRow > 0 ? startRow - 1 : startRow;
            int columnLeft = startColumn > 0 ? startColumn - 1 : startColumn;
            int rowDown = rowUp + 1 + (direction ? 1 : size);
            int columnRight = columnLeft + 1 + (!direction ? 1 : size);
            rowDown = rowDown < 10 ? rowDown : 9;
            columnRight = columnRight < 10 ? columnRight : 9;

            for (int r = rowUp; r <= rowDown; r++)
                for (int c = columnLeft; c <= columnRight; c++)
                    if (field[r, c]) check = false;

            //размещение если проверка прошла
            if (check)
            {
                row = startRow;
                column = startColumn;
                for (int i = 0; i < size; i++)
                {
                    field[row, column] = true;
                    //вот это круто, не знал раньше про пустые переменные
                    _ = direction ? column++ : row++;
                }
            }
            return check;
        }

    }
}
