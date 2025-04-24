
namespace TalabatPL.Erros
{
    public class ApiResponseError
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponseError(int StatusCode,string? Message=null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetDefaultMessage(StatusCode);

        }

        private string? GetDefaultMessage(int statusCode)
        {

            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not Found",
                500 => "Server error, we have",
                _ => null
            };

        }
    }
}
