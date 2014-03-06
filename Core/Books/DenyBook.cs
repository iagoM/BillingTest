using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vtex.Billing.Core.Books
{
    public class DenyBook
    {
        private ConcurrentBag<string> deniedList;

        public ConcurrentBag<string> DeniedList
        {
            get{ return deniedList;}

            private set { deniedList = value; }
        }

        public DenyBook()
        {
            this.DeniedList = new ConcurrentBag<string>();
            UnchargeableRules();
        }

        private void UnchargeableRules()
        {
            deniedList.Add("automacaoqa");
            deniedList.Add("basedevmkp");
            deniedList.Add("walmartv5");
            deniedList.Add("ambienteqa");
            deniedList.Add("personalhomolog");
            deniedList.Add("vtexbeta");
            deniedList.Add("walteste");
        }
    }
}
