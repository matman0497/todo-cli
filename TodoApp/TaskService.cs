using System.Reflection.Metadata.Ecma335;

namespace TodoApp
{
    class TaskService(string path)
    {

        private string _path = path;
        private List<Task> _items;

        public List<Task> GetTodoItems()
        {
            List<Task> items = new List<Task>();

            StreamReader streamReader = new StreamReader(this._path);
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

            _items = items;

            return items;
        }

        public Task CreateTask(string title, string description)
        {
            var task = new Task() { Name = title, Key = 0, Description = description, Status = Status.Scheduled };

            var line = CsvParser.CreateLine([task.Name, task.Description, task.Status.ToString()]);

            StreamWriter streamWriter = new StreamWriter(this._path, true);
            streamWriter.WriteLine(line);
            streamWriter.Close();

            return task;
        }

        public List<Task> DeleteTask(int position) {
            _items.RemoveAt(position);
            
            return _items;
        }

        public Task ChangeStatus(Task task, Status to)
        {
            task.Status = to;

            return task;
        }

        public void Save()
        {
            File.Delete(_path);

            var lines = _items.Select(task => CsvParser.CreateLine([task.Name, task.Description, task.Status.ToString()]));

            StreamWriter streamWriter = new StreamWriter(this._path, true);

            foreach(var line in lines) {
                streamWriter.WriteLine(line);
            }

            streamWriter.Close();
        }
    }
}