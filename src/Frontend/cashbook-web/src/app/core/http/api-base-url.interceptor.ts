import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../config/environment';

const absoluteUrl = /^https?:\/\//i;

/**
 * Prefixa a URL base da API. As features só conhecem o caminho do recurso (ex.: 'api/accounts').
 */
export const apiBaseUrlInterceptor: HttpInterceptorFn = (request, next) => {
  if (absoluteUrl.test(request.url)) {
    return next(request);
  }

  const baseUrl = environment.apiBaseUrl.replace(/\/+$/, '');
  const path = request.url.replace(/^\/+/, '');
  return next(request.clone({ url: `${baseUrl}/${path}` }));
};
