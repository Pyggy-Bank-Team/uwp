namespace Peppa.Contracts.Responses
{
    public class ChartByCategoriesResponse
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryHexColor { get; set; }
        public decimal Amount { get; set; }
    }
}