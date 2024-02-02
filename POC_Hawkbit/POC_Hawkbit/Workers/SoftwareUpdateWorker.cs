using POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

namespace POC_Hawkbit.Workers;

public class SoftwareUpdateWorker(IHawkbitClient hawkbitClient): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var controller = await hawkbitClient.GetRequiredUpdateAsync(stoppingToken);

            if (controller == null)
            {
                await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken);
                continue;
            }

            if (controller.Links.DeploymentBase != null)
            {
                await PerformSoftwareUpdateAsync(controller.Links.DeploymentBase.Href, stoppingToken);
            }

            await Task.Delay(controller.Config.Polling.Sleep, stoppingToken);
        }
    }
    
    private async Task PerformSoftwareUpdateAsync(string url, CancellationToken cancellationToken)
    {
        var request = await hawkbitClient.GetAsync<DeploymentRequest>(url, cancellationToken);

        if (request is null)
            return;
        
        foreach (var VARIABLE in request.Deployment.Chunks)
        {
            
        }
    }
}