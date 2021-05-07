using System;
using System.Collections.Generic;
using System.Text;

namespace EventContracts

{
    public interface CurrencyRates
    {
        Dictionary<string, decimal> Value { get; }
    }
}
