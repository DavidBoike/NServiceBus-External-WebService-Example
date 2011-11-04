using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.Saga;

namespace InternalService.Messages
{
	public class ExternalServiceSagaData : IContainSagaData
	{
		public string ServiceTransactionIdentifier { get; set; }
		public string Payload { get; set; }
		public DateTime LastSeen { get; set; }

		// These properties used by the Saga infrastructure
		public Guid Id { get; set; }
		public string OriginalMessageId { get; set; }
		public string Originator { get; set; }
	}
}
