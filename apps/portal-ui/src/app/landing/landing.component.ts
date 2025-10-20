import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MsalAppService } from '../../config/msal.service';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="min-h-screen bg-gradient-to-br from-blue-50 via-white to-indigo-50">
      <!-- Hero Section -->
      <div class="relative overflow-hidden">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-16">
          <div class="text-center">
            <!-- Logo/Brand -->
            <div class="mb-8">
              <h1 class="text-5xl font-bold text-gray-900 mb-4">
                <span class="text-blue-600">Ops</span>Dojo Portal
              </h1>
              <p class="text-xl text-gray-600">Your gateway to operational excellence</p>
            </div>

            <!-- Hero Content -->
            <div class="max-w-3xl mx-auto mb-12">
              <h2 class="text-3xl font-bold text-gray-800 mb-6">Streamline Your Operations</h2>
              <p class="text-lg text-gray-600 mb-8">
                Access powerful tools, real-time insights, and collaborative features designed to
                optimize your workflow and boost productivity.
              </p>
            </div>

            <!-- CTA Buttons -->
            <div class="flex flex-col sm:flex-row gap-4 justify-center items-center">
              <a
                routerLink="/dashboard"
                class="bg-white hover:bg-gray-50 text-blue-600 border-2 border-blue-600 hover:border-blue-700 font-semibold py-3 px-8 rounded-lg transition-all duration-200 flex items-center gap-2"
                title="Go to Dashboard"
              >
                <i class="fas fa-tachometer-alt"></i>
                Dashboard
              </a>

              @if (msal.loginDisplay) {
                <button
                  (click)="msal.logout()"
                  class="bg-red-600 hover:bg-red-700 text-white font-semibold py-3 px-8 rounded-lg transition-all duration-200 flex items-center gap-2"
                  title="Log Out"
                >
                  <i class="fas fa-sign-out-alt"></i>
                  Log Out
                </button>
              } @else {
                <button
                  (click)="msal.login()"
                  class="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-3 px-8 rounded-lg transition-all duration-200 flex items-center gap-2"
                  title="Log In"
                >
                  <i class="fas fa-sign-in-alt"></i>
                  Log In
                </button>
              }
            </div>
          </div>
        </div>
      </div>

      <!-- Features Section -->
      <div class="py-16 bg-white">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div class="text-center mb-12">
            <h3 class="text-2xl font-bold text-gray-900 mb-4">Why Choose OpsDojo?</h3>
            <p class="text-gray-600">Everything you need to manage your operations efficiently</p>
          </div>

          <div class="grid md:grid-cols-3 gap-8">
            <div class="text-center p-6">
              <div
                class="w-12 h-12 bg-blue-100 rounded-lg flex items-center justify-center mx-auto mb-4"
              >
                <svg
                  class="w-6 h-6 text-blue-600"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"
                  ></path>
                </svg>
              </div>
              <h4 class="text-lg font-semibold text-gray-900 mb-2">Real-time Analytics</h4>
              <p class="text-gray-600">Monitor your operations with live data and insights</p>
            </div>

            <div class="text-center p-6">
              <div
                class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center mx-auto mb-4"
              >
                <svg
                  class="w-6 h-6 text-green-600"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"
                  ></path>
                </svg>
              </div>
              <h4 class="text-lg font-semibold text-gray-900 mb-2">Secure Access</h4>
              <p class="text-gray-600">Enterprise-grade security with Microsoft authentication</p>
            </div>

            <div class="text-center p-6">
              <div
                class="w-12 h-12 bg-purple-100 rounded-lg flex items-center justify-center mx-auto mb-4"
              >
                <svg
                  class="w-6 h-6 text-purple-600"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M13 10V3L4 14h7v7l9-11h-7z"
                  ></path>
                </svg>
              </div>
              <h4 class="text-lg font-semibold text-gray-900 mb-2">Lightning Fast</h4>
              <p class="text-gray-600">Optimized performance for seamless user experience</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Footer -->
      <div class="bg-gray-50 py-8">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
          <p class="text-gray-500">
            © 2025 OpsDojo. Built with Angular and modern web technologies.
          </p>
        </div>
      </div>
    </div>
  `,
  styles: [],
})
export class LandingComponent {
  constructor(public msal: MsalAppService) {}
}
