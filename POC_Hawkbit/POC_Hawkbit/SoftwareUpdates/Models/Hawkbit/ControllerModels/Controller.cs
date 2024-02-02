using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

public class Controller
{
    [JsonPropertyName("config")]
    public required Config Config { get; set; }
    
    [JsonPropertyName("_links")]
    public required Links Links { get; set; }
}