using piggy_bank_uwp.Enums;
using System;

namespace piggy_bank_uwp.Context.Entities
{
    public class Operation : EntityModifiedBase
    {
        public long? CategoryId { get; set; }
    
        public long? CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public long? Amount { get; set; }

        public long? AccountId { get; set; }

        public string AccountTitle { get; set; }

        public string Comment { get; set; }

        public OperationType Type { get; set; }

        public DateTime? PlanDate { get; set; }

        public string FromTitle { get; set; }
  
        public string ToTitle { get; set; }

        public bool IsDeleted { get; set; }
    }
}
