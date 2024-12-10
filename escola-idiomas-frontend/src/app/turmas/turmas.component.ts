import { Component, OnInit } from '@angular/core';
import { TurmaService } from '../shared/turma.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-turmas',
  standalone: true,
  templateUrl: './turmas.component.html',
  styleUrls: ['./turmas.component.scss'],
  imports:[FormsModule, CommonModule]
})
export class TurmasComponent implements OnInit {
  turmas: any[] = []; // Lista de turmas
  turma = { nome: '' }; // Modelo para criar novas turmas
  successMessage = '';
  errorMessage = '';
  showForm = false;

  constructor(private turmaService: TurmaService) {}

  ngOnInit(): void {
    this.loadTurmas();
  }

  /**
   * Carrega todas as turmas
   */
  loadTurmas() {
    this.turmaService.getAll().subscribe(
      (data) => {
        this.turmas = data;
      },
      (error) => {
        this.errorMessage = 'Erro ao carregar turmas.';
        console.error(error);
      }
    );
  }

  /**
   * Cria uma nova turma
   */
  createTurma() {
    this.turmaService.create(this.turma).subscribe({
      next: () => {
        this.successMessage = 'Turma criada com sucesso!';
        this.errorMessage = '';
        this.turma = { nome: '' };
        this.showForm = false;
        this.loadTurmas(); // Recarrega a lista após criação
      },
      error: () => {
        this.errorMessage = 'Erro ao criar turma. Tente novamente.';
        this.successMessage = '';
      },
    });
  }
}
