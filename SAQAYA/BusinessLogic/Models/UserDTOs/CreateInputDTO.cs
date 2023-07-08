namespace SAQAYA.BusinessLogic.Models.UserDTOs
{
    public class CreateInputDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool MarketingConsent { get; set; }
    }
}
