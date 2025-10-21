import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MsalAppService } from '../../config/msal.service';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="section">
      <!-- Hero Section -->
      <div class="relative overflow-hidden">
        <div class="max-w-7xl mx-auto py-16">
          <div class="text-center">
            <!-- Logo/Brand -->
            <div class="mb-8">
              <h1 class="title mb-4"><span style="color: var(--primary)">Ops</span>Dojo Portal</h1>
              <p class="subtitle">Your gateway to operational excellence</p>
            </div>

            <!-- Hero Content -->
            <div class="max-w-3xl mx-auto mb-12">
              <h2 class="title mb-6">Streamline Your Operations</h2>
              <p class="subtitle mb-8">
                Access powerful tools, real-time insights, and collaborative features designed to
                optimize your workflow and boost productivity.
              </p>
            </div>

            <!-- CTA Buttons -->
            <div class="flex flex-col sm:flex-row gap-4 justify-center items-center">
              <a routerLink="/dashboard" class="btn btn-primary" title="Go to Dashboard">
                <i class="fas fa-tachometer-alt"></i>
                Dashboard
              </a>

              @if (msal.loginDisplay) {
                <button (click)="msal.logout()" class="btn btn-logout" title="Log Out">
                  <i class="fas fa-sign-out-alt"></i>
                  Log Out
                </button>
              } @else {
                <button (click)="msal.login()" class="btn btn-primary" title="Log In">
                  <i class="fas fa-sign-in-alt"></i>
                  Log In
                </button>
              }
            </div>
          </div>
        </div>
      </div>

      <!-- Features Section -->
      <div class="py-16">
        <div class="max-w-7xl mx-auto">
          <div class="text-center mb-12">
            <h3 class="title mb-4">Why Choose OpsDojo?</h3>
            <p class="subtitle">Everything you need to manage your operations efficiently</p>
          </div>

          <div class="grid md:grid-cols-3 gap-8">
            <div class="card text-center p-6">
              <div
                class="w-12 h-12 rounded-lg flex items-center justify-center mx-auto mb-4"
                style="background: var(--primary);"
              >
                <svg
                  class="w-6 h-6"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                  style="color: #fff;"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"
                  ></path>
                </svg>
              </div>
              <h4 class="input-label mb-2">Real-time Analytics</h4>
              <p class="subtitle">Monitor your operations with live data and insights</p>
            </div>

            <div class="card text-center p-6">
              <div
                class="w-12 h-12 rounded-lg flex items-center justify-center mx-auto mb-4"
                style="background: #22c55e;"
              >
                <svg
                  class="w-6 h-6"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                  style="color: #fff;"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"
                  ></path>
                </svg>
              </div>
              <h4 class="input-label mb-2">Secure Access</h4>
              <p class="subtitle">Enterprise-grade security with Microsoft authentication</p>
            </div>

            <div class="card text-center p-6">
              <div
                class="w-12 h-12 rounded-lg flex items-center justify-center mx-auto mb-4"
                style="background: #a78bfa;"
              >
                <svg
                  class="w-6 h-6"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                  style="color: #fff;"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M13 10V3L4 14h7v7l9-11h-7z"
                  ></path>
                </svg>
              </div>
              <h4 class="input-label mb-2">Lightning Fast</h4>
              <p class="subtitle">Optimized performance for seamless user experience</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Footer -->
      <div class="section py-8">
        <div class="max-w-7xl mx-auto text-center">
          <p class="subtitle">© 2025 OpsDojo. Built with Angular and modern web technologies.</p>
        </div>
      </div>
    </div>
  `,
  styles: [],
})
export class LandingComponent {
  constructor(public msal: MsalAppService) {}
}
