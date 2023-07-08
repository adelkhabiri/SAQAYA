﻿namespace SAQAYA.BusinessLogic.Constant
{
    public enum ResponseCodes
    {
        FailedToSendCode = 100,
        InvalidCode = 101,
        VerificationCodeRequired = 102,
        SmsIntervalSendViolation = 103,
        UserNotFound = 104,
        ValidationError = 105,
        RegisterationFieldAlreadyExist = 106,
        ForgetPasswordFieldRequired = 107,
        PhoneNumberAlreadyExist = 108,
        InvalidRefreshToken = 109,
        EmailNotConfirmed = 110,
        PhoneNumberNotConfirmed = 111,
        MaximumSmsSendCodeReached = 112,
        PhoneNumberRequired = 113,
        EmailRequired = 114,
        EmailAlreadyExist = 115,
        WrongPassword = 116,
        WrongEmail = 117,
        CustomTextRequired = 118,
        CustomDropDownRequired = 119,
        InvalidClient = 120,
        FailedToUpdateUser = 121,
        InvalidFacebookToken = 122,
        FacebookTokenDeosntExist = 123,
        WrongUserName = 124,
        GoogleTokenDeosntExist = 125,
        InvalidGoogleToken = 126,
        InvalidRegistrationType = 127,
        InvalidLoginType = 128,
        SocialUserExist = 129,
        VerificationTokenRequired = 130,
        InvalidResetPasswordToken = 131,
        CannotDeleteFields = 132,
        FailChangePassword = 134,
        InvalidIdentifier = 135,
        BannedUser = 136,
        DeletedUser = 137,
        IncorrectOldPassword = 138,
        UserNameAlreadyExist = 139,
        FailToSendSMS = 140,
        FailToSendEmail = 141,
        InvalidAppleToken = 142,
        UserTypeNotAllowed = 142,
        AppleTokenDeosntExist = 143,
        DeviceTokenRemoved = 144,
        InvalidEmailConfirmationToken = 145,
        Success = 200
    }
}
