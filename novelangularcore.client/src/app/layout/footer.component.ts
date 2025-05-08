import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <footer class="footer">
      <p>Novels</p>
    </footer>
  `,
  styles: [`
    .footer {
      background: #222;
      color: #aaa;
      text-align: center;
      padding: 1rem;
      font-size: 0.8rem;
      position: static;
      bottom: 0;
      width: 100%;
    }
  `]
})
export class FooterComponent { }
