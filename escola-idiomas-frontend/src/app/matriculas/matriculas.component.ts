import { Component, OnInit } from '@angular/core';
import { MatriculaService } from '../shared/matricula.service';
import { AlunoService } from '../shared/AlunoService';
import { TurmaService } from '../shared/turma.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-matriculas',
  standalone: true,
  templateUrl: './matriculas.component.html',
  styleUrls: ['./matriculas.component.scss'],
  imports: [FormsModule, CommonModule]
})
export class MatriculasComponent implements OnInit {
  alunos: any[] = [];
  turmas: any[] = [];
  matricula = { alunoId: 0, turmaId: 0 };
  successMessage = '';
  errorMessage = '';

  constructor(
    private matriculaService: MatriculaService,
    private alunoService: AlunoService,
    private turmaService: TurmaService
  ) {}

  ngOnInit(): void {
    this.loadAlunos();
    this.loadTurmas();
  }

  loadAlunos() {
    this.alunoService.getAll().subscribe((data) => (this.alunos = data));
  }

  loadTurmas() {
    this.turmaService.getAll().subscribe((data) => (this.turmas = data));}

    /**
   * Cria uma nova matrícula
   */
  createMatricula() {
    this.matriculaService.create(this.matricula).subscribe({
      next: () => {
        this.successMessage = 'Matrícula criada com sucesso!';
        this.errorMessage = '';
        this.matricula = { alunoId: 0, turmaId: 0 };
      },
      error: (err) => {
        this.errorMessage = 'Erro ao criar matrícula. Tente novamente.';
        this.successMessage = '';
      },
    });
  }


}
