export class SpaConfig {

  /** The root url of the api. */
  readonly apiUrl: string;

  constructor() {
    const env = (window as any).__env;
    this.apiUrl = env.apiUrl;
  }
}