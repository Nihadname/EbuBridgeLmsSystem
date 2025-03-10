namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode
{
    public class FailedAttemptData
    {
        public int Count { get; set; } = 0;
        public DateTime BlockUntil { get; set; } = DateTime.MinValue;
    }
}
