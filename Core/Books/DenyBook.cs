using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vtex.Billing.Core.Books
{
    class DenyBook
    {
        private List<string> deniedList
        {
            get;
            private set;
        }


        public void insertAccount(string accountName)
        {
            throw new ArgumentException();
        }


        public void removeAccount(string accountName)
        {
            throw new ArgumentException();
        }

        public Boolean findAccount(string accountName)
        {
            throw new ArgumentException();
        }

    }
}
