namespace TodoApp
{
    class TaskService(string path)
    {
        private List<Task> _items;

        public List<Task> GetTodoItems()
        {
            var items = new List<Task>();

            var streamReader = new StreamReader(path);
            var line = streamReader.ReadLine();

            var lineCount = 0;
            while (line != null)
            {
                var columns = CsvParser.Parse(line);
                Task task = new()
                {
                    Name = columns.ElementAt(0), Key = lineCount, Description = columns.ElementAt(1),
                    Status = Enum.Parse<Status>(columns.ElementAt(2)),
                };

                if (columns.Count > 3)
                {
                    task.Due = columns.ElementAt(3) != string.Empty ? DateTime.Parse(columns.ElementAt(3)) : null;
                }

                items.Add(task);

                line = streamReader.ReadLine();
                lineCount++;
            }

            _items = items;

            return items;
        }

        public Task CreateTask(string title, string description, DateTime? due = null)
        {
            var task = new Task()
                { Name = title, Key = 0, Description = description, Status = Status.Scheduled, Due = due };

            CsvParser.CreateLine([
                task.Name, task.Description, task.Status.ToString(), due.ToString() ?? string.Empty
            ]);


            _items.Add(task);

            return task;
        }

        public List<Task> DeleteTask(int position)
        {
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
            File.Delete(path);

            var lines = _items.Select(task =>
                CsvParser.CreateLine([
                    task.Name, task.Description, task.Status.ToString(), task.Due.ToString() ?? string.Empty
                ]));

            StreamWriter streamWriter = new StreamWriter(path, true);

            foreach (var line in lines)
            {
                streamWriter.WriteLine(line);
            }

            streamWriter.Close();
        }
    }
}