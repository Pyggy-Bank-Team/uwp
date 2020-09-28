using System.ComponentModel;

namespace Peppa.Enums
{
    public enum CategoryType
    {
        [Description("Undefined")]
        Undefined = 0,

        [Description("Income")]
        Income,

        [Description("Expense")]
        Expense
    }
}