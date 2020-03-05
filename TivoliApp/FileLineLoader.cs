using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TivoliApp
{
    public class FileLineLoader : IFileLineLoader
    {
        public IList<LogEntry> Load()
        {
            var allEntries = new List<LogEntry>();
            var allLines = new List<string>();

            string line;

            var file = new StreamReader(@"errors.csv");
            var sb = new StringBuilder();
            while ((line = file.ReadLine()) != null)
            {
                allLines.Add(line);
            }

            file.Close();

            //Remove header
            allLines.RemoveAt(0);

            var index = 1;

            foreach (var currentLine in allLines)
            {
                sb.Append($"{currentLine} ");

                if (index % 3 == 0)
                {
                    allEntries.Add(new LogEntry(sb.ToString()));
                    sb.Clear();
                }

                index++;
            }

            return allEntries;
        }
    }
}