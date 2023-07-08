using SAQAYA.BusinessLogic.Models.UserDTOs;
using SAQAYA.Helpers.Network;

namespace SAQAYA.BusinessLogic.IServices
{
    public interface IAccountService
    {
        Task<BaseResponse> CreateUser(CreateInputDTO model);
        Task<BaseResponse> GetUser(string ID);

    }
}
