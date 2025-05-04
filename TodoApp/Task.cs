namespace TodoApp
{
    internal class Task
    {
        public required string Name { get; set; }
        public required int Key { get; set; }
        public required Status Status { get; set; }
        public required string Description { get; set; }

        public override string ToString()
        {
            return Key + " - " + Name + " (" + Enum.GetName(typeof(Status), Status) + "): " + Description;
        }
    }
}