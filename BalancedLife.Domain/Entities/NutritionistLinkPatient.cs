namespace BalancedLife.Domain.Entities {
    public class NutritionistLinkPatient
    {
        public UserPatientLink Link { get; set; }
        public UserInfo Nutritionist { get; set; }
    }
}
