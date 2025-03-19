﻿namespace ClientConnectorApi.Configurations
{
    public class JwtConfiguration
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
