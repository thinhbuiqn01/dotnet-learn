namespace api.Models
{
  public class ResponseSuccess<D>
  {

    public D Data { get; set; }

    public bool Status { get; set; } = true;

    public string? Error { get; set; } = null;
  }

  public class ResponseError<E>
  {

    public string? Data { get; set; } = null;

    public bool Status { get; set; } = false;
    public E Error { get; set; }
  }

}