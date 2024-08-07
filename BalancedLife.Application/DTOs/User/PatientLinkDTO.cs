namespace BalancedLife.Application.DTOs.User {
    public class PatientLinkDTO {
        public long? Id { get; set; }

        public long IdNutritionist { get; set; }

        public long IdPatient { get; set; }

        public bool IsCurrentNutritionist { get; set; }

        public int LinkStatus { get; set; }
    }
}
