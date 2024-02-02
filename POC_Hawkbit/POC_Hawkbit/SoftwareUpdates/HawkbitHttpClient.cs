using System.Net.Http.Json;
using POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

namespace POC_Hawkbit.SoftwareUpdates;

public interface IHawkbitClient
{
    Task<Controller?> GetRequiredUpdate(CancellationToken cancellationToken);
    Task<string> GetDeployment(string url, CancellationToken cancellationToken);
}

public class HawkbitHttpClient(IHttpClientFactory httpClientFactory) : IHawkbitClient
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(HttpClients.HawkHttpClient);

    public async Task<Controller?> GetRequiredUpdate(CancellationToken cancellationToken)
    {
        const string url = "/default/controller/v1/gate1";
        return await _client.GetFromJsonAsync<Controller>(url, cancellationToken);
    }

    public async Task<string> GetDeployment(string url, CancellationToken cancellationToken)
    {
        var result = await _client.GetAsync(url, cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}