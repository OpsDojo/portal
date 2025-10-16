import {
  BrowserCacheLocation,
  InteractionType,
  IPublicClientApplication,
  LogLevel,
  PublicClientApplication,
} from '@azure/msal-browser';
import { MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';

import { SpaConfig } from './spa-config.model';

export function loggerCallback(logLevel: LogLevel, message: string) {
  if (logLevel == LogLevel.Warning) console.warn(message);
  else if (
    logLevel == LogLevel.Error &&
    !message.includes('acquireTokenSilent rejected with error')
  ) {
    console.error(message);
  }
}

export function MSALInstanceFactory(env: SpaConfig): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: env.ciamSpaClientId,
      authority: `https://${env.ciamSubdomain}.ciamlogin.com/${env.ciamTenantId}/v2.0`,
      redirectUri: '/',
      postLogoutRedirectUri: '/',
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: true,
    },
    system: {
      allowPlatformBroker: false, // Disables WAM Broker
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Warning,
        piiLoggingEnabled: false,
      },
    },
  });
}

export function MSALInterceptorConfigFactory(env: SpaConfig): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string> | null>();

  protectedResourceMap.set(env.apiUrl, [env.ciamApiScope]);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(env: SpaConfig): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    loginFailedRoute: '/login-failed',
    authRequest: {
      scopes: [env.ciamApiScope],
    },
  };
}
