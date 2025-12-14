namespace APICatalogo.Logging;

public class CustomLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel == loggerConfig.LogLevel;

    public IDisposable BeginScope<TState>(TState state) => null;

    public void Log<TState>(
        LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        var mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        var caminhoArquivoLog = @"C:\Users\Vini\Documents\logs\Kaio_Log.txt";
        using var writer = new StreamWriter(caminhoArquivoLog, true);

        try
        {
            writer.WriteLine(mensagem);
            writer.Close();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
