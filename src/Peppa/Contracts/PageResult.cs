namespace piggy_bank_uwp.Contracts
{
    public class PageResult<T>
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int CountItemsOnPage { get; set; }
        public T[] Result { get; set; }
    }
}
