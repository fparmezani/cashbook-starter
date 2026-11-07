import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HealthResponse } from './home.model';

@Injectable({ providedIn: 'root' })
export class HomeApi {
  private readonly http = inject(HttpClient);

  checkHealth(): Observable<HealthResponse> {
    return this.http.get<HealthResponse>('health');
  }
}
