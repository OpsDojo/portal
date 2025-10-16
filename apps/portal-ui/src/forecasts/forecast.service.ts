import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { SpaConfig } from '../config/spa-config.model';
import { Forecast } from './forecast.model';

@Injectable({ providedIn: 'root' })
export class ForecastService {
  private readonly url: string;

  constructor(
    private httpClient: HttpClient,
    env: SpaConfig
  ) {
    this.url = `${env.apiUrl}/forecasts`;
  }

  public getForecast() {
    return this.httpClient.get<Forecast[]>(this.url);
  }
}
