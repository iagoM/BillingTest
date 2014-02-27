using System;
using Vtex.Billing.Core.Interfaces;

namespace Vtex.Billing.Core.Data
{
    public class OMSData : IOMSData
    {
        public decimal Value
        {
            get;
            private set;
        }

        public Guid AccountId
        {
            get;
            private set;
        }

        public string AccountName
        {
            get;
            private set;
        }

        public OMSData(Guid accountId, decimal value)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException();

            if (value < 0)
                throw new ArgumentOutOfRangeException();

            this.AccountId = accountId;
            this.Value = value;
            //TODO: Setar valor para AccountName
        }
    }
}