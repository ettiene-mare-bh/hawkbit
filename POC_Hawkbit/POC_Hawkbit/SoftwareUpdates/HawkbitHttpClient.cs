using System.Net;
using System.Net.Http.Json;
using System.Text;
using POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

namespace POC_Hawkbit.SoftwareUpdates;

public interface IHawkbitClient
{
    Task<Controller?> GetRequiredUpdateAsync(CancellationToken cancellationToken);
    Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken);

    Task<byte[]> DownloadFileAsync(string url, CancellationToken cancellationToken);

    Task<bool> UpdateStatusAsync(int id, CancellationToken cancellationToken);
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

    public async Task<bool> UpdateStatusAsync(int id, CancellationToken cancellationToken)
    {
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffff");
        var url = $"/DEFAULT/controller/v1/gate1/deploymentBase/{id}/feedback";
        
        var request = $$"""
                      {
                        "id": "{{id}}",
                        "time": "{{timestamp}}",
                        "status": {
                          "execution0": "downloaded",
                          "execution": "closed",
                          "result": {
                            "finished": "success",
                            "progress": {
                              "cnt": 1,
                              "of": 5
                            }
                          },
                          "code": 200,
                          "details": [
                            "string"
                          ]
                        }
                      }
                      """;

        var content = new StringContent(request, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
        return response.StatusCode == HttpStatusCode.OK;
    }
}