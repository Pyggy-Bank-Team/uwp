using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.Services.Windows;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Diagram
{
    public class DiagramViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IReportModel _model;

        public DiagramViewModel(IReportModel model)
        {
            _model = model;
            Datas = new List<DataDiagramViewModel>();
        }

        public async Task Initialization()
        {
            var items = await _model.GetChartByCategories(CategoryType.Expense, DateTime.Now.AddYears(-1), DateTime.Now, GetCancellationToken());
            if (items != null)
            {
                Datas = items.Select(d => new DataDiagramViewModel
                {
                    Value = d.Amount,
                    Color = d.CategoryHexColor,
                    Title = d.CategoryTitle
                }).ToList();
            }
        }

        public void Finalization()
        {

        }

        public void ApplyFilter(DateTime startDate, DateTime endDate)
        {
            //var costs = DbWorker.Current.GetCosts().Where(c => DateUtility.GetLocalTimeFromUTCMilliseconds(c.Date) > startDate && DateUtility.GetLocalTimeFromUTCMilliseconds(c.Date) < endDate);

            Datas.Clear();

            //foreach (var category in MainViewModel.Current.Categories)
            //{
            //    var tempCosts = costs.Where(c => c.CategoryId == category.Id);

            //    if (tempCosts.Count() > 0)
            //    {
            //        double sumInCategory = tempCosts.Sum(c => c.Cost);
            //        Datas.Add(
            //            new DataDiagramViewModel
            //            {
            //                Value = (sumInCategory / AllCosts) * 100,
            //                Color = category.Color,
            //                Title = category.Title
            //            });
            //    }
            //}
        }

        public Task UpdateTile()
        {
            return Task.Factory.StartNew(() =>
            {
                if (Datas.Count == 0)
                    return;

                TileContent content = TileService.GenerateTileContent(Datas);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(content.GetXml()));
            });
        }

        public double AllCosts { get; private set; }

        public List<DataDiagramViewModel> Datas { get; set; }

        public bool IsEmpty
        {
            get
            {
                return Datas.Count == 0;
            }
        }
    }
}
