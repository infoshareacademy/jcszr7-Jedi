﻿using JediApp.Database.Domain;
using JediApp.Database.Interface;
using JediApp.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace JediApp.Database.Repositories
{
    public class TransactionHistoryRepositoryDB : ITransactionHistoryRepository
    {
        private readonly JediAppDbContext _jediAppDb;

        public TransactionHistoryRepositoryDB(JediAppDbContext jediAppDb)
        {
            _jediAppDb = jediAppDb;
        }
        public bool AddTransaction(TransactionHistory transactionHistory)
        {
            //using (StreamWriter file = new StreamWriter(fileName, true))
            //{
            //    file.WriteLine($"{transactionHistory.Id};{transactionHistory.UserId};{transactionHistory.UserLogin};{transactionHistory.CurrencyName};{transactionHistory.Amount};{transactionHistory.DateOfTransaction};{transactionHistory.Description}");
            //}

            return true;
        }

        public List<TransactionHistory> GetAllUsersHistories()
        {
            //List<TransactionHistory> transactionHistory = new List<TransactionHistory>();

            //if (!File.Exists(fileName))
            //    return new List<TransactionHistory>();

            //var transactionHistoryFromFile = File.ReadAllLines(fileName);
            //foreach (var line in transactionHistoryFromFile)
            //{
            //    var columns = line.Split(';');
            //    Guid.TryParse(columns[0], out var transactionId);
            //    Guid.TryParse(columns[1], out var userId);
            //    decimal.TryParse(columns[4], out var amount);
            //    DateTime.TryParse(columns[5], out var dateOfTransaction);
            //    transactionHistory.Add(new TransactionHistory { Id = transactionId, UserId = userId, UserLogin = columns[2], CurrencyName = columns[3], Amount = amount, DateOfTransaction = dateOfTransaction, Description = columns[6] });
            //}
            //return transactionHistory;
            return _jediAppDb.TransactionHistory.Include(t => t.User).OrderBy(c => c.DateOfTransaction).ToList();
            //return _jediAppDb.TransactionHistory.OrderBy(c => c.DateOfTransaction).ToList();
        }

        public List<TransactionHistory> GetUserHistoryByUserId(Guid userId)
        {
            List<TransactionHistory> transactionHistory = GetAllUsersHistories();

            //return transactionHistory.Where(x => x.UserId == userId).ToList();

            return transactionHistory;

        }
    }
}
