using System.ComponentModel.DataAnnotations;

namespace BalancedLife.Application.DTOs.User {
    public class UserDTO {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime Birth { get; set; }

        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Formato de senha inválido")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        public string? UrlImage { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido.")]
        public string Cpf { get; set; }

        public string? Street { get; set; }

        public long? Number { get; set; }

        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido.")]
        public string? ZipCode { get; set; }

        public long? IdCity { get; set; }

        [Required(ErrorMessage = "O campo ID do Nível do Usuário é obrigatório.")]
        public long IdUserRole { get; set; }

        [Phone(ErrorMessage = "Formato de número de telefone inválido.")]
        public string PhoneNumber { get; set; }

        public string? Instagram { get; set; }

        public string? Facebook { get; set; }

        [Phone(ErrorMessage = "Formato de número de telefone inválido.")]
        public string? Whatsapp { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime? ExpirationLicence { get; set; }
        public bool IsCompleteProfile { get; set; }
        public string? District { get; set; }
    }
}
