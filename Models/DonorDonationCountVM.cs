namespace BloodBankDB.Models
{
    public class DonorDonationCountVM
    {
        public int DonorId { get; set; }

        public string? FullName { get; set; }

        public string? BloodGroup { get; set; }

        public int TotalDonations { get; set; }
    }
}
