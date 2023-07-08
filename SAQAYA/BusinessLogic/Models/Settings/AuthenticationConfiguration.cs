namespace SAQAYA.BusinessLogic.Models.Settings
{
    public class AuthenticationConfiguration
    {
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public int TokenExpireDurationInHours { get; set; }
        public int RefreshTokenExpireDurationInDays { get; set; }
        public bool EmailVerificationRequired { get; set; }
        public int EmailVerificationExpriyTimeInHours { get; set; }
        public int EmailForgotPasswordExpriyTimeInHours { get; set; }
        public bool SMSVerificationRequired { get; set; }
        public int SMSVerificationExpriyTimeInMinutes { get; set; }
        public int SMSMaxNumberPerUserPerDay { get; set; }
    }
}
