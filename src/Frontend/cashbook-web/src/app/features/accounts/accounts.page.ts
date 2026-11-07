import { Component, OnInit, inject, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { describeApiError } from '../../core/http/describe-api-error';
import { AccountsApi } from './accounts.api';
import { Account, accountNameMaxLength } from './accounts.model';

@Component({
  selector: 'app-accounts-page',
  imports: [ReactiveFormsModule],
  templateUrl: './accounts.page.html',
})
export class AccountsPage implements OnInit {
  private readonly api = inject(AccountsApi);

  protected readonly nameMaxLength = accountNameMaxLength;
  protected readonly accounts = signal<Account[]>([]);
  protected readonly loading = signal(true);
  protected readonly saving = signal(false);
  protected readonly listError = signal<string | null>(null);
  protected readonly formError = signal<string | null>(null);
  /** Conta em edição; null = formulário de criação. */
  protected readonly editing = signal<Account | null>(null);

  protected readonly form = inject(NonNullableFormBuilder).group({
    name: ['', [Validators.required, Validators.maxLength(accountNameMaxLength)]],
    isActive: [true],
  });

  ngOnInit(): void {
    this.load();
  }

  protected edit(account: Account): void {
    this.editing.set(account);
    this.formError.set(null);
    this.form.setValue({ name: account.name, isActive: account.isActive });
  }

  protected cancelEdit(): void {
    this.editing.set(null);
    this.formError.set(null);
    this.form.reset({ name: '', isActive: true });
  }

  protected save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { name, isActive } = this.form.getRawValue();
    const current = this.editing();
    const request$ = current
      ? this.api.update(current.id, { name, isActive })
      : this.api.create({ name });

    this.saving.set(true);
    this.formError.set(null);
    request$.subscribe({
      next: () => {
        this.saving.set(false);
        this.cancelEdit();
        this.load();
      },
      error: (error: unknown) => {
        this.saving.set(false);
        this.formError.set(describeApiError(error));
      },
    });
  }

  private load(): void {
    this.loading.set(true);
    this.listError.set(null);
    this.api.list().subscribe({
      next: (accounts) => {
        this.accounts.set(accounts);
        this.loading.set(false);
      },
      error: (error: unknown) => {
        this.listError.set(describeApiError(error));
        this.loading.set(false);
      },
    });
  }
}
