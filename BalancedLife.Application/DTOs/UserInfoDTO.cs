using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.DTOs {
    public class UserInfoDTO {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Birth { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public string UrlImage { get; set; }

        public string Sex { get; set; }

        public string Cpf { get; set; }

        public string Street { get; set; }

        public long Number { get; set; }

        public string ZipCode { get; set; }

        public long IdCity { get; set; }

        public long IdUserLevel { get; set; }

        public string PhoneNumber { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public string Whatsapp { get; set; }

        public DateTime? ExpirationLicence { get; set; }
    }

}

