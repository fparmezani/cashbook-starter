import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
})
export class App {
  // Menu: cada feature nova ganha uma entrada aqui (skill create-angular-feature).
  protected readonly links = [
    { path: '/accounts', label: 'Contas' },
    { path: '/categories', label: 'Categorias' },
  ];
}
