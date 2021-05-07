using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TransactionStore.Core.CustomExceptions
{
    public class CurrencyRatesServiceException : Exception
    {
        public int StatusCode { get; private set; } 
        public string ErrorMessage { get; private set; }

        public CurrencyRatesServiceException()
        {
            StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            ErrorMessage = "Service unavailable"; // get errors from modelState and combine them into ErrorMessage
        }
    }
}
