using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
namespace BalancedLife.Domain.Entities;

public partial class UserInfo {
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime Birth { get; set; }

    public string? Password { get; set; }
    public string Email { get; set; }

    public string? UrlImage { get; set; }

    public string Sex { get; set; }

    public string Cpf { get; set; }

    public string Street { get; set; }

    public long Number { get; set; }

    public string ZipCode { get; set; }

    public long IdCity { get; set; }

    public long IdUserLevel { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Instagram { get; set; }

    public string? Facebook { get; set; }

    public string? Whatsapp { get; set; }

    public DateTime? ExpirationLicence { get; set; }
    public bool IsCompleteProfile { get; set; }
    public string District { get; set; }

    public virtual ICollection<Body> Bodies { get; set; } = new List<Body>();

    public virtual City IdCityNavigation { get; set; }

    public virtual UserLevel IdUserLevelNavigation { get; set; }

    public virtual ICollection<PlanDiet> PlanDiets { get; set; } = new List<PlanDiet>();

    public virtual ICollection<Snack> Snacks { get; set; } = new List<Snack>();

    public virtual ICollection<StatusUser> StatusUserIdNutricionistNavigations { get; set; } = new List<StatusUser>();

    public virtual ICollection<StatusUser> StatusUserIdUserNavigations { get; set; } = new List<StatusUser>();

    public bool VerifyPassword(string password, string hashedPassword) {
        return HashPassword(password) == hashedPassword;
    }
    private bool isValidPassword(string password) {
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

        return Regex.IsMatch(password, pattern);
    }

    private string HashPassword(string password) {
        using ( var sha256 = SHA256.Create() ) {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}