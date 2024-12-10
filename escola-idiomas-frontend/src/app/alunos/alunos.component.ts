import { Component, OnInit, TemplateRef } from '@angular/core';
import { AlunoService } from '../shared/AlunoService';
import { NgbModal, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-alunos',
  standalone: true,
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.scss'],
  imports: [FormsModule, CommonModule]
})
export class AlunosComponent implements OnInit {
  alunos: any[] = [];
  alunoSelecionado: any = null;
  cpfBusca: string = '';
  successMessage = '';
  errorMessage = '';

  private modalRef!: NgbModalRef;
  constructor(public alunoService: AlunoService, public modalService: NgbModal) {}

  ngOnInit(): void {
    this.loadAlunos();
  }

  /**
   * Carrega todos os alunos
   */
  loadAlunos() {
    this.alunoService.getAll().subscribe({
      next: (data) => (this.alunos = data),
      error: () => (this.errorMessage = 'Erro ao carregar alunos.')
    });
  }

  /**
   * Busca um aluno pelo CPF
   */
  buscarPorCpf(cpf: string, callback?: (aluno: any) => void) {
    if (!cpf) return;

    this.alunoService.getByCpf(cpf).subscribe({
      next: (data) => {
        if (callback) callback(data);
        this.errorMessage = '';
      },
      error: () => {
        this.errorMessage = 'Aluno não encontrado com o CPF informado.';
      }
    });
  }

  /**
   * Abre o modal para edição de aluno
   */
  abrirModalEdicao(content: any, aluno: any) {
    this.alunoSelecionado = { ...aluno }; // Cria uma cópia do aluno selecionado
    this.modalService.open(content, { size: 'lg' });
  }




  /**
   * Atualiza o aluno após edição
   */
  atualizarAluno() {
    if (!this.alunoSelecionado) {
      this.errorMessage = 'Nenhum aluno selecionado para edição.';
      return;
    }

    this.alunoService.update(this.alunoSelecionado.id, this.alunoSelecionado).subscribe({
      next: () => {
        this.successMessage = 'Aluno atualizado com sucesso!';
        this.errorMessage = '';
        this.modalService.dismissAll(); // Fecha o modal
        this.loadAlunos(); // Recarrega a lista de alunos
      },
      error: () => {
        this.errorMessage = 'Erro ao atualizar aluno. Verifique os dados e tente novamente.';
        this.successMessage = '';
      },
    });
  }

  /**
   * Exclui um aluno
   */
  deletarAluno(id: number) {
    if (confirm('Deseja realmente excluir este aluno?')) {
      this.alunoService.delete(id).subscribe({
        next: () => {
          this.successMessage = 'Aluno excluído com sucesso!';
          this.loadAlunos();
        },
        error: () => (this.errorMessage = 'Erro ao excluir aluno.')
      });
    }
  }

  abrirModal(content: any, aluno: any) {
    this.alunoSelecionado = aluno; // Define o aluno selecionado
    this.modalService.open(content, { size: 'lg' });
  }


  /**
   * Fecha o modal
   */
  fecharModal() {
    if (this.modalRef) {
      this.modalRef.close();
    }
  }

}

