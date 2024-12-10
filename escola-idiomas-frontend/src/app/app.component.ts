import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule], // Importa módulos necessários
  template: `
   <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
  <div class="container">
    <a class="navbar-brand fw-bold" href="#">Escola de Idiomas</a>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarNav"
      aria-controls="navbarNav"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a
            class="nav-link fw-semibold text-dark"
            routerLink="/alunos"
            routerLinkActive="active"
          >
            Alunos
          </a>
        </li>
      </ul>
      <div class="d-flex gap-2">
        <!-- Botão Gerenciar Matrículas -->
        <button
          class="btn btn-action btn-primary rounded-pill"
          routerLink="/matriculas"
          routerLinkActive="active"
        >
          Gerenciar Matrículas
        </button>

        <!-- Botão Gerenciar Turmas -->
        <button
          class="btn btn-action btn-primary rounded-pill"
          routerLink="/turmas"
          routerLinkActive="active"
        >
          Gerenciar Turmas
        </button>
      </div>
    </div>
  </div>
</nav>

<router-outlet></router-outlet>


  `,
})
export class AppComponent {}
