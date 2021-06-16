using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.Notifications;
using Peppa.ViewModels.Reports;

namespace Peppa.Services.Windows
{
    public static class TileService
    {
        public static TileContent GenerateTileContent(List<TelerikItemReportViewModel> datas)
        {
            return new TileContent()
            {
                Visual = new TileVisual()
                {
                    DisplayName = "PiggyBank",
                    TileMedium = GenerateTileBinding(datas),
                    TileWide = GenerateTileBinding(datas),
                    TileLarge = GenerateTileBinding(datas)
                }
            };
        }

        private static TileBinding GenerateTileBinding(List<TelerikItemReportViewModel> datas)
        {
            TileBinding binding = new TileBinding { DisplayName = "PiggyBank" };
            TileBindingContentAdaptive content = new TileBindingContentAdaptive();

            foreach (var data in datas)
            {
                content.Children.Add(new AdaptiveText
                {
                    Text = String.Format("{0} {1:##}%", data.Title, data.Value),
                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                });
            }

            binding.Content = content;

            return binding;
        }
    }
}
