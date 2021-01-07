using System;
using Newtonsoft.Json;
using Peppa.Enums;
using piggy_bank_uwp.Enums;

namespace Peppa.Contracts.Responses
{
    public class OperationResponse
    {
        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("toAccount")]
        public Account ToAccount { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("type")]
        public OperationType Type { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class Account
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class Category
    {
        [JsonProperty("type")]
        public CategoryType Type { get; set; }

        [JsonProperty("hexColor")]
        public string HexColor { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

}
