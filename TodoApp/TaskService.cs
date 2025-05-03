using System.Reflection.Metadata.Ecma335;

namespace TodoApp
{
    class TaskService
    {
        public static List<Task> GetTodoItems()
        {

            List<Task> items = new List<Task>();

            StreamReader streamReader = new StreamReader("/Users/matman/DotNet/01/TodoApp/tasks.csv");
            string? line = streamReader.ReadLine();

            int lineCount = 0;
            while (line != null)
            {
                var columns = CsvParser.Parse(line);
                Task task = new() { Name = columns.ElementAt(0), Key = lineCount, Description = columns.ElementAt(1), Status = Enum.Parse<Status>(columns.ElementAt(2)) };
                items.Add(task);

                line = streamReader.ReadLine();
                lineCount++;
            }

            return items;
        }

        public static Task CreateTask(string title, string description)
        {
            var task = new Task() { Name = title, Key = 0, Description = description, Status = Status.Scheduled };

            var line = CsvParser.CreateLine([task.Name, task.Description, task.Status.ToString()]);

            StreamWriter streamWriter = new StreamWriter("/Users/matman/DotNet/01/TodoApp/tasks.csv", true);
            streamWriter.WriteLine(line);
            streamWriter.Close();

            return task;
        }
    }
}