// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

public class Links
{
    [JsonPropertyName("download-http")]
    public required Link Download { get; set; }

    [JsonPropertyName("md5sum-http")]
    public required Link Md5Sum { get; set; }
}