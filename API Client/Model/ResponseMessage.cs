namespace API_Client.Model;

public class ResponseMessage
{
    public ResponseMessage(string action, int status, string message)
    {
        Action = action;
        Status = status;
        Message = message;

    }
    public string Action { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
}
