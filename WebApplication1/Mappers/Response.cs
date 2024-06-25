using api.Models;

namespace api.Mappers
{
  public static class ResponseMappers
  {

    public static ResponseError<E> ToError<E>(E error)
    {
      return new ResponseError<E>
      {
        Data = null,
        Status = false,
        Error = error
      };
    }
  }
}