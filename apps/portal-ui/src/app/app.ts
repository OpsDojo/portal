import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

import { SpaConfig } from '../config/spa-config.model';
import { Forecast } from '../forecasts/forecast.model';
import { ForecastService } from '../forecasts/forecast.service';
import { MsalAppService } from '../config/msal.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit, AfterViewInit, OnDestroy {

  public $forecast: Observable<Forecast[]> | undefined;

  constructor(
    public msal: MsalAppService,
    public readonly spaConfig: SpaConfig,
    private forecastService: ForecastService) {}

  ngOnInit(): void {
    this.$forecast = this.forecastService.getForecast();
  }

  ngAfterViewInit() { this.msal.init(); }
  ngOnDestroy() { this.msal.dispose(); }


  protected readonly title = signal('portal-ui');
}
