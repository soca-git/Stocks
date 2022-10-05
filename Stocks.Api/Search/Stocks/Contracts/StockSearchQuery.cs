﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Stocks.Api.Search.Stocks.Contracts
{
    public class StockSearchQuery
    {
        /// <summary>
        /// Instrument's ticker symbol.
        /// </summary>
        /// <remarks>
        /// Example; "QCOM".
        /// </remarks>
        [BindRequired]
        public string TickerSymbol { get; set; }
    }
}
