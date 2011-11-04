using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using log4net;
using InternalService.Messages;

namespace Subscriber1
{
	public class EventHandler : IHandleMessages<IExternalMessageReceivedEvent>
	{
		public void Handle(IExternalMessageReceivedEvent message)
		{
			Console.WriteLine();
			Console.WriteLine("Received Event IExternalMessageReceivedEvent on Subscriber1");
			Console.WriteLine("  ServiceTransactionIdentifier: {0}", message.ServiceTransactionIdentifier);
			Console.WriteLine("  Payload: {0}", message.Payload);
			Console.WriteLine("  Timestamp: {0}", message.Timestamp);
		}
	}
}
