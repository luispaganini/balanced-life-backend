using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.DTOs {
    public class UserInfoDTO {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Birth { get; set; }

        public string Email { get; set; }

        public string UrlImage { get; set; }

        public string Gender { get; set; }

        public string Cpf { get; set; }

        public string Street { get; set; }

        public long Number { get; set; }

        public string ZipCode { get; set; }

        public LocationDTO Location { get; set; }

        public UserRoleDTO UserRole { get; set; }

        public string PhoneNumber { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public string Whatsapp { get; set; }

        public DateTime? ExpirationLicence { get; set; }
        public bool IsCompleteProfile { get; set; }
        public string District { get; set; }
    }

}

