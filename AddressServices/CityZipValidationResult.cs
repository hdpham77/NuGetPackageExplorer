using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CERS.AddressServices
{
	public class CityZipValidationResult
	{
		public bool ErrorsOccurred { get; set; }

		public string CityName { get; set; }

		public bool ZipCodeFound { get; set; }

		public bool CityInState { get; set; }

		public bool CityFound { get; set; }

		public bool CityProvidedMatchesCityFound { get; set; }

		public string Message { get; set; }

		public string EndpointUri { get; set; }

		public CityZipValidationResult()
		{
			ErrorsOccurred = true;
			CityFound = false;
			CityInState = false;
			ZipCodeFound = false;
			CityProvidedMatchesCityFound = false;
		}
	}
}