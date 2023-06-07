using tasks._0.Interfaces;

namespace tasks._0.Services;

public class LogService :ILogService
{
    private readonly string filePath;
    public LogService(IWebHostEnvironment web)
    {
        filePath = Path.Combine(web.ContentRootPath, "Logs", "_Tasks.log")      ;
    }
    public void Log(LogLevel level, string message)
    {
        using (var sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} [{level}] {message}");
          

        }

    }

}

