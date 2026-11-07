using CashBook.BuildingBlocks.Results;

namespace CashBook.BuildingBlocks.Application;

/// <summary>
/// Contrato de caso de uso. Cada caso de uso é registrado um a um no *ApplicationExtensions do módulo.
/// </summary>
public interface IUseCase<in TRequest, TResponse>
{
    Task<Result<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}
