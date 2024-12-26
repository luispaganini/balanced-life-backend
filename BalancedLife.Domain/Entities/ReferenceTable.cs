namespace BalancedLife.Domain.Entities;

public partial class ReferenceTable
{
    public int Id { get; set; }

    public string ReferenceTable1 { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}