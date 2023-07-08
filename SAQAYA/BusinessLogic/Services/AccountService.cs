using SAQAYA.BusinessLogic.Constant;
using SAQAYA.BusinessLogic.IServices;
using SAQAYA.BusinessLogic.Models.UserDTOs;
using SAQAYA.BusinessLogic.Models.Settings;
using SAQAYA.DataAccess.Entities;
using SAQAYA.Helpers.Bases;
using SAQAYA.Helpers.Network;
using Microsoft.Extensions.Options;
using SAQAYA.Helpers.Hash;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace SAQAYA.BusinessLogic.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly AuthenticationConfiguration _AuthenticationSettingsModel;


        public AccountService(IOptions<AuthenticationConfiguration> authenticationSettingsModel,
                                     IConfiguration configuration,
                                     IJwtTokenProvider jwtTokenProvider)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _AuthenticationSettingsModel = authenticationSettingsModel.Value;
        }



        #region Registration and login
        public async Task<BaseResponse> CreateUser(CreateInputDTO model)
        {
            var response = new BaseResponse();

            if (await IsEmailExit(model.Email))
            {
                return response = new BaseResponse
                {
                    ResponseCode = (int)ResponseCodes.EmailAlreadyExist,
                    ResponseMessage = "Email Already Exist"
                };
            }

            var user = new Users()
            {
                ID = HashHelper.CreateHash(model.Email, "450d0b0db2bcf4adde5032eca1a7c416e560cf44", "SHA1"),
                FirstName = model.FirstName,
                LastName = model.LastName,
                MarkrtingConsent = model.MarketingConsent,
                Email = model.Email
            };

            _UnitOfWork.Repository<Users>().Add(user);
            _UnitOfWork.SaveChanges();
            //var result = await _userManager.CreateAsync(user);

            //if (result.Succeeded)
            //{
                response = await GenerateTokenResponseAsync(user/*, model.ClientId, model.ClientSecret*/);
                return response;
            //}
            //else
            //{
            //    response.ValidationList ??= new(); 
            //    foreach (var error in result.Errors)
            //    {
            //        response.ValidationList.Add(new ValidationError { ValidationMessage = error.Description });
            //    }
            //    return response;
            //}
        }

        public async Task<BaseResponse> GetUser(string ID)
        {
            try
            {
                var CurrentUser = await _UnitOfWork.Repository<Users>().GetSingleAsync(Q => Q.ID == ID);
                if (CurrentUser == null)
                {
                    var Response = new BaseResponse
                    {
                        ResponseCode = (int)ResponseCodes.UserNotFound,
                        ResponseMessage = "User Not Found"
                    };
                    return Response;
                }
                var User = new UserInfoDTO
                {
                    ID = CurrentUser.ID,
                    FirstName = CurrentUser.FirstName,
                    LastName = CurrentUser.LastName,
                    Email = CurrentUser.Email,
                    MarkrtingConsent = CurrentUser.MarkrtingConsent
                };

                var temp = JObject.Parse(JsonConvert.SerializeObject(User));
                temp.Descendants()
                .OfType<JProperty>()
                .Where(attr => attr.Name == "Email" && !CurrentUser.MarkrtingConsent)
                .ToList() // you should call ToList because you're about to changing the result, which is not possible if it is IEnumerable
                .ForEach(attr => attr.Remove()); // removing unwanted attributes

                var Result = temp.ToString();

                return new BaseResponse { ResponseData = Result, ResponseCode = (int)ResponseCodes.Success };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        internal async Task<bool> IsEmailExit(string Email)
        {
            try
            {
                var User = await _UnitOfWork.Repository<Users>().GetSingleAsync(u => u.Email == Email);
                if (User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Token Helpers
        private async Task<BaseResponse> GenerateTokenResponseAsync(Users user)
        {
            string refreshToken = null;
            BaseResponse response;

            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var userRoles = await _userManager.GetRolesAsync(user);

            var TokenData = _jwtTokenProvider.GenerateJwtTokenAsync(user.ID, user.Email);
            var RefreshTokenExpiration = DateTime.UtcNow.AddDays(_AuthenticationSettingsModel.RefreshTokenExpireDurationInDays);

            response = new BaseResponse
            {
                ResponseCode = (int)ResponseCodes.Success,
                ResponseData = new CreateOutputDTO
                {
                    ID = user.ID.ToString(),
                    AccessToken = TokenData.token
                }
            };

            return response;
        }

        #endregion
    }
}
