using System;

namespace Vtex.Billing.Core.Interfaces
{
    public interface IOMSData
    {
        decimal Value { get; }
        Guid AccountId { get; }
        string AccountName { get; }
    }
}