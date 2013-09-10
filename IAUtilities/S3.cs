using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MOBOT.IA.Utilities
{
	public class S3
	{
		#region Properties

		private string _accessKey = "zEoAD8Wa9KCnDcJ7";
		private string _secretKey = "hWMpGUEnSeAiXRnZ";
		private string _s3BaseDomain = "http://s3.us.archive.org";
		private string _bucketAddressFormat = "{0}/{1}";
		private string _objectAddressFormat = "{0}/{1}/{2}";

		private WebClient _webClient = null;

		public WebClient WebClient
		{
			get
			{
				if ( _webClient == null )
				{
					_webClient = new WebClient();
					_webClient.Headers.Add( "authorization", this.GetAuthHeaderValue() );
				}
				return _webClient;
			}
		}

		#endregion Properties

		#region Constructors

		public S3()
		{
		}

		public S3( string accessKey, string secretKey )
		{
			_accessKey = accessKey;
			_secretKey = secretKey;
		}

		~S3()
		{
			if ( _webClient != null )
			{
				_webClient.Dispose();
				_webClient = null;
			}
		}

		#endregion Constructors

		#region Object operations

		public string PutObject( string fileName, string bucketName, string objectName, string contentType,
			List<KeyValuePair<string, string>> headers, bool preventDerive, bool makeBucket )
		{
			return PutObject( fileName, bucketName, objectName, contentType, EncodingType.Unknown, headers, preventDerive, makeBucket );
		}

		public string PutObject( string fileName, string bucketName, string objectName, string contentType, EncodingType encodingType,
			List<KeyValuePair<string, string>> headers, bool preventDerive, bool makeBucket )
		{
			string result = string.Empty;
			try
			{
				if ( preventDerive )
				{
					// Set a header to prevent IA from initiating a derive process on this item
					if ( headers == null )
					{
						headers = new List<KeyValuePair<string, string>>();
					}
					headers.Add( new KeyValuePair<string, string>( "x-archive-queue-derive", "0" ) );
				}
				if ( makeBucket )
				{
					// Set a header to allow IA to create a "bucket" in which to place this item
					if ( headers == null )
					{
						headers = new List<KeyValuePair<string, string>>();
					}
					headers.Add( new KeyValuePair<string, string>( "x-archive-auto-make-bucket", "1" ) );
				}

				string destination = String.Format( _objectAddressFormat, _s3BaseDomain, bucketName, objectName );
				this.HttpRequest( destination, fileName, "PUT", contentType, encodingType, headers );
				result = "Success";
			}
			catch ( Exception ex )
			{
				result = "Error: " + ex.Message;
			}

			return result;
		}

		public PutResult PutObjectAdv( string fileName, string bucketName, string objectName, string contentType, EncodingType encodingType,
			List<KeyValuePair<string, string>> headers, bool preventDerive, bool makeBucket )
		{
			var result = new PutResult();
			result.PutResultStatus = PutResultStatusEnum.Success;

			try
			{
				if ( preventDerive )
				{
					// Set a header to prevent IA from initiating a derive process on this item
					if ( headers == null )
					{
						headers = new List<KeyValuePair<string, string>>();
					}
					headers.Add( new KeyValuePair<string, string>( "x-archive-queue-derive", "0" ) );
				}
				if ( makeBucket )
				{
					// Set a header to allow IA to create a "bucket" in which to place this item
					if ( headers == null )
					{
						headers = new List<KeyValuePair<string, string>>();
					}
					headers.Add( new KeyValuePair<string, string>( "x-archive-auto-make-bucket", "1" ) );
				}

				string destination = String.Format( _objectAddressFormat, _s3BaseDomain, bucketName, objectName );
				this.HttpRequest( destination, fileName, "PUT", contentType, encodingType, headers );
			}
			catch ( Exception ex )
			{
				result.PutResultStatus = PutResultStatusEnum.Exception;
				result.Exception = ex;
			}

			return result;
		}

		public bool GetObject( string bucketName, string objectName )
		{
			throw ( new NotImplementedException() );
		}

		public bool GetObject( string bucketName, string objectName, string fileName )
		{
			bool result = true;
			try
			{
				this.WebClient.DownloadFile( String.Format( _objectAddressFormat, _s3BaseDomain, bucketName, objectName ), fileName );
			}
			catch
			{
				result = false;
			}

			return result;
		}

		#endregion Object operations

		#region Bucket operations

		public string ListBuckets()
		{
			return this.WebClient.DownloadString( _s3BaseDomain );
		}

		public bool PutBucket( string bucketName )
		{
			throw ( new NotImplementedException() );
		}

		public string GetBucket( string bucketName )
		{
			return this.WebClient.DownloadString( String.Format( _bucketAddressFormat, _s3BaseDomain, bucketName ) );
		}

		#endregion Bucket operations

		#region Helper methods

		private string GetAuthHeaderValue()
		{
			return String.Format( "LOW {0}:{1}", _accessKey, _secretKey );
		}

		private void HttpRequest( string url, string fileName, string method, string contentType, EncodingType encodingType,
			List<KeyValuePair<string, string>> headers )
		{
			Stream writeStream = null;
			Stream fileStream = null;

			// Buffer to read 10K bytes in chunk:
			byte[] buffer = new Byte[ 10000 ];

			try
			{
				// open the file
				fileStream = new FileStream( fileName, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read );

				// Prepare the web request
				var req = (HttpWebRequest)WebRequest.Create( url );
				req.AllowWriteStreamBuffering = false; // Getting OutOfMemory exceptions. Ref: http://support.microsoft.com/kb/908573
				req.Method = method;
				req.Timeout = 86400000;    // 24 hours 
				req.ContentType = contentType;
				req.ContentLength = fileStream.Length;
				req.Headers.Add( "authorization", this.GetAuthHeaderValue() );
				if ( headers != null )
				{
					foreach ( KeyValuePair<string, string> header in headers )
					{
						req.Headers.Add( header.Key, header.Value );
					}
				}

				// total bytes to read
				long dataToRead = fileStream.Length;

				// Send the data in chunks
				int length;
				writeStream = req.GetRequestStream();
				while ( dataToRead > 0 )
				{
					// Read the data from the stream into a buffer.
					length = fileStream.Read( buffer, 0, 10000 );

					// Write the data to the current output stream.
					writeStream.Write( buffer, 0, length );

					buffer = new Byte[ 10000 ];
					dataToRead = dataToRead - length;
				}
				writeStream.Close();
				fileStream.Close();

				// Make sure we were successful
				using ( var webresponse = req.GetResponse() )
				{
					var response = (HttpWebResponse)webresponse;
					if ( ( response.StatusCode != HttpStatusCode.Created ) && ( response.StatusCode != HttpStatusCode.OK ) )
					{
						throw new UnauthorizedAccessException( "File not written to " + url + ".  HTTP status: " + response.StatusCode.ToString() + "\r\n" );
					}
				}
			}
			catch ( Exception ex )
			{
				throw;
			}
			finally
			{
				if ( writeStream != null )
				{
					writeStream.Dispose();
					writeStream = null;
				}
				if ( fileStream != null )
				{
					fileStream.Dispose();
					fileStream = null;
				}
			}
		}

		#endregion Helper methods
	}

	public enum EncodingType
	{
		Unknown = 0,
		ASCII = 1,
		Binary = 2
	}

	[Serializable]
	public class PutResult
	{
		public List<string> UserMessages = new List<string>();
		public Exception Exception { get; set; }
		public PutResultStatusEnum PutResultStatus = PutResultStatusEnum.Unknown;

		public PutResult()
		{ }

		public PutResult( PutResultStatusEnum resultStatusEnum, List<string> userMessages, Exception exception )
		{
			Exception = exception;
			PutResultStatus = resultStatusEnum;
			UserMessages = userMessages;
		}

		public string UserMessageString
		{
			get
			{
				var sb = new StringBuilder();
				foreach ( string userMessage in UserMessages )
				{
					sb.Append( userMessage );
					sb.Append( "\r\n" );
				}
				return sb.ToString();
			}
		}

		public string UserMessageWebString
		{
			get
			{
				var sb = new StringBuilder();
				foreach ( string userMessage in UserMessages )
				{
					sb.Append( userMessage );
					sb.Append( "<br/>" );
				}

				return sb.ToString();
			}
		}
	}

	public enum PutResultStatusEnum
	{
		Unknown = 0,
		Success = 1,
		Failure = 2,
		Exception = 3
	}

}

