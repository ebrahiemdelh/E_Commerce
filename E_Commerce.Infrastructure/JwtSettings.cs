namespace E_Commerce.Infrastructure
{
    public class JwtSettings
    {
        public const string SectionName = "Jwt";
        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpireMiutes { get; set; }
    }
}
