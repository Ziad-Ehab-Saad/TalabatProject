namespace TalabatPL.Erros
{
    public class ApiResponseException :ApiResponseError
    {
        public string? details { get; set; }
        public ApiResponseException(int code, string? message=null ,  string? details=null)
            :base(code,message)
        {
            this.details = details;
        }
    }
}
