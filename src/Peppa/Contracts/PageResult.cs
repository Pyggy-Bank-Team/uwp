namespace Peppa.Contracts
{
    public class PageResult<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public T[] Result { get; set; }
    }
}
