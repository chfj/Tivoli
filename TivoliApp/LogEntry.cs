namespace TivoliApp
{
    public class LogEntry
    {
        public LogEntry(string logLine)
        {
            LogLine = logLine;

            var lineSplit = logLine.Split(';');

            if (lineSplit?.Length == 5)
            {
                CustomerContactId = lineSplit[0];
                StatusCode = lineSplit[1];
                StatusName = lineSplit[2];
                Details = lineSplit[3];

                double timeSpent;
                double.TryParse(lineSplit[4].Replace("\"", ""), out timeSpent);
                MillisecondsSpent = timeSpent;

                if (Details.Contains("with a balance of "))
                {
                    var detailsSplit = Details.Split("with a balance of ");
                    double balance;
                    double.TryParse(detailsSplit[1].Split(" ")[0], out balance);
                    Balance = balance;
                }

                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }

        public string CustomerContactId { get; }
        public string StatusCode { get; }
        public string StatusName { get; }
        public string Details { get; }
        public double MillisecondsSpent { get; }
        public double Balance { get; }
        public string LogLine { get; }
        public bool IsValid { get; }
    }
}