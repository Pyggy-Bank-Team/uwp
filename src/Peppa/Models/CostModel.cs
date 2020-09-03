using Peppa.Interface;
using Peppa.Models;

namespace Peppa.Models
{
    public sealed class CostModel : IBaseModel
    {
        public long Date { get; set; }

        public int Cost { get; set; }

        public string Comment { get; set; }

        public string CategoryId { get; set; }

        public string BalanceId { get; set; }

        public string Id { get; set; }
    }
}
