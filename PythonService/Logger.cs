using System;
using System.IO;

public class Logger
{
    // Caminho do diretório onde o executável está localizado
    private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    // Caminho completo do arquivo de log dentro do diretório de execução
    private static readonly string LogFilePath = Path.Combine(BaseDirectory, "log.txt");

    public static void WriteLog(string message = "")
    {
        try
        {
            // Garante que o diretório existe (o diretório base já deve existir, mas é bom garantir)
            Directory.CreateDirectory(BaseDirectory);

            // Mensagem de log com data e hora
            string logMessage = $"Log registrado em: {DateTime.Now:G}";

            // Adiciona o log ao arquivo
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);

            File.AppendAllText(LogFilePath, message + Environment.NewLine);
            

            Console.WriteLine("Log gravado com sucesso em: " + LogFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gravar o log: {ex.Message}");
        }
    }
}
