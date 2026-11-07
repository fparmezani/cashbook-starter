import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadChildren: () => import('./home/home.routes').then((m) => m.homeRoutes) },
  { path: 'accounts', loadChildren: () => import('./features/accounts/accounts.routes').then((m) => m.accountsRoutes) },
  { path: 'categories', loadChildren: () => import('./features/categories/categories.routes').then((m) => m.categoriesRoutes) },
  // Features transactions e reports entram nas próximas specs.
  { path: '**', redirectTo: '' },
];
