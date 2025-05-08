import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <nav class="menu">
      <a routerLink="/">Home</a>
      <a routerLink="/novels">Novels</a>
      <a routerLink="/autores">Autores</a>
      <a routerLink="/painel" *ngIf="isLoggedIn">Painel</a>
      <a routerLink="/login" *ngIf="!isLoggedIn">Login</a>
    </nav>
  `,
  styles: [`
    .menu {
      background: #333;
      color: white;
      padding: 1rem;
      display: flex;
      gap: 1rem;
    }
    .menu a {
      color: white;
      text-decoration: none;
    }
  `]
})
export class MenuComponent {
  get isLoggedIn() {
    return !!localStorage.getItem('token');
  }
}
