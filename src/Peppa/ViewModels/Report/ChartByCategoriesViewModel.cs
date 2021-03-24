﻿using System.Collections.Generic;

namespace Peppa.ViewModels.Report
{
    public class ChartByCategoriesViewModel
    {
        public ChartByCategoriesViewModel(string title)
        {
            Title = title;
            Data = new List<DataDiagramViewModel>();
        }
        
        public string  Title { get; set; }
        public List<DataDiagramViewModel> Data { get; set; }
    }
}