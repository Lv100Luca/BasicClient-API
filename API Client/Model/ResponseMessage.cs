namespace API_Client.Model;

public class ResponseMessage
{
    public ResponseMessage(string action, int status, string message)
    {
        Action = action;
        Status = status;
        Message = message;
        
    }
    private string Action { get; set; }
    private int Status { get; set; }
    private string Message { get; set; }
}