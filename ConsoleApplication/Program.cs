using System;
using Vtex.Billing.Core.Data;
using Vtex.Billing.Core.Books;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //OMSData data = new OMSData(Guid.NewGuid(), 10);
            //string s = data.AccountName

            TestDenyBook();

        }

        static void TestDenyBook()
        {

            DenyBook teste = new DenyBook();

            foreach (string item in teste.DeniedList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();

        }

    }
}
