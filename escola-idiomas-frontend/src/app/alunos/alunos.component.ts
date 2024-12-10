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
  isSearching = false;

  private modalRef!: NgbModalRef;
modalContent: any;
modalEdicao: any;
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
   /**
   * Busca um aluno pelo CPF
   */
  buscarPorCpf() {
    if (!this.cpfBusca) {
      this.loadAlunos(); // Se o CPF estiver vazio, lista todos os alunos
      return;
    }

    this.isSearching = true; // Entra no modo de busca
    this.alunoService.getByCpf(this.cpfBusca).subscribe({
      next: (data) => {
        this.alunos = [data]; // Mostra apenas o aluno encontrado
        this.errorMessage = '';
      },
      error: () => {
        this.errorMessage = 'Aluno não encontrado com o CPF informado.';
        this.alunos = []; // Limpa a lista para indicar que não há resultados
      }
    });
  }

  /**
   * Limpa o campo de busca e lista todos os alunos
   */
  limparBusca() {
    this.cpfBusca = ''; // Limpa o campo
    this.loadAlunos(); // Carrega todos os alunos
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
  deletarAlunoConfirmado(id: number) {
    this.alunoService.delete(id).subscribe({
      next: () => {
        this.successMessage = 'Aluno excluído com sucesso!';
        this.errorMessage = '';
        this.modalService.dismissAll(); // Fecha o modal
        this.loadAlunos(); // Recarrega a lista de alunos
      },
      error: (error) => {
        if (error.error?.Error) {
          this.errorMessage = error.error.Error; // Mostra o erro retornado pela API
        } else {
          this.errorMessage = 'Erro ao excluir aluno. Tente novamente.';
        }
        this.successMessage = '';
      }
    });
  }



  abrirModalExclusao(content: any, aluno: any) {
    this.alunoSelecionado = aluno; // Define o aluno selecionado
    this.modalService.open(content, { size: 'md', backdrop: 'static', centered: true });
  }


  abrirModal(content: any, aluno: any) {
    this.alunoSelecionado = aluno;

    // Supondo que as turmas sejam carregadas dinamicamente
    this.alunoService.getById(aluno.id).subscribe({
      next: (data) => {
        this.alunoSelecionado.turmas = data.turmas; // Carregar turmas do aluno
        this.modalService.open(content, { size: 'lg', backdrop: 'static', centered: true });
      },
      error: () => {
        this.errorMessage = 'Erro ao carregar as turmas do aluno.';
      }
    });
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

