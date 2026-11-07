export type CategoryType = 'Income' | 'Expense';

export interface Category {
  id: string;
  name: string;
  type: CategoryType;
}

export interface CreateCategoryRequest {
  name: string;
  type: CategoryType;
}

/** O tipo é fixo após a criação; só o nome muda. */
export interface UpdateCategoryRequest {
  name: string;
}

export const categoryTypeLabels: Record<CategoryType, string> = {
  Income: 'Entrada',
  Expense: 'Saída',
};

/** Mesmo limite de Category.NameMaxLength no backend. */
export const categoryNameMaxLength = 100;
