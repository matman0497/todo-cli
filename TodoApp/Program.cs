using System;

namespace TodoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Todo App v0.1 by Mattia");

            ConsoleKeyInfo key;

            Console.WriteLine("Choose you action");
            Console.WriteLine("1) Show all Todo's");
            Console.WriteLine("2) Show all completed Todo's");
            Console.WriteLine("3) Show all open Todo's");
            Console.WriteLine("4) Create a new Todo");

            key = Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("");

            var todos = TaskService.GetTodoItems();

            if (key.KeyChar == '1')
            {
                foreach (Task item in todos)
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '2')
            {
                foreach (Task item in todos.Where(n => n.Status == Status.Done))
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '3')
            {
                foreach (Task item in todos.Where(n => n.Status == Status.Pending || n.Status == Status.Scheduled))
                {
                    Console.WriteLine(item);
                }
            }

            if (key.KeyChar == '4')
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

                TaskService.CreateTask(title, description);
            }



        }
    }
}