import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {
  constructor(private http: HttpClient) {}

  /**
   * Obtém todos os alunos
   */
  getAll(): Observable<any[]> {
    return this.http.get<any[]>('/Alunos');
  }

  /**
   * Atualiza os dados de um aluno
   */
  update(id: number, aluno: { nome: string; cpf: string }): Observable<any> {
    return this.http.put(`/Alunos/${id}`, aluno);
  }

  /**
   * Deleta um aluno pelo ID
   */
  delete(id: number): Observable<any> {
    return this.http.delete(`/Alunos/${id}`);
  }

  /**
   * Obtém um aluno pelo CPF
   */
  getByCpf(cpf: string): Observable<any> {
    return this.http.get<any>(`/Alunos/cpf/${cpf}`);
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`/Alunos/${id}`);
  }

}
