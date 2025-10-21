import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MsalAppService } from '../../config/msal.service';
import { ThemeService } from '../config/theme.service';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule],
  template: `
    <main class="min-h-screen bg-gray-50 p-6">
      <div class="max-w-4xl mx-auto">
        <!-- Header -->
        <div class="bg-white rounded-lg shadow-lg p-6 mb-6">
          <div class="flex justify-between items-center">
            <div>
              <h1 class="text-2xl font-bold text-gray-800">Settings</h1>
              <p class="text-gray-600">Manage your account and application preferences</p>
            </div>
            <div class="flex gap-3">
              <button
                type="button"
                (click)="goBack()"
                class="bg-gray-600 hover:bg-gray-700 text-white font-semibold py-3 px-6 rounded-lg transition-all duration-200 flex items-center gap-2"
                title="Go Back"
              >
                <i class="fas fa-arrow-left"></i>
                Back
              </button>
              <button
                type="button"
                (click)="logout()"
                class="bg-red-600 hover:bg-red-700 text-white font-semibold py-3 px-6 rounded-lg transition-all duration-200 flex items-center gap-2"
                title="Log Out"
              >
                <i class="fas fa-sign-out-alt"></i>
                Log Out
              </button>
            </div>
          </div>
        </div>

        <div class="grid gap-6 lg:grid-cols-3">
          <!-- Settings Navigation -->
          <div class="lg:col-span-1">
            <div class="bg-white rounded-lg shadow-lg p-6">
              <h2 class="text-lg font-semibold text-gray-800 mb-4">Categories</h2>
              <nav class="space-y-2">
                <button
                  (click)="activeTab = 'profile'"
                  [class]="
                    activeTab === 'profile'
                      ? 'bg-blue-50 text-blue-600 border-blue-200'
                      : 'text-gray-600 hover:bg-gray-50'
                  "
                  class="w-full text-left px-4 py-3 rounded-lg border transition-all duration-200 flex items-center gap-3"
                >
                  <i class="fas fa-user"></i>
                  Profile
                </button>
                <button
                  (click)="activeTab = 'notifications'"
                  [class]="
                    activeTab === 'notifications'
                      ? 'bg-blue-50 text-blue-600 border-blue-200'
                      : 'text-gray-600 hover:bg-gray-50'
                  "
                  class="w-full text-left px-4 py-3 rounded-lg border transition-all duration-200 flex items-center gap-3"
                >
                  <i class="fas fa-bell"></i>
                  Notifications
                </button>
                <button
                  (click)="activeTab = 'appearance'"
                  [class]="
                    activeTab === 'appearance'
                      ? 'bg-blue-50 text-blue-600 border-blue-200'
                      : 'text-gray-600 hover:bg-gray-50'
                  "
                  class="w-full text-left px-4 py-3 rounded-lg border transition-all duration-200 flex items-center gap-3"
                >
                  <i class="fas fa-palette"></i>
                  Appearance
                </button>
                <button
                  (click)="activeTab = 'security'"
                  [class]="
                    activeTab === 'security'
                      ? 'bg-blue-50 text-blue-600 border-blue-200'
                      : 'text-gray-600 hover:bg-gray-50'
                  "
                  class="w-full text-left px-4 py-3 rounded-lg border transition-all duration-200 flex items-center gap-3"
                >
                  <i class="fas fa-shield-alt"></i>
                  Security
                </button>
              </nav>
            </div>
          </div>

          <!-- Settings Content -->
          <div class="lg:col-span-2">
            <div class="bg-white rounded-lg shadow-lg p-6">
              @switch (activeTab) {
                @case ('profile') {
                  <div>
                    <h3 class="text-xl font-semibold text-gray-800 mb-3">Profile Settings</h3>
                    <p class="text-sm text-gray-500 mb-6">
                      These details are fetched from your identity provider.
                    </p>
                    <div class="space-y-4">
                      <div>
                        <label class="block text-sm font-medium text-gray-700 mb-2"
                          >Display Name</label
                        >
                        <input
                          type="text"
                          class="w-full px-4 py-3 text-gray-500 border border-gray-300 rounded-lg bg-gray-100"
                          [value]="userDisplayName"
                          readonly
                          disabled
                        />
                      </div>
                      <div>
                        <label class="block text-sm font-medium text-gray-700 mb-2">Email</label>
                        <input
                          type="email"
                          class="w-full px-4 py-3 text-gray-500 border border-gray-300 rounded-lg bg-gray-100"
                          [value]="userEmail"
                          readonly
                          disabled
                        />
                      </div>
                    </div>
                  </div>
                }
                @case ('notifications') {
                  <div>
                    <h3 class="text-xl font-semibold text-gray-800 mb-6">
                      Notification Preferences
                    </h3>
                    <div class="space-y-4">
                      <div class="flex items-center justify-between p-4 border rounded-lg">
                        <div>
                          <h4 class="font-medium text-gray-800">Email Notifications</h4>
                          <p class="text-sm text-gray-600">
                            Receive email updates about important events
                          </p>
                        </div>
                        <label class="relative inline-flex items-center cursor-pointer">
                          <input type="checkbox" class="sr-only peer" checked />
                          <div
                            class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"
                          ></div>
                        </label>
                      </div>
                      <div class="flex items-center justify-between p-4 border rounded-lg">
                        <div>
                          <h4 class="font-medium text-gray-800">Browser Notifications</h4>
                          <p class="text-sm text-gray-600">Show notifications in your browser</p>
                        </div>
                        <label class="relative inline-flex items-center cursor-pointer">
                          <input type="checkbox" class="sr-only peer" />
                          <div
                            class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"
                          ></div>
                        </label>
                      </div>
                    </div>
                  </div>
                }
                @case ('appearance') {
                  <div>
                    <h3 class="text-xl font-semibold text-gray-800 mb-6">Appearance Settings</h3>
                    <div class="space-y-6">
                      <div>
                        <label class="block text-sm font-medium text-gray-700 mb-3">Theme</label>
                        <div class="grid grid-cols-2 gap-4">
                          <div
                            class="border-2 rounded-lg p-4 cursor-pointer flex flex-col gap-2"
                            [class]="
                              theme === 'light'
                                ? 'border-blue-500'
                                : 'border-gray-300 hover:border-gray-400'
                            "
                            (click)="setTheme('light')"
                          >
                            <div class="flex items-center gap-3">
                              <div
                                class="w-4 h-4 bg-white border-2 border-gray-300 rounded-full flex items-center justify-center"
                              >
                                <div
                                  class="w-2 h-2 rounded-full"
                                  [class.bg-blue-500]="theme === 'light'"
                                ></div>
                              </div>
                              <span class="font-medium">Light</span>
                              <span *ngIf="theme === 'light'" class="ml-2 text-xs text-blue-500"
                                >Selected</span
                              >
                            </div>
                            <div
                              class="mt-2 h-12 bg-gradient-to-r from-blue-50 to-indigo-50 rounded border"
                            ></div>
                          </div>
                          <div
                            class="border-2 rounded-lg p-4 cursor-pointer flex flex-col gap-2"
                            [class]="
                              theme === 'dark'
                                ? 'border-blue-500'
                                : 'border-gray-300 hover:border-gray-400'
                            "
                            (click)="setTheme('dark')"
                          >
                            <div class="flex items-center gap-3">
                              <div
                                class="w-4 h-4 bg-white border-2 border-gray-300 rounded-full flex items-center justify-center"
                              >
                                <div
                                  class="w-2 h-2 rounded-full"
                                  [class.bg-blue-500]="theme === 'dark'"
                                ></div>
                              </div>
                              <span class="font-medium">Dark</span>
                              <span *ngIf="theme === 'dark'" class="ml-2 text-xs text-blue-500"
                                >Selected</span
                              >
                            </div>
                            <div
                              class="mt-2 h-12 bg-gradient-to-r from-gray-800 to-gray-900 rounded border"
                            ></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                }
                @case ('security') {
                  <div>
                    <h3 class="text-xl font-semibold text-gray-800 mb-6">Security Settings</h3>
                    <div class="space-y-6">
                      <div class="p-4 bg-green-50 border border-green-200 rounded-lg">
                        <div class="flex items-center gap-3">
                          <i class="fas fa-shield-alt text-green-600"></i>
                          <div>
                            <h4 class="font-medium text-green-800">Microsoft Authentication</h4>
                            <p class="text-sm text-green-700">
                              Your account is secured with Microsoft SSO
                            </p>
                          </div>
                        </div>
                      </div>
                      <div>
                        <h4 class="font-medium text-gray-800 mb-3">Session Management</h4>
                        <button
                          type="button"
                          class="bg-red-600 hover:bg-red-700 text-white font-medium py-2 px-4 rounded-lg transition-all duration-200"
                          (click)="logout()"
                        >
                          Sign Out of All Devices
                        </button>
                        <p class="text-sm text-gray-600 mt-2">
                          This will sign you out of all active sessions
                        </p>
                      </div>
                    </div>
                  </div>
                }
              }
            </div>
          </div>
        </div>
      </div>
    </main>
  `,
  styles: [],
})
export class SettingsComponent {
  activeTab: 'profile' | 'notifications' | 'appearance' | 'security' = 'profile';
  themeService: ThemeService = inject(ThemeService);

  ngOnInit(): void {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark' || savedTheme === 'light') {
      this.setTheme(savedTheme as 'light' | 'dark');
    }
  }

  get theme() {
    return this.themeService.theme();
  }

  setTheme(theme: 'light' | 'dark'): void {
    this.themeService.setTheme(theme);
  }

  constructor(
    public msal: MsalAppService,
    private router: Router
  ) {}

  get userDisplayName(): string {
    return this.msal.authService.instance.getActiveAccount()?.name || 'Not available';
  }

  get userEmail(): string {
    return this.msal.authService.instance.getActiveAccount()?.username || 'Not available';
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }

  logout(): void {
    try {
      this.msal.logout();
      this.router.navigate(['/']);
    } catch (error) {
      console.error('Logout failed:', error);
    }
  }
}
