import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'bingo-front';

  ngOnInit() {
    // Inicializar tema (padrão claro)
    const theme = localStorage.getItem('theme') || 'theme-light';
    document.body.classList.add(theme);
  }
}
