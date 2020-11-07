using Newtonsoft.Json;
using System;

namespace piggy_bank_uwp.Contracts.Responses
{
    public class OperationResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("categoryId")]
        public long CategoryId { get; set; }

        [JsonProperty("categoryType")]
        public long CategoryType { get; set; }

        [JsonProperty("categoryHexColor")]
        public string CategoryHexColor { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("accountTitle")]
        public string AccountTitle { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

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
