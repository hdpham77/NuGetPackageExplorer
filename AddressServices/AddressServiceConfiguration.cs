using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CERS.AddressServices
{
	public class AddressServiceConfiguration : ServiceConfiguration
	{
		public string CityZipValidationResultEndpointUri
		{
			get
			{
				return BaseUri + "VerifyCityByZip";
			}
		}

		public string AddressInformationEndpointUri
		{
			get
			{
				return BaseUri + "GetAddressInformation";
			}
		}

		public string StandardizeBusinessNameEndpointUri
		{
			get
			{
				return BaseUri + "StandardizeBusinessName";
			}
		}

		public string ParseAddressEndpointUri
		{
			get
			{
				return BaseUri + "ParseAddress";
			}
		}

		public string StandardizedAddressEndpointUri
		{
			get
			{
				return BaseUri + "GetStandardizedAddress";
			}
		}

        // web service request timeout
        public int Timeout { get; set; }

	}
}