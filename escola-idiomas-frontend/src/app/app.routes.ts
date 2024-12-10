import { Routes } from '@angular/router';
import { AlunosComponent } from './alunos/alunos.component';
import { TurmasComponent } from './turmas/turmas.component';
import { MatriculasComponent } from './matriculas/matriculas.component';

export const routes: Routes = [
  { path: '', redirectTo: 'alunos', pathMatch: 'full' },
  { path: 'alunos', component: AlunosComponent },
  { path: 'turmas', component: TurmasComponent },
  { path: 'matriculas', component: MatriculasComponent }
];
