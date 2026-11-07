import { Component, OnInit, inject, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { describeApiError } from '../../core/http/describe-api-error';
import { CategoriesApi } from './categories.api';
import { Category, CategoryType, categoryNameMaxLength, categoryTypeLabels } from './categories.model';

@Component({
  selector: 'app-categories-page',
  imports: [ReactiveFormsModule],
  templateUrl: './categories.page.html',
})
export class CategoriesPage implements OnInit {
  private readonly api = inject(CategoriesApi);

  protected readonly nameMaxLength = categoryNameMaxLength;
  protected readonly typeLabels = categoryTypeLabels;
  protected readonly types: CategoryType[] = ['Income', 'Expense'];
  protected readonly categories = signal<Category[]>([]);
  protected readonly loading = signal(true);
  protected readonly saving = signal(false);
  protected readonly listError = signal<string | null>(null);
  protected readonly formError = signal<string | null>(null);
  /** Categoria em edição; null = formulário de criação. */
  protected readonly editing = signal<Category | null>(null);

  protected readonly form = inject(NonNullableFormBuilder).group({
    name: ['', [Validators.required, Validators.maxLength(categoryNameMaxLength)]],
    type: ['Expense' as CategoryType, Validators.required],
  });

  ngOnInit(): void {
    this.load();
  }

  protected edit(category: Category): void {
    this.editing.set(category);
    this.formError.set(null);
    this.form.setValue({ name: category.name, type: category.type });
    this.form.controls.type.disable();
  }

  protected cancelEdit(): void {
    this.editing.set(null);
    this.formError.set(null);
    this.form.reset({ name: '', type: 'Expense' });
    this.form.controls.type.enable();
  }

  protected save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { name, type } = this.form.getRawValue();
    const current = this.editing();
    const request$ = current ? this.api.update(current.id, { name }) : this.api.create({ name, type });

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
      next: (categories) => {
        this.categories.set(categories);
        this.loading.set(false);
      },
      error: (error: unknown) => {
        this.listError.set(describeApiError(error));
        this.loading.set(false);
      },
    });
  }
}
