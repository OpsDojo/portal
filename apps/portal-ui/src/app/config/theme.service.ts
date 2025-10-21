import { Injectable, signal } from '@angular/core';

export type Theme = 'light' | 'dark';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private readonly _theme = signal<Theme>(this.getInitialTheme());

  readonly theme = this._theme.asReadonly();

  setTheme(theme: Theme) {
    this._theme.set(theme);
    localStorage.setItem('theme', theme);
  }

  private getInitialTheme(): Theme {
    const saved = localStorage.getItem('theme');
    return saved === 'dark' ? 'dark' : 'light';
  }
}
