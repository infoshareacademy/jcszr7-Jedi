using JediApp.Database.Domain;
using JediApp.Database.Repositories;

namespace JediApp.Services.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TransactionHistoryService(ITransactionHistoryRepository transactionHistoryRepository)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public bool AddTransaction(TransactionHistory transactionHistory)
        {
            return _transactionHistoryRepository.AddTransaction(transactionHistory);
        }

        public List<TransactionHistory> GetUserHistoryByUserId(Guid userId)
        {
            return _transactionHistoryRepository.GetUserHistoryByUserId(userId);
        }        
        
        public List<TransactionHistory> GetAllUsersHistories()
        {
            return _transactionHistoryRepository.GetAllUsersHistories();
        }

    }
}
