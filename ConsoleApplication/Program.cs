using System;
using Vtex.Billing.Core.Data;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            OMSData data = new OMSData(Guid.NewGuid(), 10);
            string s = data.AccountName;
        }
    }
}
