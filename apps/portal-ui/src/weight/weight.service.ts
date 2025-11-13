import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { SpaConfig } from '../config/spa-config.model';
import { Page, PageRequest } from '../shared/shared.model';
import { WeightLog, WeightLogLite } from './weight.model';

@Injectable({ providedIn: 'root' })
export class WeightService {
  private readonly url: string;

  constructor(
    private httpClient: HttpClient,
    env: SpaConfig
  ) {
    this.url = `${env.apiUrl}/weight/logs`;
  }

  public getWeightLogs(pageRequest?: PageRequest) {
    // Set default values if not provided
    const pageNumber = pageRequest?.pageNumber ?? 1;
    const pageSize = pageRequest?.pageSize ?? 5;

    // Create query parameters
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.httpClient.get<Page<WeightLog>>(this.url, { params });
  }

  public addWeightLog(log: WeightLogLite) {
    return this.httpClient.post(this.url, log);
  }
}
