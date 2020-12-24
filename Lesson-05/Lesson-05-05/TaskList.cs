using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lesson_05_05
{
    /// <summary> список заданий пользователя </summary>
    class TaskList
    {
        /// <summary> список заданий </summary>
        private List<ToDo> taskList;
        /// <summary> количество элементов в списке заданий </summary>
        public int Count
        {
            get
            {
                return taskList.Count;
            }
        }

        public TaskList()
        {
            taskList = new List<ToDo> { };
        }

        /// <summary> добавляет задачу в список</summary>
        /// <param name="number">содержание задачи</param>
        public void AddItem(string item)
        {
            taskList.Add(new ToDo(item));
        }

        /// <summary> удаляет задачу из списка</summary>
        /// <param name="number">номер задачи в списке</param>
        public void DeleteItem(int number)
        {
            if (number >= 0 && number < taskList.Count())
                taskList.RemoveAt(number);
        }

        /// <summary> меняет статус задачи</summary>
        /// <param name="number">номер задачи в списке</param>
        public void ChangeStatus(int number)
        {
            if (number >= 0 && number < taskList.Count())
                taskList[number].IsDone = !taskList[number].IsDone;
        }

        /// <summary> меняет статус задачи</summary>
        /// <param name="number">номер задачи в списке</param>
        /// <param name="status">устанавливаемый статус</param>
        public void SetStatus(int number, bool status)
        {
            if (number >= 0 && number < taskList.Count())
                taskList[number].IsDone = status;
        }


        /// <summary> возвращает содержание задачи </summary>
        /// <param name="number">номер задачи</param>
        /// <returns> содержание задачи, либо пустая строка если задачи не существует</returns>
        public string GetTask(int number)
        {
            if (number >= 0 && number < taskList.Count())
                return taskList[number].Title;
            else return String.Empty;
        }

        /// <summary> возвращает статус задачи </summary>
        /// <param name="number">номер задачи</param>
        /// <returns> статус задачи, либо false если задачи не существует</returns>
        public bool GetStatus(int number)
        {
            if (number >= 0 && number < taskList.Count())
                return taskList[number].IsDone;
            else return false;
        }

        /// <summary> возвращает полную задачу </summary>
        /// <param name="number">номер задачи</param>
        /// <returns> полную задачу, либо пустую если задачи не существует</returns>
        public ToDo GetFullTask(int number)
        {
            if (number >= 0 && number < taskList.Count())
                return taskList[number];
            else return new ToDo();
        }

        /// <summary> Сериализация списка задач в JSON формат </summary>
        /// <returns>строку содержащую список задач в формате JSON</returns>
        public string SerializeJSON()
        {
            return JsonSerializer.Serialize(taskList);
        }

        /// <summary> Десериализация списка задач из JSON формата </summary>
        /// <param name="json">строка содержащая список задач в формате JSON</param>
        public void DeserializeJSON(string json)
        {
            taskList = JsonSerializer.Deserialize<List<ToDo>>(json);
        }


    }
}
