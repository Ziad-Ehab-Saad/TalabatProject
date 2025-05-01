namespace TalabatPL.Erros
{
    public class ApiValidationErrorResponse : ApiResponseError
    {
        public List<string> Erros { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            Erros = new List<string>();


        }
    }
}
