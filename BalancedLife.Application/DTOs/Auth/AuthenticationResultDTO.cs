﻿public class AuthenticationResultDTO {
    public bool Success { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public IEnumerable<string> Errors { get; set; }
}