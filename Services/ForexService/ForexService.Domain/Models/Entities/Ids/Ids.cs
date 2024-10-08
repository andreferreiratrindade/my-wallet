namespace ForexService.Domain.Models.Entities.Ids;

public readonly record struct ForexId (Guid Value);
public readonly record struct TransactionForexId(Guid Value);
public readonly record struct ForexResultTransactionId(Guid Value);
