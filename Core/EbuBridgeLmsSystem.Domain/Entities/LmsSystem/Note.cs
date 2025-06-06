﻿using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
