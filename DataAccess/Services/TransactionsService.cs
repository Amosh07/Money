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

        public void ActiveDeactive(Guid Id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == Id);

            if (transaction != null)
            {
                transaction.IsActive = false;
            }
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
            return _transactions.Where(t => t.IsActive).ToList();
        }

        public Transactions GetById(Guid Id)
        {
            return _transactions.FirstOrDefault(t => t.Id == Id);
        }
    }
}
