using DataModel.Model;

namespace DataAccess.Interface
{
    public interface ITransactionsInterface
    {
        bool AddTransactions(Transactions transactions);
    }
}
