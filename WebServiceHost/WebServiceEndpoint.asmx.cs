using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NServiceBus;
using InternalService.Messages;

namespace WebServiceHost
{
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	public class ExternalWebService : WebService
	{
		[WebMethod]
		public void Process(string transactionID, string payload)
		{
			// Translate the incoming web request into a message
			ExternalServiceMsg msg = new ExternalServiceMsg();
			msg.ServiceTransactionIdentifier = transactionID;
			msg.Payload = payload;
			msg.Timestamp = DateTime.UtcNow;

			// Send the message on the Bus
			Global.Bus.Send(msg);

			/* Because the web application is not transactional, as soon as we call
			 * Bus.Send(), the message is sent on the Bus and cannot be recalled.
			 * 
			 * Since this is the end of the WebMethod, the web service will return
			 * a 200 OK status code to the client.  This will inform the calling
			 * client that we successfully received and have taken ownership of
			 * the message.
			 * 
			 * HOWEVER, it is still possible for a server crash or network
			 * interference to prevent the 200 OK status code from being received
			 * by the client.  This can cause the client to resend duplicate
			 * messages. This is why we need to ensure that our receiver
			 * (ReceivingSaga.cs) is capable of gracefully handling duplicate messages.
			 */
		}
	}
}
