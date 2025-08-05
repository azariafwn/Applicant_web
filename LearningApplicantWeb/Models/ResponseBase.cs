namespace LearningApplicantWeb.Models
{
    public class ResponseBase
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; } = null;
    }
}
