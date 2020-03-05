using System.Collections.Generic;

namespace TivoliApp
{
    public interface IFileLineLoader
    {
        IList<LogEntry> Load();
    }
}