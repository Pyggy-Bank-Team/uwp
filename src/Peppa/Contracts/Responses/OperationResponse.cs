using System;
using Peppa.Enums;

namespace Peppa.Contracts.Responses
{
    public class OperationResponse
    {
        public bool IsDeleted { get; set; }
        public Account Account { get; set; }
        public Account ToAccount { get; set; }
        public Category Category { get; set; }
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public OperationType Type { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }

    public class Account
    {
        public string Title { get; set; }
        public string Currency { get; set; }
    }

    public class Category
    {
        public CategoryType Type { get; set; }
        public string HexColor { get; set; }
        public string Title { get; set; }
    }
}
