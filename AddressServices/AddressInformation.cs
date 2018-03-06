using CERS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERS.AddressServices
{
	public class AddressInformation : StandardizedAddress
	{
		public string AdminDistrict { get; set; }

		public string AdminDistrict2 { get; set; }

		public string BingConfidence { get; set; }

		public string BingFormattedAddress { get; set; }

		public string BingMatchCode { get; set; }

		public string BingStatusCode { get; set; }

		public string BingStatusDescription { get; set; }

		public string CountryRegion { get; set; }

		public int HorizontalAccuracyMeasure { get; set; }

		public decimal Latitude { get; set; }

		public string Locality { get; set; }

		public decimal Longitude { get; set; }
	}
}