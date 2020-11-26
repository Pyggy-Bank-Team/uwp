using piggy_bank_uwp.Enums;
using System;
using Peppa.Enums;

namespace piggy_bank_uwp.Context.Entities
{
    public class Operation : EntityModifiedBase
    {
        public long? CategoryId { get; set; }
    
        public CategoryType? CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public string CategoryTitle { get; set; }

        public decimal? Amount { get; set; }

        public long? AccountId { get; set; }

        public string AccountTitle { get; set; }

        public string Currency { get; set; }

        public string Symbol { get; set; }

        public string Comment { get; set; }

        public OperationType Type { get; set; }

        public DateTime? PlanDate { get; set; }

        public string FromTitle { get; set; }
  
        public string ToTitle { get; set; }

        public bool IsDeleted { get; set; }
    }
}
