using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using log4net.Appender;
using log4net.Core;

namespace InternalService
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
	{

	}
}
