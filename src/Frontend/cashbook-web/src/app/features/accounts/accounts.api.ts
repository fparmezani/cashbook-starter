import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Account, CreateAccountRequest, UpdateAccountRequest } from './accounts.model';

const resource = 'api/accounts';

@Injectable({ providedIn: 'root' })
export class AccountsApi {
  private readonly http = inject(HttpClient);

  list(): Observable<Account[]> {
    return this.http.get<Account[]>(resource);
  }

  create(request: CreateAccountRequest): Observable<Account> {
    return this.http.post<Account>(resource, request);
  }

  update(id: string, request: UpdateAccountRequest): Observable<Account> {
    return this.http.put<Account>(`${resource}/${id}`, request);
  }
}
