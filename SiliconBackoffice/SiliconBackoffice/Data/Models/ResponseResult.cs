namespace SiliconBackoffice.Data.Models;

public class ResponseResult
{
    public int StatusCode { get; set; }

    public string? Data { get; set; }

    public string? Error { get; set; }
}
