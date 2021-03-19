﻿using System;
using Peppa.Enums;

namespace Peppa.Contracts.Requests
{
    public class GetChartByCategoriesRequest
    {
        public CategoryType Type { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}