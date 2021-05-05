using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TransactionStore.API.Attributes
{
    public class CustomAccountsValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<int> ids = (List<int>)value;
            List<int> compareIds = new List<int> { 0 };
            for(int i = 0; i < ids.Count; i++)
            {
                if(ids[i] <= 0 || compareIds.Contains(ids[i])) return false;
                compareIds.Add(ids[i]);
            }
            return true;
        }
    }
}
