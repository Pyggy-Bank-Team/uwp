using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models.Operations
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

            Operations = new List<IOperationModel>();
            CurrentPageNumber = 1;
            _totalPages = 1;
        }
        
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

        public async Task SaveOperation(IOperationModel newOperation, CancellationToken token)
        {
            if (newOperation == null)
                return;

            await newOperation.Save();
            
            //Adding an new operation in begin of list
            Operations.Insert(0, newOperation);
            OnPropertyChanged(nameof(Operations));
        }

        public async Task UpdateOperation(IOperationModel operation, CancellationToken token)
        {
            if (operation == null)
                return;

            await operation.Update();
            OnPropertyChanged(nameof(Operations));
        }

        public async Task DeleteOperation(IOperationModel operation, CancellationToken token)
        {
            if (operation == null)
                return;

            await operation.Delete();
            
            Operations.Remove(operation);
            OnPropertyChanged(nameof(Operations));
        }

        public void Dispose()
            => _repository?.Dispose();
        
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

        public List<IOperationModel> Operations { get; private set; }
        
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