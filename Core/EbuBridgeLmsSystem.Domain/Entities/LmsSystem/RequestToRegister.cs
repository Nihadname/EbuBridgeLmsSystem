﻿using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

public sealed class RequestToRegister : BaseEntity
{
    public string FullName { get; set; }
    public int Age { get; private set; }
    public bool IsParent { get; set; }
    public string PhoneNumber { get; set; }
    public Guid ChoosenCourse { get; set; }
    public string ChildName { get; set; }
    public int? ChildAge { get; set; }
    public string Email { get; set; }
    public string AiResponse { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string VerificationToken { get; set; }
}
