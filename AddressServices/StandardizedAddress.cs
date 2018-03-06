using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERS.AddressServices
{
	public class StandardizedAddress : ParsedAddress, IAddress
	{
		public string Company { get; set; }

		public string Street { get; set; }

		public string StreetWithoutSuiteNumber { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }

		public int CountyID { get; set; }

		public string CountyName { get; set; }

		public string CountyFIPS { get; set; }

		public string ErrorOccurred { get; set; }

		public string ErrorMessage { get; set; }

		public string MelissaErrorCode { get; set; }

		public string MelissaStatusCode { get; set; }

		public string MelissaAddressWashConfidence { get; set; }

		public string MelissaAddressWashStatusText { get; set; }

		public string MelissaAddressWashAdditionalInfo { get; set; }

		public string MelissaAddressWashResults { get; set; }

		public int HorizontalCollectionMethodID { get; set; }

		public int HorizontalReferenceDatumID { get; set; }

		public int GeographicReferencePointID { get; set; }

		public DateTime DataCollectionDate { get; set; }

		public bool MelissaAddressWashSucceeded { get; set; }
	}
}