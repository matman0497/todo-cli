namespace TodoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Todo App v0.1 by Mattia");

            Console.WriteLine("Choose you action");
            Console.WriteLine("1) Show all Todo's");
            Console.WriteLine("2) Show all completed Todo's");
            Console.WriteLine("3) Show all open Todo's");
            Console.WriteLine("4) Show Todo");
            Console.WriteLine("5) Create a new Todo");

            var key = Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("");

            var taskService = new TaskService("/Users/matman/DotNet/01/TodoApp/tasks.csv");

            var todos = taskService.GetTodoItems();

            if (key.KeyChar == '1')
            {
                foreach (var item in todos)
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '2')
            {
                foreach (var item in todos.Where(n => n.Status == Status.Done))
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '3')
            {
                foreach (var item in todos.Where(n => n.Status == Status.Progress || n.Status == Status.Scheduled))
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '4')
            {
                Console.Write("Enter ID of the TODO item: ");
                var id = Console.ReadKey();
                Console.WriteLine("");

                var todo = todos.ElementAt(int.Parse(id.KeyChar.ToString()));


                Console.WriteLine("Title: " + todo.Name);

                Console.WriteLine("");
                Console.WriteLine("(d) Delete   (c) Complete    (x) Exit");
                var action = Console.ReadKey();

                HandleTodoAction(action.KeyChar, todo, taskService);
            }

            if (key.KeyChar == '5')
            {
                Console.Write("Enter a title: ");
                var title = Console.ReadLine();
                Console.Write("Enter a description: ");
                var description = Console.ReadLine();

                if (title == null || description == null)
                {
                    Console.Write("Title and Description is required");

                    return;
                }

                taskService.CreateTask(title, description);
            }

            taskService.Save();
        }

        private static void HandleTodoAction(char action, Task task, TaskService taskService)
        {
            switch (action)
            {
                case 'd':
                {
                    taskService.DeleteTask(task.Key);

                    break;
                }
                case 'c':
                {
                    taskService.ChangeStatus(task, Status.Done);

                    break;
                }
            }
        }
    }
}