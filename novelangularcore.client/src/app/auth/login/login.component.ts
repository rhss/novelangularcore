import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // necessário para ngModel
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  template: `
    <h2>Login</h2>
    <form (submit)="login()">
      <input type="text" placeholder="Username" [(ngModel)]="username" name="username" required />
      <input type="password" placeholder="Password" [(ngModel)]="password" name="password" required />
      <button type="submit">Login</button>
    </form>
  `,
  imports: [CommonModule, FormsModule],
})
export class LoginComponent {
  username = '';
  password = '';

  constructor(private router: Router) { }

  login() {
    if (this.username && this.password) {
      this.router.navigateByUrl('/');
    } else {
      alert('Invalid credentials');
    }
  }
}
