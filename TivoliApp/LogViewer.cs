using System;
using System.Linq;

namespace TivoliApp
{
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
}