// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.DeploymentModels;

public class Artifact
{
    [JsonPropertyName("filename")]
    public required string Filename { get; set; }
    
    [JsonPropertyName("hashes")]
    public required Hashes Hashes { get; set; }
    
    [JsonPropertyName("size")]
    public required int Size { get; set; }
    
    [JsonPropertyName("_links")]
    public required Links Links { get; set; }
}