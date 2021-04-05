using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;
using System.Collections.Generic;
using Peppa.Context.Entities;
using System.Linq;
using Peppa.Contracts;
using Peppa.Contracts.Requests;
using Peppa.Interface.Models;
using Account = Peppa.Context.Entities.Account;
using Category = Peppa.Context.Entities.Category;

namespace Peppa.Models
{
    public class OperationsModel : BaseModel, IOperationsModel
    {
        private const int NumberOfOperationOnOnePage = 30;
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _operationService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly Dictionary<string, string> _availableCurrencies;
        private int _totalPages;

        public OperationsModel(IPiggyRepository repository, IOperationService operationService, IAccountService accountService, ICategoryService categoryService)
        {
            _repository = repository;
            _operationService = operationService;
            _accountService = accountService;
            _categoryService = categoryService;
            _availableCurrencies = new Dictionary<string, string>
            {
                {"RUB", "₽"},
                {"BYN", "Br"},
                {"UAH", "₴"},
                {"KZT", "₸"},
                {"USD", "$"},
                {"EUR", "€"}
            };

            Operations = new List<OperationModel>();
            CurrentPageNumber = 1;
            _totalPages = 1;
        }

        #region Old stuff
        
        public IPiggyRepository Repository => _repository;

        public async Task<PageResult<Operation>> GetOperations(int page, CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_operationService.IsAuthorized)
            {
                var response = await _operationService.GetOperations(page, token);
                if (response != null)
                {

                    var entities = response.Result.Select(o => new Operation
                    {
                        Id = o.Id,
                        IsDeleted = o.IsDeleted,
                        AccountTitle = o.Account.Title,
                        ToTitle = o.ToAccount?.Title,
                        CategoryType = o.Category?.Type,
                        CategoryHexColor = o.Category?.HexColor,
                        CategoryTitle = o.Category?.Title,
                        Amount = o.Amount,
                        Type = o.Type,
                        CreatedOn = o.Date,
                        Comment = o.Comment
                    });

                    //TODO Save into db

                    return new PageResult<Operation>
                    {
                        TotalPages = response.TotalPages,
                        CurrentPage = response.CurrentPage,
                        Result = response.Result.Select(o => new Operation
                        {
                            Id = o.Id,
                            IsDeleted = o.IsDeleted,
                            AccountTitle = o.Account.Title,
                            ToTitle = o.ToAccount?.Title,
                            CategoryType = o.Category?.Type,
                            CategoryHexColor = o.Category?.HexColor,
                            CategoryTitle = o.Category?.Title,
                            Amount = o.Amount,
                            Type = o.Type,
                            CreatedOn = o.Date,
                            Comment = o.Comment,
                            Symbol = GetSymbol(o.Account.Currency)
                        }).ToArray()
                    };
                }
            }

            return null;
            //return await _repository.GetOperations(token);
        }

        public Task<Account[]> GetAccounts(bool all, CancellationToken token)
           => _repository.GetAccounts(token, all);

        public Task<Category[]> GetCategories(bool all, CancellationToken token)
            => _repository.GetCategories(token, all);

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<Operation> GetBudgetOperation(int operationId, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var response = await _operationService.GetBudgetOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        CategoryId = response.CategoryId,
                        AccountId = response.AccountId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }

        public async Task<Operation> GetTransferOperation(int operationId, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var response = await _operationService.GetTransferOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        AccountId = response.FromId,
                        ToId = response.ToId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }

        public async Task CreateBudgetOperation(Operation operation, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var request = new CreateBudgetOperationRequest
                {
                    Amount = operation.Amount,
                    AccountId = operation.AccountId.Value,
                    CategoryId = operation.CategoryId.Value,
                    OperationDate = operation.CreatedOn,
                    Comment = operation.Comment
                };

                var result = await _operationService.CreateBudgetOperation(request, token);
                
                //TODO: save to db
            }
        }

        public async Task CreateTransferOperation(Operation operation, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var request = new CreateTransferOperationRequest
                {
                    Amount = operation.Amount,
                    From = operation.AccountId.Value,
                    To = operation.ToId.Value,
                    OperationDate = operation.CreatedOn,
                    Comment = operation.Comment
                };

                var result = await _operationService.CreateTransferOperation(request, token);
                
                //TODO: save to db
            }
        }

        public async Task UpdateBudgetOperation(Operation operation, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var request = new UpdateBudgetOperationRequest
                {
                    AccountId = operation.AccountId.Value,
                    Amount = operation.Amount,
                    CategoryId = operation.CategoryId.Value,
                    Comment = operation.Comment
                };

                await _operationService.UpdateBudgetOperation(operation.Id, request, token);
            }
        }

        public async Task UpdateTransferOperation(Operation operation, CancellationToken token)
        {
            if (_operationService.IsAuthorized)
            {
                var request = new UpdateTransferOperationRequest
                {
                    From = operation.AccountId.Value,
                    Amount = operation.Amount,
                    To = operation.ToId.Value,
                    Comment = operation.Comment
                };

                await _operationService.UpdateTransferOperation(operation.Id, request, token);
            }
        }

        private string GetSymbol(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return null;

            try
            {
                return _availableCurrencies[currency];
            }
            catch
            {
                //TODO log
            }

            return null;
        }

        #endregion

        public async Task UpdateOperations(CancellationToken token)
        {
            if (!_operationService.IsAuthorized)
            {
                var operations = await _repository.GetOperations(token);

                //TODO: Extract the constant to config file
                TotalPages = operations.Length / NumberOfOperationOnOnePage;

                foreach (var operation in operations)
                    Operations.Add(new OperationModel(operation, _repository, _operationService, _accountService, _categoryService));
                
                return;
            }

            var response = await _operationService.GetOperations(CurrentPageNumber, token);
            if (response == null)
                return;

            TotalPages = response.TotalPages;

            foreach (var receivedOperation in response.Result)
            {
                var entity = new Operation
                {
                    Id = receivedOperation.Id,
                    IsDeleted = receivedOperation.IsDeleted,
                    AccountTitle = receivedOperation.Account.Title,
                    ToTitle = receivedOperation.ToAccount?.Title,
                    CategoryType = receivedOperation.Category?.Type,
                    CategoryHexColor = receivedOperation.Category?.HexColor,
                    CategoryTitle = receivedOperation.Category?.Title,
                    Amount = receivedOperation.Amount,
                    Type = receivedOperation.Type,
                    CreatedOn = receivedOperation.Date,
                    Comment = receivedOperation.Comment,
                    Symbol = GetSymbol(receivedOperation.Account.Currency)
                };

                await _repository.AddOrUpdateOperation(entity, token);
                
                Operations.Add(new OperationModel(entity, _repository, _operationService, _accountService, _categoryService));
            }
        }

        public async Task SaveSelectedOperation(OperationModel newOperation)
        {
            if (newOperation == null)
                return;

            await newOperation.Save();
            
            //Adding an new operation in begin of list
            Operations.Insert(0, newOperation);
            OnPropertyChanged(nameof(Operations));
        }

        public async Task UpdateSelectedOperation(OperationModel operation)
        {
            if (operation == null)
                return;

            await operation.Update();
            OnPropertyChanged(nameof(Operations));
        }

        public async Task DeleteSelectedOperation(OperationModel operation)
        {
            if (operation == null)
                return;

            await operation.Delete();
            
            Operations.Remove(operation);
            OnPropertyChanged(nameof(Operations));
        }

        public List<OperationModel> Operations { get; private set; }
        
        public int CurrentPageNumber { get; set; }

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));
                }
            }
        }
    }
}