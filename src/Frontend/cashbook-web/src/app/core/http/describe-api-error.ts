import { HttpErrorResponse } from '@angular/common/http';

/**
 * Converte um erro HTTP (ProblemDetails do backend) em mensagem legível.
 */
export function describeApiError(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    if (error.status === 0) {
      return 'Não foi possível conectar à API. Ela está rodando em http://localhost:5000?';
    }

    const problem = error.error as { title?: string; detail?: string; code?: string } | null;
    if (problem?.detail) {
      return problem.code ? `${problem.detail} (${problem.code})` : problem.detail;
    }

    return `Erro ${error.status}: ${error.statusText || 'falha na requisição'}`;
  }

  return 'Erro inesperado.';
}
