using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using log4net;
using NServiceBus.Saga;
using InternalService.Messages;

namespace InternalService
{
	public class ReceivingSaga : Saga<ExternalServiceSagaData>,
		IAmStartedByMessages<ExternalServiceMsg>
	{

		public override void ConfigureHowToFindSaga()
		{
			ConfigureMapping<ExternalServiceMsg>(
				data => data.ServiceTransactionIdentifier, // Match this property on the saga data
				msg => msg.ServiceTransactionIdentifier);  // with this property on the incoming message
		}

		public void Handle(ExternalServiceMsg msg)
		{
			/* As established in the WebServiceEndpoint.asmx.cs comments, it's possible to
			 * receive duplicate messages from the web service. Ways to handle duplicates
			 * vary depending on business requirements, but for this example we'll assume
			 * that if we receive a message with the same ServiceTransactionIdentifier
			 * property value within 5 minutes, we should just ignore it.
			 */

			if (Data.LastSeen == DateTime.MinValue)
			{
				// This is the first message received, so set Saga data.
				Data.ServiceTransactionIdentifier = msg.ServiceTransactionIdentifier;
				Data.Payload = msg.Payload;
				Data.LastSeen = msg.Timestamp;

				// Publish an event for consumption by other subscribing services.
				Bus.Publish<IExternalMessageReceivedEvent>(e =>
					{
						e.ServiceTransactionIdentifier = msg.ServiceTransactionIdentifier;
						e.Payload = msg.Payload;
						e.Timestamp = msg.Timestamp;
					}
				);
			}

			// If we request a new timeout on every message received, it results in a 
			// sliding 5-minute window. Every time we request a timeout, it clears the
			// previous timeout for this Saga, so we won't get multiple notifications.
			RequestUtcTimeout(TimeSpan.FromMinutes(5), null);
		}

		public override void Timeout(object state)
		{
			// When the 5 minutes are up, we will throw away the Saga data, and assume
			// that any new message coming in with the same transaction identifier
			// should be processed as new.
			MarkAsComplete();
		}

	}
}
