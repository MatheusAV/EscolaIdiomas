import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatriculaService {
  constructor(private http: HttpClient) {}

  create(matricula: { alunoId: number; turmaId: number }): Observable<any> {
    return this.http.post('/Matriculas', matricula);
  }
}
