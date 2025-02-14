using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Student
{
    public class StudentCreateDto
    {
        [JsonIgnore]
        public decimal? AvarageScore { get; set; }
        [JsonIgnore]
        public string AppUserId { get; set; }
        [JsonIgnore]
        public bool IsEnrolled { get; set; }
    }
}
