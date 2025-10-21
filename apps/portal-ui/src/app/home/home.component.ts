import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { MsalAppService } from '../../config/msal.service';
import { ForecastService } from '../../forecasts/forecast.service';
import { Forecast } from '../../forecasts/forecast.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  template: `
    <main class="section">
      <div class="max-w-4xl mx-auto">
        <!-- Header -->
        <div class="card">
          <div class="header">
            <div>
              <h1 class="title">Portal Dashboard</h1>
              <p class="subtitle">Welcome back!</p>
            </div>
            <div class="flex gap-3">
              <button
                type="button"
                (click)="goToSettings()"
                class="btn btn-primary"
                title="Settings"
              >
                <i class="fas fa-cog"></i>
                Settings
              </button>
              <button type="button" (click)="logout()" class="btn btn-logout" title="Log Out">
                <i class="fas fa-sign-out-alt"></i>
                Log Out
              </button>
            </div>
          </div>
        </div>

        <!-- Forecast Section -->
        <div class="card">
          <h2 class="title mb-4">Weather Forecast</h2>

          @if ($forecast | async; as forecasts) {
            <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              @for (f of forecasts; track f.date) {
                <div class="card p-4">
                  <div class="flex justify-between items-center">
                    <div>
                      <p class="subtitle">{{ f.date | date: 'mediumDate' }}</p>
                      <p class="title">{{ f.temperatureC }}°C</p>
                      <p class="subtitle">{{ f.temperatureF }}°F</p>
                    </div>
                    <div class="text-right">
                      <p class="input-label">{{ f.summary }}</p>
                    </div>
                  </div>
                </div>
              }
            </div>
          } @else {
            <div class="text-center py-8">
              <div
                class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600 mx-auto mb-4"
              ></div>
              <p class="subtitle">Loading forecast...</p>
            </div>
          }
        </div>
      </div>
    </main>
  `,
  styles: [],
})
export class HomeComponent implements OnInit {
  public $forecast: Observable<Forecast[]> | undefined;

  constructor(
    public msal: MsalAppService,
    private forecastService: ForecastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.$forecast = this.forecastService.getForecast();
  }

  goToSettings(): void {
    this.router.navigate(['/settings']);
  }

  async logout() {
    try {
      await this.msal.logout();
      this.router.navigate(['/']);
    } catch (error) {
      console.error('Logout failed:', error);
    }
  }
}
