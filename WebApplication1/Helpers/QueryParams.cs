namespace api.Helpers
{

  public class QueryParams
  {
    public string? SearchKey { get; set; }

    public int PageIndex { get; set; } = 0;

    public int PageSize { get; set; } = 10;
  }
}