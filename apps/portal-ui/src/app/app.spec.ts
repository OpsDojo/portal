import { TestBed } from '@angular/core/testing';
import { App } from './app';
import { provideSpaConfig } from '../config/spa-config.provider';
import { provideHttpClient } from '@angular/common/http';

describe('App', () => {
  beforeEach(async () => {
    // Mock the global environment object that SpaConfig depends on
    (window as any).__env = {
      apiUrl: 'http://localhost:3000/api'
    };

    await TestBed.configureTestingModule({
      imports: [App],
      providers: [
        provideHttpClient(),
        provideSpaConfig()
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, portal-ui');
  });
});
