import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SpaConfig } from '../config/spa-config.model';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  constructor(public readonly spaConfig: SpaConfig) {}

  protected readonly title = signal('portal-ui');
}
