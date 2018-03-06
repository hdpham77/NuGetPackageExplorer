using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using UPF;
using CERS.Configuration;

namespace CERS.AddressServices
{
	public class AddressServiceProxy
	{
		#region Fields

		private AddressServiceConfiguration _Config;

		#endregion Fields

		#region Properties

		public AddressServiceConfiguration Configuration
		{
			get
			{
				if ( _Config == null )
				{
					_Config = ServiceConfiguration.GetConfiguration<AddressServiceConfiguration>( ServiceType.Address );

                    // get the config parameters specific for AddressServiceProxy
                    var current = CERSConfigurationSection.Current;
                    var serviceConfigElement = current.ServiceExtensions.Services[ServiceType.Address];
                    _Config.Timeout = serviceConfigElement.Timeout;
				}
				return _Config;
			}
		}

		#endregion Properties

		#region Constructor

		public AddressServiceProxy()
		{
		}

		#endregion Constructor

		#region Service Call Methods

		#region StandardizeAddress Method Service Call

		public virtual StandardizedAddress StandardizeAddress( string street, string city, string state, string zipCode )
		{
            string endpointUri = "";
			StandardizedAddress result = new StandardizedAddress { Street = street, City = city, State = state, ZipCode = zipCode, MelissaAddressWashSucceeded = false, ErrorOccurred = "true" };
			try
			{
				endpointUri = BuildStandarizedAddressEndpointUri( street, city, state, zipCode );
				XElement xml = GetXml( endpointUri );
				result = xml.ToStandardizedAddress( endpointUri );
                if ( String.IsNullOrWhiteSpace( result.CountyName ) )
                {
                    result.CountyID = LookupCountyID( zipCodeNumber: zipCode );
                }
                else
                {
                    result.CountyID = LookupCountyID( countyName: result.CountyName );
                }
			}
			catch ( Exception ex )
			{
				using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
				{
					ICERSSystemServiceManager services = ServiceLocator.GetSystemServiceManager( repo );
                    services.ErrorReporting.Report( StandardizedAddressErrorMessage( "AddressServiceProxy::StandardizeAddress", endpointUri, ex ), ex );
				}
			}
			return result;
		}

		#endregion StandardizeAddress Method Service Call

		#region StandardizeBusinesssName Service Call

		public virtual StandardizedBusinessName StandardizeBusinessName( string businessName )
		{
            string endpointUri = "";
			StandardizedBusinessName result = new StandardizedBusinessName { BusinessName = businessName, EndPointUri = "", Message = "" };
			try
			{
				endpointUri = BuildStandardizedBusinessNameEndpointUri( businessName );
				XElement xml = GetXml( endpointUri );
				result = xml.ToStandardizedBusinessName( endpointUri );
			}
			catch ( Exception ex )
			{
				using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
				{
					ICERSSystemServiceManager services = ServiceLocator.GetSystemServiceManager( repo );
					services.ErrorReporting.Report( StandardizedAddressErrorMessage( "AddressServiceProxy::StandardizedBusinessName", endpointUri, ex ), ex );
				}
			}
			return result;
		}

		#endregion StandardizeBusinesssName Service Call

		#region AddressInformation Service Call

		public AddressInformation AddressInformation( string businessName, string street, string city, string state, string zipCode )
		{
            string endpointUri = "";
			AddressInformation result = new AddressInformation { Company = businessName, Street = street, City = city, ZipCode = zipCode, MelissaAddressWashSucceeded = false, ErrorOccurred = "true", };
			try
			{
				endpointUri = BuildAddressInformationEndpointUri( businessName, street, city, state, zipCode );
				XElement xml = GetXml( endpointUri );
				result = xml.ToAddressInformation( endpointUri );
                if ( String.IsNullOrWhiteSpace( result.CountyName ) )
                {
                    result.CountyID = LookupCountyID( zipCodeNumber: zipCode );
                }
                else
                {
                    result.CountyID = LookupCountyID( countyName: result.CountyName );
                }

			}
			catch ( Exception ex )
			{
				using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
				{
					ICERSSystemServiceManager services = ServiceLocator.GetSystemServiceManager( repo );
					services.ErrorReporting.Report( StandardizedAddressErrorMessage( "AddressServiceProxy::AddressInformation", endpointUri, ex ), ex );
				}
			}
			return result;
		}

		#endregion AddressInformation Service Call

		#region ParseAddress Service Call

		public ParsedAddress ParseAddress( string street )
		{
            string endpointUri = "";
			ParsedAddress result = new ParsedAddress();
			try
			{
				endpointUri = BuildParseAddressEndpointUri( street );
				XElement xml = GetXml( endpointUri );
				result = xml.ToParsedAddress( endpointUri );
			}
			catch ( Exception ex )
			{
				using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
				{
					ICERSSystemServiceManager services = ServiceLocator.GetSystemServiceManager( repo );
					services.ErrorReporting.Report( StandardizedAddressErrorMessage( "AddressServiceProxy::ParseAddress", endpointUri, ex ), ex );
				}
			}
			return result;
		}

		#endregion ParseAddress Service Call

		#region VerifyCityByZip

		public virtual CityZipValidationResult VerifyCityByZip( string city, string state, string zip )
		{
            string endpointUri = "";
			CityZipValidationResult result = new CityZipValidationResult { CityFound = false, ErrorsOccurred = true, ZipCodeFound = false };

			try
			{
				endpointUri = BuildVerifyCityByZipEndpointUri( city, state, zip );
				XElement xml = GetXml( endpointUri );
				result = xml.ToCityZipValidationResult( endpointUri );
			}
			catch ( Exception ex )
			{
				using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
				{
					ICERSSystemServiceManager services = ServiceLocator.GetSystemServiceManager( repo );
					services.ErrorReporting.Report( StandardizedAddressErrorMessage( "AddressServiceProxy::VerifyCityByZip", endpointUri, ex ), ex );
				}
			}
			return result;
		}

		#endregion VerifyCityByZip

		#endregion Service Call Methods

		#region Build Endpoint Uri Methods

		protected virtual string BuildAddressInformationEndpointUri( string businessName, string street, string city, string state, string zipCode )
		{
			string endpointUri = Configuration.AddressInformationEndpointUri;
			return string.Format( endpointUri + "?co={0}&s={1}&c={2}&st={3}&z={4}", UrlEncode( businessName ), UrlEncode( street ), UrlEncode( city ), UrlEncode( state ), UrlEncode( zipCode ) );
		}

		protected virtual string BuildParseAddressEndpointUri( string street )
		{
			string endpointUri = Configuration.ParseAddressEndpointUri;
			return string.Format( endpointUri + "?s={0}", UrlEncode( street ) );
		}

		protected virtual string BuildStandardizedBusinessNameEndpointUri( string businessName )
		{
			string endpointUri = Configuration.StandardizeBusinessNameEndpointUri;
			return string.Format( endpointUri + "?b={0}", UrlEncode( businessName ) );
		}

		protected virtual string BuildStandarizedAddressEndpointUri( string street, string city, string state, string zipCode )
		{
			string endpointUri = Configuration.StandardizedAddressEndpointUri;
			return string.Format( endpointUri + "?s={0}&c={1}&st={2}&z={3}", UrlEncode( street ), UrlEncode( city ), UrlEncode( state ), UrlEncode( zipCode ) );
		}

		protected virtual string BuildVerifyCityByZipEndpointUri( string city, string state, string zipCode )
		{
			string endpointUri = Configuration.CityZipValidationResultEndpointUri;
			return string.Format( endpointUri + "?&c={0}&s={1}&z={2}", UrlEncode( city ), UrlEncode( state ), UrlEncode( zipCode ) );
		}

		#endregion Build Endpoint Uri Methods

		#region Helper Methods

		protected virtual XElement GetXml( string endpointUri )
		{
			WebRequest request = WebRequest.Create( endpointUri );
            // set the timeout for the web request to the address service
            request.Timeout = _Config.Timeout;
			WebResponse response = request.GetResponse();

			string responseString;
			using ( Stream respStream = response.GetResponseStream() )
			{
				using ( StreamReader reader = new StreamReader( respStream ) )
				{
					responseString = reader.ReadToEnd();
				}
			}

			XElement xElement = XElement.Parse( responseString );
			return xElement;
		}

        protected virtual int LookupCountyID( string countyName = "", string zipCodeNumber ="")
		{
			int result = 0;
			using ( ICERSRepositoryManager repo = ServiceLocator.GetRepositoryManager() )
			{
                if ( !String.IsNullOrWhiteSpace( countyName ) )
                {
                    var county = repo.Counties.Search( name: countyName ).FirstOrDefault();
                    if ( county != null )
                    {
                        result = county.ID;
                    }
                }
                else
                {
                    var zipCode = repo.ZipCodes.Search( zipCodeNumber ).FirstOrDefault();
                    if ( zipCode != null )
                    {
                        result = zipCode.PrimaryCountyID;
                    }
                }
			}
			return result;
		}

		protected virtual string UrlEncode( string input )
		{
			return HttpUtility.UrlEncode( input );
		}

        private string StandardizedAddressErrorMessage(string method, string endpointUri, Exception ex)
        {
            StringBuilder error = new StringBuilder("An error occurred while attempting to Standardize an address. Please make sure the Address Service is available. \nInvoked from " );
            error.Append( method );
            error.Append(" method. \n");

            error.Append( "Web Request Configuration: End Point URI = " );
            error.Append( endpointUri );
            error.Append( ", Timeout = " );
            error.Append( Configuration.Timeout );
            error.Append( ". \n" );

            if (ex.GetType() == typeof(WebException)) {
                WebException wex = (WebException)ex;
                error.Append("Web Exception Status: ");
                error.Append( wex.Status.ToString() );
                error.Append( ". \n" );
                if (wex.Status == WebExceptionStatus.ProtocolError) {
                    if ( wex.Response != null && wex.Response.GetType() == typeof(HttpWebResponse) )
                    {
                        error.Append( "Protocol Error Status Code: " );
                        error.Append( (( HttpWebResponse )wex.Response).StatusCode.ToString() );
                        error.Append( ", Status Description: " );
                        error.Append( (( HttpWebResponse )wex.Response).StatusDescription );
                        error.Append( ". \n" );
                    }
                }
            }

            error.Append( "Excecution will continue with original address. \n" );
            return error.ToString();
        }
		#endregion Helper Methods
	}
}