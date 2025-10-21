import { AfterViewInit, Component, OnDestroy, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { SpaConfig } from '../config/spa-config.model';
import { MsalAppService } from '../config/msal.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements AfterViewInit, OnDestroy {
  theme: 'light' | 'dark' = 'light';

  ngOnInit() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark' || savedTheme === 'light') {
      this.theme = savedTheme as 'light' | 'dark';
      this.applyTheme();
    }
    window.addEventListener('themeChange', (event: any) => {
      if (event.detail === 'dark' || event.detail === 'light') {
        this.theme = event.detail;
        localStorage.setItem('theme', this.theme);
        this.applyTheme();
      }
    });
  }

  applyTheme(): void {
    const appRoot = document.documentElement;
    if (this.theme === 'dark') {
      appRoot.classList.add('dark');
    } else {
      appRoot.classList.remove('dark');
    }
  }
  constructor(
    public msal: MsalAppService,
    public readonly spaConfig: SpaConfig
  ) {}

  ngAfterViewInit() {
    this.msal.init();
    this.ngOnInit();
  }

  ngOnDestroy() {
    this.msal.dispose();
  }

  protected readonly title = signal('portal-ui');
}
