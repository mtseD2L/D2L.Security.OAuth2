﻿using System;
using System.IdentityModel.Tokens;
using D2L.Security.OAuth2.Utilities;

namespace D2L.Security.OAuth2.Keys.Default {
	internal sealed class D2LSecurityTokenFactory : ID2LSecurityTokenFactory {

		private readonly IDateTimeProvider m_dateTimeProvider;
		private readonly TimeSpan m_keyLifetime;

		public D2LSecurityTokenFactory(
			IDateTimeProvider dateTimeProvider,
			TimeSpan keyLifetime
		) {
			m_dateTimeProvider = dateTimeProvider;
			m_keyLifetime = keyLifetime;
		}

		D2LSecurityToken ID2LSecurityTokenFactory.Create(
			Func<Tuple<AsymmetricSecurityKey, IDisposable>> keyFactory
		) {
			Guid id = Guid.NewGuid();
			DateTime validFrom = m_dateTimeProvider.UtcNow;
			DateTime validTo = validFrom + m_keyLifetime;

			var result = new D2LSecurityToken(
				id: id,
				validFrom: validFrom,
				validTo: validTo,
				keyFactory: keyFactory
			);

			return result;
		}
	}
}
