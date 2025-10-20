import { Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { LandingComponent } from './landing/landing.component';
import { HomeComponent } from './home/home.component';
import { SettingsComponent } from './settings/settings.component';

export const routes: Routes = [
  {
    path: '',
    component: LandingComponent, // Public landing page
  },
  {
    path: 'dashboard',
    component: HomeComponent,
    canActivate: [MsalGuard], // Protected dashboard with your MsalGuard
  },
  {
    path: 'settings',
    component: SettingsComponent,
    canActivate: [MsalGuard], // Protected settings page
  },
  {
    path: '**',
    redirectTo: '',
  },
];
