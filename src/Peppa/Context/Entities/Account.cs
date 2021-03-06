﻿using Peppa.Enums;

namespace Peppa.Context.Entities
{
    public class Account
    {
        public int Id { get; set; }
        
        public AccountType Type { get; set; }

        public string Title { get; set; }

        public string Currency { get; set; }

        public double Balance { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDeleted { get; set; }
        
        public bool IsSynchronized { get; set; }
    }
}