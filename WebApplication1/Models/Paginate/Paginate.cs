namespace api.Models
{

  public class Paginate<T>
  {

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public int TotalCount { get; set; } = 0;

    public List<T> Items { get; set; } = [];
  }
}