// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

public class DeploymentRequest
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    
    [JsonPropertyName("deployment")]
    public required Deployment Deployment { get; set; }
}