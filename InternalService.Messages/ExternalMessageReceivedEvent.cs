using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace InternalService.Messages
{
	public interface IExternalMessageReceivedEvent : IMessage
	{
		string ServiceTransactionIdentifier { get; set; }
		string Payload { get; set; }
		DateTime Timestamp { get; set; }
	}
}
