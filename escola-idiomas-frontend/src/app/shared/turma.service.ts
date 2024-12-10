import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TurmaService {
  constructor(private http: HttpClient) {}

  /**
   * Obtém todas as turmas
   */
  getAll(): Observable<any[]> {
    return this.http.get<any[]>('/Turmas');
  }

  /**
   * Obtém uma turma pelo ID
   */
  getById(id: number): Observable<any> {
    return this.http.get<any>(`/Turmas/${id}`);
  }

  /**
   * Cria uma nova turma
   */
  create(turma: { nome: string }): Observable<any> {
    return this.http.post('/Turmas', turma);
  }
}
