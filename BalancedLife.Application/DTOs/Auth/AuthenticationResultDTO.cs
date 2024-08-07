public class AuthenticationResultDTO {
    public bool Success { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public IEnumerable<string> Errors { get; set; }
}