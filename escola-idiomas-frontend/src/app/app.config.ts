import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44366/api' // URL base da API
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        (req, next) => {
          // Atualiza a URL da requisição com a URL base
          const updatedReq = req.clone({ url: `${environment.apiUrl}${req.url}` });
          return next(updatedReq); // Chama o próximo manipulador
        }
      ])
    )
  ]
};
