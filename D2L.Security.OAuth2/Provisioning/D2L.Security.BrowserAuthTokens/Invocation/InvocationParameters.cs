﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace D2L.Security.BrowserAuthTokens.Invocation {

	/// <summary>
	/// Configures a provision invocation
	/// </summary>
	internal sealed class InvocationParameters {

		private readonly string m_authorization;
		private readonly string m_scopes;
		private readonly string m_assertionToken;

		internal InvocationParameters( 
			string clientId, 
			string clientSecret, 
			IEnumerable<string> scopes, 
			string assertionToken 
			) {

			m_authorization = WebUtility.UrlEncode( clientId ) + ":" + WebUtility.UrlEncode( clientSecret );
			m_authorization = ToBase64( m_authorization );
			m_authorization = "Basic " + m_authorization;

			m_scopes = SerializeScopes( scopes );
			m_scopes = WebUtility.UrlEncode( m_scopes );

			m_assertionToken = assertionToken;
		}

		internal string Authorization {
			get { return m_authorization; }
		}

		internal string Scope {
			get { return m_scopes; }
		}

		internal string GrantType {
			get { return Constants.ASSERTION_GRANT_TYPE; }
		}

		internal string Assertion {
			get { return m_assertionToken; }
		}

		private string ToBase64( string me ) {
			byte[] plainTextBytes = Encoding.UTF8.GetBytes( me );
			return Convert.ToBase64String( plainTextBytes );
		}

		private string SerializeScopes( IEnumerable<string> scopes ) {
			const string separator = " ";

			if( !scopes.Any() ) {
				return string.Empty;
			}

			StringBuilder builder = new StringBuilder();
			foreach( string scope in scopes ) {
				builder.Append( scope );
				builder.Append( separator );
			}

			string result = builder.ToString();
			// remove last separator
			result = result.Substring( 0, result.Length - separator.Length );

			return result;
		}
	}
}
