// ReSharper disable ClassNeverInstantiated.Global
using System.Text.Json.Serialization;

namespace POC_Hawkbit.SoftwareUpdates.Models.Hawkbit.ControllerModels;

public class Config
{
    [JsonPropertyName("polling")]
    public required Polling Polling { get; set; }
}