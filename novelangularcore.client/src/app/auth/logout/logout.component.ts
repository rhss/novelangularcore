import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  standalone: true,
  template: `<p>Logging out...</p>`
})
export class LogoutComponent implements OnInit {
  constructor(private router: Router) { }

  ngOnInit() {
    // Simula limpeza de sessão/localStorage
    // localStorage.removeItem('token');
    this.router.navigateByUrl('/login');
  }
}
