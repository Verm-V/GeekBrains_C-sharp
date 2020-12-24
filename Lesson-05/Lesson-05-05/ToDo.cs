using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_05
{
    public class ToDo
    {
        /// <summary> Описание задания </summary>
        public string Title { get; set; }
        /// <summary> Отметка о выполнении </summary>
        public bool IsDone { get; set; }

        public ToDo()
        {
            Title = ".";
            IsDone = false;
        }


        public ToDo(string title)
        {
            Title = title;
            IsDone = false;
        }

    }
}
