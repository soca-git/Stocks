﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Stocks.Api.Common.Contracts
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrencyCode
    {
        XXX,
        EUR,
        GBP,
        USD
    }
}
