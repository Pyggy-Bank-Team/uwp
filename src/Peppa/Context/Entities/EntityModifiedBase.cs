using System;

namespace piggy_bank_uwp.Context.Entities
{
    public abstract class EntityModifiedBase : EntityBase
    {
        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
