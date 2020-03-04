using System;
using System.Collections.Generic;
using System.IO;

namespace TivoliApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var logViewer = new LogViewer();
            logViewer.AnalyzeFile();
        }
    }
    public class LogViewer
    {
        private readonly IFileLineLoader fileLineLoader;
        public LogViewer() : this(new FileLineLoader())
        {

        }

        public LogViewer(IFileLineLoader fileLineLoader)
        {
            this.fileLineLoader = fileLineLoader;
        }

        public void AnalyzeFile()
        {
            var allLogLines = fileLineLoader.Load();

            var rowsByStatusCode = "";

            

            //Hvor mange rækker er der af hver statuskode
            //Hvad er gennemsnittet af millisecods spent
            //Største og mindste værdi af millisecods spent
            //Hvad er den mindste balance, der er udestående på en ordre
        }

    }

    public interface IFileLineLoader
    {
        public IList<LogEntry> Load();
    }

    public class FileLineLoader : IFileLineLoader
    {
        public IList<LogEntry> Load()
        {
            var allLines = new List<LogEntry>();
            
            string line;
  
            StreamReader file = new StreamReader(@"errors.csv");
            while ((line = file.ReadLine()) != null)
            {
                allLines.Add(new LogEntry(line));
            }

            file.Close();
    
            return allLines;
        }
    }

    public class LogEntry
    {
        public string CustomerContactId { get; }
        public string StatusCode { get; }
        public string StatusName { get; }
        public string details { get; }
        public string MillisecondsSpent { get; }
        public string LogLine { get; }

        public LogEntry(string LogLine)
        {
            this.LogLine = LogLine;
        }

    }
}
