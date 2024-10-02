namespace WalletService.Domain.Models.Entities.Ids;

public readonly record struct WalletId (Guid Value);
public readonly record struct TransactionId(Guid Value);
public readonly record struct WalletResultTransactionId(Guid Value);
