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
    <main class="min-h-screen bg-gray-50 p-6">
      <div class="max-w-4xl mx-auto">
        <!-- Header -->
        <div class="bg-white rounded-lg shadow-lg p-6 mb-6">
          <div class="flex justify-between items-center">
            <div>
              <h1 class="text-2xl font-bold text-gray-800">Portal Dashboard</h1>
              <p class="text-gray-600">Welcome back!</p>
            </div>
            <button
              type="button"
              (click)="logout()"
              class="bg-red-600 hover:bg-red-700 text-white font-semibold py-2 px-4 rounded-lg transition-colors"
            >
              Log out
            </button>
          </div>
        </div>

        <!-- Forecast Section -->
        <div class="bg-white rounded-lg shadow-lg p-6">
          <h2 class="text-xl font-bold text-gray-800 mb-4">Weather Forecast</h2>

          @if ($forecast | async; as forecasts) {
            <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              @for (f of forecasts; track f.date) {
                <div
                  class="bg-gradient-to-r from-blue-50 to-indigo-50 rounded-lg p-4 border border-blue-100"
                >
                  <div class="flex justify-between items-center">
                    <div>
                      <p class="text-sm text-gray-600">{{ f.date | date: 'mediumDate' }}</p>
                      <p class="text-2xl font-bold text-gray-800">{{ f.temperatureC }}°C</p>
                      <p class="text-sm text-gray-500">{{ f.temperatureF }}°F</p>
                    </div>
                    <div class="text-right">
                      <p class="text-lg font-semibold text-blue-600">{{ f.summary }}</p>
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
              <p class="text-gray-600">Loading forecast...</p>
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

  async logout() {
    try {
      await this.msal.logout();
      this.router.navigate(['/']);
    } catch (error) {
      console.error('Logout failed:', error);
    }
  }
}
