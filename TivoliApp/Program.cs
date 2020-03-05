using System;

namespace TivoliApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Velkommen");

            var logViewer = new LogViewer();
            logViewer.AnalyzeFile();
        }
    }
}