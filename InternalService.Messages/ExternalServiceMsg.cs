using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace InternalService.Messages
{
	public class ExternalServiceMsg : IMessage
	{
		public string ServiceTransactionIdentifier { get; set; }
		public string Payload { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public enum ExternalReturnCode
	{
		OK = 0,
		Error = 1,
	}
}
