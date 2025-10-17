export class SpaConfig {
  /** The root url of the api. */
  readonly apiUrl: string;

  /** The CIAM spa application (client) id. */
  readonly ciamSpaClientId: string;

  /** The CIAM tenant subdomain. */
  readonly ciamSubdomain: string;

  /** The CIAM tenant id. */
  readonly ciamTenantId: string;

  /** The CIAM API scope. */
  readonly ciamApiScope: string;

  constructor() {
    const env = (window as any).__env;
    this.apiUrl = env.apiUrl;
    this.ciamSpaClientId = env.ciamSpaClientId;
    this.ciamSubdomain = env.ciamSubdomain;
    this.ciamTenantId = env.ciamTenantId;
    this.ciamApiScope = env.ciamApiScope;
  }
}
