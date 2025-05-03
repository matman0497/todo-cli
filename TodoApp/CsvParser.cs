using System.Text;

namespace TodoApp {
    class CsvParser {
        public static List<string> Parse(string line) {

            List<string> columns = new();
            var builder = new StringBuilder();
            foreach(char c in line.ToCharArray()) {
                if (c == ',') {
                    columns.Add(builder.ToString());
                    builder.Clear();
                    continue;
                }

                builder.Append(c);
            }

            columns.Add(builder.ToString());

            return columns;

        }

        public static string CreateLine(string[] columns) {
            var builder = new StringBuilder();

            bool first = true;
            foreach(string column in columns) {

                if (first == false) {
                    builder.Append(',');    
                }
                builder.Append(column);
                first = false;
            }

            return builder.ToString();
        }
    }
}