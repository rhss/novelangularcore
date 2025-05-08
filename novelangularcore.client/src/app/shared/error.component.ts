import { Component } from '@angular/core';

@Component({
  selector: 'app-error',
  standalone: true,
  template: `
    <h3 class="text-danger">404 - Page Not Found</h3>
    <p>The page you're looking for does not exist.</p>
  `
})
export class ErrorComponent { }
