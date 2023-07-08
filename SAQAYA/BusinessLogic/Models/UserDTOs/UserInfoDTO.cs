using System.ComponentModel.DataAnnotations;

namespace SAQAYA.BusinessLogic.Models.UserDTOs
{
    public class UserInfoDTO
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool MarkrtingConsent { get; set; }

    }
}
