using SAQAYA.EntityFramework.Repository;
using SAQAYA.Helpers.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SAQAYA.Helpers.Bases
{
    public class BaseService
    {
        protected IUnitOfWork _UnitOfWork;
        public readonly HttpContext CurrentHttpContext;

        public BaseService()
        {
            CurrentHttpContext = new HttpContextAccessor().HttpContext;
            var services = CurrentHttpContext.RequestServices;

            // Resolve the dependency injection of IUnitOfWork
            var unitOfWork = (IUnitOfWork)services.GetService(typeof(IUnitOfWork));
            _UnitOfWork = unitOfWork;

            // Resolve the dependency injection of IHttpContextDetailsProvider
            //var iHttpContextDetailsProvider = (IHttpContextDetailsProvider)services.GetService(typeof(IHttpContextDetailsProvider));
            //ContextInfo = iHttpContextDetailsProvider;
        }
        public static string GetImageUrl(string imageRelativePath)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            string ImagePath = root.GetSection("Settings").GetSection("ImageBasePath").Value;



            if (string.IsNullOrEmpty(imageRelativePath))
            {
                return "";
            }
            if (imageRelativePath.ToLower().StartsWith("http"))
            {
                return imageRelativePath;
            }
            ImagePath = Path.Combine(ImagePath, imageRelativePath);
            return ImagePath.Replace("\\", "//");
        }

        protected BaseResponse MergeResponses(BaseResponse response, BaseResponse toBeMergedResponse)
        {
            if (response == null || response.ValidationList == null)
                return null;

            if (toBeMergedResponse == null || toBeMergedResponse.ValidationList == null || !toBeMergedResponse.ValidationList.Any())
                return null;

            response.ValidationList.AddRange(toBeMergedResponse.ValidationList);

            return response;
        }
        protected BaseResponse<T> MergeResponses<T>(BaseResponse<T> response, BaseResponse<T> toBeMergedResponse) where T : class
        {
            if (response == null || response.ValidationList == null)
                return null;

            if (toBeMergedResponse == null || toBeMergedResponse.ValidationList == null || !toBeMergedResponse.ValidationList.Any())
                return null;

            response.ValidationList.AddRange(toBeMergedResponse.ValidationList);

            return response;
        }
        protected BaseResponse GetValidationResponse(BaseResponse response, int responseCode)
        {
            if (response.ValidationList == null || response.ValidationList.Count == 0)
                return null;

            response.ResponseCode = responseCode;
            response.ResponseMessage = "Model has validation errors";
            return response;
        }
        protected BaseResponse<T> GetValidationResponse<T>(BaseResponse<T> response, int responseCode) where T : class
        {
            if (response.ValidationList == null || response.ValidationList.Count == 0)
                return null;

            response.ResponseCode = responseCode;
            response.ResponseMessage = "Model has validation errors";
            return response;
        }
    }
}
