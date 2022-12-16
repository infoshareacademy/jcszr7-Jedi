using JediApp.Database.Domain;

namespace JediApp.Services.Interfaces
{
    public interface ITransactionHistoryService
    {
        public bool AddTransaction(TransactionHistory transactionHistory);
        public List<TransactionHistory> GetUserHistoryByUserId(Guid userId);

        public List<TransactionHistory> GetAllUsersHistories();

    }
}
