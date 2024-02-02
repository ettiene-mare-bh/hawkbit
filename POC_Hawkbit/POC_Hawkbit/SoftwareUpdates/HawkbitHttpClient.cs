using System.Net.Http.Json;
using POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

namespace POC_Hawkbit.SoftwareUpdates;

public interface IHawkbitClient
{
    Task<Controller?> GetRequiredUpdateAsync(CancellationToken cancellationToken);
    Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken);

    Task<byte[]> DownloadFileAsync(string url, CancellationToken cancellationToken);
}

public class HawkbitHttpClient(IHttpClientFactory httpClientFactory) : IHawkbitClient
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(HttpClients.HawkHttpClient);

    public async Task<Controller?> GetRequiredUpdateAsync(CancellationToken cancellationToken)
    {
        const string url = "/default/controller/v1/gate1";
        return await _client.GetFromJsonAsync<Controller>(url, cancellationToken).ConfigureAwait(false);
    }

    public Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken) =>
        _client.GetFromJsonAsync<T>(url, cancellationToken);

    public Task<byte[]> DownloadFileAsync(string url, CancellationToken cancellationToken) =>
        _client.GetByteArrayAsync(url, cancellationToken);
}