// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

public class Link
{
    [JsonPropertyName("href")]
    public required string Href { get; set; }
}