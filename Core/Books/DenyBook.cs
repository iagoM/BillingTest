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
        private List<string> deniedList;

        public List<string> DeniedList
        {
            get{ return deniedList;}

            private set { deniedList = value; }
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


        public DenyBook()
        {
            this.DeniedList = new List<string>();
            UnchargeableRules();
        }


    }
}
