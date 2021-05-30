using System;

namespace TransactionStore.Core.Settings
{
    public class AppSettings
    {
        public string TSTORE_CONNECTION_STRING { get; set; }
        public string TSTORE_ALLOWED_IPADDRESS { get; set; }
    }
}