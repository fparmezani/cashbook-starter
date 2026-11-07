import { Component, inject, signal } from '@angular/core';
import { describeApiError } from '../core/http/describe-api-error';
import { HomeApi } from './home.api';

type ConnectionState =
  | { kind: 'idle' }
  | { kind: 'loading' }
  | { kind: 'success'; status: string }
  | { kind: 'error'; message: string };

@Component({
  selector: 'app-home-page',
  templateUrl: './home.page.html',
})
export class HomePage {
  private readonly api = inject(HomeApi);

  protected readonly state = signal<ConnectionState>({ kind: 'idle' });

  protected testConnection(): void {
    this.state.set({ kind: 'loading' });
    this.api.checkHealth().subscribe({
      next: (response) => this.state.set({ kind: 'success', status: response.status }),
      error: (error: unknown) => this.state.set({ kind: 'error', message: describeApiError(error) }),
    });
  }
}
