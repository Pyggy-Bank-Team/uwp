using Newtonsoft.Json;
using Peppa.Enums;
using System;

namespace Peppa.Contracts.Responses
{
    public class BudgetOperationResponse
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("accountId")]
        public int AccountId { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("type")]
        public OperationType Type { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
