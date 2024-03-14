using System.ComponentModel.DataAnnotations;

namespace BalancedLife.Application.DTOs {
    public class RegisterUserDTO {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        public string? UrlImage { get; set; }

        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        public long Number { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo ID da Cidade é obrigatório.")]
        public long IdCity { get; set; }

        [Required(ErrorMessage = "O campo ID do Nível do Usuário é obrigatório.")]
        public long IdUserLevel { get; set; }

        [Phone(ErrorMessage = "Formato de número de telefone inválido.")]
        public string PhoneNumber { get; set; }

        public string? Instagram { get; set; }

        public string? Facebook { get; set; }

        [Phone(ErrorMessage = "Formato de número de telefone inválido.")]
        public string? Whatsapp { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime? ExpirationLicence { get; set; }
    }
}
