﻿using System;
using Newtonsoft.Json;
using Peppa.Enums;

namespace Peppa.Contracts
{
    public class CategoryContract
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("hexColor")]
        public string HexColor { get; set; }

        [JsonProperty("type")]
        public CategoryType Type { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("createdBy")]
        public Guid CreatedBy { get; set; }

        [JsonProperty("createdOn")]
        public DateTimeOffset CreatedOn { get; set; }
    }
}