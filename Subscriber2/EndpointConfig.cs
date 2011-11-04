using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using log4net.Appender;
using log4net.Core;

namespace Subscriber2
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
	{

	}
}
