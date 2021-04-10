using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Enums;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Account = Peppa.Dto.Account;
using Category = Peppa.Dto.Category;

namespace Peppa.Models.Operations
{
    public class OperationModel : BaseModel, IOperationModel
    {
        private readonly Operation _operation;
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _service;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        public OperationModel(Operation operation, IPiggyRepository repository, IOperationService service, IAccountService accountService, ICategoryService categoryService)
        {
            _operation = operation;
            _repository = repository;
            _service = service;
            _accountService = accountService;
            _categoryService = categoryService;
            Accounts = new List<Account>();
            Categories = new List<Category>();
        }

        public decimal Amount { get; set; }
        
        public OperationType Type { get; set; }
        
        public string Comment { get; set; }
        
        public DateTime OperationDate { get; set; }
        
        public CategoryType CategoryType { get; }
        
        public string CategoryHexColor { get; }
        public string CategoryTitle { get;  }
        public string AccountTitle { get; }
        public string ToAccountTitle { get; }
        public string Symbol { get; }
        
        public List<Account> Accounts { get; private set; }
        
        public List<Category> Categories { get; set; }

        public Task Save()
            => throw new NotImplementedException();

        public Task Update()
            => throw new NotImplementedException();

        public Task Delete()
            => throw new NotImplementedException();

        public Task UpdateAccounts()
            => throw new NotImplementedException();

        public Task UpdateCategories()
            => throw new NotImplementedException();
    }
}