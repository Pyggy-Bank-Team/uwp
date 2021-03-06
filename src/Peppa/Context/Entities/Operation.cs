﻿using Peppa.Enums;

namespace Peppa.Context.Entities
{
    public class Operation : EntityModifiedBase
    {
        public int? CategoryId { get; set; }
    
        public CategoryType? CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public string CategoryTitle { get; set; }

        public double Amount { get; set; }

        public string AccountTitle { get; set; }

        public int? AccountId { get; set; }

        public string Symbol { get; set; }

        public string Comment { get; set; }

        public OperationType Type { get; set; }
  
        public string ToTitle { get; set; }

        public int? ToId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
