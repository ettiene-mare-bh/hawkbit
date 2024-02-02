// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

public class Links
{
    [JsonPropertyName("deploymentBase")]
    public Link? DeploymentBase { get; set; }
    
    [JsonPropertyName("installedBase")]
    public Link? InstalledBase { get; set; }
    
    [JsonPropertyName("configData")]
    public Link? ConfigData { get; set; }
}