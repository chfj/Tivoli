using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            foreach (var logEntry in allLogLines.Where(x => x.IsValid))
            {
                Console.WriteLine(logEntry.StatusCode);
            }

            //Hvor mange rækker er der af hver statuskode
            //Hvad er gennemsnittet af millisecods spent
            //Største og mindste værdi af millisecods spent
            //Hvad er den mindste balance, der er udestående på en ordre

            Console.WriteLine("Her er resultaterne:");
            Console.WriteLine($"Antal linjer i fil:                     {allLogLines.Count()}");
            Console.WriteLine($"Antal linier der kunne analyseres:       {allLogLines.Where(x => x.IsValid).Count()}");
            Console.WriteLine($"Antal linier der ikke kunne analyseres:  {allLogLines.Where(x => !x.IsValid).Count()}");

            var allStatus = allLogLines.GroupBy(x => x.StatusCode)
                   .Select(y => y.First())
                   .ToList();

            foreach (var status in allStatus)
            {
                Console.WriteLine($"Antal linier med status {status.StatusCode} ({status.StatusName}) er: {allLogLines.Where(x => x.StatusCode == status.StatusCode).Count()}");
            }

            Console.WriteLine($"gennemsnittet af millisecods brugt {allLogLines.Where(x => x.IsValid).Select(y => y.MillisecondsSpent).Average()}");
            Console.WriteLine($"Største og mindste værdi af millisecods spent Størst: {allLogLines.Where(x => x.IsValid).Select(y => y.MillisecondsSpent).Max()} Mindst: {allLogLines.Where(x => x.IsValid).Select(y => y.MillisecondsSpent).Min()}");
            Console.WriteLine($"mindste balance, der er udestående på en ordre: {allLogLines.Where(x => x.IsValid).Select(y => y.Balance).Min()}");
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
            var allEntries = new List<LogEntry>();

            var allLines = new List<string>();

            string line;

            StreamReader file = new StreamReader(@"errors.csv");
            StringBuilder sb = new StringBuilder();
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
            // sb.Append(line);
            // if (line.EndsWith("\""))
            // {
            //     allEntries.Add(new LogEntry(sb.ToString()));
            //     sb.Clear();
            // }


            return allEntries;
        }
    }

    public class LogEntry
    {
        public string CustomerContactId { get; }
        public string StatusCode { get; }
        public string StatusName { get; }
        public string Details { get; }
        public double MillisecondsSpent { get; }

        public double Balance { get; }
        public string LogLine { get; }

        public bool IsValid { get; }

        public LogEntry(string logLine)
        {
            this.LogLine = logLine;

            var lineSplit = logLine.Split(';');

            if (lineSplit?.Length == 5)
            {
                this.CustomerContactId = lineSplit[0];
                this.StatusCode = lineSplit[1];
                this.StatusName = lineSplit[2];
                this.Details = lineSplit[3];

                Double timeSpent;
                Double.TryParse(lineSplit[4].Replace("\"", ""), out timeSpent);
                this.MillisecondsSpent = timeSpent;

                if (this.Details.Contains("with a balance of "))
                {
                    var detailsSplit = this.Details.Split("with a balance of ");
                    Double balance;
                    Double.TryParse(detailsSplit[1].Split(" ")[0], out balance);
                    this.Balance = balance;
                }

                this.IsValid = true;

            }
            else
            {
                this.IsValid = false;
            }
        }

    }
}
