using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Helpers.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FailuresEnum
    {
        None,
        Few,
        Many
    }
}
