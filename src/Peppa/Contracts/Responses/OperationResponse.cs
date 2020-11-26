using Newtonsoft.Json;
using System;
using Peppa.Enums;
using piggy_bank_uwp.Enums;

namespace piggy_bank_uwp.Contracts.Responses
{
    public class OperationResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("categoryType")]
        public CategoryType? CategoryType { get; set; }

        [JsonProperty("categoryHexColor")]
        public string CategoryHexColor { get; set; }

        [JsonProperty("categoryTitle")]
        public string CategoryTitle { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("accountId")]
        public int? AccountId { get; set; }

        [JsonProperty("accountTitle")]
        public string AccountTitle { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("type")]
        public OperationType Type { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("planDate")]
        public DateTime? PlanDate { get; set; }

        [JsonProperty("fromTitle")]
        public string FromTitle { get; set; }

        [JsonProperty("toTitle")]
        public string ToTitle { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
