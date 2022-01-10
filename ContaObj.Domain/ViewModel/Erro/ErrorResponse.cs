namespace ContaObj.Domain.ViewModel
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public ErrorResponse(string id, string requestId)
        {
            Id = id;
            RequestId = requestId;
            Date = DateTime.Now;
            Message = "Unexpected error";
        }
    }
}
