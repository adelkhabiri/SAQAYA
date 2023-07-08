using SAQAYA.Helpers.Bases;
using SAQAYA.Helpers.Network;
using Microsoft.AspNetCore.Mvc;
using SAQAYA.BusinessLogic.Constant;
using SAQAYA.BusinessLogic.IServices;
using SAQAYA.BusinessLogic.Models.UserDTOs;
using Microsoft.AspNetCore.Authorization;

namespace SAQAYA.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : BaseController
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateInputDTO model)
        {
            try
            {
                var response = new BaseResponse();

                if (ModelState.IsValid)
                {
                    response = await _accountService.CreateUser(model);
                    return ReturnRquest(response);
                }
                response = new BaseResponse
                {
                    ResponseCode = (int)ResponseCodes.ValidationError,
                    ValidationList = new List<ValidationError>()
                };
                response.ValidationList.AddRange(ModelState.Values.SelectMany(Q => Q.Errors).Select(error => new ValidationError { ValidationMessage = error.ErrorMessage }));

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }

        }

        //[Authorize]
        [HttpGet("GetUserInfo")]
        public async Task<ActionResult> GetUser(string ID)
        {
            try
            {
                var Result = await _accountService.GetUser(ID);
                return ReturnRquest(Result);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}