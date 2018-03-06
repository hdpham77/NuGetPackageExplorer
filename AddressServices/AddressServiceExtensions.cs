using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CERS.AddressServices
{
	public static class AddressServiceExtensions
	{
		public static AddressInformation ToAddressInformation( this XElement element, string endPointUri )
		{
			XNamespace ns = element.GetDefaultNamespace();
			var addressInformation = new AddressInformation();
			addressInformation.Message = "Result";
			addressInformation.Company = element.Element( ns + "Company" ).Value;
			addressInformation.Street = element.Element( ns + "Street" ).Value;
			addressInformation.StreetWithoutSuiteNumber = element.Element( ns + "StreetWithoutSuiteNumber" ).Value;
			addressInformation.City = element.Element( ns + "City" ).Value;
			addressInformation.State = element.Element( ns + "State" ).Value;
			addressInformation.ZipCode = element.Element( ns + "Zip" ).Value;
			addressInformation.CountyName = element.Element( ns + "CountyName" ).Value;
			addressInformation.CountyFIPS = element.Element( ns + "CountyFIPS" ).Value;
			//addressInformation.CountyID = element.Element(ns + "CountyID").Value;
			addressInformation.ErrorOccurred = element.Element( ns + "ErrorOccurred" ).Value;
			addressInformation.ErrorMessage = element.Element( ns + "ErrorMessage" ).Value;
			addressInformation.MelissaErrorCode = element.Element( ns + "MelissaErrorCode" ).Value;
			addressInformation.MelissaStatusCode = element.Element( ns + "MelissaStatusCode" ).Value;
			addressInformation.MelissaAddressWashConfidence = element.Element( ns + "MelissaAddressWashConfidence" ).Value;
			addressInformation.MelissaAddressWashStatusText = element.Element( ns + "MelissaAddressWashStatusText" ).Value;
			addressInformation.MelissaAddressWashAdditionalInfo = element.Element( ns + "MelissaAddressWashAdditionalInfo" ).Value;
			addressInformation.Latitude = Convert.ToDecimal( element.Element( ns + "Latitude" ).Value );
			addressInformation.Longitude = Convert.ToDecimal( element.Element( ns + "Longitude" ).Value );
			addressInformation.HorizontalAccuracyMeasure = Convert.ToInt32( element.Element( ns + "HorizontalAccuracyMeasure" ).Value );
			addressInformation.BingConfidence = element.Element( ns + "BingConfidence" ).Value;
			addressInformation.BingMatchCode = element.Element( ns + "BingMatchCode" ).Value;
			addressInformation.BingFormattedAddress = element.Element( ns + "BingFormattedAddress" ).Value;
			addressInformation.AdminDistrict = element.Element( ns + "AdminDistrict" ).Value;
			addressInformation.AdminDistrict2 = element.Element( ns + "AdminDistrict2" ).Value;
			addressInformation.CountryRegion = element.Element( ns + "CountryRegion" ).Value;
			addressInformation.Locality = element.Element( ns + "Locality" ).Value;
			addressInformation.BingStatusCode = element.Element( ns + "BingStatusCode" ).Value;
			addressInformation.BingStatusDescription = element.Element( ns + "BingStatusDescription" ).Value;
			addressInformation.Range = element.Element( ns + "Range" ).Value;
			addressInformation.Suite = element.Element( ns + "Suite" ).Value;
			addressInformation.SuiteName = element.Element( ns + "SuiteName" ).Value;
			addressInformation.Suffix = element.Element( ns + "Suffix" ).Value;
			addressInformation.PostDirection = element.Element( ns + "PostDirection" ).Value;
			addressInformation.StreetName = element.Element( ns + "StreetName" ).Value;
			addressInformation.PreDirection = element.Element( ns + "PreDirection" ).Value;
			addressInformation.GeographicReferencePointID = Convert.ToInt32( element.Element( ns + "GeographicReferencePointID" ).Value );
			addressInformation.HorizontalReferenceDatumID = Convert.ToInt32( element.Element( ns + "HorizontalReferenceDatumID" ).Value );
			addressInformation.HorizontalCollectionMethodID = Convert.ToInt32( element.Element( ns + "HorizontalCollectionMethodID" ).Value );
			addressInformation.DataCollectionDate = Convert.ToDateTime( element.Element( ns + "DataCollectionDate" ).Value );
			addressInformation.EndpointUri = endPointUri;
			if ( element.Element( ns + "MelissaAddressWashSuceeded" ) != null )
			{
				addressInformation.MelissaAddressWashSucceeded = Convert.ToBoolean( element.Element( ns + "MelissaAddressWashSuceeded" ).Value );
			}
			else
			{
				addressInformation.MelissaAddressWashSucceeded = true;
			}
			return addressInformation;
		}

		public static CityZipValidationResult ToCityZipValidationResult( this XElement element, string endPointUri )
		{
			var cityZipValidationResult = new CityZipValidationResult();

			XNamespace ns = element.GetDefaultNamespace();
			cityZipValidationResult.ErrorsOccurred = Convert.ToBoolean( element.Element( ns + "ErrorsOccurred" ).Value );
			cityZipValidationResult.CityFound = Convert.ToBoolean( element.Element( ns + "CityFound" ).Value );
			cityZipValidationResult.CityInState = Convert.ToBoolean( element.Element( ns + "CityInState" ).Value );
			cityZipValidationResult.ZipCodeFound = Convert.ToBoolean( element.Element( ns + "ZipCodeFound" ).Value );
			cityZipValidationResult.CityProvidedMatchesCityFound = Convert.ToBoolean( element.Element( ns + "CityProvidedMatchesCityFound" ).Value );
			cityZipValidationResult.CityName = element.Element( ns + "CityName" ).Value;
			cityZipValidationResult.Message = element.Element( ns + "Message" ).Value;
			cityZipValidationResult.EndpointUri = endPointUri;

			return cityZipValidationResult;
		}

		public static ParsedAddress ToParsedAddress( this XElement element, string endPointUri )
		{
			XNamespace ns = element.GetDefaultNamespace();
			var parsedAddress = new ParsedAddress();
			parsedAddress.Message = "Result";
			parsedAddress.Range = element.Element( ns + "Range" ).Value;
			parsedAddress.Suite = element.Element( ns + "Suite" ).Value;
			parsedAddress.SuiteName = element.Element( ns + "SuiteName" ).Value;
			parsedAddress.Suffix = element.Element( ns + "Suffix" ).Value;
			parsedAddress.PostDirection = element.Element( ns + "PostDirection" ).Value;
			parsedAddress.StreetName = element.Element( ns + "StreetName" ).Value;
			parsedAddress.PreDirection = element.Element( ns + "PreDirection" ).Value;
			parsedAddress.EndpointUri = endPointUri;
			return parsedAddress;
		}

		public static StandardizedAddress ToStandardizedAddress( this XElement element, string endPointUri )
		{
			var standardizedAddress = new StandardizedAddress();
			XNamespace ns = element.GetDefaultNamespace();
			standardizedAddress.Message = "Result";
			standardizedAddress.Company = element.Element( ns + "Company" ).Value;
			standardizedAddress.Street = element.Element( ns + "Street" ).Value;
			standardizedAddress.City = element.Element( ns + "City" ).Value;
			standardizedAddress.State = element.Element( ns + "State" ).Value;
			standardizedAddress.ZipCode = element.Element( ns + "Zip" ).Value;
			standardizedAddress.CountyName = element.Element( ns + "CountyName" ).Value;
			standardizedAddress.CountyFIPS = element.Element( ns + "CountyFIPS" ).Value;
			//standardizedAddress.CountyID = element.Element(ns + "CountyID").Value;
			standardizedAddress.ErrorOccurred = element.Element( ns + "ErrorOccurred" ).Value;
			standardizedAddress.ErrorMessage = element.Element( ns + "ErrorMessage" ).Value;
			standardizedAddress.MelissaErrorCode = element.Element( ns + "MelissaErrorCode" ).Value;
			standardizedAddress.MelissaStatusCode = element.Element( ns + "MelissaStatusCode" ).Value;
			standardizedAddress.MelissaAddressWashConfidence = element.Element( ns + "MelissaAddressWashConfidence" ).Value;
			standardizedAddress.MelissaAddressWashStatusText = element.Element( ns + "MelissaAddressWashStatusText" ).Value;
			standardizedAddress.MelissaAddressWashAdditionalInfo = element.Element( ns + "MelissaAddressWashAdditionalInfo" ).Value;
			standardizedAddress.MelissaAddressWashResults = element.Element( ns + "MelissaAddressWashResults" ).Value;
			standardizedAddress.Range = element.Element( ns + "Range" ).Value;
			standardizedAddress.Suite = element.Element( ns + "Suite" ).Value;
			standardizedAddress.SuiteName = element.Element( ns + "SuiteName" ).Value;
			standardizedAddress.Suffix = element.Element( ns + "Suffix" ).Value;
			//standardizedAddress.CountyID = element.Element(ns + "CountyID").Value;
			standardizedAddress.PostDirection = element.Element( ns + "PostDirection" ).Value;
			standardizedAddress.StreetName = element.Element( ns + "StreetName" ).Value;
			standardizedAddress.PreDirection = element.Element( ns + "PreDirection" ).Value;
			standardizedAddress.GeographicReferencePointID = Convert.ToInt32( element.Element( ns + "GeographicReferencePointID" ).Value );
			standardizedAddress.HorizontalReferenceDatumID = Convert.ToInt32( element.Element( ns + "HorizontalReferenceDatumID" ).Value );
			standardizedAddress.HorizontalCollectionMethodID = Convert.ToInt32( element.Element( ns + "HorizontalCollectionMethodID" ).Value );
			standardizedAddress.DataCollectionDate = Convert.ToDateTime( element.Element( ns + "DataCollectionDate" ).Value );
			standardizedAddress.EndpointUri = endPointUri;
			if ( element.Element( ns + "MelissaAddressWashSuceeded" ) != null )
			{
				standardizedAddress.MelissaAddressWashSucceeded = Convert.ToBoolean( element.Element( ns + "MelissaAddressWashSuceeded" ).Value );
			}
			else
			{
				standardizedAddress.MelissaAddressWashSucceeded = true;
			}

			return standardizedAddress;
		}

		public static StandardizedBusinessName ToStandardizedBusinessName( this XElement element, string endPointUri )
		{
			var standardizedBusinessName = new StandardizedBusinessName();
			XNamespace ns = element.GetDefaultNamespace();
			standardizedBusinessName.Message = "Result";
			standardizedBusinessName.BusinessName = element.Value;
			standardizedBusinessName.EndPointUri = endPointUri;
			return standardizedBusinessName;
		}
	}
}