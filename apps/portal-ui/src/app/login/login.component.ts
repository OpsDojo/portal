import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MsalAppService } from '../../config/msal.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  template: `
    <main class="min-h-screen bg-gray-50 flex items-center justify-center">
      <div class="max-w-md mx-auto bg-white rounded-lg shadow-lg p-6">
        <div class="text-center mb-6">
          <h1 class="text-2xl font-bold text-gray-800">Welcome to Portal</h1>
          <p class="text-gray-600 mt-2">Please log in to continue</p>
        </div>

        @if (!msal.isIframe) {
          <button
            type="button"
            (click)="login()"
            class="w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded-lg transition-colors"
          >
            Log in with Microsoft
          </button>
        }
      </div>
    </main>
  `,
  styles: [],
})
export class LoginComponent {
  constructor(
    public msal: MsalAppService,
    private router: Router
  ) {}

  async login() {
    try {
      await this.msal.login();
      // Navigate to home after successful login
      if (this.msal.loginDisplay) {
        this.router.navigate(['/']);
      }
    } catch (error) {
      console.error('Login failed:', error);
    }
  }
}
