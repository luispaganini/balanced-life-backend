using System.ComponentModel.DataAnnotations;

namespace BalancedLife.Application.DTOs.Body {
    public class BodyDTO {
        [Required]
        public long Id { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Height { get; set; }

        public DateTime? Datetime { get; set; }

        [Required]
        public long IdUser { get; set; }

    }
}
