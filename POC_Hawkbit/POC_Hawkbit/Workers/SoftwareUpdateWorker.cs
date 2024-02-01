namespace POC_Hawkbit.Workers;

public class SoftwareUpdateWorker: BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            WriteLine("Polling...");
            await Task.Delay(2000, stoppingToken);
        }
    }
}