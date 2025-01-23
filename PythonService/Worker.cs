using System.Diagnostics;
namespace PythonService;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private const string PythonExecutable = "C:\\Users\\Administrador\\AppData\\Local\\Programs\\Python\\Python313\\python.exe"; // Caminho do Python (ou "python3" no Linux/Mac)
    private readonly string _pythonScriptPath;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

        // Caminho absoluto para o script main.py
        _pythonScriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "main.py");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Serviço iniciado em: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Executando o script Python: {path}", _pythonScriptPath);

                Logger.WriteLog();

                Logger.WriteLog("Executou o Start...");

                // Configurar o processo para executar o Python
                var startInfo = new ProcessStartInfo
                {
                    FileName = PythonExecutable,
                    Arguments = $"\"{_pythonScriptPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    Logger.WriteLog("Executou o Start...");

                    // Ler a saída do script Python
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    Logger.WriteLog("Output o Start..." + output);
                    Logger.WriteLog("Output o Error..." + error);

                    if (!string.IsNullOrEmpty(output))
                        _logger.LogInformation("Saída do Python: {output}", output);

                    if (!string.IsNullOrEmpty(error))
                        _logger.LogError("Erro no Python: {error}", error);

                    await process.WaitForExitAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar o script Python.");
                Logger.WriteLog("Executou o Start..." + ex.StackTrace);
            }

            await Task.Delay(10000, stoppingToken); // Aguarda 10 segundos antes de executar novamente
        }

        _logger.LogInformation("Serviço está sendo encerrado.");
    }
}
