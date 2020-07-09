using Newtonsoft.Json;

namespace piggy_bank_uwp.Entities
{
    public class Account
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }
    }
}