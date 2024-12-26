using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.DTOs.Snack {
    public class ReferenceTableDTO {
        public int Id { get; set; }

        public string ReferenceTable { get; set; }

        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}