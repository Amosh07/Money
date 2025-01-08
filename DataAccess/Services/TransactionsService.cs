using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;

namespace DataAccess.Services
{
    public class TransactionsService : UserBase<Transactions>, ITransactionsInterface
    {
        private List<Transactions> _transactions;
        public TransactionsService() : base("Transactions.json") 
        {
            _transactions = LoadData();
        }
        public bool AddTransactions(Transactions transactions)
        {
            throw new NotImplementedException();
        }
    }
}
