using System.Linq.Expressions;
using POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

namespace POC_Hawkbit.Workers;

public class SoftwareUpdateWorker(IHawkbitClient hawkbitClient): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var controller = await hawkbitClient.GetRequiredUpdateAsync(stoppingToken).ConfigureAwait(false);

            if (controller == null)
            {
                await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken).ConfigureAwait(false);
                continue;
            }

            if (controller.Links.DeploymentBase != null)
            {
                await PerformSoftwareUpdateAsync(controller.Links.DeploymentBase.Href, stoppingToken).ConfigureAwait(false);
            }

            await Task.Delay(controller.Config.Polling.Sleep, stoppingToken).ConfigureAwait(false);
        }
    }
    
    private async Task PerformSoftwareUpdateAsync(string url, CancellationToken cancellationToken)
    {
        var request = await hawkbitClient.GetAsync<DeploymentRequest>(url, cancellationToken).ConfigureAwait(false);

        if (request is null)
            return;

        await DownloadFilesAsync(request.Deployment, cancellationToken).ConfigureAwait(false);
        await Task.Delay(10_000, cancellationToken).ConfigureAwait(false);
        await UpdateStatusAsync(request.Id, cancellationToken).ConfigureAwait(false);
    }

    private async Task DownloadFilesAsync(Deployment deployment, CancellationToken cancellationToken)
    {
        var downloadTasks = deployment.Chunks
                                                        .SelectMany(chunk => chunk.Artifacts)
                                                        .Select(a => DownloadArtifact(a, cancellationToken));

        await Task.WhenAll(downloadTasks).ConfigureAwait(false);
    }

    private async Task<bool> DownloadArtifact(Artifact artifact, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken);

        var file = await hawkbitClient.DownloadFileAsync(artifact.Links.Download.Href, cancellationToken)
                                           .ConfigureAwait(false);

        var folder = $@"C:\SVC\software-update\{DateTime.UtcNow:dd-MMM-yyyy HH-mm-ss-fff}";

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        
        var path = $@"{folder}\{artifact.Filename}";
        await File.WriteAllBytesAsync(path, file, cancellationToken).ConfigureAwait(false);

        return true;
    }

    private async Task UpdateStatusAsync(int id, CancellationToken cancellationToken)
    {
        var response = await hawkbitClient.UpdateStatusAsync(id, cancellationToken).ConfigureAwait(false);
        await Task.Delay(2000, cancellationToken);
    }
}