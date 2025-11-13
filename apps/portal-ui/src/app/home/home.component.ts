import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { MsalAppService } from '../../config/msal.service';
import { WeightService } from '../../weight/weight.service';
import { WeightLog, WeightLogLite } from '../../weight/weight.model';
import { Page } from '../../shared/shared.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <main class="section">
      <div class="max-w-4xl mx-auto">
        <!-- Header -->
        <div class="card">
          <div class="header">
            <div>
              <h1 class="title">Portal Dashboard</h1>
              <p class="subtitle">Welcome back!</p>
            </div>
            <div class="flex gap-3">
              <button
                type="button"
                (click)="goToSettings()"
                class="btn btn-primary"
                title="Settings"
              >
                <i class="fas fa-cog"></i>
                Settings
              </button>
              <button type="button" (click)="logout()" class="btn btn-logout" title="Log Out">
                <i class="fas fa-sign-out-alt"></i>
                Log Out
              </button>
            </div>
          </div>
        </div>

        <!-- Add Weight Log Section -->
        <div class="card">
          <div class="flex justify-between items-center mb-4">
            <h2 class="title">Add Weight Log</h2>
            <button type="button" (click)="toggleAddForm()" class="btn btn-primary">
              <i class="fas" [class.fa-plus]="!showAddForm" [class.fa-times]="showAddForm"></i>
              {{ showAddForm ? 'Cancel' : 'Add New' }}
            </button>
          </div>

          @if (showAddForm) {
            <form [formGroup]="weightLogForm" (ngSubmit)="onSubmit()" class="space-y-4">
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label class="input-label" for="date">Date</label>
                  <input
                    id="date"
                    type="date"
                    formControlName="day"
                    class="input-field"
                    [class.border-red-500]="
                      weightLogForm.get('day')?.invalid && weightLogForm.get('day')?.touched
                    "
                  />
                  @if (weightLogForm.get('day')?.invalid && weightLogForm.get('day')?.touched) {
                    <p class="text-red-500 text-sm mt-1">Date is required</p>
                  }
                </div>

                <div>
                  <label class="input-label" for="weight">Weight (kg)</label>
                  <input
                    id="weight"
                    type="number"
                    step="0.1"
                    min="0"
                    max="1000"
                    formControlName="kg"
                    class="input-field"
                    placeholder="Enter weight in kg"
                    [class.border-red-500]="
                      weightLogForm.get('kg')?.invalid && weightLogForm.get('kg')?.touched
                    "
                  />
                  @if (weightLogForm.get('kg')?.invalid && weightLogForm.get('kg')?.touched) {
                    <p class="text-red-500 text-sm mt-1">
                      @if (weightLogForm.get('kg')?.errors?.['required']) {
                        Weight is required
                      } @else if (weightLogForm.get('kg')?.errors?.['min']) {
                        Weight must be greater than 0
                      } @else if (weightLogForm.get('kg')?.errors?.['max']) {
                        Weight must be less than 1000 kg
                      }
                    </p>
                  }
                </div>
              </div>

              <div class="flex justify-end gap-3">
                <button type="button" (click)="resetForm()" class="btn btn-secondary">Reset</button>
                <button
                  type="submit"
                  [disabled]="weightLogForm.invalid || isSubmitting"
                  class="btn btn-primary"
                  [class.opacity-50]="weightLogForm.invalid || isSubmitting"
                >
                  @if (isSubmitting) {
                    <i class="fas fa-spinner fa-spin mr-2"></i>
                    Adding...
                  } @else {
                    <i class="fas fa-plus mr-2"></i>
                    Add Weight Log
                  }
                </button>
              </div>
            </form>
          }
        </div>

        <!-- Weight Logs Section -->
        <div class="card">
          <h2 class="title mb-4">Weight Logs</h2>

          @if ($weightLogs | async; as weightLogsPage) {
            <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              @for (log of weightLogsPage.items; track log.id) {
                <div class="card p-4">
                  <div class="flex justify-between items-center">
                    <div>
                      <p class="input-label text-blue-600 font-medium">
                        {{ getDaysAgo(log.date) }}
                      </p>
                      <p class="subtitle text-xs">{{ log.date | date: 'mediumDate' }}</p>
                      <p class="title">{{ log.weight.kg }} kg</p>
                      <p class="subtitle">{{ log.weight.lbs }} lbs</p>
                    </div>
                    <div class="text-right">
                      @if (log.notes) {
                        <p class="input-label">{{ log.notes }}</p>
                      }
                    </div>
                  </div>
                </div>
              }
            </div>
            @if (weightLogsPage.items.length === 0) {
              <div class="text-center py-8">
                <p class="subtitle">No weight logs found. Start logging your weight!</p>
              </div>
            }
          } @else {
            <div class="text-center py-8">
              <div
                class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600 mx-auto mb-4"
              ></div>
              <p class="subtitle">Loading weight logs...</p>
            </div>
          }
        </div>
      </div>
    </main>
  `,
  styles: [],
})
export class HomeComponent implements OnInit {
  public $weightLogs: Observable<Page<WeightLog>> | undefined;
  public weightLogForm: FormGroup;
  public showAddForm = false;
  public isSubmitting = false;

  constructor(
    public msal: MsalAppService,
    private weightService: WeightService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.weightLogForm = this.createForm();
  }

  ngOnInit(): void {
    this.loadWeightLogs();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      day: [new Date().toISOString().split('T')[0], [Validators.required]],
      kg: [null, [Validators.required, Validators.min(0.1), Validators.max(1000)]],
    });
  }

  private loadWeightLogs(): void {
    this.$weightLogs = this.weightService.getWeightLogs();
  }

  toggleAddForm(): void {
    this.showAddForm = !this.showAddForm;
    if (!this.showAddForm) {
      this.resetForm();
    }
  }

  resetForm(): void {
    this.weightLogForm.reset();
    this.weightLogForm.patchValue({
      day: new Date().toISOString().split('T')[0],
    });
  }

  async onSubmit(): Promise<void> {
    if (this.weightLogForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;

      try {
        const formValue = this.weightLogForm.value;
        const weightLogLite: WeightLogLite = {
          date: formValue.day, // Send as string (YYYY-MM-DD) for DateOnly
          kg: formValue.kg,
        };

        await this.weightService.addWeightLog(weightLogLite).toPromise();

        // Reset form and hide it
        this.resetForm();
        this.showAddForm = false;

        // Reload the weight logs to show the new entry
        this.loadWeightLogs();
      } catch (error) {
        console.error('Failed to add weight log:', error);
        // You could add a toast notification here
      } finally {
        this.isSubmitting = false;
      }
    }
  }

  getDaysAgo(date: Date | string): string {
    const now = new Date();
    const logDate = new Date(date);

    // Check if the date is valid
    if (isNaN(logDate.getTime())) {
      return 'Invalid date';
    }

    const diffTime = now.getTime() - logDate.getTime();
    const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24));

    if (diffDays === 0) {
      return 'Today';
    } else if (diffDays === 1) {
      return '1 day ago';
    } else if (diffDays < 0) {
      return 'Future date';
    } else {
      return `${diffDays} days ago`;
    }
  }

  goToSettings(): void {
    this.router.navigate(['/settings']);
  }

  async logout() {
    try {
      await this.msal.logout();
      this.router.navigate(['/']);
    } catch (error) {
      console.error('Logout failed:', error);
    }
  }
}
