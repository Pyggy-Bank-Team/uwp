using Newtonsoft.Json;
using Peppa.Enums;

namespace Peppa.Contracts.Responses
{
    public class AccountResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public AccountType Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }
    }
}