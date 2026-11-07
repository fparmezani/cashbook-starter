export interface Account {
  id: string;
  name: string;
  isActive: boolean;
}

export interface CreateAccountRequest {
  name: string;
}

export interface UpdateAccountRequest {
  name: string;
  isActive: boolean;
}

/** Mesmo limite de Account.NameMaxLength no backend. */
export const accountNameMaxLength = 100;
