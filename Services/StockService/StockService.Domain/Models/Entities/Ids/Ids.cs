namespace StockService.Domain.Models.Entities.Ids;

public readonly record struct StockId (Guid Value);
public readonly record struct TransactionId(Guid Value);
public readonly record struct StockResultTransactionId(Guid Value);
