using Newtonsoft.Json;
using Peppa.Enums;
using System;

namespace Peppa.Contracts.Responses
{
    public class TransferOperationResponse
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("fromId")]
        public int FromId { get; set; }

        [JsonProperty("toId")]
        public int ToId { get; set; }

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
