// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

public class Polling
{
    [JsonPropertyName("sleep")]
    public required TimeSpan Sleep { get; set; }
}