namespace EbuBridgeLmsSystem.Application.Helpers.Extensions
{
    public static class GuidValidatorExtension
    {
        public static bool IsValidGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false; 
            }
            bool isGuidFormat = Guid.TryParse(input, out Guid parsedGuid);
            bool isEmptyGuid = parsedGuid == Guid.Empty;
            return isGuidFormat && !isEmptyGuid;
        }
    }
}
