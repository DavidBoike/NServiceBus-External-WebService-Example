using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.Write("Enter Transaction Identifier: ");
				string transactionID = Console.ReadLine();
				Console.Write("Enter Payload: ");
				string payload = Console.ReadLine();

				using (TestClient.WebServiceProxy.ExternalWebService svc = new WebServiceProxy.ExternalWebService())
				{
					Console.WriteLine("Invoking web service {0}", svc.Url);
					try
					{
						svc.Process(transactionID, payload);
						Console.WriteLine("Success");
					}
					catch (Exception x)
					{
						Console.WriteLine(x.Message);
					}
				}
			}
		}

		
	}
}
