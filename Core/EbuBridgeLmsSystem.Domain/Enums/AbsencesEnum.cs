using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AbsencesEnum
    {
        Rarely,
        Sometimes,
        Often
    }
}
