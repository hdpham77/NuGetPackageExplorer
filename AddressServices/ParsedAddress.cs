using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERS.AddressServices
{
	public class ParsedAddress
	{
		public string Message { get; set; }

		public string Range { get; set; }

		public string Suite { get; set; }

		public string SuiteName { get; set; }

		public string Suffix { get; set; }

		public string PostDirection { get; set; }

		public string StreetName { get; set; }

		public string PreDirection { get; set; }

		public string EndpointUri { get; set; }
	}
}