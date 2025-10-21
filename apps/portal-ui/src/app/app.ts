import { AfterViewInit, Component, OnDestroy, signal, inject, effect } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { SpaConfig } from '../config/spa-config.model';
import { MsalAppService } from '../config/msal.service';
import { ThemeService } from './config/theme.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
})
export class App implements AfterViewInit, OnDestroy {
  themeService = inject(ThemeService);

  constructor(
    public msal: MsalAppService,
    public readonly spaConfig: SpaConfig
  ) {
    effect(() => {
      const theme = this.themeService.theme();
      const appRoot = document.documentElement;
      if (theme === 'dark') {
        appRoot.classList.add('dark');
      } else {
        appRoot.classList.remove('dark');
      }
    });
  }

  ngAfterViewInit() {
    this.msal.init();
  }

  ngOnDestroy() {
    this.msal.dispose();
  }

  protected readonly title = signal('portal-ui');
}
