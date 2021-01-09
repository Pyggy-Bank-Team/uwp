namespace Peppa.Dto
{
    public class PageResult<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public T[] Result { get; set; }
    }
}
