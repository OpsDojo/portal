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
  constructor(
    public msal: MsalAppService,
    public readonly spaConfig: SpaConfig
  ) {}

  ngAfterViewInit() {
    this.msal.init();
  }

  ngOnDestroy() {
    this.msal.dispose();
  }

  protected readonly title = signal('portal-ui');
}
