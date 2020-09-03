using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Peppa.Fabrics;
using Peppa.Models;
using Peppa.Services;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Category;
using Peppa.ViewModels.Diagram;
using Peppa.ViewModels.Donate;
using Peppa.ViewModels.Interface;
using Peppa.ViewModels.Operations;
using Peppa.Workers;

namespace Peppa.ViewModels
{
    public class MainViewModel : BaseViewModel, IToastViewModel
    {
        private const int TOTAL_COUNT_COSTS = 10;
        private const int DAY_REMINDER = 5;
        private MainViewModel()
        {
            Costs = new ObservableCollection<CostViewModel>();
            Categories = new ObservableCollection<CategoryViewModel>();
            DbWorker = DbWorker.Current;
            //Accounts = new AccountsViewModel();
            Diagram = new DiagramViewModel();
            Donate = new DonateViewModel();
        }

        public void Init()
        {
            IsInit = false;

            List<CategoryModel> categories = null;

            if (DbWorker.GetCategories().Count == 0)
            {
                categories = CategoryFactory.GetCategories().ToList();
                foreach (var category in categories)
                {
                    DbWorker.AddCategory(category);
                }
            }
            else
            {
                categories = DbWorker.GetCategories();
            }

            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            foreach (var cost in DbWorker.GetCosts().Take(TOTAL_COUNT_COSTS))
            {
                Costs.Add(new CostViewModel(cost));
            }

            IsInit = true;
        }

        public void Finit()
        {
        }

        public void ShowToast()
        {
            ToastContent content = ToastService.GenerateToastContent();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }

        public void SaveLastTimeShow()
        {
            SettingsWorker.Current.SaveLastTimeShow(DateTime.UtcNow);
        }

        public void UpdateData()
        {
            List<CategoryModel> categories = DbWorker.GetCategories();

            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            Costs.Clear();
            foreach (var cost in DbWorker.GetCosts().Take(TOTAL_COUNT_COSTS))
            {
                Costs.Add(new CostViewModel(cost));
            }

            Accounts.UpdateData();
        }

        internal async Task FetchCosts()
        {
            foreach (CostModel item in DbWorker.Current.GetCosts(Costs.Count))
            {
                await Task.Delay(600);

                await App.RunUIAsync(() =>
                {
                    Costs.Add(new CostViewModel(item));
                });
            }
        }

        #region Costs

        internal Task AddCost(CostViewModel newCost)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Costs.Insert(0, newCost);
                });

                // AccountViewModel currentAccount = Accounts.List.FirstOrDefault(b => b.Id == newCost.BalanceId);
                //
                // if (currentAccount != null)
                // {
                //     currentAccount.AddCost(newCost.Cost);
                //     DbWorker.UpdateBalance(currentAccount.Model);
                // }

                DbWorker.AddCost(newCost.Model);

                Accounts.RaiseBalance();
            });
        }

        internal Task UpdateCost(CostViewModel updateCost)
        {
            return Task.Factory.StartNew(() =>
            {
                updateCost.Update();

                if (updateCost.HavePrevCost)
                {
                    //TODO: o(n) - bad
                    // AccountViewModel currentAccount = Accounts.List.FirstOrDefault(b=> b.Id == updateCost.BalanceId);
                    //
                    // if(currentAccount != null)
                    // {
                    //     currentAccount.ChangeBalance(DbWorker.GetCost(updateCost.Id).Cost);
                    //     currentAccount.AddCost(updateCost.Cost);
                    //     DbWorker.UpdateBalance(currentAccount.Model);
                    // }
                    updateCost.HavePrevCost = false;
                }

                DbWorker.UpdateCost(updateCost.Model);
            });
        }

        internal Task DeleteCost(CostViewModel cost)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Costs.Remove(cost);
                });

                DbWorker.RemoveCost(cost.Model);
            });
        }
        #endregion

        #region Category

        internal Task AddCategory(CategoryViewModel newCategory)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Categories.Add(newCategory);
                });

                DbWorker.AddCategory(newCategory.Model);
            });
        }

        internal Task UpdateCategory(CategoryViewModel updateTag)
        {
            return Task.Factory.StartNew(() =>
            {
                updateTag.Update();
                DbWorker.UpdateCategory(updateTag.Model);
            });
        }

        internal Task DeleteCategory(CategoryViewModel category)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Categories.Remove(category);
                });

                DbWorker.RemoveCategory(category.Model);
            });
        }

        #endregion

        #region Balances

        internal Task AddBalance(AccountViewModel newAccount)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Accounts.List.Add(newAccount);
                });

                // DbWorker.AddBalance(newAccount.Model);
                // Accounts.RaiseBalance();
            });
        }

        internal Task UpdateBalance(AccountViewModel updateAccount)
        {
            return Task.Factory.StartNew(() =>
            {
                // updateAccount.Update();
                // DbWorker.UpdateBalance(updateAccount.Model);
            });
        }

        internal Task DeleteBalance(AccountViewModel account)
        {
            return Task.Factory.StartNew(() =>
            {
                App.RunUIAsync(() =>
                {
                    Accounts.List.Remove(account);
                });

                // DbWorker.RemoveBalance(account.Model);
                // Accounts.RaiseBalance();
            });
        }

        #endregion

        public bool IsInit { get; private set; }

        public ObservableCollection<CostViewModel> Costs { get; }

        public ObservableCollection<CategoryViewModel> Categories { get; }

        public AccountsViewModel Accounts { get; }

        public DiagramViewModel Diagram { get; }

        public DonateViewModel Donate { get; }

        public DbWorker DbWorker { get; }

        public bool CanShowToast => SettingsWorker.Current.GetNotificatinsSetting() && ((DateTime.UtcNow - SettingsWorker.Current.GetLastTimeShow())?.Days ?? DAY_REMINDER - 1) > DAY_REMINDER;

        public bool HaveCategories => Categories.GetEnumerator().MoveNext();

        public static MainViewModel Current = new MainViewModel();
    }
}
