﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vtex.Billing.Core.Books;

namespace Vtex.Billing.Core.Data
{
	public class BillingAccountManager
	{
		public class LicenseManagerDatum
		{
			public string Id { get; private set; }
			public string MainAccountName { get; private set; }
			public string LV { get; private set; }
			public bool IsRemoved { get; private set; }
			public bool IsActive { get; private set; }
			public bool IsOperating { get; set; }

			public LicenseManagerDatum(JToken token)
			{
				this.Id = token["Id"].Value<string>();
				this.MainAccountName = token["MainAccountName"].Value<string>();
				this.LV = token["LV"].Value<string>();
				this.IsRemoved = token["IsRemoved"].Value<bool>();
				this.IsActive = token["IsActive"].Value<bool>();
				this.IsOperating = token["IsOperating"].Value<bool>();
			}
		}

		public static readonly BillingAccountManager Instance = new BillingAccountManager();

        //TODO: public BindingList<BillingAccountSummary> Summary = new BindingList<BillingAccountSummary>();

		private ConcurrentDictionary<string, LicenseManagerDatum> LicenseManagerData = new ConcurrentDictionary<string, LicenseManagerDatum>();
		private ConcurrentBag<string> UnchargeableAccounts = new ConcurrentBag<string>();

		private BillingAccountManager() { }

		private bool IsValidAccount(LicenseManagerDatum licenseManagerDatum)
		{
			return licenseManagerDatum.IsActive && licenseManagerDatum.IsOperating && !licenseManagerDatum.IsRemoved && !this.UnchargeableAccounts.Contains(licenseManagerDatum.MainAccountName);
		}

		public void Warmup()
		{
			WarmupLicenseManager();
            WarmupUnchargeableAccounts();
		}

		private void WarmupLicenseManager()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("x-vtex-api-appKey", "vtexappkey-appvtex");
			client.DefaultRequestHeaders.Add("x-vtex-api-appToken", "QOLGVNXYIALKLOFANGJQOYTJEPQQGDSNDQCHQKIUXUWQYSWCSIHXYCJRUCCGXGRFIYHULYJCYOTTBCKMSNYYVUOAGLMNIBXRCRIAKCRUYFZJJPPRRPOKITJOJGNALVUW");
			HttpResponseMessage responseMessage = client.GetAsync("http://licensemanager.vtex.com.br/api/license-manager/pvt/accounts").Result;
			string data = responseMessage.Content.ReadAsStringAsync().Result;

			Parallel.ForEach(
				JArray.Parse(data),
				token =>
				{
					LicenseManagerDatum datum = new LicenseManagerDatum(token);
					LicenseManagerData.AddOrUpdate(datum.Id, datum, (k, v) => v);
                    
				});
		}

		private void WarmupUnchargeableAccounts()
		{
			//TODO: Buscar a lista de contas que nao devem ser cobradas

            this.UnchargeableAccounts = new DenyBook().DeniedList;

            foreach (var datum in LicenseManagerData)
            {
                LicenseManagerDatum removedItem;

                if ( ! this.IsValidAccount(datum.Value ) )
                {
                    LicenseManagerData.TryRemove(datum.Key, out removedItem);
                }

            }

		}

		private string GetAccountFromName(string name)
		{
			return LicenseManagerData.Where(a => a.Value.MainAccountName.Equals(name)).Select(a => a.Key).FirstOrDefault();
		}


        public void TestGetAllAccounts()
        {
            WarmupLicenseManager();

            List<string> lines = new List<string>();
            

            foreach (var datum in LicenseManagerData)
            {
                lines.Add( "ID: " + datum.Value.Id +"   |   MainNameAccount: "+ datum.Value.MainAccountName +"   |  LV:" + datum.Value.LV) ;
                
                
            }
            System.IO.File.WriteAllLines("C:\\Users\\igMoreira\\Desktop\\teste2.txt", lines.ToArray() );
            
        }

        public void TestGetAllChargeableAccount()
        {

            WarmupLicenseManager();
            WarmupUnchargeableAccounts();

            List<string> lines = new List<string>();


            foreach (var datum in LicenseManagerData)
            {
                lines.Add("ID: " + datum.Value.Id + "   |   MainNameAccount: " + datum.Value.MainAccountName + "   |  LV:" + datum.Value.LV);


            }
            System.IO.File.WriteAllLines("C:\\Users\\igMoreira\\Desktop\\teste.txt", lines.ToArray());

        }
	}
}