namespace PT_SalasDario.API.Infra
{
    public class ErrorResult
    {
        public ErrorResult()
        {
        }

        public ErrorResult(int statusCode)
        {
            StatusCode = statusCode;
            Message = GetMessageForStatusCode(statusCode);
            Errors = new List<string>();
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private static string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request. Please check your input and try again.",
                401 => "Unauthorized. Please authenticate and try again.",
                403 => "Forbidden. You do not have permission to access this resource.",
                404 => "Not found. The requested resource could not be located.",
                500 => "We encountered an error with your request.",
                _ => "An unexpected error occurred. Please try again later."
            };
        }
    }
}
