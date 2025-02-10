using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Helpers.Enums
{
    public enum FileResourceType
    {
        //
        // Summary:
        //     Images in various formats (jpg, png, etc.)
        [EnumMember(Value = "image")]
        Image,
        //
        // Summary:
        //     Any files (text, binary)
        [EnumMember(Value = "raw")]
        Raw,
        //
        // Summary:
        //     Video files in various formats (mp4, etc.)
        [EnumMember(Value = "video")]
        Video,
        //
        // Summary:
        //     Auto upload format
        [EnumMember(Value = "auto")]
        Auto
    }
}
