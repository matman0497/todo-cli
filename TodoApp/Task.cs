namespace TodoApp
{
    internal class Task
    {
        public required string Name { get; set; }
        public required int Key { get; set; }
        public required Status Status { get; set; }
        public required string Description { get; set; }

        public DateTime? Due { get; set; }

        public override string ToString()
        {
            var toString = $"{Key} - {Name} ({Enum.GetName(typeof(Status), Status)}): {Description} ";

            if (Due.HasValue)
            {
                toString += "Due: " + Due;
            }

            return toString;
        }
    }
}