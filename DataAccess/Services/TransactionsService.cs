using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Services
{
    public class TransactionService : UserBase<Transactions>, ITransactionsInterface
    {
        private List<Transactions> _transactions;

        public TransactionService() : base("Transaction.json")
        {
            _transactions = LoadData();
        }

        public void ActiveDeactive(Guid Id, bool status)
        {
            UpdateItem(t => t.Id == Id, t =>
            {
                t.IsActive = status;
            });
        }

        public async Task AddTransaction(CreatedTransactionDto createTransaction)
        {
            try
            {
                var modelTransaction = new Transactions()
                {
                    Id = Guid.NewGuid(),
                    Title = createTransaction.Title,
                    TransactionAmount = createTransaction.TransactionAmount,
                    TransactionDate = DateTime.Now,
                    IsActive = true,
                    Remarks = createTransaction.Remarks,
                    TagId = createTransaction.TagId,
                    TransactionTypes = createTransaction.TransactionType
                };

                _transactions.Add(modelTransaction);

                SaveData(_transactions);
            }
            catch (Exception ex)
            {
                throw new Exception("some this is wrong");
            }
        }

        public List<Transactions> GetAllTransaction()
        {
            return _transactions.OrderByDescending(t => t.IsActive).ToList();
        }

        public Transactions GetById(Guid Id)
        {
            return _transactions.FirstOrDefault(t => t.Id == Id);
        }

        public async Task<Decimal> CurrentBalance()
        {
            var transaction = GetAllTransaction();

            var totalCredit = transaction.Where(t => t.TransactionTypes == 4)
                             .Sum(t => t.TransactionAmount);

            var totalDebit = transaction.Where(t => t.TransactionTypes == 5)
                            .Sum(t => t.TransactionAmount);

            var totalDebt = transaction.Where(t => t.TransactionTypes == 6).Sum(t => t.TransactionAmount);

            var sumofTransaction = totalCredit - totalDebit;

            var currentBalance = sumofTransaction + totalDebt;

            return currentBalance;

        }

        public async Task<List<Transactions>> HighestTransaction()
        {
            var transaction = GetAllTransaction();
            return transaction.OrderByDescending(t => t.TransactionAmount).Take(5).ToList();
        }

        public async Task UpdateTransaction(UpdateTransactionDto transaction)
        {
            UpdateItem(t => t.Id == transaction.Id, t =>
            {
                t.Title = transaction.Title;
                t.TransactionDate = transaction.TransactionDate;
                t.TransactionAmount = transaction.TransactionAmount;
                t.TransactionTypes = transaction.TransactionTypes;
                t.Remarks = transaction.Remarks;
            });
        }
    }
}
