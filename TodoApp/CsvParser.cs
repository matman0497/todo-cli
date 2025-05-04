using System.Text;

namespace TodoApp
{
    internal static class CsvParser
    {
        public static List<string> Parse(string line)
        {
            List<string> columns = [];
            var builder = new StringBuilder();
            foreach (var c in line)
            {
                if (c == ',')
                {
                    columns.Add(builder.ToString());
                    builder.Clear();
                    continue;
                }

                builder.Append(c);
            }

            columns.Add(builder.ToString());

            return columns;
        }

        public static string CreateLine(string[] columns)
        {
            var builder = new StringBuilder();

            var first = true;
            foreach (var column in columns)
            {
                if (first == false)
                {
                    builder.Append(',');
                }

                builder.Append(column);
                first = false;
            }

            return builder.ToString();
        }
    }
}