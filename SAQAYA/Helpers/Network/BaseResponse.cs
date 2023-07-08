using System.Runtime.Serialization;

namespace SAQAYA.Helpers.Network
{
    public class BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(int responseCode, string responseMessage, List<ValidationError>? validationList = null, dynamic? responseData = null)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ValidationList = validationList;
            ResponseData = responseData;
        }

        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<ValidationError>? ValidationList { get; set; }
        public dynamic? ResponseData { get; set; }
        public bool IsSuccess
        {
            get
            {
                return ResponseCode == (int)StanderResponseCodes.Success;
            }
        }
    }
    public class BaseResponse<T> where T : class
    {
        public BaseResponse() { }

        public BaseResponse(int responseCode, string responseMessage, List<ValidationError>? validationList = null, T responseData = null)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ValidationList = validationList;
            ResponseData = responseData;
        }

        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<ValidationError>? ValidationList { get; set; }
        public T? ResponseData { get; set; }
        public bool IsSuccess
        {
            get
            {
                return ResponseCode == (int)StanderResponseCodes.Success;
            }
        }
    }
    [DataContract]
    public class ValidationError
    {
        [DataMember(Order = 1)]
        public string FieldName { get; set; }
        [DataMember(Order = 2)]
        public string ValidationMessage { get; set; }
    }
}
